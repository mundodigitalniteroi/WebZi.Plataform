using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.Pagamento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AtendimentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AtendimentoViewModel> GetById(int AtendimentoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorAtendimentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(i => i.Atendimento)
                .Where(w => w.Atendimento.AtendimentoId == AtendimentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadrao.GrvNaoEncontrado);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadrao.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }
            else if (Grv.Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadrao.AtendimentoNaoEncontrado);

                return ResultView;
            }

            ResultView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<AtendimentoViewModel> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new();

            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                erros.Add(MensagemPadrao.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(NumeroProcesso) || Convert.ToInt64(NumeroProcesso) <= 0)
            {
                erros.Add(MensagemPadrao.NumeroProcessoInvalido);
            }

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorDepositoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(i => i.Atendimento)
                .Where(w => w.NumeroFormularioGrv == NumeroProcesso && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadrao.GrvNaoEncontrado);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadrao.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }
            else if (Grv.Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadrao.AtendimentoNaoEncontrado);

                return ResultView;
            }

            ResultView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(AtendimentoCadastroInputViewModel Atendimento)
        {
            MensagemViewModel ResultView = new();

            #region Validações de IDs
            List<string> erros = new();

            if (Atendimento.GrvId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorGrvInvalido);
            }

            if (Atendimento.UsuarioId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }
            #endregion Validações de IDs

            #region Validações do Usuário
            if (!new UsuarioService(_context).IsUserActive(Atendimento.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized();
            }
            #endregion Validações do Usuário

            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Include(i => i.Atendimento)
                .Where(w => w.GrvId == Atendimento.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadrao.GrvNaoEncontrado);
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, Atendimento.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized(MensagemPadrao.UsuarioSemPermissaoAcessoGrv);
            }

            if (Grv.StatusOperacao.StatusOperacaoId != "V" && Grv.StatusOperacao.StatusOperacaoId != "1")
            {
                return MensagemViewHelper.GetBadRequest($"Status do GRV não está apto para o cadastro do Atendimento: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }
            else if (Grv.Atendimento != null)
            {
                return MensagemViewHelper.GetBadRequest($"Este GRV já possui um Atendimento cadastrado: {Grv.Atendimento.AtendimentoId}");
            }
            #endregion Consultas

            #region Leilão
            ResultView = await new LeilaoService(_context, _mapper)
                .GetAvisosLeilao(Grv.GrvId, Grv.StatusOperacaoId);

            if (ResultView != null)
            {
                foreach (string item in ResultView.AvisosInformativos)
                {
                    ResultView.AvisosInformativos.Add(item);
                }

                if (ResultView.Erros.Count > 0)
                {
                    return MensagemViewHelper.GetBadRequest(ResultView.Erros);
                }
            }
            else
            {
                ResultView = new();
            }
            #endregion Leilão

            #region Dados do Responsável
            if (Atendimento.QualificacaoResponsavelId <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(Atendimento.ResponsavelDocumento))
            {
                ResultView.Erros.Add($"CPF do Responsável inválido: {Atendimento.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(Atendimento.ResponsavelCnh))
                {
                    ResultView.Erros.Add($"CNH do Responsável inválido: {Atendimento.ResponsavelCnh}");
                }
            }
            #endregion Dados do Responsável

            #region Endereço do Responsável
            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelCEP))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(Atendimento.ResponsavelCEP))
            {
                ResultView.Erros.Add($"CEP do Responsável inválido: {Atendimento.ResponsavelCEP}");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelEndereco))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNumero))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelBairro))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelMunicipio))
            {
                ResultView.AvisosImpeditivos.Add("Informe Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelUF))
            {
                ResultView.AvisosImpeditivos.Add("Informe a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(Atendimento.ResponsavelUF))
            {
                ResultView.Erros.Add("Unidade Federativa do Responsável inválida");
            }
            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(Atendimento.ResponsavelTelefone) && !ContactHelper.IsCellphone(Atendimento.ResponsavelTelefone)))
                {
                    ResultView.AvisosImpeditivos.Add($"Telefone/Celular do Responsável inválido: {Atendimento.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDDD))
                {
                    ResultView.Erros.Add("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(Atendimento.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add($"DDD do Número do Telefone/Celular do Responsável inválido: {Atendimento.ResponsavelDDD}");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário
            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Documento do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId > 0)
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TipoDocumentoIdentificacao
                    .Where(w => w.TipoDocumentoIdentificacaoId == Atendimento.ProprietarioTipoDocumentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    ResultView.Erros.Add($"Tipo do Documento do Proprietário inexistente: {Atendimento.ProprietarioTipoDocumentoId}");
                }
                else if (TipoDocumentoIdentificacao.Codigo != "CPF" && TipoDocumentoIdentificacao.Codigo != "CNPJ")
                {
                    ResultView.AvisosImpeditivos.Add("O Tipo do Documento do Proprietário precisa ser CPF ou CNPJ");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF"
                    && !string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento)
                    && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    ResultView.Erros.Add($"O CPF do Proprietário inválido: {Atendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ"
                    && !string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento)
                    && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    ResultView.Erros.Add($"O CNPJ do Proprietário inválido: {Atendimento.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNome))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(Atendimento.NotaFiscalDocumento) && !DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    ResultView.Erros.Add($"CPF ou CNPJ do Receptor da Nota Fiscal inválido: {Atendimento.NotaFiscalDocumento}");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(Atendimento.NotaFiscalCEP))
                {
                    ResultView.Erros.Add($"CEP do Receptor da Nota Fiscal inválido: {Atendimento.NotaFiscalCEP}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEndereco))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNumero))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalBairro))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalMunicipio))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalUF))
                {
                    ResultView.AvisosImpeditivos.Add("Informe a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(Atendimento.NotaFiscalUF))
                {
                    ResultView.Erros.Add($"Unidade Federativa do Receptor da Nota Fiscal inválida: {Atendimento.NotaFiscalUF}");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(Atendimento.NotaFiscalTelefone) && !ContactHelper.IsCellphone(Atendimento.NotaFiscalTelefone))
                {
                    ResultView.Erros.Add($"Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {Atendimento.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(Atendimento.NotaFiscalDDD))
                {
                    ResultView.Erros.Add($"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {Atendimento.NotaFiscalDDD}");
                }

                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEmail) && !EmailHelper.IsEmail(Atendimento.NotaFiscalEmail))
                {
                    ResultView.Erros.Add($"E-mail do Receptor da Nota Fiscal inválido: {Atendimento.NotaFiscalEmail}");
                }
                #endregion Contatos do Receptor da Nota Fiscal

                #region Inscrição Municipal do Tomador do Serviço
                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento) && DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel FaturamentoRegra = await _context.FaturamentoRegra
                        .Include(i => i.FaturamentoRegraTipo)
                        .Where(w => w.ClienteId == Grv.ClienteId &&
                                    w.FaturamentoRegraTipo.Codigo == "ATENDINSCRICMUNIC")
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (FaturamentoRegra != null)
                    {
                        ResultView.AvisosImpeditivos.Add("Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }
                #endregion Inscrição Municipal do Tomador do Serviço
            }
            #endregion Nota Fiscal

            #region Faturamento
            if (Atendimento.TipoMeioCobrancaId <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe a Forma de Pagamento");
            }
            else
            {
                TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                    .Where(w => w.TipoMeioCobrancaId == Atendimento.TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoMeioCobranca == null)
                {
                    ResultView.Erros.Add($"Forma de Pagamento inexistente: {Atendimento.TipoMeioCobrancaId}");
                }

                if ((TipoMeioCobranca.Alias == "PIX" || TipoMeioCobranca.Alias == "PIXEST") && Grv.Cliente.FlagPossuiPixEstatico == "N")
                {
                    ResultView.AvisosImpeditivos.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Estático");
                }
                else if (TipoMeioCobranca.Alias == "PIXDIN" && Grv.Cliente.FlagPossuiPixDinamico == "N")
                {
                    ResultView.AvisosImpeditivos.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Dinâmico");
                }
            }
            #endregion

            if (ResultView.AvisosImpeditivos.Count > 0 || ResultView.Erros.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
            }
            else
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.Ok;
            }

            return ResultView;
        }

        // TODO: Este método não está finalizado
        public async Task<MensagemViewModel> ValidarInformacoesParaPagamento(PagamentoViewModel Atendimento)
        {
            MensagemViewModel mensagem = new();

            #region Consultas
            if (Atendimento.FaturamentoId <= 0)
            {
                mensagem.Erros.Add(MensagemPadrao.IdentificadorAtendimentoInvalido);
            }
            else if (Atendimento.UsuarioId <= 0)
            {
                mensagem.Erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            if (mensagem.Erros.Count > 0)
            {
                return mensagem;
            }

            GrvModel grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Include(i => i.Atendimento)
                .Where(w => w.Atendimento.AtendimentoId == Atendimento.FaturamentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (grv == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadrao.GrvNaoEncontrado);
            }

            // TODO: Verificar o Status para confirmação do Pagamento
            if (grv.StatusOperacao.StatusOperacaoId != "V" && grv.StatusOperacao.StatusOperacaoId != "1")
            {
                mensagem.AvisosImpeditivos.Add($"Status do GRV não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpper()}");

                return mensagem;
            }
            #endregion Consultas

            return mensagem;
        }

        public async Task<AtendimentoCadastroResultViewModel> Cadastrar(AtendimentoCadastroInputViewModel AtendimentoInput)
        {
            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId == AtendimentoInput.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context, _mapper)
                .GetDataHoraPorDeposito(Grv.DepositoId);
            #endregion Consultas

            #region Dados do Atendimento
            AtendimentoModel Atendimento = new()
            {
                GrvId = AtendimentoInput.GrvId,

                QualificacaoResponsavelId = AtendimentoInput.QualificacaoResponsavelId,

                UsuarioCadastroId = AtendimentoInput.UsuarioId,

                DataHoraInicioAtendimento = AtendimentoInput.DataHoraInicioAtendimento ?? DataHoraPorDeposito,

                DataCadastro = DataHoraPorDeposito,

                DataImpressao = DataHoraPorDeposito,

                TotalImpressoes = 1,

                ResponsavelNome = AtendimentoInput.ResponsavelNome.ToUpper(),

                ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento.Replace(".", "").Replace("/", "").Replace("-", ""),

                ResponsavelCnh = AtendimentoInput.ResponsavelCnh,

                ResponsavelEndereco = AtendimentoInput.ResponsavelEndereco.ToUpper(),

                ResponsavelNumero = AtendimentoInput.ResponsavelNumero.ToUpper(),

                ResponsavelComplemento = AtendimentoInput.ResponsavelComplemento.ToUpper(),

                ResponsavelBairro = AtendimentoInput.ResponsavelBairro.ToUpper(),

                ResponsavelMunicipio = AtendimentoInput.ResponsavelMunicipio.ToUpper(),

                ResponsavelUF = AtendimentoInput.ResponsavelUF.ToUpper(),

                ResponsavelCEP = AtendimentoInput.ResponsavelCEP.Replace("-", ""),

                ResponsavelDDD = AtendimentoInput.ResponsavelDDD,

                ResponsavelTelefone = AtendimentoInput.ResponsavelTelefone.Replace("-", ""),

                ProprietarioNome = AtendimentoInput.ProprietarioNome.ToUpper(),

                ProprietarioTipoDocumentoId = AtendimentoInput.ProprietarioTipoDocumentoId,

                ProprietarioDocumento = AtendimentoInput.ProprietarioDocumento,

                ProprietarioEndereco = AtendimentoInput.ProprietarioEndereco.ToUpper(),

                ProprietarioNumero = AtendimentoInput.ProprietarioNumero.ToUpper(),

                ProprietarioComplemento = AtendimentoInput.ProprietarioComplemento.ToUpper(),

                ProprietarioBairro = AtendimentoInput.ProprietarioBairro.ToUpper(),

                ProprietarioMunicipio = AtendimentoInput.ProprietarioMunicipio.ToUpper(),

                ProprietarioUF = AtendimentoInput.ProprietarioUF.ToUpper(),

                ProprietarioCEP = AtendimentoInput.ProprietarioCEP.Replace("-", ""),

                ProprietarioDDD = AtendimentoInput.ProprietarioDDD,

                ProprietarioTelefone = AtendimentoInput.ProprietarioTelefone.Replace("-", "")
            };

            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                Atendimento.NotaFiscalNome = AtendimentoInput.NotaFiscalNome.ToUpper();

                Atendimento.NotaFiscalDocumento = AtendimentoInput.NotaFiscalDocumento.Replace(".", "").Replace("/", "").Replace("-", "");

                Atendimento.NotaFiscalEndereco = AtendimentoInput.NotaFiscalEndereco.ToUpper();

                Atendimento.NotaFiscalNumero = AtendimentoInput.NotaFiscalNumero.ToUpper();

                Atendimento.NotaFiscalComplemento = AtendimentoInput.NotaFiscalComplemento.ToUpper();

                Atendimento.NotaFiscalBairro = AtendimentoInput.NotaFiscalBairro.ToUpper();

                Atendimento.NotaFiscalMunicipio = AtendimentoInput.NotaFiscalMunicipio.ToUpper();

                Atendimento.NotaFiscalUF = AtendimentoInput.NotaFiscalUF.ToUpper();

                Atendimento.NotaFiscalCEP = AtendimentoInput.NotaFiscalCEP.Replace("-", "");

                Atendimento.NotaFiscalDDD = AtendimentoInput.NotaFiscalDDD;

                Atendimento.NotaFiscalTelefone = AtendimentoInput.NotaFiscalTelefone.Replace("-", "");

                Atendimento.NotaFiscalEmail = AtendimentoInput.NotaFiscalEmail.ToLower();

                Atendimento.NotaFiscalInscricaoMunicipal = AtendimentoInput.NotaFiscalInscricaoMunicipal.ToUpper();
            }
            #endregion Dados do Atendimento

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = await ConfigurarParametrosCalculoFaturamento(Grv, Atendimento, AtendimentoInput.TipoMeioCobrancaId, DataHoraPorDeposito);

            AtendimentoCadastroResultViewModel AtendimentoCadastroResultView = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Atendimento.Add(Atendimento);

                    _context.SaveChanges();

                    ParametrosCalculoFaturamento.Atendimento = Atendimento;

                    ParametrosCalculoFaturamento.Faturamento = new FaturamentoService(_context, _mapper)
                        .Faturar(ParametrosCalculoFaturamento);

                    CadastrarFoto(Atendimento.AtendimentoId, AtendimentoInput);

                    AtualizarStatusERP(ParametrosCalculoFaturamento);

                    CadastrarLiberacaoLeilao(ParametrosCalculoFaturamento);

                    AtualizarGrv(ParametrosCalculoFaturamento);

                    _context.SaveChanges();

                    transaction.Commit();

                    AtendimentoCadastroResultView.AtendimentoId = Atendimento.AtendimentoId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.Message);

                    if (ex.InnerException.Message != null)
                    {
                        AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.InnerException.Message);
                    }
                }
            }

            // TODO:
            // GerarFormaPagamento(ParametrosCalculoFaturamento);

            AtendimentoCadastroResultView.AtendimentoId = ParametrosCalculoFaturamento.Atendimento.AtendimentoId;

            AtendimentoCadastroResultView.Mensagem = MensagemViewHelper.GetOkCreate();

            return AtendimentoCadastroResultView;
        }

        private async Task<CalculoFaturamentoParametroModel> ConfigurarParametrosCalculoFaturamento(GrvModel Grv, AtendimentoModel Atendimento, int TipoMeioCobrancaId, DateTime DataHoraPorDeposito)
        {
            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                UsuarioCadastroId = Atendimento.UsuarioCadastroId,

                Grv = Grv,

                Cliente = Grv.Cliente,

                Deposito = Grv.Deposito,

                ClienteDeposito = await _context.ClienteDeposito
                    .Where(w => w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(),

                Atendimento = Atendimento,

                DataHoraPorDeposito = DataHoraPorDeposito,

                // Esta funcionalidade altera o GRV com Status de Leilão para Status de Atendimento
                // para que o fluxo do Atendimento/Faturamento/Liberação funcionem.
                StatusOperacaoLeilaoId = new[] { "1", "2", "4" }.Contains(Grv.StatusOperacaoId) ? Grv.StatusOperacaoId : string.Empty,

                TiposMeiosCobrancas = await _context.TipoMeioCobranca
                    .AsNoTracking()
                    .ToListAsync(),

                FaturamentoRegras = await _context.FaturamentoRegra
                        .Include(i => i.FaturamentoRegraTipo)
                        .Where(w => w.ClienteId == Grv.Cliente.ClienteId && w.DepositoId == Grv.Deposito.DepositoId)
                        .AsNoTracking()
                        .ToListAsync()
            };

            ParametrosCalculoFaturamento.TipoMeioCobranca = ParametrosCalculoFaturamento.TiposMeiosCobrancas
                .FirstOrDefault(w => w.TipoMeioCobrancaId == (ParametrosCalculoFaturamento.Cliente.TipoMeioCobrancaId.HasValue &&
                                                              ParametrosCalculoFaturamento.Cliente.TipoMeioCobrancaId.Value > 0 ? ParametrosCalculoFaturamento.Cliente.TipoMeioCobrancaId.Value : TipoMeioCobrancaId));

            // L: Aguardando Pagamento
            // U: Aguardando Liberação Especial
            ParametrosCalculoFaturamento.StatusOperacaoId = ParametrosCalculoFaturamento.TiposMeiosCobrancas
                .Where(w => w.TipoMeioCobrancaId == ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId)
                .FirstOrDefault().Alias != "LIBESP" ? "L" : "U";

            return ParametrosCalculoFaturamento;
        }

        private void CadastrarFoto(int AtendimentoId, AtendimentoCadastroInputViewModel AtendimentoInput)
        {
            if (AtendimentoInput.ResponsavelFoto != null)
            {
                new BucketArquivoService(_context).SendFile("ATENDIMFOTORESP", AtendimentoId, AtendimentoInput.UsuarioId, AtendimentoInput.ResponsavelFoto);
            }
        }

        private void AtualizarStatusERP(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.Cliente.FlagEmissaoNotaFiscal == "S" && !string.IsNullOrWhiteSpace(ParametrosCalculoFaturamento.ClienteDeposito.CodigoERPOrdemVenda))
            {
                ParametrosCalculoFaturamento.Atendimento.StatusCadastroERP = "P";

                if (ParametrosCalculoFaturamento.Faturamento.ValorFaturado > 0)
                {
                    ParametrosCalculoFaturamento.Atendimento.StatusCadastroOrdemVendaERP = "P";
                }

                _context.Atendimento.Update(ParametrosCalculoFaturamento.Atendimento);
            }
        }

        private void CadastrarLiberacaoLeilao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (new[] { "1", "2", "3" }.Contains(ParametrosCalculoFaturamento.StatusOperacaoLeilaoId))
            {
                _context.LiberacaoLeilao.Add(new()
                {
                    GrvId = ParametrosCalculoFaturamento.Grv.GrvId,

                    StatusOperacaoLeilaoId = ParametrosCalculoFaturamento.StatusOperacaoLeilaoId,

                    UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId
                });
            }
        }

        private void AtualizarGrv(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            GrvModel Grv = _context.Grv
                .Where(w => w.GrvId == ParametrosCalculoFaturamento.Grv.GrvId)
                .FirstOrDefault();

            Grv.StatusOperacaoId = ParametrosCalculoFaturamento.StatusOperacaoId;

            _context.Grv.Update(Grv);
        }

        public async Task<ImageViewModel> GetResponsavelFoto(int AtendimentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorAtendimentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == "ATENDIMFOTORESP")
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ResultView.Imagem = HttpClientHelper.DownloadFile(BucketArquivo.Url);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();

                return ResultView;
            }
            else
            {
                AtendimentoFotoResponsavelModel AtendimentoFotoResponsavel = await _context.AtendimentoFotoResponsavel
                    .Where(w => w.AtendimentoId == AtendimentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (AtendimentoFotoResponsavel != null)
                {
                    ResultView.Imagem = AtendimentoFotoResponsavel.Foto;

                    ResultView.Mensagem = MensagemViewHelper.GetOkFound();

                    return ResultView;
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                    return ResultView;
                }
            }
        }

        //private async void GerarFormaPagamento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        //{
        //    if (ParametrosCalculoFaturamento.Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao != "N" || ParametrosCalculoFaturamento.Faturamento.ValorFaturado <= 0)
        //    {
        //        return;
        //    }

        //    // BOLETO
        //    if (ParametrosCalculoFaturamento.TipoMeioCobranca.CodigoERP == "D")
        //    {

        //    }

        //    return;
        //}
    }
}