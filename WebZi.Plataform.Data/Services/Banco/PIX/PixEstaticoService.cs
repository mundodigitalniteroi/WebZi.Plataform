using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Code;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Banco.PIX.Base;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Banco.PIX.Estatico;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixEstaticoService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public PixEstaticoService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public PixEstaticoDTO Create(int FaturamentoId, int UsuarioId)
        {
            PixEstaticoDTO ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .AsNoTracking()
                .FirstOrDefault(x => x.FaturamentoId == FaturamentoId);

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

            if (Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("A Forma de Pagamento PIX Estático não está configurada para este Cliente");

                return ResultView;
            }

            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.PixEstatico)
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

            // Exclui o PIX Estático da Fatura caso exista
            _context.PixEstatico
                .Where(x => x.FaturamentoId == FaturamentoId)
                .Delete();

            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            PixBaseModel PixBaseEnvio = new()
            {
                Chave = Faturamento.Atendimento.Grv.Cliente.PixChave,

                SolicitacaoPagador = Faturamento.Atendimento.Grv.NumeroFormularioGrv,

                Valor = new PixBaseValorModel()
                {
                    Original = Math.Round(Faturamento.ValorFaturado, 2).ToString().Replace(",", ".")
                },

                Merchant = new PixBaseMerchantModel()
                {
                    Name = StringHelper.Normalize(Faturamento.Atendimento.Grv.Cliente.Nome.ToUpper().Trim()),

                    City = Faturamento.Atendimento.Grv.Cliente.Endereco.UF
                }
            };

            PixEstaticoRetornoModel PixEstaticoRetorno = new();

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    PixEstaticoRetorno = new HttpClientFactoryService(_httpClientFactory)
                        .PostBasicAuth<PixEstaticoRetornoModel>(
                            Configuracao.PixUrl,
                            Configuracao.PixUsername,
                            Configuracao.PixPassword,
                            PixBaseEnvio);

                    break;
                }
                catch (Exception ex)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex);

                    return ResultView;
                }
            }

            PixEstaticoModel Pix = new()
            {
                FaturamentoId = FaturamentoId,

                Chave = PixBaseEnvio.Chave,

                SolicitacaoPagador = PixBaseEnvio.SolicitacaoPagador,

                Valor = Math.Round(Faturamento.ValorFaturado, 2),

                MerchantName = PixBaseEnvio.Merchant.Name,

                MerchantCity = PixBaseEnvio.Merchant.City,

                QRString = PixEstaticoRetorno.QrString,

                QRCode = PixEstaticoRetorno.QrCode
            };

            _context.PixEstatico.Add(Pix);

            _context.SaveChanges();

            return new()
            {
                IdentificadorPix = Pix.PixId,

                Chave = Pix.Chave,

                SolicitacaoPagador = Pix.SolicitacaoPagador,

                Valor = Pix.Valor,

                MerchantName = Pix.MerchantName,

                MerchantCity = Pix.MerchantCity,

                QRString = Pix.QRString,

                QRCode = Pix.QRCode,

                Mensagem = MensagemViewHelper.SetCreateSuccess("PIX Estático gerado com sucesso")
            };
        }

        public async Task<SenhaPixEstaticoDTO> SearchPassword(int FaturamentoId, int UsuarioId)
        {
            #region Validacao
            if (FaturamentoId <= 0)
            {
                return new()
                {
                    Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido)
                };
            }
            #endregion Validacao

            #region Consulta
            PixDinamicoSenhaConfirmacaoTranferenciaModel ConfirmacaoSenha = await _context.PixDinamicoSenhaConfirmacaoTranferencia
                .AsTracking()
                .OrderByDescending(x => x.DataCadastro)
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId);
            #endregion Consulta

            if (ConfirmacaoSenha == null)
            {
                string Senha = CodeHelper.GenerateCode();
                string SenhaFinanceira = CodeHelper.GenerateCode();
                PixDinamicoSenhaConfirmacaoTranferenciaModel PixSenha = new()
                {
                    FaturamentoId = FaturamentoId,
                    UsuarioCadastroId = UsuarioId,
                    Senha = Senha,
                    SenhaFinanceiro = SenhaFinanceira,
                    DataCadastro = DateTime.Now
                };
                _context.PixDinamicoSenhaConfirmacaoTranferencia.Add(PixSenha);

                await _context.SaveChangesAsync();

                return new()
                {
                    IdentificadorFaturamento = FaturamentoId,
                    Senha = Senha,
                    Mensagem = MensagemViewHelper.SetOk()
                };
            }

            return new()
            {
                IdentificadorFaturamento = FaturamentoId,
                Senha = ConfirmacaoSenha.Senha,
                Mensagem = MensagemViewHelper.SetOk()
            };
        }

        public async Task<SenhaValidandoDTO> ValidatePassword(string senha)
        {
            SenhaValidandoDTO ResultView = new();
            #region Validacao
            if (String.IsNullOrEmpty(senha))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Senha não pode ser vazia");
                return ResultView;
            }
            if (senha.Length != 6)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Senha deve conter 6 caracteres");
                return ResultView;
            }

            #endregion Validacao

            #region Consulta
            PixDinamicoSenhaConfirmacaoTranferenciaModel ConfirmacaoSenha = await _context.PixDinamicoSenhaConfirmacaoTranferencia
                .AsTracking()
                .OrderByDescending(x => x.DataCadastro)
                .FirstOrDefaultAsync(x => x.SenhaFinanceiro == senha);
            #endregion Consulta

            if (ConfirmacaoSenha == null)
            {
                return new()
                {
                    EValida = false,
                    Mensagem = MensagemViewHelper.SetBadRequest("Senha inválida")
                };
            }
            if (ConfirmacaoSenha.FlagConfirmado == "S")
            {
                return new()
                {
                    EValida = false,
                    Mensagem = MensagemViewHelper.SetBadRequest("Já foi confirmado")
                };
            }
            ConfirmacaoSenha.FlagConfirmado = "S";
            await _context.SaveChangesAsync();

            return new()
            {
                EValida = true,
                Mensagem = MensagemViewHelper.SetOk()
            };
        }
    }
}