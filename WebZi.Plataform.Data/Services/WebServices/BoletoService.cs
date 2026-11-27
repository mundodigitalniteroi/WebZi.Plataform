using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.WsBoleto;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.WebServices.Boleto;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Views.Faturamento;
using Z.EntityFramework.Plus;
using static WebZi.Plataform.Data.WsBoleto.WsBoletoSoapClient;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class BoletoService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public BoletoService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BoletoDTO> GetBoletoNaoPagoAsync(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
        {
            return await GetBoletoAsync(FaturamentoId, UsuarioId, "N", ct);
        }

        public async Task<BoletoDTO> GetBoletoNaoCanceladoAsync(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
        {
            return await GetBoletoAsync(FaturamentoId, UsuarioId, "", ct);
        }

        public Task<BoletoDTO> GetBoletoNaoPago(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
        {
            return GetBoletoNaoPagoAsync(FaturamentoId, UsuarioId, ct);
        }

        public Task<BoletoDTO> GetBoletoNaoCancelado(int FaturamentoId, int UsuarioId, CancellationToken ct = default)
        {
            return GetBoletoNaoCanceladoAsync(FaturamentoId, UsuarioId, ct);
        }

        private async Task<BoletoDTO> GetBoletoAsync(int FaturamentoId, int UsuarioId, string StatusBoleto = "", CancellationToken ct = default)
        {
            BoletoDTO ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = null;

            if (!string.IsNullOrWhiteSpace(StatusBoleto))
            {
                Faturamento = await _context.Faturamento
                    .Include(x => x.ListagemBoleto.Where(x => x.Status == StatusBoleto))
                    .Include(x => x.TipoMeioCobranca)
                    .Include(x => x.Atendimento)
                    .ThenInclude(x => x.Grv)
                    .OrderByDescending(x => x.DataCadastro)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId, ct);
            }
            else
            {
                Faturamento = await _context.Faturamento
                    .Include(x => x.ListagemBoleto.Where(x => x.Status != "C"))
                    .Include(x => x.TipoMeioCobranca)
                    .Include(x => x.Atendimento)
                    .ThenInclude(x => x.Grv)
                    .OrderByDescending(x => x.DataCadastro)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId, ct);
            }

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

            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.Boleto &&
                Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(
                    $"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.ListagemBoleto == null || Faturamento.ListagemBoleto.Count == 0)
            {
                if (Faturamento.Status != "P")
                {
                    return await CreateAsync(FaturamentoId, UsuarioId, false, ct);
                }

                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Boleto não encontrado");

                return ResultView;
            }

            BoletoModel Boleto = Faturamento.ListagemBoleto
                .OrderByDescending(x => x.DataEmissao)
                .FirstOrDefault();

            if (Boleto == null)
            {
                if (Faturamento.Status != "P")
                {
                    return await CreateAsync(FaturamentoId, UsuarioId, false, ct);
                }

                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Boleto não encontrado");

                return ResultView;
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.Boleto &&
                                     x.TabelaOrigemId == Boleto.FaturamentoBoletoId, ct);

            if (BucketArquivo != null)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                ResultView.Identificador = Boleto.BoletoId;

                ResultView.LinhaDigitavel = Boleto.Linha;

                ResultView.Imagem = await new HttpClientFactoryService(_httpClientFactory)
                    .DownloadFileAsync(BucketArquivo.Url, ct);

                return ResultView;
            }
            else
            {
                BoletoImagemModel FaturamentoBoletoImagem = await _context.FaturamentoBoletoImagem
                    .Include(x => x.FaturamentoBoleto)
                    .OrderByDescending(x => x.FaturamentoBoletoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.FaturamentoBoleto.FaturamentoId == FaturamentoId &&
                                         x.FaturamentoBoleto.Status != "C", ct);

                if (FaturamentoBoletoImagem != null)
                {
                    ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                    ResultView.Identificador = Boleto.BoletoId;

                    ResultView.LinhaDigitavel = Boleto.Linha;

                    ResultView.Imagem = FaturamentoBoletoImagem.Imagem;

                    return ResultView;
                }
                else
                {
                    if (Faturamento.Status != "P")
                    {
                        return await CreateAsync(FaturamentoId, UsuarioId, false, ct);
                    }

                    ResultView.Mensagem =
                        MensagemViewHelper.SetNotFound("A imagem do Boleto não foi gerado ou foi excluído");

                    return ResultView;
                }
            }
        }

        public async Task<BoletoDTO> CreateAsync(int FaturamentoId, int UsuarioId, bool reprocessar = false, CancellationToken ct = default)
        {
            BoletoDTO ResultView = new();

            if (reprocessar)
            {
                bool permitirEdicao = await _context.PerfilAcessoUsuario
                    .AsNoTracking()
                    .AnyAsync(x => x.UsuarioId == UsuarioId
                              && (x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditProd ||
                                  x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog)
                              && _context.SistemaPerfilAcessoSubModulos
                                  .Any(s => (s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditProd ||
                                             s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog)
                                            && s.IdSubModulo == (int)SubModuloEnum.EditarAtendimento), ct);

                if (!permitirEdicao)
                {
                    ResultView.Mensagem =
                        MensagemViewHelper.SetBadRequest("Não possui permissão para reprocessar o boleto");

                    return ResultView;
                }
            }

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
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

            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.Boleto &&
                Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest(
                        $"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P" && !reprocessar)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento não possui valor");

                return ResultView;
            }

            ViewFaturamentoBoletoModel ViewBoleto = await _context.ViewFaturamentoBoleto
                .FirstOrDefaultAsync(x => x.FaturamentoId == Faturamento.FaturamentoId, ct);

            if (ViewBoleto == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Dados para a geração do Boleto inexistentes");

                return ResultView;
            }

            await CancelAsync(Faturamento.FaturamentoId, ct);

            #region Preenchimento do Modelo

            BoletoTodos DadosBoleto = new()
            {
                cedente_agencia = ViewBoleto.CedenteAgencia,

                banco = ViewBoleto.CedenteCodigoFebraban,

                cedente_codigo = ViewBoleto.CedenteCodigo,

                cedente_conta = ViewBoleto.CedenteContaCorrente,

                cedente_cpfCnpj = DocumentHelper.FormatCNPJ(ViewBoleto.CedenteDocumento.Trim()),

                cedente_digitoConta = ViewBoleto.CedenteDv,

                cedente_nome = StringHelper.Normalize(ViewBoleto.CedenteNome),

                cedente_nossoNumeroBoleto = ViewBoleto.CedenteNossoNumero,

                numeroDocumento = ViewBoleto.NumeroDocumento,

                sacado_bairro = StringHelper.Normalize(ViewBoleto.SacadoBairro),

                sacado_cep = StringHelper.Normalize(ViewBoleto.SacadoCEP),

                sacado_cidade = StringHelper.Normalize(ViewBoleto.SacadoCidade),

                sacado_cpfCnpj = ViewBoleto.SacadoDocumento.Trim(),

                sacado_endereco = StringHelper.Normalize(ViewBoleto.SacadoEndereco),

                sacado_nome = StringHelper.Normalize(ViewBoleto.SacadoNome ?? string.Empty),

                sacado_uf = StringHelper.Normalize(ViewBoleto.SacadoUF),

                valor_boleto = ViewBoleto.ValorBoleto,

                vencimento = ViewBoleto.Vencimento,

                carteira = ViewBoleto.SacadoCarteira,

                instrucoes = ViewBoleto.SacadoInstrucoes
            };

            BoletoGeradoModel BoletoGerado = new()
            {
                DataVencimento = DateTimeHelper.GetDateTime(ViewBoleto.Vencimento[..10], "dd/MM/yyyy")
            };

            if (Faturamento.TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                List<FaturamentoRegraModel> FaturamentoRegras = await _context.FaturamentoRegra
                    .Include(x => x.FaturamentoRegraTipo)
                    .Where(x => x.ClienteId == Faturamento.Atendimento.Grv.ClienteId &&
                                x.DepositoId == Faturamento.Atendimento.Grv.DepositoId)
                    .AsNoTracking()
                    .ToListAsync(ct);

                if (FaturamentoRegras?.Count > 0)
                {
                    FaturamentoRegraModel FaturamentoRegra = FaturamentoRegras
                        .FirstOrDefault(x =>
                            x.FaturamentoRegraTipo.Codigo ==
                            FaturamentoRegraTipoEnum.QuantidadeDiasSomarDataVencimentoBoleto);

                    if (FaturamentoRegra != null)
                    {
                        BoletoGerado.DiasConfiguracaoDataVencimento = int.Parse(FaturamentoRegra.Valor);

                        BoletoGerado.DataVencimento = DateTimeHelper.AddDays(BoletoGerado.DataVencimento,
                            BoletoGerado.DiasConfiguracaoDataVencimento);

                        DadosBoleto.vencimento = BoletoGerado.DataVencimento.ToString("dd/MM/yyyy");
                    }
                }
            }

            #endregion Preenchimento do Modelo

            #region Execução do WebService de geração do Boleto

            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "WsBoletoSoap", ct);

            int linhaId;

            string linha;

            try
            {
                WsBoletoSoapClient soapClient = new(EndpointConfiguration.WsBoletoSoap, WebServiceUrl.Url);

                BoletoBancosRetornoLinhaResponse retornoSoap = await soapClient.BoletoBancosRetornoLinhaAsync(
                    boleto: DadosBoleto,
                    login: WebServiceUrl.Username,
                    senha: WebServiceUrl.Password,
                    Tipo: "img",
                    isdev: false);

                BoletoGerado.Boleto = retornoSoap.Body.BoletoBancosRetornoLinhaResult;
                linhaId = retornoSoap.Body.linha_id;
                linha = retornoSoap.Body.linha;
            }
            catch (Exception)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.ErroNaAplicacaoExterna);
                return ResultView;
            }

            BoletoGerado.BoletoId = linhaId;

            BoletoGerado.Linha = linha;

            #endregion Execução do WebService de geração do Boleto

            #region Cadastro do Boleto e da Imagem

            BoletoModel Boleto = new()
            {
                FaturamentoId = Faturamento.FaturamentoId,

                BoletoId = BoletoGerado.BoletoId,

                UsuarioCadastroId = UsuarioId,

                SequenciaEmissao = 1,

                Linha = !string.IsNullOrWhiteSpace(BoletoGerado.Linha) ? BoletoGerado.Linha : "LINHA NÃO RETORNADA",

                Valor = Faturamento.ValorFaturado,

                DataEmissao = DateTime.Now
            };

            await using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    await _context.FaturamentoBoleto.AddAsync(Boleto, ct);

                    await _context.SaveChangesAsync(ct);

                    await new BucketService(_context, _httpClientFactory)
                        .SendFileAsync(BucketNomeTabelaOrigemEnum.Boleto, Boleto.FaturamentoBoletoId, UsuarioId,
                            BoletoGerado.Boleto, true, "", ct);

                    await transaction.CommitAsync(ct);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(ct);

                    throw;
                }
            }

            #endregion Cadastro do Boleto e da Imagem

            ResultView.Identificador = linhaId;

            ResultView.LinhaDigitavel = Boleto.Linha;

            ResultView.Imagem = BoletoGerado.Boleto;

            ResultView.Mensagem = MensagemViewHelper.SetOk("Boleto gerado com sucesso");

            return ResultView;
        }

        public async Task CancelAsync(int FaturamentoId, CancellationToken ct = default)
        {
            // Se reprocessar for verdadeiro, cancela qualquer boleto anterior que não esteja cancelado ("N" ou "P")
            // Caso contrário, cancela apenas boleto não pago ("N")
            List<BoletoModel> result = await _context.FaturamentoBoleto
                .Where(x => x.FaturamentoId == FaturamentoId
                            && x.Status == "N")
                .ToListAsync(ct);

            if (result?.Count > 0)
            {
                BucketService bucketService = new(_context, _httpClientFactory);

                foreach (BoletoModel FaturamentoBoleto in result)
                {
                    FaturamentoBoleto.Status = "C";

                    _context.FaturamentoBoleto.Update(FaturamentoBoleto);

                    await _context.FaturamentoBoletoImagem
                        .Where(x => x.FaturamentoBoletoId == FaturamentoBoleto.FaturamentoBoletoId)
                        .DeleteAsync(ct);

                    await bucketService.DeleteFilesAsync("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId, ct);
                }

                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
