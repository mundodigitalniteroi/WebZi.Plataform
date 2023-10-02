using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Strings;
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
            AtendimentoViewModel ReturnView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add("Identificador do Atendimento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNewMessage(erros, MensagemTipoAvisoEnum.Impeditivo);

                return ReturnView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ReturnView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário desativado ou inexistente");

                return ReturnView;
            }

            GrvModel Grv = await _context.Grv
                .Include(i => i.Atendimento)
                .Where(w => w.Atendimento.AtendimentoId == AtendimentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNotFound("GRV não encontrado");

                return ReturnView;
            }
            else if (!await new GrvService(_context).UserCanAccessGrv(Grv.GrvId, UsuarioId))
            {
                ReturnView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

                return ReturnView;
            }
            else if (Grv.Atendimento == null)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNotFound("Atendimento não encontrado");

                return ReturnView;
            }

            ReturnView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ReturnView.Mensagem = MensagemViewHelper.GetOk("Registro encontrado com sucesso");

            return ReturnView;
        }

        public async Task<AtendimentoViewModel> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoViewModel ReturnView = new();

            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                erros.Add("Informe o Número do Processo");
            }
            else if (!StringHelper.IsNumber(NumeroProcesso) || Convert.ToInt64(NumeroProcesso) <= 0)
            {
                erros.Add("Número do Processo inválido");
            }

            if (ClienteId <= 0)
            {
                erros.Add("Identificador do Cliente inválido");
            }

            if (DepositoId <= 0)
            {
                erros.Add("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNewMessage(erros, MensagemTipoAvisoEnum.Impeditivo);

                return ReturnView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ReturnView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário desativado ou inexistente");

                return ReturnView;
            }

            GrvModel Grv = await _context.Grv
                .Include(i => i.Atendimento)
                .Where(w => w.NumeroFormularioGrv == NumeroProcesso && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNotFound("GRV não encontrado");

                return ReturnView;
            }
            else if (!await new GrvService(_context).UserCanAccessGrv(Grv.GrvId, UsuarioId))
            {
                ReturnView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

                return ReturnView;
            }
            else if (Grv.Atendimento == null)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNotFound("Atendimento não encontrado");

                return ReturnView;
            }

            ReturnView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ReturnView.Mensagem = MensagemViewHelper.GetOk("Registro encontrado com sucesso");

            return ReturnView;
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(AtendimentoCadastroViewModel Atendimento)
        {
            MensagemViewModel Mensagem = new();

            #region Validações de IDs
            List<string> erros = new();

            if (Atendimento.GrvId <= 0)
            {
                erros.Add("Primeiro é necessário informar o Identificador do GRV");
            }

            if (Atendimento.UsuarioId <= 0)
            {
                erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando o cadastro");
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.GetNewMessage(erros, MensagemTipoAvisoEnum.Impeditivo);
            }
            #endregion Validações de IDs

            #region Validações do Usuário
            if (!new UsuarioService(_context).IsUserActive(Atendimento.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized("Usuário desativado ou inexistente");
            }
            else if (!await new GrvService(_context).UserCanAccessGrv(Atendimento.GrvId, Atendimento.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV ou GRV inexistente");
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
            Mensagem = await new LeilaoService(_context)
                .GetAvisosLeilao(Grv.GrvId, Grv.StatusOperacaoId);

            if (Mensagem != null)
            {
                foreach (var item in Mensagem.AvisosInformativos)
                {
                    Mensagem.AvisosInformativos.Add(item);
                }

                if (Mensagem.Erros.Count > 0)
                {
                    return MensagemViewHelper.GetNewMessage(Mensagem.Erros, MensagemTipoAvisoEnum.Erro);
                }
            }
            else
            {
                Mensagem = new();
            }
            #endregion Leilão

            #region Dados do Responsável
            if (Atendimento.QualificacaoResponsavelId <= 0)
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNome))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(Atendimento.ResponsavelDocumento))
            {
                Mensagem.Erros.Add($"CPF do Responsável é inválido: {Atendimento.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(Atendimento.ResponsavelCnh))
                {
                    Mensagem.Erros.Add($"CNH do Responsável é inválido: {Atendimento.ResponsavelCnh}");
                }
            }
            #endregion Dados do Responsável

            #region Endereço do Responsável
            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelCep))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(Atendimento.ResponsavelCep))
            {
                Mensagem.Erros.Add($"CEP do Responsável inválido: {Atendimento.ResponsavelCep}");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelEndereco))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNumero))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelBairro))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelMunicipio))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelUf))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(Atendimento.ResponsavelUf))
            {
                Mensagem.Erros.Add("Unidade Federativa do Responsável inválida");
            }
            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(Atendimento.ResponsavelTelefone) && !ContactHelper.IsCellphone(Atendimento.ResponsavelTelefone)))
                {
                    Mensagem.AvisosImpeditivos.Add($"Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDdd))
                {
                    Mensagem.Erros.Add("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(Atendimento.ResponsavelDdd))
                {
                    Mensagem.AvisosImpeditivos.Add($"DDD do Número do Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelDdd}");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário
            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioNome))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Nome do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento))
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Documento do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId > 0)
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TipoDocumentoIdentificacao
                    .Where(w => w.TipoDocumentoIdentificacaoId == Atendimento.ProprietarioTipoDocumentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    Mensagem.Erros.Add($"Tipo do Documento do Proprietário inexistente: {Atendimento.ProprietarioTipoDocumentoId}");
                }
                else if (TipoDocumentoIdentificacao.Codigo != "CPF" && TipoDocumentoIdentificacao.Codigo == "CNPJ")
                {
                    Mensagem.AvisosImpeditivos.Add("O Tipo do Documento do Proprietário precisa ser CPF ou CNPJ");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF"
                    && !string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento)
                    && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    Mensagem.Erros.Add($"O CPF do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ"
                    && !string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento)
                    && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    Mensagem.Erros.Add($"O CNPJ do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNome))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(Atendimento.NotaFiscalDocumento) && !DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    Mensagem.Erros.Add($"CPF ou CNPJ do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDocumento}");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalCep))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(Atendimento.NotaFiscalCep))
                {
                    Mensagem.Erros.Add($"CEP do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalCep}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEndereco))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNumero))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalBairro))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalMunicipio))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalUf))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(Atendimento.NotaFiscalUf))
                {
                    Mensagem.Erros.Add($"Unidade Federativa do Receptor da Nota Fiscal inválida: {Atendimento.NotaFiscalUf}");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalTelefone))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(Atendimento.NotaFiscalTelefone) && !ContactHelper.IsCellphone(Atendimento.NotaFiscalTelefone))
                {
                    Mensagem.Erros.Add($"Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDdd))
                {
                    Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(Atendimento.NotaFiscalDdd))
                {
                    Mensagem.Erros.Add($"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDdd}");
                }

                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEmail) && !EmailHelper.IsEmail(Atendimento.NotaFiscalEmail))
                {
                    Mensagem.Erros.Add($"E-mail do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalEmail}");
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
                        Mensagem.AvisosImpeditivos.Add("Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }
                #endregion Inscrição Municipal do Tomador do Serviço
            }
            #endregion Nota Fiscal

            #region Faturamento
            if (Atendimento.TipoMeioCobrancaId <= 0)
            {
                Mensagem.AvisosImpeditivos.Add("Primeiro é necessário informar a Forma de Pagamento");
            }
            else
            {
                TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                    .Where(w => w.TipoMeioCobrancaId == Atendimento.TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoMeioCobranca == null)
                {
                    Mensagem.Erros.Add($"Forma de Pagamento inexistente: {Atendimento.TipoMeioCobrancaId}");
                }

                if ((TipoMeioCobranca.Alias == "PIX" || TipoMeioCobranca.Alias == "PIXEST") && Grv.Cliente.FlagPossuiPixEstatico == "N")
                {
                    Mensagem.AvisosImpeditivos.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Estático");
                }
                else if (TipoMeioCobranca.Alias == "PIXDIN" && Grv.Cliente.FlagPossuiPixDinamico == "N")
                {
                    Mensagem.AvisosImpeditivos.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Dinâmico");
                }
            }
            #endregion

            if (Mensagem.AvisosImpeditivos.Count > 0 || Mensagem.Erros.Count > 0)
            {
                Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
            }
            else
            {
                Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;
            }

            return Mensagem;
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaPagamento(PagamentoViewModel Atendimento)
        {
            MensagemViewModel mensagem = new();

            #region Consultas
            if (Atendimento.FaturamentoId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Identificador do Atendimento");
            }
            else if (Atendimento.UsuarioId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando a atualização");
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
                mensagem.Erros.Add("GRV sem permissão de acesso ou inexistente");

                return mensagem;
            }

            // TODO: Verificar o Status para confirmação do Pagamento
            if (grv.StatusOperacao.StatusOperacaoId != "V" && grv.StatusOperacao.StatusOperacaoId != "1")
            {
                mensagem.Erros.Add($"Status do GRV não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpper()}");

                return mensagem;
            }
            #endregion Consultas

            return mensagem;
        }

        public async Task<AtendimentoCadastroResultViewModel> Cadastrar(AtendimentoCadastroViewModel AtendimentoInput)
        {
            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId == AtendimentoInput.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = await GetDataHoraPorDeposito(Grv.DepositoId);
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

                ResponsavelUf = AtendimentoInput.ResponsavelUf.ToUpper(),

                ResponsavelCep = AtendimentoInput.ResponsavelCep.Replace("-", ""),

                ResponsavelDdd = AtendimentoInput.ResponsavelDdd,

                ResponsavelTelefone = AtendimentoInput.ResponsavelTelefone.Replace("-", ""),

                ProprietarioNome = AtendimentoInput.ProprietarioNome.ToUpper(),

                ProprietarioTipoDocumentoId = AtendimentoInput.ProprietarioTipoDocumentoId,

                ProprietarioDocumento = AtendimentoInput.ProprietarioDocumento,

                ProprietarioEndereco = AtendimentoInput.ProprietarioEndereco.ToUpper(),

                ProprietarioNumero = AtendimentoInput.ProprietarioNumero.ToUpper(),

                ProprietarioComplemento = AtendimentoInput.ProprietarioComplemento.ToUpper(),

                ProprietarioBairro = AtendimentoInput.ProprietarioBairro.ToUpper(),

                ProprietarioMunicipio = AtendimentoInput.ProprietarioMunicipio.ToUpper(),

                ProprietarioUf = AtendimentoInput.ProprietarioUf.ToUpper(),

                ProprietarioCep = AtendimentoInput.ProprietarioCep.Replace("-", ""),

                ProprietarioDdd = AtendimentoInput.ProprietarioDdd,

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

                Atendimento.NotaFiscalUf = AtendimentoInput.NotaFiscalUf.ToUpper();

                Atendimento.NotaFiscalCep = AtendimentoInput.NotaFiscalCep.Replace("-", "");

                Atendimento.NotaFiscalDdd = AtendimentoInput.NotaFiscalDdd;

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

            return AtendimentoCadastroResultView;
        }

        private async Task<DateTime> GetDataHoraPorDeposito(int DepositoId)
        {
            return await new DepositoService(_context)
                .GetDataHoraPorDeposito(DepositoId);
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

        private void CadastrarFoto(int AtendimentoId, AtendimentoCadastroViewModel AtendimentoInput)
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

        public async Task<AtendimentoFotoResponsavelViewModel> GetResponsavelFoto(int AtendimentoId, int UsuarioId)
        {
            AtendimentoFotoResponsavelViewModel ReturnView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add("Identificador do Atendimento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ReturnView.Mensagem = MensagemViewHelper.GetNewMessage(erros, MensagemTipoAvisoEnum.Impeditivo);

                return ReturnView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ReturnView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário desativado ou inexistente");

                return ReturnView;
            }

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == "ATENDIMFOTORESP")
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ReturnView.Foto = HttpClientHelper.DownloadFile(BucketArquivo.Url);

                ReturnView.Mensagem = MensagemViewHelper.GetOk("Registro encontrado com sucesso");

                return ReturnView;
            }
            else
            {
                AtendimentoFotoResponsavelModel AtendimentoFotoResponsavel = await _context.AtendimentoFotoResponsavel
                    .Where(w => w.AtendimentoId == AtendimentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (AtendimentoFotoResponsavel != null)
                {
                    ReturnView.Foto = AtendimentoFotoResponsavel.Foto;

                    ReturnView.Mensagem = MensagemViewHelper.GetOk("Registro encontrado com sucesso");

                    return ReturnView;
                }
                else
                {
                    ReturnView.Mensagem = MensagemViewHelper.GetNotFound("Registro não encontrado");

                    return ReturnView;
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