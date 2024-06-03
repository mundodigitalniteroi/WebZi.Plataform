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
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Faturamento.Cadastro;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Pagamento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public AtendimentoService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public AtendimentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MensagemDTO> CheckInformacoesParaCadastroAsync(AtendimentoParameters AtendimentoCadastro)
        {
            if (AtendimentoCadastro.IdentificadorTipoMeioCobranca <= 0)
            {
                return MensagemViewHelper.SetBadRequest("Identificador da Forma de Pagamento inválido");
            }

            MensagemDTO ResultView = new GrvService(_context)
                .ValidateInputGrv(AtendimentoCadastro.IdentificadorProcesso, AtendimentoCadastro.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Atendimento)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == AtendimentoCadastro.IdentificadorProcesso);

            if (!new[] { "B", "D", "V", "L", "E", "1", "2", "3", "4", "7" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste Processo não permite o cadastro do Atendimento. " +
                    $"Descrição do Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }
            else if (Grv.Atendimento != null)
            {
                return MensagemViewHelper.SetBadRequest($"Este Processo já possui um Atendimento cadastrado. Identificador do Atendimento: {Grv.Atendimento.AtendimentoId}");
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

            if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelCNH))
            {
                if (!DocumentHelper.IsCNH(AtendimentoCadastro.ResponsavelCNH))
                {
                    ResultView.AvisosImpeditivos.Add($"CNH do Responsável inválido: {AtendimentoCadastro.ResponsavelCNH}");
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
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w => w.TipoDocumentoIdentificacaoId == AtendimentoCadastro.IdentificadorProprietarioTipoDocumento);

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
                        .Include(x => x.FaturamentoRegraTipo)
                        .Where(x => x.ClienteId == Grv.ClienteId &&
                                    x.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.ObrigatorioInformarInscricaoMunicipal)
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
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == AtendimentoCadastro.IdentificadorTipoMeioCobranca);

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
        public async Task<MensagemDTO> CheckInformacoesParaPagamentoAsync(PagamentoParameters Atendimento)
        {
            MensagemDTO mensagem = new();

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
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Atendimento)
                .Where(x => x.Atendimento.AtendimentoId == Atendimento.IdentificadorFaturamento)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            // TODO: Verificar o Status para confirmação do Pagamento
            if (grv.StatusOperacao.StatusOperacaoId != "V" && grv.StatusOperacao.StatusOperacaoId != "1")
            {
                mensagem.AvisosImpeditivos.Add($"Status do Processo não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpperTrim()}");

                return mensagem;
            }
            #endregion Consultas

            return mensagem;
        }

        public async Task<AtendimentoCadastroDTO> CreateAtendimentoAsync(AtendimentoParameters AtendimentoInput)
        {
            #region Consultas
            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Where(x => x.GrvId == AtendimentoInput.IdentificadorProcesso)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);
            #endregion Consultas

            #region Dados do Atendimento
            AtendimentoModel Atendimento = new()
            {
                GrvId = AtendimentoInput.IdentificadorProcesso,

                QualificacaoResponsavelId = AtendimentoInput.IdentificadorQualificacaoResponsavel,

                UsuarioCadastroId = AtendimentoInput.IdentificadorUsuario,

                DataHoraInicioAtendimento = AtendimentoInput.DataHoraInicioAtendimento,

                DataCadastro = DataHoraPorDeposito,

                ResponsavelNome = AtendimentoInput.ResponsavelNome.ToUpperTrim(),

                ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento.Replace(".", "").Replace("/", "").Replace("-", ""),

                ResponsavelCnh = AtendimentoInput.ResponsavelCNH,

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

                ProprietarioTelefone = AtendimentoInput.ProprietarioTelefone.Replace("-", ""),

                FormaLiberacao = AtendimentoInput.FormaLiberacao.ToUpperTrim(),

                FormaLiberacaoCNH = AtendimentoInput.FormaLiberacaoCNH,

                FormaLiberacaoCPF = AtendimentoInput.FormaLiberacaoCPF.Replace(".", "").Replace("/", "").Replace("-", ""),

                FormaLiberacaoNome = AtendimentoInput.FormaLiberacaoNome.ToUpperTrim(),

                FormaLiberacaoPlaca = AtendimentoInput.FormaLiberacaoPlaca.Replace("-", "").ToUpperTrim(),                
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

                Atendimento.NotaFiscalEmail = AtendimentoInput.NotaFiscalEmail.ToLowerTrim();

                Atendimento.NotaFiscalInscricaoMunicipal = AtendimentoInput.NotaFiscalInscricaoMunicipal.ToUpperTrim();
            }
            #endregion Dados do Atendimento

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = await ConfigParametrosCalculoFaturamentoAsync(Grv, AtendimentoInput.IdentificadorTipoMeioCobranca, AtendimentoInput.IdentificadorUsuario, DataHoraPorDeposito);

            AtendimentoCadastroDTO ResultView = new();

            FaturamentoModel Faturamento = new();

            CalculoDiariasModel CalculoDiarias = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Atendimento.Add(Atendimento);

                    _context.SaveChanges();

                    Faturamento = new FaturamentoService(_context)
                        .Faturar(ParametrosCalculoFaturamento, out CalculoDiarias);

                    CreateFotoResponsavel(Atendimento.AtendimentoId, AtendimentoInput);

                    UpdateStatusERP(ParametrosCalculoFaturamento.ClienteDeposito, Faturamento, Atendimento);

                    CreateLiberacaoLeilao(ParametrosCalculoFaturamento);

                    UpdateGrv(ParametrosCalculoFaturamento);

                    _context.SaveChanges();

                    transaction.Commit();

                    ResultView.IdentificadorAtendimento = Atendimento.AtendimentoId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            ResultView.Faturamento = _mapper.Map<FaturamentoCadastroDTO>(Faturamento);

            ResultView.Faturamento.ListagemServico = _mapper.Map<List<FaturamentoCadastroComposicaoDTO>>(Faturamento.ListagemFaturamentoComposicao);

            FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

            foreach (var item in ResultView.Faturamento.ListagemServico)
            {
                FaturamentoServicoTipoVeiculo = _context.FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.FaturamentoServicoTipoVeiculoId == item.IdentificadorFaturamentoServicoTipoVeiculo);

                item.DescricaoTipoServico = ListagemTipoCobranca.Where(x => x.ValorCadastro == item.TipoServico).FirstOrDefault().Descricao;

                item.NomeServico = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao;

                item.DataVigenciaInicial = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaInicial;

                item.DataVigenciaFinal = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaFinal;
            }

            // TODO:
            // GerarFormaPagamento(ParametrosCalculoFaturamento);

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();

            return ResultView;
        }

        private void CreateFotoResponsavel(int AtendimentoId, AtendimentoParameters AtendimentoInput)
        {
            if (AtendimentoInput.ResponsavelFoto != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel, AtendimentoId, AtendimentoInput.IdentificadorUsuario, AtendimentoInput.ResponsavelFoto);
            }
        }

        public void CreateLiberacaoLeilao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (new[] { "1", "2", "3" }.Contains(ParametrosCalculoFaturamento.StatusOperacaoLeilaoId))
            {
                _context.LiberacaoLeilao.Add(new()
                {
                    GrvId = ParametrosCalculoFaturamento.GrvId,

                    StatusOperacaoLeilaoId = ParametrosCalculoFaturamento.StatusOperacaoLeilaoId,

                    UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId
                });
            }
        }

        private async Task<CalculoFaturamentoParametroModel> ConfigParametrosCalculoFaturamentoAsync(GrvModel Grv, int TipoMeioCobrancaId, int UsuarioCadastroId, DateTime DataHoraPorDeposito)
        {
            // Quando no cadastro do Cliente foi configurado o Tipo de Cobrança, este cadastro é o que será usado para o cadastro da Fatura.
            var TipoMeioCobranca = await _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId ==
                    (Grv.Cliente.TipoMeioCobrancaId.HasValue && Grv.Cliente.TipoMeioCobrancaId.Value > 0 ? Grv.Cliente.TipoMeioCobrancaId.Value : TipoMeioCobrancaId));

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                DataHoraInicialParaCalculo = Grv.DataHoraGuarda.Value,

                DataHoraFinalParaCalculo = DateTime.MinValue,

                DataHoraPorDeposito = DataHoraPorDeposito,

                IsComboio = Grv.FlagComboio == "S",

                // L: Aguardando Pagamento
                // U: Aguardando Liberação Especial
                StatusOperacaoId = TipoMeioCobranca.Alias != "LIBESP" ? "L" : "U",

                FaturamentoProdutoId = Grv.FaturamentoProdutoId,

                GrvId = Grv.GrvId,

                NumeroFormularioGrv = Grv.NumeroFormularioGrv,

                TipoVeiculoId = Grv.TipoVeiculoId,

                UsuarioCadastroId = UsuarioCadastroId,

                // Esta funcionalidade altera o GRV com Status de Leilão para Status de Atendimento
                // para que o fluxo do Atendimento/Faturamento/Liberação funcionem.
                StatusOperacaoLeilaoId = new[] { "1", "2", "4" }
                    .Contains(Grv.StatusOperacaoId) ? Grv.StatusOperacaoId : string.Empty,

                TipoMeioCobrancaId = TipoMeioCobranca.TipoMeioCobrancaId,

                ClienteDeposito = await _context.ClienteDeposito
                    .Include(x => x.Cliente)
                    .ThenInclude(x => x.Endereco)
                    .Include(x => x.Deposito)
                    .ThenInclude(x => x.Endereco)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == Grv.ClienteId && x.DepositoId == Grv.DepositoId)
            };

            return ParametrosCalculoFaturamento;
        }

        public async Task<AtendimentoDTO> GetByIdAsync(int AtendimentoId, int UsuarioId)
        {
            AtendimentoDTO ResultView = new();

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

            ResultView = _mapper.Map<AtendimentoDTO>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<AtendimentoDTO> GetByProcessoAsync(string NumeroProcesso, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoDTO ResultView = new()
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

            ResultView = _mapper.Map<AtendimentoDTO>(Grv.Atendimento);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<ImageListDTO> GetFotoResponsavelAsync(int AtendimentoId, int UsuarioId)
        {
            ImageListDTO ResultView = new();

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
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel &&
                            x.TabelaOrigemId == AtendimentoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ResultView.Listagem.Add(new ImageDTO
                {
                    Imagem = new HttpClientFactoryService(_httpClientFactory)
                        .DownloadFile(BucketArquivo.Url)
                });

                ResultView.Mensagem = MensagemViewHelper.SetFound();

                return ResultView;
            }
            else
            {
                AtendimentoFotoResponsavelModel AtendimentoFotoResponsavel = await _context.AtendimentoFotoResponsavel
                    .Where(x => x.AtendimentoId == AtendimentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (AtendimentoFotoResponsavel != null)
                {
                    ResultView.Listagem.Add(new ImageDTO { Imagem = AtendimentoFotoResponsavel.Foto });

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

        public async Task<QualificacaoResponsavelListDTO> ListQualificacaoResponsavelAsync()
        {
            QualificacaoResponsavelListDTO ResultView = new();

            List<QualificacaoResponsavelModel> result = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<QualificacaoResponsavelDTO>>(result
                    .OrderBy(x => x.Descricao)
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

        private void UpdateStatusERP(ClienteDepositoModel ClienteDeposito, FaturamentoModel Faturamento, AtendimentoModel Atendimento)
        {
            if (ClienteDeposito.Cliente.FlagEmissaoNotaFiscal == "S" && !string.IsNullOrWhiteSpace(ClienteDeposito.CodigoERPOrdemVenda))
            {
                Atendimento.StatusCadastroERP = "P";

                if (Faturamento.ValorFaturado > 0)
                {
                    Atendimento.StatusCadastroOrdemVendaERP = "P";
                }

                _context.Atendimento.Update(Atendimento);
            }
        }

        private void UpdateGrv(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            GrvModel Grv = _context.Grv
                .Where(x => x.GrvId == ParametrosCalculoFaturamento.GrvId)
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