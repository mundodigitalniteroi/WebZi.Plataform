using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<DetranRioVeiculoViewModel> GetByIdAsync(int DetranVeiculoId, bool forcarNormalizacao = false)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (DetranVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Veículo inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, string.Empty, forcarNormalizacao);
        }

        public async Task<DetranRioVeiculoViewModel> GetByPlacaAsync(string Placa, bool forcarNormalizacao = false)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Placa.IsPlaca())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Placa inválida");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Placa.NormalizePlaca(), forcarNormalizacao);
        }

        public async Task<DetranRioVeiculoViewModel> GetByChassiAsync(string Chassi, bool forcarNormalizacao = false)
        {
            DetranRioVeiculoViewModel ResultView = new();

            if (!Chassi.IsChassi())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Chassi inválido");

                return ResultView;
            }

            return await GetByIdPlacaOrChassyAsync(0, Chassi.NormalizeChassi(), forcarNormalizacao);
        }

        private async Task<DetranRioVeiculoViewModel> GetByIdPlacaOrChassyAsync(int DetranVeiculoId, string PlacaChassi, bool forcarNormalizacao = false)
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

            DetranRioVeiculoModel result = new();

            forcarNormalizacao = false;

            if (DetranVeiculoId <= 0)
            {
                await DeleteDuplicatedAsync(Placa, Chassi);
            }

            if (!forcarNormalizacao)
            {
                result = await _context.DetranRioVeiculo
                    .Include(x => x.Cor)
                    .Include(x => x.MarcaModelo)
                    .Include(x => x.ListagemDetranRioVeiculoRestricao)
                    .ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                    .AsNoTracking()
                    .OrderByDescending(x => x.DetranVeiculoId)
                    .FirstOrDefaultAsync(x => DetranVeiculoId > 0 ? x.DetranVeiculoId == DetranVeiculoId : !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : x.Chassi == Chassi);

                if (result != null)
                {
                    ResultView = _mapper.Map<DetranRioVeiculoViewModel>(result);

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

                        List<TabelaGenericaModel> ListTipoRestricao = await new TabelaGenericaService(_context)
                            .ListAsync("DETRANRJ_TIPO_RESTRICAO");

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
                else if (DetranVeiculoId > 0)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetFound();

                    return ResultView;
                }
            }

            ResultView = await Normalizar(Placa + Chassi, "ROOT");

            if (ResultView != null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetFound();

                return ResultView;
            }
            else
            {
                ResultView = new()
                {
                    Mensagem = MensagemViewHelper.SetNotFound()
                };

                return ResultView;
            }
        }

        private async Task<DetranRioVeiculoViewModel> Normalizar(string PlacaChassi, string Operador)
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
                return null;
            }
            else if (DetranRetornoConsulta.Retorno.StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (DetranRetornoConsulta.Retorno.StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (!DetranRetornoConsulta.Retorno.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }

            DetranRioVeiculoModel model = new()
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

                Uf = DetranRetornoConsulta.Uf.ToUpperTrim()
            };

            if (!model.Placa.IsPlaca())
            {
                model.Placa = string.Empty;

                // throw new Exception("Placa Retornada Pelo Ws É Inválida: " + Lista[17].Toupper());
            }

            model.Cor = await _context.Cor
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CorSecundaria == DetranRetornoConsulta.DescricaoCor.ToUpperTrim());

            model.MarcaModelo = await _context.MarcaModelo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MarcaModelo == DetranRetornoConsulta.DescricaoMarca.ToUpperTrim());

            DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao = await _context.DetranRioVeiculoOrigemRestricao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Descricao == "DETRAN RJ");

            DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

            if (!model.InformacaoRoubo.IsNullOrWhiteSpace() || !model.RestricaoEstelionato.IsNullOrWhiteSpace())
            {
                model.ListagemDetranRioVeiculoRestricao = new List<DetranRioVeiculoRestricaoModel>();

                if (!model.InformacaoRoubo.IsNullOrWhiteSpace())
                {
                    DetranRioVeiculoRestricao = new()
                    {
                        DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                        TipoRestricao = "R",

                        Restricao = model.InformacaoRoubo
                    };

                    model.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                }

                if (!model.RestricaoEstelionato.IsNullOrWhiteSpace())
                {
                    DetranRioVeiculoRestricao = new()
                    {
                        DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                        TipoRestricao = "E",

                        Restricao = model.RestricaoEstelionato
                    };

                    model.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
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

                    model.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
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

                    model.ListagemDetranRioVeiculoRestricao.Add(DetranRioVeiculoRestricao);
                }
            }

            return await GetByIdPlacaOrChassyAsync(0, PlacaChassi);
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
                        _context.DetranRioVeiculoRestricao
                            .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                            .Delete();
                    }

                    _context.DetranRioVeiculo
                        .Where(x => x.DetranVeiculoId == item.DetranVeiculoId)
                        .Delete();
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