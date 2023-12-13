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
using WebZi.Plataform.Data.Services.WebServices;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public AtendimentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MensagemViewModel> CheckInformacoesParaCadastroAsync(AtendimentoCadastroInputViewModel AtendimentoCadastro)
        {
            if (AtendimentoCadastro.IdentificadorTipoMeioCobranca <= 0)
            {
                return MensagemViewHelper.SetBadRequest("Identificador da Forma de Pagamento inválido");
            }

            MensagemViewModel ResultView = new GrvService(_context)
                .ValidateInputGrv(AtendimentoCadastro.IdentificadorGrv, AtendimentoCadastro.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Include(i => i.Atendimento)
                .Where(w => w.GrvId == AtendimentoCadastro.IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (!new[] { "B", "D", "V", "L", "E", "1", "2", "3", "4", "7" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o cadastro do Atendimento. " +
                    $"Descrição do Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }
            else if (Grv.Atendimento != null)
            {
                return MensagemViewHelper.SetBadRequest($"Este GRV já possui um Atendimento cadastrado. Identificador do Atendimento: {Grv.Atendimento.AtendimentoId}");
            }
            #endregion Consultas

            #region Leilão
            ResultView = await new LeilaoService(_context)
                .GetAvisosLeilaoAsync(Grv.GrvId, Grv.StatusOperacaoId);

            if (ResultView != null)
            {
                foreach (string item in ResultView.AvisosInformativos.ToList())
                {
                    ResultView.AvisosInformativos.Add(item);
                }

                if (ResultView.Erros.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(ResultView.Erros);
                }
            }
            else
            {
                ResultView = new();
            }
            #endregion Leilão

            #region Dados do Responsável
            if (AtendimentoCadastro.IdentificadorQualificacaoResponsavel <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(AtendimentoCadastro.ResponsavelDocumento))
            {
                ResultView.AvisosImpeditivos.Add($"CPF do Responsável inválido: {AtendimentoCadastro.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(AtendimentoCadastro.ResponsavelCnh))
                {
                    ResultView.AvisosImpeditivos.Add($"CNH do Responsável inválido: {AtendimentoCadastro.ResponsavelCnh}");
                }
            }
            #endregion Dados do Responsável

            #region Endereço do Responsável
            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelCEP))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(AtendimentoCadastro.ResponsavelCEP))
            {
                ResultView.AvisosImpeditivos.Add($"CEP do Responsável inválido: {AtendimentoCadastro.ResponsavelCEP}");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelEndereco))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelNumero))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelBairro))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelMunicipio))
            {
                ResultView.AvisosImpeditivos.Add("Informe Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelUF))
            {
                ResultView.AvisosImpeditivos.Add("Informe a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(AtendimentoCadastro.ResponsavelUF))
            {
                ResultView.AvisosImpeditivos.Add("Unidade Federativa do Responsável inválida");
            }
            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(AtendimentoCadastro.ResponsavelTelefone) && !ContactHelper.IsCellphone(AtendimentoCadastro.ResponsavelTelefone)))
                {
                    ResultView.AvisosImpeditivos.Add($"Telefone/Celular do Responsável inválido: {AtendimentoCadastro.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(AtendimentoCadastro.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add($"DDD do Número do Telefone/Celular do Responsável inválido: {AtendimentoCadastro.ResponsavelDDD}");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário
            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Proprietário");
            }

            if (AtendimentoCadastro.IdentificadorProprietarioTipoDocumento <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Documento do Proprietário");
            }

            if (AtendimentoCadastro.IdentificadorProprietarioTipoDocumento > 0)
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TipoDocumentoIdentificacao
                    .Where(w => w.TipoDocumentoIdentificacaoId == AtendimentoCadastro.IdentificadorProprietarioTipoDocumento)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    ResultView.AvisosImpeditivos.Add($"Tipo do Documento do Proprietário inexistente: {AtendimentoCadastro.IdentificadorProprietarioTipoDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo != "CPF" && TipoDocumentoIdentificacao.Codigo != "CNPJ")
                {
                    ResultView.AvisosImpeditivos.Add("O Tipo do Documento do Proprietário precisa ser CPF ou CNPJ");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF"
                    && !string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioDocumento)
                    && !DocumentHelper.IsCPF(AtendimentoCadastro.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add($"O CPF do Proprietário inválido: {AtendimentoCadastro.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ"
                    && !string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioDocumento)
                    && !DocumentHelper.IsCNPJ(AtendimentoCadastro.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add($"O CNPJ do Proprietário inválido: {AtendimentoCadastro.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalNome))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(AtendimentoCadastro.NotaFiscalDocumento) && !DocumentHelper.IsCNPJ(AtendimentoCadastro.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add($"CPF ou CNPJ do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalDocumento}");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(AtendimentoCadastro.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add($"CEP do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalCEP}");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalEndereco))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalNumero))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalBairro))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalMunicipio))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalUF))
                {
                    ResultView.AvisosImpeditivos.Add("Informe a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(AtendimentoCadastro.NotaFiscalUF))
                {
                    ResultView.AvisosImpeditivos.Add($"Unidade Federativa do Receptor da Nota Fiscal inválida: {AtendimentoCadastro.NotaFiscalUF}");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(AtendimentoCadastro.NotaFiscalTelefone) && !ContactHelper.IsCellphone(AtendimentoCadastro.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add($"Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(AtendimentoCadastro.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add($"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalDDD}");
                }

                if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalEmail) && !EmailHelper.IsEmail(AtendimentoCadastro.NotaFiscalEmail))
                {
                    ResultView.AvisosImpeditivos.Add($"E-mail do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalEmail}");
                }
                #endregion Contatos do Receptor da Nota Fiscal

                #region Inscrição Municipal do Tomador do Serviço
                if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalDocumento) && DocumentHelper.IsCNPJ(AtendimentoCadastro.NotaFiscalDocumento))
                {
                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel FaturamentoRegra = await _context.FaturamentoRegra
                        .Include(i => i.FaturamentoRegraTipo)
                        .Where(w => w.ClienteId == Grv.ClienteId &&
                                    w.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.ObrigatorioInformarInscricaoMunicipal)
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

            #region Forma de Pagamento
            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == AtendimentoCadastro.IdentificadorTipoMeioCobranca)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoMeioCobranca == null)
            {
                ResultView.AvisosImpeditivos.Add($"Forma de Pagamento inexistente: {AtendimentoCadastro.IdentificadorTipoMeioCobranca}");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico && Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                ResultView.AvisosImpeditivos.Add("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Estático");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico && Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                ResultView.AvisosImpeditivos.Add("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Dinâmico");
            }
            #endregion Forma de Pagamento

            if (ResultView.AvisosImpeditivos.Count > 0)
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
        public async Task<MensagemViewModel> CheckInformacoesParaPagamentoAsync(PagamentoViewModel Atendimento)
        {
            MensagemViewModel mensagem = new();

            #region Consultas
            if (Atendimento.IdentificadorFaturamento <= 0)
            {
                mensagem.Erros.Add(MensagemPadraoEnum.IdentificadorAtendimentoInvalido);
            }
            else if (Atendimento.IdentificadorUsuario <= 0)
            {
                mensagem.Erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
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
                .Where(w => w.Atendimento.AtendimentoId == Atendimento.IdentificadorFaturamento)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            // TODO: Verificar o Status para confirmação do Pagamento
            if (grv.StatusOperacao.StatusOperacaoId != "V" && grv.StatusOperacao.StatusOperacaoId != "1")
            {
                mensagem.AvisosImpeditivos.Add($"Status do GRV não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpperTrim()}");

                return mensagem;
            }
            #endregion Consultas

            return mensagem;
        }

        public async Task<AtendimentoCadastroResultViewModel> CreateAtendimentoAsync(AtendimentoCadastroInputViewModel AtendimentoInput)
        {
            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId == AtendimentoInput.IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);
            #endregion Consultas

            #region Dados do Atendimento
            AtendimentoModel Atendimento = new()
            {
                GrvId = AtendimentoInput.IdentificadorGrv,

                QualificacaoResponsavelId = AtendimentoInput.IdentificadorQualificacaoResponsavel,

                UsuarioCadastroId = AtendimentoInput.IdentificadorUsuario,

                DataHoraInicioAtendimento = AtendimentoInput.DataHoraInicioAtendimento ?? DataHoraPorDeposito,

                DataCadastro = DataHoraPorDeposito,

                DataImpressao = DataHoraPorDeposito,

                TotalImpressoes = 1,

                ResponsavelNome = AtendimentoInput.ResponsavelNome.ToUpperTrim(),

                ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento.Replace(".", "").Replace("/", "").Replace("-", ""),

                ResponsavelCnh = AtendimentoInput.ResponsavelCnh,

                ResponsavelEndereco = AtendimentoInput.ResponsavelEndereco.ToUpperTrim(),

                ResponsavelNumero = AtendimentoInput.ResponsavelNumero.ToUpperTrim(),

                ResponsavelComplemento = AtendimentoInput.ResponsavelComplemento.ToUpperTrim(),

                ResponsavelBairro = AtendimentoInput.ResponsavelBairro.ToUpperTrim(),

                ResponsavelMunicipio = AtendimentoInput.ResponsavelMunicipio.ToUpperTrim(),

                ResponsavelUF = AtendimentoInput.ResponsavelUF.ToUpperTrim(),

                ResponsavelCEP = AtendimentoInput.ResponsavelCEP.Replace("-", ""),

                ResponsavelDDD = AtendimentoInput.ResponsavelDDD,

                ResponsavelTelefone = AtendimentoInput.ResponsavelTelefone.Replace("-", ""),

                ProprietarioNome = AtendimentoInput.ProprietarioNome.ToUpperTrim(),

                ProprietarioTipoDocumentoId = AtendimentoInput.IdentificadorProprietarioTipoDocumento,

                ProprietarioDocumento = AtendimentoInput.ProprietarioDocumento,

                ProprietarioEndereco = AtendimentoInput.ProprietarioEndereco.ToUpperTrim(),

                ProprietarioNumero = AtendimentoInput.ProprietarioNumero.ToUpperTrim(),

                ProprietarioComplemento = AtendimentoInput.ProprietarioComplemento.ToUpperTrim(),

                ProprietarioBairro = AtendimentoInput.ProprietarioBairro.ToUpperTrim(),

                ProprietarioMunicipio = AtendimentoInput.ProprietarioMunicipio.ToUpperTrim(),

                ProprietarioUF = AtendimentoInput.ProprietarioUF.ToUpperTrim(),

                ProprietarioCEP = AtendimentoInput.ProprietarioCEP.Replace("-", ""),

                ProprietarioDDD = AtendimentoInput.ProprietarioDDD,

                ProprietarioTelefone = AtendimentoInput.ProprietarioTelefone.Replace("-", "")
            };

            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                Atendimento.NotaFiscalNome = AtendimentoInput.NotaFiscalNome.ToUpperTrim();

                Atendimento.NotaFiscalDocumento = AtendimentoInput.NotaFiscalDocumento.Replace(".", "").Replace("/", "").Replace("-", "");

                Atendimento.NotaFiscalEndereco = AtendimentoInput.NotaFiscalEndereco.ToUpperTrim();

                Atendimento.NotaFiscalNumero = AtendimentoInput.NotaFiscalNumero.ToUpperTrim();

                Atendimento.NotaFiscalComplemento = AtendimentoInput.NotaFiscalComplemento.ToUpperTrim();

                Atendimento.NotaFiscalBairro = AtendimentoInput.NotaFiscalBairro.ToUpperTrim();

                Atendimento.NotaFiscalMunicipio = AtendimentoInput.NotaFiscalMunicipio.ToUpperTrim();

                Atendimento.NotaFiscalUF = AtendimentoInput.NotaFiscalUF.ToUpperTrim();

                Atendimento.NotaFiscalCEP = AtendimentoInput.NotaFiscalCEP.Replace("-", "");

                Atendimento.NotaFiscalDDD = AtendimentoInput.NotaFiscalDDD;

                Atendimento.NotaFiscalTelefone = AtendimentoInput.NotaFiscalTelefone.Replace("-", "");

                Atendimento.NotaFiscalEmail = AtendimentoInput.NotaFiscalEmail.ToLower();

                Atendimento.NotaFiscalInscricaoMunicipal = AtendimentoInput.NotaFiscalInscricaoMunicipal.ToUpperTrim();
            }
            #endregion Dados do Atendimento

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = await ConfigParametrosCalculoFaturamentoAsync(Grv, Atendimento, AtendimentoInput.IdentificadorTipoMeioCobranca, DataHoraPorDeposito);

            AtendimentoCadastroResultViewModel AtendimentoCadastroResultView = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Atendimento.Add(Atendimento);

                    _context.SaveChanges();

                    ParametrosCalculoFaturamento.Atendimento = Atendimento;

                    ParametrosCalculoFaturamento.Faturamento = new FaturamentoService(_context, _mapper, _httpClientFactory)
                        .Faturar(ParametrosCalculoFaturamento);

                    CreateFotoResponsavel(Atendimento.AtendimentoId, AtendimentoInput);

                    UpdateStatusERP(ParametrosCalculoFaturamento);

                    CreateLiberacaoLeilao(ParametrosCalculoFaturamento);

                    UpdateGrv(ParametrosCalculoFaturamento);

                    _context.SaveChanges();

                    transaction.Commit();

                    AtendimentoCadastroResultView.IdentificadorAtendimento = Atendimento.AtendimentoId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    AtendimentoCadastroResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                    return AtendimentoCadastroResultView;
                }
            }

            // TODO:
            // GerarFormaPagamento(ParametrosCalculoFaturamento);

            AtendimentoCadastroResultView.IdentificadorAtendimento = ParametrosCalculoFaturamento.Atendimento.AtendimentoId;

            AtendimentoCadastroResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();

            return AtendimentoCadastroResultView;
        }

        private void CreateFotoResponsavel(int AtendimentoId, AtendimentoCadastroInputViewModel AtendimentoInput)
        {
            if (AtendimentoInput.ResponsavelFoto != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile("ATENDIMFOTORESP", AtendimentoId, AtendimentoInput.IdentificadorUsuario, AtendimentoInput.ResponsavelFoto);
            }
        }

        public void CreateLiberacaoLeilao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
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

        private async Task<CalculoFaturamentoParametroModel> ConfigParametrosCalculoFaturamentoAsync(GrvModel Grv, AtendimentoModel Atendimento, int TipoMeioCobrancaId, DateTime DataHoraPorDeposito)
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

        public async Task<AtendimentoViewModel> GetByIdAsync(int AtendimentoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new();

            if (AtendimentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorAtendimentoInvalido);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.Atendimento)
                .Where(x => x.Atendimento.AtendimentoId == AtendimentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Grv.Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoAtendimento);

                return ResultView;
            }

            ResultView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<AtendimentoViewModel> GetByProcessoAsync(string NumeroProcesso, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidateInputGrv(NumeroProcesso, CodigoProduto, ClienteId, DepositoId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.Atendimento)
                .Where(x => x.NumeroFormularioGrv == NumeroProcesso
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv.Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoAtendimento);

                return ResultView;
            }

            ResultView = _mapper.Map<AtendimentoViewModel>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<ImageViewModelList> GetResponsavelFotoAsync(int AtendimentoId, int UsuarioId)
        {
            ImageViewModelList ResultView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAtendimentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            if (!await new UsuarioService(_context).IsUserActiveAsync(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.SetUnauthorized();

                return ResultView;
            }

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel)
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ResultView.Listagem.Add(new ImageViewModel { Imagem = HttpClientHelper.DownloadFile(BucketArquivo.Url) });

                ResultView.Mensagem = MensagemViewHelper.SetFound();

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
                    ResultView.Listagem.Add(new ImageViewModel { Imagem = AtendimentoFotoResponsavel.Foto });

                    ResultView.Mensagem = MensagemViewHelper.SetFound();

                    return ResultView;
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                    return ResultView;
                }
            }
        }

        public async Task<QualificacaoResponsavelViewModelList> ListQualificacaoResponsavelAsync()
        {
            QualificacaoResponsavelViewModelList ResultView = new();

            List<QualificacaoResponsavelModel> result = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<QualificacaoResponsavelViewModel>>(result
                    .OrderBy(o => o.Descricao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(ResultView.Listagem.Count);

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                return ResultView;
            }
        }

        private void UpdateStatusERP(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
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

        private void UpdateGrv(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            GrvModel Grv = _context.Grv
                .Where(w => w.GrvId == ParametrosCalculoFaturamento.Grv.GrvId)
                .FirstOrDefault();

            Grv.StatusOperacaoId = ParametrosCalculoFaturamento.StatusOperacaoId;

            _context.Grv.Update(Grv);
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