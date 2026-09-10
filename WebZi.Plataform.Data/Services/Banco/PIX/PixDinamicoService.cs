using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Envio;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Retorno;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Envio;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Retorno;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Faturamento;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixDinamicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public PixDinamicoService(AppDbContext context)
        {
            _context = context;
        }

        public PixDinamicoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PixDinamicoDTO> CreateAsync(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
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
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId, ct);

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
                .FirstOrDefaultAsync(x => x.ClienteId == Faturamento.Atendimento.Grv.Cliente.ClienteId, ct);

            if (PixDinamicoConfiguracao == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Configuração do PIX Dinâmico não encontrada para este Cliente");

                return ResultView;
            }

            BancoModel Banco = await _context.Banco
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BancoId == PixDinamicoConfiguracao.BancoPixId, ct);

            if (Banco == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Banco do PIX Dinâmico não encontrado");

                return ResultView;
            }

            PixDinamicoUrlModel PixDinamicoUrl = await _context.PixDinamicoUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NomeMetodo == "GERARPIX", ct);

            if (PixDinamicoUrl == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Configuração de URL para geração do PIX Dinâmico não encontrada");

                return ResultView;
            }

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
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable("Erro ao se comunicar com a API de geração do PIX Dinâmico", ex);

                return ResultView;
            }

            if (PixDinamicoRetorno == null || string.IsNullOrWhiteSpace(PixDinamicoRetorno.TransactionId))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Retorno inválido da API de geração do PIX Dinâmico");

                return ResultView;
            }

            PixDinamicoModel PixDinamico = new()
            {
                FaturamentoId = FaturamentoId,

                TransactionId = PixDinamicoRetorno.TransactionId,

                Revisao = PixDinamicoRetorno.Revisao,

                QrString = PixDinamicoRetorno.QrString,

                QrCode = PixDinamicoRetorno.QrCode,

                CalendarioCriacao = PixDinamicoRetorno.Calendario?.Criacao,

                CalendarioExpiracao = PixDinamicoRetorno.Calendario?.Expiracao,

                Devedor = PixDinamicoRetorno.Devedor,

                Location = PixDinamicoRetorno.Location,

                LocationId = PixDinamicoRetorno.LocationAttributes?.Id,

                TipoCobranca = PixDinamicoRetorno.LocationAttributes?.TipoCobranca,

                Chave = PixDinamicoRetorno.Chave,

                SolicitacaoPagador = PixDinamicoRetorno.SolicitacaoPagador,

                InfoAdicionais = PixDinamicoRetorno.InfoAdicionais,

                ValorOriginal = PixDinamicoRetorno.Valor?.Original,

                Json = JsonHelper.Serialize(PixDinamicoRetorno)
            };

            await _context.PixDinamico.AddAsync(PixDinamico, ct);

            await _context.SaveChangesAsync(ct);

            ResultView = _mapper.Map<PixDinamicoDTO>(PixDinamico);

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess("PIX Dinâmico gerado com sucesso");

            return ResultView;
        }

        public async Task<PixDinamicoDTO> ConsultaAsync(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
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
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId, ct);

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

            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.PixDinamico)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }

            PixDinamicoModel pixDinamico = await _context.PixDinamico
                .AsNoTracking()
                .OrderByDescending(x => x.DataCadastro)
                .FirstOrDefaultAsync(x => x.FaturamentoId == Faturamento.FaturamentoId, ct);

            if (pixDinamico == null)
            {
                if (Faturamento.Status != "P")
                {
                    return await CreateAsync(FaturamentoId, UsuarioId, ct);
                }

                ResultView.Mensagem = MensagemViewHelper.SetNotFound("PIX Dinâmico não encontrado");

                return ResultView;
            }

            if (pixDinamico.PixDinamicoTipoStatusGeracaoId == 2) // CONCLUIDA
            {
                ResultView = _mapper.Map<PixDinamicoDTO>(pixDinamico);

                ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess("Pagamento PIX Dinâmico concluido!");

                return ResultView;
            }

            PixDinamicoConfiguracaoModel pixDinamicoConfiguracao = await _context.PixDinamicoConfiguracao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == Faturamento.Atendimento.Grv.Cliente.ClienteId, ct);

            if (pixDinamicoConfiguracao == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Configuração do PIX Dinâmico não encontrada para este Cliente");

                return ResultView;
            }

            PixDinamicoUrlModel pixDinamicoUrl = await _context.PixDinamicoUrl
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.NomeMetodo == "CONSULTARPIX", ct);

            if (pixDinamicoUrl == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Configuração de URL para consulta do PIX Dinâmico não encontrada");

                return ResultView;
            }

            PixConsultaEnvioModel pixConsultaEnvio = new PixConsultaEnvioModel
            {
                TransactionId = pixDinamico.TransactionId,

                Parametros = new PixConsultaEnvioParametrosModel()
                {
                    BaseUrl = pixDinamicoConfiguracao.BaseUrl,

                    ClientId = pixDinamicoConfiguracao.ClientId,

                    ClientSecret = pixDinamicoConfiguracao.ClientSecret,

                    Certificate = pixDinamicoConfiguracao.Certificate,

                    SenhaCertificado = pixDinamicoConfiguracao.SenhaCertificado,
                }
            };

            PixConsultaRetornoModel pixConsultaRetorno = new();

            try
            {
                Debug.WriteLine(JsonHelper.Serialize(pixConsultaEnvio));

                pixConsultaRetorno = new HttpClientFactoryService(_httpClientFactory)
                    .Post<PixConsultaRetornoModel>(pixDinamicoUrl.UrlApi, pixConsultaEnvio);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable("Erro ao se comunicar com a API de consulta do PIX Dinâmico", ex);

                return ResultView;
            }

            if (pixConsultaRetorno == null || string.IsNullOrWhiteSpace(pixConsultaRetorno.Status))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Retorno inválido da API de consulta do PIX Dinâmico");

                return ResultView;
            }

            PixDinamicoTipoStatusGeracaoModel pixDinamicoTipoStatusGeracao = await _context.PixDinamicoTipoStatusGeracao
                .FirstOrDefaultAsync(x => x.Descricao == pixConsultaRetorno.Status, ct);

            if (pixDinamicoTipoStatusGeracao == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound($"Status '{pixConsultaRetorno.Status}' do PIX Dinâmico não cadastrado na base");

                return ResultView;
            }

            PixDinamicoConsultaModel pixDinamicoConsulta = new PixDinamicoConsultaModel
            {
                PixDinamicoId = pixDinamico.PixDinamicoId,

                PixDinamicoTipoStatusGeracaoId = pixDinamicoTipoStatusGeracao.PixDinamicoTipoStatusGeracaoId,

                Json = JsonHelper.Serialize(pixConsultaRetorno),

                DataCadastro = DateTime.Now
            };

            try
            {
                pixDinamico.PixDinamicoTipoStatusGeracaoId = pixDinamicoConsulta.PixDinamicoTipoStatusGeracaoId;
                pixDinamico.Revisao = pixConsultaRetorno.Revisao;
                pixDinamico.DataAlteracao = DateTime.Now;

                if (pixConsultaRetorno.Pix != null && pixConsultaRetorno.Pix.Length > 0)
                {
                    var pix = pixConsultaRetorno.Pix.FirstOrDefault();

                    if (pix != null)
                    {
                        pixDinamico.PixHorario = pix.Horario;
                        pixDinamico.PagadorNome = pix.Pagador?.Nome;
                        pixDinamico.PagadorCnpj = pix.Pagador?.Cnpj;
                        pixDinamico.PagadorCpf = pix.Pagador?.Cpf;
                    }
                }

                _context.PixDinamico.Update(pixDinamico);

                await _context.PixDinamicoConsulta.AddAsync(pixDinamicoConsulta, ct);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao salvar a consulta do PIX Dinâmico no banco de dados", ex);

                return ResultView;
            }

            ResultView = _mapper.Map<PixDinamicoDTO>(pixDinamico);

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess("Consulta PIX Dinâmico com sucesso");

            return ResultView;
        }
    }
}