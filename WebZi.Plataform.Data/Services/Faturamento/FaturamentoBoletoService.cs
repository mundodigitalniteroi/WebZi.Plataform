using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.WsBoleto;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.Views.Faturamento;
using Z.EntityFramework.Plus;
using static WebZi.Plataform.Data.WsBoleto.WsBoletoSoapClient;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoBoletoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public FaturamentoBoletoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public ImageViewModelList GetBoletoNaoPago(int FaturamentoId, int UsuarioId)
        {
            return GetBoleto(FaturamentoId, UsuarioId, "N");
        }

        public ImageViewModelList GetBoletoNaoCancelado(int FaturamentoId, int UsuarioId)
        {
            return GetBoleto(FaturamentoId, UsuarioId);
        }

        private ImageViewModelList GetBoleto(int FaturamentoId, int UsuarioId, string StatusBoleto = "")
        {
            ImageViewModelList ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = new();

            if (!string.IsNullOrWhiteSpace(StatusBoleto))
            {
                Faturamento = _context.Faturamento
                    .Include(i => i.FaturamentoBoletos.Where(w => w.Status == StatusBoleto))
                    .Include(i => i.TipoMeioCobranca)
                    .Include(i => i.Atendimento)
                    .ThenInclude(t => t.Grv)
                    .Where(w => w.FaturamentoId == FaturamentoId)
                    .OrderByDescending(o => o.DataCadastro)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
            else
            {
                Faturamento = _context.Faturamento
                    .Include(i => i.FaturamentoBoletos.Where(w => w.Status != "C"))
                    .Include(i => i.TipoMeioCobranca)
                    .Include(i => i.Atendimento)
                    .ThenInclude(t => t.Grv)
                    .Where(w => w.FaturamentoId == FaturamentoId)
                    .OrderByDescending(o => o.DataCadastro)
                    .AsNoTracking()
                    .FirstOrDefault();
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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

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
            else if (Faturamento.FaturamentoBoletos?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Boleto foi cancelado ou inexistente");

                return ResultView;
            }

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.Boleto)
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                ResultView.Listagem.Add(new ImageViewModel { Imagem = HttpClientHelper.DownloadFile(BucketArquivo.Url) });

                return ResultView;
            }
            else
            {
                FaturamentoBoletoImagemModel FaturamentoBoletoImagem = _context.FaturamentoBoletoImagem
                    .Include(i => i.FaturamentoBoleto)
                    .Where(w => w.FaturamentoBoleto.FaturamentoId == Faturamento.FaturamentoBoletos.FirstOrDefault().FaturamentoBoletoId && w.FaturamentoBoleto.Status != "C")
                    .OrderByDescending(o => o.FaturamentoBoletoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (FaturamentoBoletoImagem != null)
                {
                    ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                    ResultView.Listagem.Add(new ImageViewModel { Imagem = FaturamentoBoletoImagem.Imagem });

                    return ResultView;
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound("A imagem do Boleto não foi gerado ou foi excluído");

                    return ResultView;
                }
            }
        }

        public ImageViewModelList Create(int FaturamentoId, int UsuarioId)
        {
            ImageViewModelList ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefault();

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

            ViewFaturamentoBoletoModel ViewBoleto = _context.ViewFaturamentoBoleto
                .FirstOrDefault(w => w.FaturamentoId == Faturamento.FaturamentoId);

            if (ViewBoleto == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Dados para a geração do Boleto inexistentes");

                return ResultView;
            }

            Cancel(Faturamento.FaturamentoId);

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

                sacado_bairro = ViewBoleto.SacadoBairro,

                sacado_cep = ViewBoleto.SacadoCEP,

                sacado_cidade = ViewBoleto.SacadoCidade,

                sacado_cpfCnpj = ViewBoleto.SacadoDocumento.Trim(),

                sacado_endereco = ViewBoleto.SacadoEndereco,

                sacado_nome = StringHelper.Normalize(ViewBoleto.SacadoNome ?? string.Empty),

                sacado_uf = ViewBoleto.SacadoUF,

                valor_boleto = ViewBoleto.ValorBoleto,

                vencimento = ViewBoleto.Vencimento,

                carteira = ViewBoleto.SacadoCarteira,

                instrucoes = ViewBoleto.SacadoInstrucoes
            };

            FaturamentoBoletoGeradoModel BoletoGerado = new()
            {
                DataVencimento = DateTimeHelper.GetDateTime(ViewBoleto.Vencimento[..10], "dd/MM/yyyy")
            };

            if (Faturamento.TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                List<FaturamentoRegraModel> FaturamentoRegras = _context.FaturamentoRegra
                    .Include(i => i.FaturamentoRegraTipo)
                    .Where(w => w.ClienteId == Faturamento.Atendimento.Grv.ClienteId && w.DepositoId == Faturamento.Atendimento.Grv.DepositoId)
                    .AsNoTracking()
                    .ToList();

                if (FaturamentoRegras?.Count > 0)
                {
                    FaturamentoRegraModel FaturamentoRegra = FaturamentoRegras
                        .Where(w => w.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.QuantidadeDiasSomarDataVencimentoBoleto)
                        .FirstOrDefault();

                    if (FaturamentoRegra != null)
                    {
                        BoletoGerado.DiasConfiguracaoDataVencimento = int.Parse(FaturamentoRegra.Valor);

                        BoletoGerado.DataVencimento = DateTimeHelper.AddDays(BoletoGerado.DataVencimento, BoletoGerado.DiasConfiguracaoDataVencimento);

                        DadosBoleto.vencimento = BoletoGerado.DataVencimento.ToString("dd/MM/yyyy");
                    }
                }
            }
            #endregion Preenchimento do Modelo

            #region Execução do WebService de geração do Boleto
            WebServiceUrlModel WebServiceUrl = _context.WebServiceUrl
                .Where(w => w.WsName == "WsBoletoSoap")
                .AsNoTracking()
                .FirstOrDefault();

            int linhaId;

            string linha;

            string url;

            BoletoGerado.Boleto = new WsBoletoSoapClient(EndpointConfiguration.WsBoletoSoap, WebServiceUrl.WsUrl).BoletoBancosRetornoLinha(
                boleto: DadosBoleto,
                login: WebServiceUrl.WsUsername,
                senha: WebServiceUrl.WsPassword,
                Tipo: "img",
                isdev: true,
                linha: out linha,
                linha_id: out linhaId,
                url: out url);

            BoletoGerado.BoletoId = linhaId;

            BoletoGerado.Linha = linha;
            #endregion Execução do WebService de geração do Boleto

            #region Cadastro do Boleto e da Imagem
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    FaturamentoBoletoModel FaturamentoBoleto = new()
                    {
                        FaturamentoId = Faturamento.FaturamentoId,

                        BoletoId = BoletoGerado.BoletoId,

                        UsuarioCadastroId = UsuarioId,

                        SequenciaEmissao = 1,

                        Linha = !string.IsNullOrWhiteSpace(BoletoGerado.Linha) ? BoletoGerado.Linha : "LINHA NÃO RETORNADA",

                        Valor = Faturamento.ValorFaturado,

                        DataEmissao = DateTime.Now
                    };

                    _context.FaturamentoBoleto.Add(FaturamentoBoleto);

                    _context.SaveChanges();

                    new BucketArquivoService(_context, _httpClientFactory).SendFile("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId, UsuarioId, BoletoGerado.Boleto);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
            #endregion Cadastro do Boleto e da Imagem

            ResultView.Listagem.Add(new ImageViewModel { Imagem = BoletoGerado.Boleto });

            ResultView.Mensagem = MensagemViewHelper.SetOk("Boleto gerado com sucesso");

            return ResultView;
        }

        public void Cancel(int FaturamentoId)
        {
            // Apesar de ser uma lista, por regra, só pode haver 1 Boleto cadastrado não pago
            List<FaturamentoBoletoModel> result = _context.FaturamentoBoleto
                    .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                    .ToList();

            if (result?.Count > 0)
            {
                foreach (FaturamentoBoletoModel FaturamentoBoleto in result)
                {
                    FaturamentoBoleto.Status = "C";

                    _context.FaturamentoBoleto.Update(FaturamentoBoleto);

                    _context.FaturamentoBoletoImagem
                        .Where(w => w.FaturamentoBoletoId == FaturamentoBoleto.FaturamentoBoletoId)
                    .Delete();

                    new BucketArquivoService(_context, _httpClientFactory)
                        .DeleteFiles("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId);
                }
            }
        }
    }
}