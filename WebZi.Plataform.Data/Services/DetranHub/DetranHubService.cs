using AutoMapper;
using Microsoft.Extensions.Options;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.DetranHub;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Retorno;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.ViewModel.DetranHub;

namespace WebZi.Plataform.Data.Services.DetranHub;

public class DetranHubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    private readonly IOptions<DetranHubOptions> _options;

    public DetranHubService(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }
    public DetranHubService(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<DetranHubOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
        _options = options;
    }

    public async Task<ConsultarPorPlacaOuChassiDTO> SearchToPlateOrChassi(string Placa, string Chassi)
    {
        ConsultarPorPlacaOuChassiDTO ResultView = new();
        
        var placa = !string.IsNullOrWhiteSpace(Placa);
        var chassi = !string.IsNullOrWhiteSpace(Chassi);
        if (placa && chassi)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetBadRequest("Não é permitido informar placa e chassi simultaneamente.");
            return ResultView;
        }
        Placa = Placa != null ? Placa!.NormalizePlaca() : null;
        Chassi = Chassi != null ? Chassi!.NormalizeChassi() : null;
        if (placa && !Placa.IsPlaca())
        {
            ResultView.Mensagem = MensagemViewHelper.SetBadRequest("A placa informada é inválida.");
            return ResultView;
        }

        if (chassi && !Chassi.IsChassi())
        {
            ResultView.Mensagem = MensagemViewHelper.SetBadRequest("O chassi informada é inválida.");
            return ResultView;
        }
        ConsultarDetranHubResponse ConsltarRetorno = new();
        ConsultaDetranHubParameters ConsultarParametros = new()
        {
            TipoConsulta = placa ? "placa" : "chassi",
            Valor = placa ? Placa : Chassi,
            Estado = null
        };

        try
        {
            ConsltarRetorno = new HttpClientFactoryService(_httpClientFactory)
                .PostWithApiKey<ConsultarDetranHubResponse>(
                    _options.Value.BaseUrl,
                    _options.Value.ApiKey, ConsultarParametros);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex.Message);
            return ResultView;
        }

        ResultView = _mapper.Map<ConsultarPorPlacaOuChassiDTO>(ConsltarRetorno);

        return ResultView;
    }
}