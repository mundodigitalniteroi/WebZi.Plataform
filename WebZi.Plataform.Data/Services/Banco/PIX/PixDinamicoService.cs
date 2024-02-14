using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.ServiceModel.Channels;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Envio;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Retorno;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixDinamicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public PixDinamicoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PixDinamicoDTO> CreateAsync(int FaturamentoId, int UsuarioId)
        {
            PixDinamicoDTO ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.ListagemPixEstatico)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId);

            if (Faturamento != null)
            {
                ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(Faturamento.Atendimento.Grv, UsuarioId);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return ResultView;
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);

                return ResultView;
            }

            if (Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("A Forma de Pagamento PIX Dinâmico não está configurada para este Cliente");

                return ResultView;
            }

            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.PixDinamico)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento não possui valor");

                return ResultView;
            }

            PixDinamicoConfiguracaoModel PixDinamicoConfiguracao = await _context.PixDinamicoConfiguracao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == Faturamento.Atendimento.Grv.Cliente.ClienteId);

            BancoModel Banco = await _context.Banco
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BancoId == PixDinamicoConfiguracao.BancoPixId);

            PixDinamicoUrlModel PixDinamicoUrl = await _context.PixDinamicoUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NomeMetodo == "GERARPIX");

            PixDinamicoEnvioModel PixDinamicoEnvio = new()
            {
                Chave = PixDinamicoConfiguracao.PixChave,

                SolicitacaoPagador = Faturamento.Atendimento.Grv.NumeroFormularioGrv,

                InfoAdicionais = string.Empty,

                Valor = new()
                {
                    Original = Math.Round(Faturamento.ValorFaturado, 2).ToString().Replace(",", ".")
                },

                Merchant = new()
                {
                    Name = Faturamento.Atendimento.Grv.Cliente.Nome.ToUpperTrim(),

                    City = Faturamento.Atendimento.Grv.Cliente.Endereco.UF
                },

                Parametros = new()
                {
                    BaseUrl = PixDinamicoConfiguracao.BaseUrl,

                    ClientId = PixDinamicoConfiguracao.ClientId,

                    ClientSecret = PixDinamicoConfiguracao.ClientSecret,

                    Certificate = PixDinamicoConfiguracao.Certificate,

                    SenhaCertificado = PixDinamicoConfiguracao.SenhaCertificado,

                    Banco = Banco.Nome
                }
            };

            PixDinamicoRetornoModel PixDinamicoRetorno = new();

            try
            {
                Debug.WriteLine(JsonHelper.Serialize(PixDinamicoEnvio));

                PixDinamicoRetorno = new HttpClientFactoryService(_httpClientFactory)
                    .Post<PixDinamicoRetornoModel>(PixDinamicoUrl.UrlApi, PixDinamicoEnvio);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex);

                return ResultView;
            }

            PixDinamicoModel PixDinamico = new()
            {
                FaturamentoId = FaturamentoId,

                TransactionId = PixDinamicoRetorno.TransactionId,

                Revisao = PixDinamicoRetorno.Revisao,

                QrString = PixDinamicoRetorno.QrString,

                QrCode = PixDinamicoRetorno.QrCode,

                CalendarioCriacao = PixDinamicoRetorno.Calendario.Criacao,

                CalendarioExpiracao = PixDinamicoRetorno.Calendario.Expiracao,

                Devedor = PixDinamicoRetorno.Devedor,

                Location = PixDinamicoRetorno.Location,

                LocationId = PixDinamicoRetorno.LocationAttributes.Id,

                TipoCobranca = PixDinamicoRetorno.LocationAttributes.TipoCobranca,

                Chave = PixDinamicoRetorno.Chave,

                SolicitacaoPagador = PixDinamicoRetorno.SolicitacaoPagador,

                InfoAdicionais = PixDinamicoRetorno.InfoAdicionais,

                ValorOriginal = PixDinamicoRetorno.Valor.Original,

                Json = JsonHelper.Serialize(PixDinamicoRetorno)
            };

            await _context.PixDinamico.AddAsync(PixDinamico);

            await _context.SaveChangesAsync();

            ResultView = _mapper.Map<PixDinamicoDTO>(PixDinamico);

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess("PIX Dinâmico gerado com sucesso");

            return ResultView;
        }
    }
}