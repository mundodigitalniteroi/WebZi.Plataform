using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.ServiceModel;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.WsDetranRio;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class DetranRioService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public DetranRioService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DetranRioVeiculoModel> GetByIdAsync(int DetranVeiculoId)
        {
            return await _context.DetranRioVeiculoModel
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DetranVeiculoId == DetranVeiculoId);
        }

        public async Task<DetranRioVeiculoModel> GetByPlacaAsync(string Placa)
        {
            DetranRioVeiculoModel ResultView = await _context.DetranRioVeiculoModel
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Placa == Placa);

            //if (ResultView == null || ResultView.FlagRegistroNormalizado == "N")
            //{
            var aux = await Normalizar(Placa, "ROOT");
            //}

            return null;
        }

        public async Task<DetranRioVeiculoModel> GetByChassiAsync(string Chassi)
        {
            return await _context.DetranRioVeiculoModel
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Chassi == Chassi);
        }

        private async Task<DetranRioVeiculoModel> Normalizar(string Placa, string Operador)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WSPatioxDetran");

            List<string> response = ClientConfig(WebServiceUrl)
                .ConsultarVeiculo(new ConsultarVeiculoRequest
                {
                    placa = Placa,

                    operador = Operador
                }).ConsultarVeiculoResult
                .Replace("\"", "")
                .Replace("{", "")
                .Replace("}", "")
                .Split(',')
                .ToList();

            List<string> Lista = new();

            foreach (string line in response)
            {
                Lista.Add(line.Split(":")[1]);
            }

            if (Lista[0].Equals("ERRO", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }
            else if (Lista[0].StartsWith("VEICULO NAO CADASTRADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (Lista[0].StartsWith("PLACA INVALIDA", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (Lista[0].StartsWith("VEICULO BAIXADO", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (!Lista[0].Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Erro não informado ao consultar o WS");
            }

            DetranRioVeiculoModel model = new()
            {
                FlagRegistroNormalizado = "S",

                AnoFabricacao = Lista[1].ToShort(),

                AnoModelo = Lista[2].ToShort(),

                AnoUltimaLicenca = Lista[3].ToShort(),

                CapacidadeCarga = Lista[4].ToDecimal(),

                CapacidadePassageiros = Lista[5].ToByte(),

                ChassiRemarcado = Lista[6].ToUpperTrim().Left(1),

                Renavam = Lista[7].Trim(),

                Chassi = Lista[8].ToUpperTrim(),

                Classificacao = Lista[9].ToUpperTrim(),

                CodigoCategoria = Lista[10].ToUpperTrim(),

                DescricaoCategoria = Lista[11].ToUpperTrim(),

                DescricaoTipo = Lista[14].ToUpperTrim(),

                InformacaoRoubo = Lista[15].ToUpperTrim(),

                PesoBrutoTotal = Lista[16].ToUpperTrim(),

                Placa = Lista[17].ToUpperTrim(),

                RestricaoEstelionato = Lista[18].ToUpperTrim(),

                Uf = Lista[19].ToUpperTrim()
            };

            if (!model.Placa.IsNullOrWhiteSpace() && !model.Placa.IsPlaca())
            {
                model.Placa = string.Empty;

                // throw new Exception("Placa Retornada Pelo Ws É Inválida: " + Lista[17].Toupper());
            }

            return model;
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