using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.ServiceModel;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.WsDetranRio;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.Models.WebServices.Rio;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class DetranRioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DetranRioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetranRioVeiculoDTO> GetViewByIdAsync(int DetranVeiculoId)
        {
            DetranRioVeiculoDTO ResultView = new();

            if (DetranVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Veículo inválido");

                return ResultView;
            }

            return await GetViewByIdPlacaOrChassyAsync(0, string.Empty);
        }

        public async Task<DetranRioVeiculoDTO> GetViewByPlacaAsync(string Placa)
        {
            DetranRioVeiculoDTO ResultView = new();

            if (!Placa.IsPlaca())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Placa inválida");

                return ResultView;
            }

            return await GetViewByIdPlacaOrChassyAsync(0, Placa.NormalizePlaca());
        }

        public async Task<DetranRioVeiculoDTO> GetViewByChassiAsync(string Chassi)
        {
            DetranRioVeiculoDTO ResultView = new();

            if (!Chassi.IsChassi())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Chassi inválido");

                return ResultView;
            }

            return await GetViewByIdPlacaOrChassyAsync(0, Chassi.NormalizeChassi());
        }

        private async Task<DetranRioVeiculoDTO> GetViewByIdPlacaOrChassyAsync(int DetranVeiculoId, string PlacaChassi)
        {
            DetranRioVeiculoDTO ResultView = new();

            string Placa = string.Empty;

            string Chassi = string.Empty;

            if (DetranVeiculoId <= 0)
            {
                if (PlacaChassi.IsPlaca())
                {
                    Placa = PlacaChassi;
                }
                else
                {
                    Chassi = PlacaChassi;
                }
            }

            DetranRioVeiculoModel DetranRioVeiculoBD;

            if (DetranVeiculoId <= 0)
            {
                await DeleteDuplicatedAsync(Placa, Chassi);
            }

            DetranRioVeiculoBD = await _context.DetranRioVeiculo
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                .AsNoTracking()
                .OrderByDescending(x => x.DetranVeiculoId)
                .FirstOrDefaultAsync(x => DetranVeiculoId > 0 ? x.DetranVeiculoId == DetranVeiculoId : !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi);

            if (DetranVeiculoId > 0)
            {
                if (DetranRioVeiculoBD == null)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                    return ResultView;
                }

                if (!DetranRioVeiculoBD.Placa.IsNullOrWhiteSpace())
                {
                    Placa = DetranRioVeiculoBD.Placa;
                }
                else
                {
                    Chassi = DetranRioVeiculoBD.Chassi;
                }
            }

            DetranRioVeiculoModel DetranRioVeiculoWS = new();

            try
            {
                if (DetranRioVeiculoBD == null || DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date)
                {
                    DetranRioVeiculoWS = await GetFromDetranAsync(Placa + Chassi, "ROOT");
                }
                else
                {
                    DetranRioVeiculoWS = null;
                }
            }
            catch (ArgumentException aex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(aex.Message);

                return ResultView;
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable("O Serviço do Departamento Estadual de Trânsito está inoperante ou indisponível", ex);

                return ResultView;
            }

            DetranRioVeiculoBD = await _context.DetranRioVeiculo
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                .AsNoTracking()
                .OrderByDescending(x => x.DetranVeiculoId)
                .FirstOrDefaultAsync(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi);

            if (DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date && DetranRioVeiculoWS != null)
            {
                DetranRioVeiculoBD = await UpdateAsync(DetranRioVeiculoBD, DetranRioVeiculoWS);
            }

            return await SetValuesToViewModelAsync(DetranRioVeiculoBD);
        }

        private async Task<DetranRioVeiculoModel> GetFromDetranAsync(string PlacaChassi, string Operador)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WSPatioxDetran");

            DetranRetornoConsultaModel DetranRioVeiculoWS = JsonHelper.DeserializeObject<DetranRetornoConsultaModel>(ClientConfig(WebServiceUrl)
                .ConsultarVeiculo(new ConsultarVeiculoRequest
                {
                    placa = PlacaChassi.ToUpperTrim(),

                    operador = Operador.ToUpperTrim()
                }).ConsultarVeiculoResult);

            if (DetranRioVeiculoWS.Retorno.StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Placa inválida no Departamento Estadual de Trânsito");
            }
            else if (DetranRioVeiculoWS.Retorno.StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Veículo baixado no Departamento Estadual de Trânsito");
            }
            else if (DetranRioVeiculoWS.Retorno.StartsWith("VEICULO NAO CADASTRADO", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Veículo não cadastrado no Departamento Estadual de Trânsito");
            }
            if (!DetranRioVeiculoWS.Retorno.Equals("OK", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception(DetranRioVeiculoWS.Retorno.RemoveLineBreaks());
            }

            DetranRioVeiculoModel DetranRioVeiculo = new()
            {
                FlagRegistroNormalizado = "S",

                AnoFabricacao = DetranRioVeiculoWS.AnoFabricacao,

                AnoModelo = DetranRioVeiculoWS.AnoModelo,

                AnoUltimaLicenca = DetranRioVeiculoWS.AnoUltimaLicenca,

                CapacidadeCarga = DetranRioVeiculoWS.CapacidadeCarga,

                CapacidadePassageiros = DetranRioVeiculoWS.CapacidadePassageiros,

                ChassiRemarcado = DetranRioVeiculoWS.ChassiRemarcado.ToUpperTrim().Left(1),

                Renavam = DetranRioVeiculoWS.NumeroRenavam.Trim(),

                Chassi = DetranRioVeiculoWS.Chassi.ToUpperTrim(),

                Classificacao = DetranRioVeiculoWS.Classificacao.ToUpperTrim(),

                CodigoCategoria = DetranRioVeiculoWS.CodigoCategoria.ToUpperTrim(),

                DescricaoCategoria = DetranRioVeiculoWS.DescricaoCategoria.ToUpperTrim(),

                DescricaoTipo = DetranRioVeiculoWS.DescricaoTipo.ToUpperTrim(),

                InformacaoRoubo = DetranRioVeiculoWS.InformacaoRoubo.ToUpperTrim().ToNullIfEmpty(),

                PesoBrutoTotal = DetranRioVeiculoWS.PesoBrutoTotal.ToUpperTrim(),

                Placa = DetranRioVeiculoWS.Placa.ToUpperTrim(),

                RestricaoEstelionato = DetranRioVeiculoWS.RestricaoEstelionato.ToUpperTrim().ToNullIfEmpty(),

                Uf = DetranRioVeiculoWS.Uf.ToUpperTrim(),

                Cor = await _context.Cor
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CorSecundaria == DetranRioVeiculoWS.DescricaoCor.ToUpperTrim()),

                MarcaModelo = await _context.MarcaModelo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.MarcaModelo == DetranRioVeiculoWS.DescricaoMarca.ToUpperTrim())
            };

            DetranRioVeiculo.CorId = DetranRioVeiculo.Cor.CorId;

            DetranRioVeiculo.MarcaModeloId = DetranRioVeiculo.MarcaModelo.MarcaModeloId;

            DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao = await _context.DetranRioVeiculoOrigemRestricao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Descricao == "DETRAN RJ");

            DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

            if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace()
             || !DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace()
             || DetranRioVeiculoWS?.RestricoesAdministrativas.Count > 0
             || DetranRioVeiculoWS?.RestricoesJuridicas.Count > 0)
            {
                DetranRioVeiculo.ListagemDetranRioVeiculoRestricao = new List<DetranRioVeiculoRestricaoModel>();

                if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace() || !DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace())
                {
                    if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace())
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "R",

                            Restricao = DetranRioVeiculo.InformacaoRoubo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }

                    if (!DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace())
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "E",

                            Restricao = DetranRioVeiculo.RestricaoEstelionato
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }

                if (DetranRioVeiculoWS?.RestricoesAdministrativas.Count > 0)
                {
                    foreach (DetranRetornoConsultaRestricaoModel item in DetranRioVeiculoWS.RestricoesAdministrativas)
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "A",

                            Restricao = item.Restricao,

                            CodigoRestricao = item.Codigo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }

                if (DetranRioVeiculoWS?.RestricoesJuridicas.Count > 0)
                {
                    foreach (DetranRetornoConsultaRestricaoModel item in DetranRioVeiculoWS.RestricoesJuridicas)
                    {
                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = "J",

                            Restricao = item.Restricao,

                            CodigoRestricao = item.Codigo
                        };

                        DetranRioVeiculo.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                    }
                }
            }

            return DetranRioVeiculo;
        }

        private async Task<DetranRioVeiculoModel> UpdateAsync(DetranRioVeiculoModel DetranRioVeiculoBD, DetranRioVeiculoModel DetranRioVeiculoWS)
        {
            bool persistir = false;

            if (DetranRioVeiculoBD?.AnoFabricacao != DetranRioVeiculoWS?.AnoFabricacao)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD?.AnoModelo != DetranRioVeiculoWS?.AnoModelo)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD?.AnoUltimaLicenca != DetranRioVeiculoWS?.AnoUltimaLicenca)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD?.CapacidadeCarga != DetranRioVeiculoWS?.CapacidadeCarga)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD?.CapacidadePassageiros != DetranRioVeiculoWS?.CapacidadePassageiros)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.Chassi != DetranRioVeiculoWS.Chassi)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.ChassiRemarcado != DetranRioVeiculoWS.ChassiRemarcado)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.Classificacao != DetranRioVeiculoWS.Classificacao)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.CodigoCategoria != DetranRioVeiculoWS.CodigoCategoria)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.DescricaoCategoria != DetranRioVeiculoWS.DescricaoCategoria)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.DescricaoTipo != DetranRioVeiculoWS.DescricaoTipo)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.InformacaoRoubo != DetranRioVeiculoWS.InformacaoRoubo.ToStringIfNull())
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.PesoBrutoTotal != DetranRioVeiculoWS.PesoBrutoTotal)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.Placa != DetranRioVeiculoWS.Placa)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.Renavam != DetranRioVeiculoWS.Renavam)
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.RestricaoEstelionato != DetranRioVeiculoWS.RestricaoEstelionato.ToStringIfNull())
            {
                persistir = true;
            }
            else if (DetranRioVeiculoBD.Uf != DetranRioVeiculoWS.Uf)
            {
                persistir = true;
            }

            if (!persistir)
            {
                return DetranRioVeiculoBD;
            }

            DetranRioVeiculoModel DetranRioVeiculo = await _context.DetranRioVeiculo
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                .FirstOrDefaultAsync(x => !DetranRioVeiculoWS.Placa.IsNullOrWhiteSpace() ? x.Placa == DetranRioVeiculoWS.Placa : x.Chassi == DetranRioVeiculoWS.Chassi);

            DetranRioVeiculo = _mapper.Map<DetranRioVeiculoModel>(DetranRioVeiculoWS);

            DetranRioVeiculo.DetranVeiculoId = DetranRioVeiculoBD.DetranVeiculoId;

            DetranRioVeiculo.DataCadastro = DetranRioVeiculoBD.DataCadastro;

            DetranRioVeiculo.FlagRegistroNormalizado = "S";

            DetranRioVeiculo.DataAlteracao = DateTime.Now;

            await _context.SaveChangesAsync();

            return DetranRioVeiculoWS;
        }

        private async Task<DetranRioVeiculoDTO> SetValuesToViewModelAsync(DetranRioVeiculoModel DetranRioVeiculoBD)
        {
            DetranRioVeiculoDTO ResultView = _mapper.Map<DetranRioVeiculoDTO>(DetranRioVeiculoBD);

            if (!DetranRioVeiculoBD.DescricaoTipo.IsNullOrWhiteSpace())
            {
                TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Descricao == DetranRioVeiculoBD.DescricaoTipo);

                if (TipoVeiculo == null)
                {
                    TipoVeiculo = new()
                    {
                        Descricao = DetranRioVeiculoBD.DescricaoTipo,

                        UsuarioCadastroId = 1
                    };

                    await _context.TipoVeiculo.AddAsync(TipoVeiculo);

                    await _context.SaveChangesAsync();
                }

                ResultView.TipoVeiculo = _mapper.Map<TipoVeiculoDTO>(TipoVeiculo);
            }
            else
            {
                ResultView.TipoVeiculo = null;
            }

            if (DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao?.Count > 0)
            {
                List<TabelaGenericaModel> ListTipoRestricao = await new TabelaGenericaService(_context)
                    .ListAsync("DETRANRJ_TIPO_RESTRICAO");

                DetranRioVeiculoRestricaoDTO DetranRioVeiculoRestricao = new();

                foreach (DetranRioVeiculoRestricaoModel item in DetranRioVeiculoBD.ListagemDetranRioVeiculoRestricao)
                {
                    DetranRioVeiculoRestricao = new()
                    {
                        IdentificadorRestricao = item.DetranVeiculoRestricaoId,

                        TipoRestricao = item.TipoRestricao,

                        CodigoRestricao = item.CodigoRestricao,

                        Restricao = item.Restricao
                    };

                    switch (item.TipoRestricao)
                    {
                        case "A":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "E":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "J":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;

                        case "R":

                            DetranRioVeiculoRestricao.TipoRestricaoDescricao = ListTipoRestricao.FirstOrDefault(x => x.ValorCadastro == item.TipoRestricao).Descricao;

                            break;
                    }

                    DetranRioVeiculoRestricao.DetranRioVeiculoOrigemRestricao.IdentificadorOrigemRestricao = item.DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId;

                    DetranRioVeiculoRestricao.DetranRioVeiculoOrigemRestricao.Descricao = item.DetranRioVeiculoOrigemRestricao.Descricao;

                    ResultView.ListagemRestricao.Add(DetranRioVeiculoRestricao);
                }
            }
            else
            {
                ResultView.ListagemRestricao = null;
            }

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        private async Task DeleteDuplicatedAsync(string Placa, string Chassi)
        {
            List<int> ids = await _context.DetranRioVeiculo
                .Where(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi)
                .Select(x => x.DetranVeiculoId)
                .ToListAsync();

            if (ids.Count > 1)
            {
                List<DetranRioVeiculoModel> ListDetranRioVeiculo = await _context.DetranRioVeiculo
                    .Include(x => x.ListagemDetranRioVeiculoRestricao)
                    .Where(x => x.DetranVeiculoId != ids.Last())
                    .Where(x => !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi)
                    .ToListAsync();

                foreach (DetranRioVeiculoModel item in ListDetranRioVeiculo)
                {
                    if (item?.ListagemDetranRioVeiculoRestricao.Count > 0)
                    {
                        await _context.DetranRioVeiculoRestricao
                            .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                            .DeleteAsync();
                    }

                    await _context.DetranRioVeiculo
                        .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                        .DeleteAsync();
                }
            }
        }

        private static WSPatioxDetranSoapClient ClientConfig(WebServiceUrlModel WebServiceUrl)
        {
            BasicHttpBinding httpBinding = new()
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue
            };

            httpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            WSPatioxDetranSoapClient client = new(httpBinding, new(new Uri(WebServiceUrl.Url)));

            client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            client.ClientCredentials.Windows.ClientCredential.Domain = string.Empty;
            client.ClientCredentials.Windows.ClientCredential.UserName = WebServiceUrl.Username;
            client.ClientCredentials.Windows.ClientCredential.Password = WebServiceUrl.Password;
            client.ClientCredentials.UserName.UserName = WebServiceUrl.Username;
            client.ClientCredentials.UserName.Password = WebServiceUrl.Password;

            client.ChannelFactory.CreateChannel();

            return client;
        }
    }
}