using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;
using System.Security.Principal;
using System.ServiceModel;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.WsDetranRio;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.Models.WebServices.Rio;
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;

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

            if (!forcarNormalizacao)
            {
                result = await _context.DetranRioVeiculoModel
                    .Include(x => x.Cor)
                    .Include(x => x.MarcaModelo)
                    //.Include(x => x.ListagemDetranRioVeiculoRestricao)
                    //.ThenInclude(x => x.DetranRioVeiculoOrigemRestricao)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => DetranVeiculoId > 0 ? x.DetranVeiculoId == DetranVeiculoId : true
                                           && !Placa.IsNullOrWhiteSpace() ? x.Placa == Placa : true
                                           && !Chassi.IsNullOrWhiteSpace() ? x.Chassi == Chassi : true);
                
                if (result != null)
                {
                    ResultView = _mapper.Map<DetranRioVeiculoViewModel>(result);

                    ResultView.Mensagem = MensagemViewHelper.SetFound();

                    return ResultView;
                }
            }

            result = await Normalizar(Placa, "ROOT");

            if (result != null)
            {
                ResultView = _mapper.Map<DetranRioVeiculoViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        private async Task<DetranRioVeiculoModel> Normalizar(string PlacaChassi, string Operador)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WSPatioxDetran");

            List<string> response = ClientConfig(WebServiceUrl)
                .ConsultarVeiculo(new ConsultarVeiculoRequest
                {
                    placa = PlacaChassi.ToUpperTrim(),

                    operador = Operador.ToUpperTrim()
                }).ConsultarVeiculoResult
                .Replace("\"", "")
                .Replace("{", "")
                .Replace("}", "")
                .Split(',')
                .ToList();

            List<string> retornoWS = new();

            foreach (string line in response)
            {
                retornoWS.Add(line.Split(":")[1]);
            }

            if (retornoWS[0].Equals("ERRO", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }
            else if (retornoWS[0].StartsWith("VEICULO NAO CADASTRADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (retornoWS[0].StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (retornoWS[0].StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (!retornoWS[0].Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }

            DetranRioVeiculoModel model = new()
            {
                FlagRegistroNormalizado = "S",

                AnoFabricacao = retornoWS[1].ToShort(),

                AnoModelo = retornoWS[2].ToShort(),

                AnoUltimaLicenca = retornoWS[3].ToShort(),

                CapacidadeCarga = retornoWS[4].ToDecimal(),

                CapacidadePassageiros = retornoWS[5].ToByte(),

                ChassiRemarcado = retornoWS[6].ToUpperTrim().Left(1),

                Renavam = retornoWS[7].Trim(),

                Chassi = retornoWS[8].ToUpperTrim(),

                Classificacao = retornoWS[9].ToUpperTrim(),

                CodigoCategoria = retornoWS[10].ToUpperTrim(),

                DescricaoCategoria = retornoWS[11].ToUpperTrim(),

                DescricaoTipo = retornoWS[14].ToUpperTrim(),

                InformacaoRoubo = retornoWS[15].ToUpperTrim().ToNullIfEmpty(),

                PesoBrutoTotal = retornoWS[16].ToUpperTrim(),

                Placa = retornoWS[17].ToUpperTrim(),

                RestricaoEstelionato = retornoWS[18].ToUpperTrim().ToNullIfEmpty(),

                Uf = retornoWS[19].ToUpperTrim()
            };

            if (!model.Placa.IsPlaca())
            {
                model.Placa = string.Empty;

                // throw new Exception("Placa Retornada Pelo Ws É Inválida: " + Lista[17].Toupper());
            }

            model.Cor = await _context.Cor
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CorSecundaria == retornoWS[12].ToUpperTrim());

            model.MarcaModelo = await _context.MarcaModelo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MarcaModelo == retornoWS[13].ToUpperTrim());

            DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao = await _context.DetranRioVeiculoOrigemRestricao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Descricao == "DETRAN RJ");

            if (!model.InformacaoRoubo.IsNullOrWhiteSpace() || !model.RestricaoEstelionato.IsNullOrWhiteSpace())
            {
                model.ListagemDetranRioVeiculoRestricao = new List<DetranRioVeiculoRestricaoModel>();

                DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

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

            if (retornoWS.Count > 25)
            {
                Debugger.Break();

                string restricoes = string.Empty;

                List<string> list = new();

                DetranRioVeiculoRestricaoModel DetranRioVeiculoRestricao = new();

                for (int i = 23; i < retornoWS.Count; i++)
                {
                    if (retornoWS[i].Equals("RestricoesAdministrativas:[]") || retornoWS[i].Equals("RestricoesJuridicas:[]") || retornoWS[i].Equals("[]"))
                    {
                        continue;
                    }
                    else if (retornoWS[i].Contains("RestricoesAdministrativas") || retornoWS[i].Contains("RestricoesJuridicas"))
                    {
                        restricoes += "   ";
                    }
                    
                    if (retornoWS[i].StartsWith("["))
                    {
                        string[] split = retornoWS[i].Split('[');

                        restricoes += split[0].Replace(":", "") + "=";

                        restricoes += split[1] + ";";
                    }
                    else
                    {
                        restricoes += retornoWS[i].Replace("]", "") + ";";
                    }
                }

                if (!string.IsNullOrWhiteSpace(restricoes))
                {
                    string[] registros = restricoes.Split('#');

                    for (int i = 0; i < registros.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(registros[i]))
                        {
                            continue;
                        }

                        list.Add(registros[i]);
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        registros = list[i].Split('=');

                        registros = registros[1].Split(';');

                        DetranRioVeiculoRestricao = new()
                        {
                            DetranVeiculoOrigemRestricaoId = DetranRioVeiculoOrigemRestricao.DetranVeiculoOrigemRestricaoId,

                            TipoRestricao = list[i].Split('=')[0].Contains("Administrativa", StringComparison.CurrentCultureIgnoreCase) ? "A" : "J"
                        };

                        for (int j = 0; j < registros.Length; j++)
                        {
                            if (string.IsNullOrWhiteSpace(registros[j]))
                            {
                                continue;
                            }

                            string[] registro = registros[j].Split(':');

                            if (registro[0].Equals("Codigo", StringComparison.CurrentCultureIgnoreCase))
                            {
                                DetranRioVeiculoRestricao.CodigoRestricao = registro[1].ToByte();
                            }
                            else if (registro[0].Equals("Restricao", StringComparison.CurrentCultureIgnoreCase))
                            {
                                DetranRioVeiculoRestricao.Restricao = registro[1];

                                /// DetranVeiculosWsRestricoesController.Cadastrar(DetranVeiculosWsRestricoesModel);

                                DetranRioVeiculoRestricao = new()
                                {
                                    TipoRestricao = list[i].Split('=')[0].Contains("Administrativa", StringComparison.CurrentCultureIgnoreCase) ? "A" : "J"
                                };
                            }
                        }
                    }
                }
            }
            
            return await _context.DetranRioVeiculoModel
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Placa == model.Placa || x.Chassi == model.Chassi);
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