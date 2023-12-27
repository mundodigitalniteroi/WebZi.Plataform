using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Security.Principal;
using System.ServiceModel;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.WsDetranRio;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.Models.WebServices.Rio;
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;
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

        public async Task<DetranRioVeiculoViewModel> GetByIdAsync(int DetranVeiculoId)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (DetranVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Veículo inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, string.Empty);
        }

        public async Task<DetranRioVeiculoViewModel> GetByPlacaAsync(string Placa)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Placa.IsPlaca())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Placa inválida");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Placa.NormalizePlaca());
        }

        public async Task<DetranRioVeiculoViewModel> GetByChassiAsync(string Chassi)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Chassi.IsChassi())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Chassi inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Chassi.NormalizeChassi());
        }

        private async Task<DetranRioVeiculoViewModel> GetByIdPlacaOrChassyAsync(int DetranVeiculoId, string PlacaChassi)
        {
            DetranRioVeiculoViewModel ResultView = new();

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

            if (DetranRioVeiculoBD == null && DetranVeiculoId > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                return ResultView;
            }

            DetranRioVeiculoModel DetranRioVeiculoWS = new();

            try
            {
                if (!DetranRioVeiculoBD.Placa.IsNullOrWhiteSpace())
                {
                    Placa = DetranRioVeiculoBD.Placa;
                }
                else
                {
                    Chassi = DetranRioVeiculoBD.Chassi;
                }

                if (DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date)
                {
                    DetranRioVeiculoWS = await GetFromDetranAsync(Placa + Chassi, "ROOT");
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex);

                return ResultView;
            }

            if (DetranRioVeiculoBD == null)
            {
                await _context.DetranRioVeiculo.AddAsync(DetranRioVeiculoWS);

                await _context.SaveChangesAsync();

                ResultView = await SetValuesToDetranRioVeiculoAsync(DetranRioVeiculoWS);

                return ResultView;
            }
            else
            {
                if (DetranRioVeiculoBD.DataCadastro != DateTime.Now.Date)
                {
                    DetranRioVeiculoBD = await UpdateVeiculoAsync(DetranRioVeiculoBD, DetranRioVeiculoWS);
                }

                ResultView = await SetValuesToDetranRioVeiculoAsync(DetranRioVeiculoBD);

                return ResultView;
            }
        }

        private async Task<DetranRioVeiculoModel> UpdateVeiculoAsync(DetranRioVeiculoModel DetranRioVeiculoBD, DetranRioVeiculoModel DetranRioVeiculoWS)
        {
            if (DetranRioVeiculoBD.InformacaoRoubo == DetranRioVeiculoWS.InformacaoRoubo
             && DetranRioVeiculoBD.RestricaoEstelionato == DetranRioVeiculoWS.RestricaoEstelionato)
            {
                return DetranRioVeiculoBD;
            }

            DetranRioVeiculoModel DetranRioVeiculo = await _context.DetranRioVeiculo
                .Include(x => x.ListagemDetranRioVeiculoRestricao)
                .FirstOrDefaultAsync(x => !DetranRioVeiculoWS.Placa.IsNullOrWhiteSpace() ? x.Placa == DetranRioVeiculoWS.Placa : x.Chassi == DetranRioVeiculoWS.Chassi);

            DetranRioVeiculo = _mapper.Map<DetranRioVeiculoModel>(DetranRioVeiculoWS);

            if (DetranRioVeiculoWS.InformacaoRoubo != DetranRioVeiculoBD.InformacaoRoubo)
            {

            }

            if (DetranRioVeiculoWS.RestricaoEstelionato != DetranRioVeiculoBD.RestricaoEstelionato)
            {

            }

            if (DetranRioVeiculoWS?.ListagemDetranRioVeiculoRestricao.Count > 0)
            {

            }

            await _context.SaveChangesAsync();

            return DetranRioVeiculo;
        }

        private async Task<DetranRioVeiculoViewModel> SetValuesToDetranRioVeiculoAsync(DetranRioVeiculoModel result)
        {
            DetranRioVeiculoViewModel ResultView = _mapper.Map<DetranRioVeiculoViewModel>(result);

            List<TabelaGenericaModel> ListTipoRestricao = await new TabelaGenericaService(_context)
                .ListAsync("DETRANRJ_TIPO_RESTRICAO");

            TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Descricao == result.DescricaoTipo.ToUpperTrim());

            if (TipoVeiculo != null)
            {
                ResultView.TipoVeiculo.IdentificadorTipoVeiculo = TipoVeiculo.TipoVeiculoId;

                ResultView.TipoVeiculo.Descricao = TipoVeiculo.Descricao;

                ResultView.TipoVeiculo.FlagNaoRequerCnhNaLiberacao = TipoVeiculo.FlagNaoRequerCnhNaLiberacao;
            }

            if (result.ListagemDetranRioVeiculoRestricao != null)
            {
                DetranRioVeiculoRestricaoViewModel DetranRioVeiculoRestricao = new();

                foreach (var item in result.ListagemDetranRioVeiculoRestricao)
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

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        private async Task<DetranRioVeiculoModel> GetFromDetranAsync(string PlacaChassi, string Operador)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WSPatioxDetran");

            DetranRetornoConsultaModel DetranRetornoConsulta = JsonHelper.DeserializeObject<DetranRetornoConsultaModel>(ClientConfig(WebServiceUrl)
                .ConsultarVeiculo(new ConsultarVeiculoRequest
                {
                    placa = PlacaChassi.ToUpperTrim(),

                    operador = Operador.ToUpperTrim()
                }).ConsultarVeiculoResult);

            if (DetranRetornoConsulta.Retorno.StartsWith("VEICULO NAO CADASTRADO", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("VEICULO NAO CADASTRADO");
            }
            else if (DetranRetornoConsulta.Retorno.StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("PLACA INVALIDA");
            }
            else if (DetranRetornoConsulta.Retorno.StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("VEICULO BAIXADO");
            }
            else if (!DetranRetornoConsulta.Retorno.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }

            DetranRioVeiculoModel DetranRioVeiculo = new()
            {
                FlagRegistroNormalizado = "S",

                AnoFabricacao = DetranRetornoConsulta.AnoFabricacao,

                AnoModelo = DetranRetornoConsulta.AnoModelo,

                AnoUltimaLicenca = DetranRetornoConsulta.AnoUltimaLicenca,

                CapacidadeCarga = DetranRetornoConsulta.CapacidadeCarga,

                CapacidadePassageiros = DetranRetornoConsulta.CapacidadePassageiros,

                ChassiRemarcado = DetranRetornoConsulta.ChassiRemarcado.ToUpperTrim().Left(1),

                Renavam = DetranRetornoConsulta.NumeroRenavam.Trim(),

                Chassi = DetranRetornoConsulta.Chassi.ToUpperTrim(),

                Classificacao = DetranRetornoConsulta.Classificacao.ToUpperTrim(),

                CodigoCategoria = DetranRetornoConsulta.CodigoCategoria.ToUpperTrim(),

                DescricaoCategoria = DetranRetornoConsulta.DescricaoCategoria.ToUpperTrim(),

                DescricaoTipo = DetranRetornoConsulta.DescricaoTipo.ToUpperTrim(),

                InformacaoRoubo = DetranRetornoConsulta.InformacaoRoubo.ToUpperTrim().ToNullIfEmpty(),

                PesoBrutoTotal = DetranRetornoConsulta.PesoBrutoTotal.ToUpperTrim(),

                Placa = DetranRetornoConsulta.Placa.ToUpperTrim(),

                RestricaoEstelionato = DetranRetornoConsulta.RestricaoEstelionato.ToUpperTrim().ToNullIfEmpty(),

                Uf = DetranRetornoConsulta.Uf.ToUpperTrim(),

                Cor = await _context.Cor
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CorSecundaria == DetranRetornoConsulta.DescricaoCor.ToUpperTrim()),

                MarcaModelo = await _context.MarcaModelo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.MarcaModelo == DetranRetornoConsulta.DescricaoMarca.ToUpperTrim())
            };

            DetranRioVeiculo.CorId = DetranRioVeiculo.Cor.CorId;

            DetranRioVeiculo.MarcaModeloId = DetranRioVeiculo.MarcaModelo.MarcaModeloId;

            DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao = await _context.DetranRioVeiculoOrigemRestricao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Descricao == "DETRAN RJ");

            DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

            if (!DetranRioVeiculo.InformacaoRoubo.IsNullOrWhiteSpace() 
             || !DetranRioVeiculo.RestricaoEstelionato.IsNullOrWhiteSpace()
             || DetranRetornoConsulta?.RestricoesAdministrativas.Count > 0
             || DetranRetornoConsulta?.RestricoesJuridicas.Count > 0)
            {
                DetranRioVeiculo.ListagemDetranRioVeiculoRestricao = new List<DetranRioVeiculoRestricaoModel>();
            }

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

            if (DetranRetornoConsulta?.RestricoesAdministrativas.Count > 0)
            {
                foreach (var item in DetranRetornoConsulta.RestricoesAdministrativas)
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

            if (DetranRetornoConsulta?.RestricoesJuridicas.Count > 0)
            {
                foreach (var item in DetranRetornoConsulta.RestricoesJuridicas)
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

            return DetranRioVeiculo;
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

                foreach (var item in ListDetranRioVeiculo)
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