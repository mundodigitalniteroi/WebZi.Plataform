using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.LiberacaoEspecial;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Usuario;
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
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.WebServices.Boleto;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Pagamento;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _provider;
        private readonly IOptions<WSNfseOptions> _options;

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

        public AtendimentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _provider = provider;
        }

        public AtendimentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IServiceProvider provider, IOptions<WSNfseOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _provider = provider;
            _options = options;
        }

        public async Task<MensagemDTO> CheckInformacoesParaCadastroAsync(AtendimentoParameters AtendimentoCadastro, CancellationToken ct)
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
                .FirstOrDefaultAsync(x => x.GrvId == AtendimentoCadastro.IdentificadorProcesso, cancellationToken: ct);

            UsuarioModel Usuario = await _context.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == AtendimentoCadastro.IdentificadorUsuario, cancellationToken: ct);

            if (!new[] { "B", "D", "V", "L", "E", "1", "2", "3", "4", "7" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o cadastro do Atendimento. " +
                    $"Descrição do Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }
            else if (Grv.Atendimento != null)
            {
                return MensagemViewHelper.SetBadRequest(
                    $"Este Processo já possui um Atendimento cadastrado. Identificador do Atendimento: {Grv.Atendimento.AtendimentoId}");
            }

            if (AtendimentoCadastro.Descontos != null && AtendimentoCadastro.Descontos.Any())
            {
                if (Usuario.FlagPermissaoDesconto != "S")
                    return MensagemViewHelper.SetBadRequest($"Este usuario não é permitido o cadastro de Descontos.");
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
                ResultView.AvisosImpeditivos.Add(
                    $"CPF do Responsável inválido: {AtendimentoCadastro.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelCNH))
            {
                if (!DocumentHelper.IsCNH(AtendimentoCadastro.ResponsavelCNH))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CNH do Responsável inválido: {AtendimentoCadastro.ResponsavelCNH}");
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
                if ((!ContactHelper.IsTelephone(AtendimentoCadastro.ResponsavelTelefone) &&
                     !ContactHelper.IsCellphone(AtendimentoCadastro.ResponsavelTelefone)))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Telefone/Celular do Responsável inválido: {AtendimentoCadastro.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        "Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(AtendimentoCadastro.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"DDD do Número do Telefone/Celular do Responsável inválido: {AtendimentoCadastro.ResponsavelDDD}");
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
                    .FirstOrDefaultAsync(w =>
                        w.TipoDocumentoIdentificacaoId == AtendimentoCadastro.IdentificadorProprietarioTipoDocumento, cancellationToken: ct);

                if (TipoDocumentoIdentificacao == null)
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Tipo do Documento do Proprietário inexistente: {AtendimentoCadastro.IdentificadorProprietarioTipoDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo != "CPF" && TipoDocumentoIdentificacao.Codigo != "CNPJ")
                {
                    ResultView.AvisosImpeditivos.Add("O Tipo do Documento do Proprietário precisa ser CPF ou CNPJ");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF"
                         && !string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioDocumento)
                         && !DocumentHelper.IsCPF(AtendimentoCadastro.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"O CPF do Proprietário inválido: {AtendimentoCadastro.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ"
                         && !string.IsNullOrWhiteSpace(AtendimentoCadastro.ProprietarioDocumento)
                         && !DocumentHelper.IsCNPJ(AtendimentoCadastro.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"O CNPJ do Proprietário inválido: {AtendimentoCadastro.ProprietarioDocumento}");
                }
            }

            #endregion Dados do Proprietário

            #region Nota Fiscal

            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == Grv.ClienteId && x.DepositoId == Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);

            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S" && permitirEmissao)
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
                else if (!DocumentHelper.IsCPF(AtendimentoCadastro.NotaFiscalDocumento) &&
                         !DocumentHelper.IsCNPJ(AtendimentoCadastro.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CPF ou CNPJ do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalDocumento}");
                }

                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(AtendimentoCadastro.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CEP do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalCEP}");
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
                    ResultView.AvisosImpeditivos.Add(
                        $"Unidade Federativa do Receptor da Nota Fiscal inválida: {AtendimentoCadastro.NotaFiscalUF}");
                }

                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(AtendimentoCadastro.NotaFiscalTelefone) &&
                         !ContactHelper.IsCellphone(AtendimentoCadastro.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(AtendimentoCadastro.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalDDD}");
                }

                if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalEmail) &&
                    !EmailHelper.IsEmail(AtendimentoCadastro.NotaFiscalEmail))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"E-mail do Receptor da Nota Fiscal inválido: {AtendimentoCadastro.NotaFiscalEmail}");
                }

                #endregion Contatos do Receptor da Nota Fiscal

                #region Inscrição Municipal do Tomador do Serviço

                if (!string.IsNullOrWhiteSpace(AtendimentoCadastro.NotaFiscalDocumento) &&
                    DocumentHelper.IsCNPJ(AtendimentoCadastro.NotaFiscalDocumento))
                {
                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel FaturamentoRegra = await _context.FaturamentoRegra
                        .Include(x => x.FaturamentoRegraTipo)
                        .Where(x => x.ClienteId == Grv.ClienteId &&
                                    x.FaturamentoRegraTipo.Codigo ==
                                    FaturamentoRegraTipoEnum.ObrigatorioInformarInscricaoMunicipal)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken: ct);

                    if (FaturamentoRegra != null)
                    {
                        ResultView.AvisosImpeditivos.Add(
                            "Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }

                #endregion Inscrição Municipal do Tomador do Serviço
            }

            #endregion Nota Fiscal

            #region Forma de Pagamento

            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == AtendimentoCadastro.IdentificadorTipoMeioCobranca, cancellationToken: ct);

            if (TipoMeioCobranca == null)
            {
                ResultView.AvisosImpeditivos.Add(
                    $"Forma de Pagamento inexistente: {AtendimentoCadastro.IdentificadorTipoMeioCobranca}");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico &&
                     Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                ResultView.AvisosImpeditivos.Add(
                    "Este Cliente não está configurado para emitir a Forma de Pagamento PIX Estático");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico &&
                     Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                ResultView.AvisosImpeditivos.Add(
                    "Este Cliente não está configurado para emitir a Forma de Pagamento PIX Dinâmico");
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

        public async Task<MensagemDTO> CheckInformacoesParaAtualizarAsync(
            AtualizarAtendimentoParameters AtualizarAtendimento, CancellationToken ct)
        {
            MensagemDTO ResultView = new MensagemDTO();
            List<string> Erros = new List<string>();

            var validandoGrv = new GrvService(_context)
                .ValidateInputGrv(AtualizarAtendimento.IdentificadorProcesso,
                    AtualizarAtendimento.IdentificadorUsuario);
            var permitirEdicao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == AtualizarAtendimento.IdentificadorUsuario
                               && (x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditProd || x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog)
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => (s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditProd || s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog) 
                                             && s.IdSubModulo == (int)SubModuloEnum.EditarAtendimento), cancellationToken: ct);
            if (!permitirEdicao)
            {
                Erros.Add("Não possui permissão para edição do atendimento");
            }

            // if (AtualizarAtendimento.IdentificadorTipoMeioCobranca <= 0)
            // {
            //     Erros.Add("Identificador da Forma de Pagamento inválido");
            // }

            if (validandoGrv.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                Erros.AddRange(validandoGrv.Erros);
            }

            if (Erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(Erros);
            }

            #region Consultas

            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Atendimento)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == AtualizarAtendimento.IdentificadorProcesso, cancellationToken: ct);

            UsuarioModel Usuario = await _context.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == AtualizarAtendimento.IdentificadorUsuario, cancellationToken: ct);

            if (Grv.Atendimento == null)
            {
                return MensagemViewHelper.SetBadRequest(
                    $"Este Processo não possui um Atendimento cadastrado.");
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

            if (AtualizarAtendimento.IdentificadorQualificacaoResponsavel <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(AtualizarAtendimento.ResponsavelDocumento))
            {
                ResultView.AvisosImpeditivos.Add(
                    $"CPF do Responsável inválido: {AtualizarAtendimento.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelCNH))
            {
                if (!DocumentHelper.IsCNH(AtualizarAtendimento.ResponsavelCNH))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CNH do Responsável inválido: {AtualizarAtendimento.ResponsavelCNH}");
                }
            }

            #endregion Dados do Responsável

            #region Endereço do Responsável

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelCEP))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(AtualizarAtendimento.ResponsavelCEP))
            {
                ResultView.AvisosImpeditivos.Add($"CEP do Responsável inválido: {AtualizarAtendimento.ResponsavelCEP}");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelEndereco))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelNumero))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelBairro))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelMunicipio))
            {
                ResultView.AvisosImpeditivos.Add("Informe Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelUF))
            {
                ResultView.AvisosImpeditivos.Add("Informe a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(AtualizarAtendimento.ResponsavelUF))
            {
                ResultView.AvisosImpeditivos.Add("Unidade Federativa do Responsável inválida");
            }

            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável

            if (!string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(AtualizarAtendimento.ResponsavelTelefone) &&
                     !ContactHelper.IsCellphone(AtualizarAtendimento.ResponsavelTelefone)))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Telefone/Celular do Responsável inválido: {AtualizarAtendimento.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        "Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(AtualizarAtendimento.ResponsavelDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"DDD do Número do Telefone/Celular do Responsável inválido: {AtualizarAtendimento.ResponsavelDDD}");
                }
            }

            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ProprietarioNome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Proprietário");
            }

            if (AtualizarAtendimento.IdentificadorProprietarioTipoDocumento <= 0)
            {
                ResultView.AvisosImpeditivos.Add("Informe o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(AtualizarAtendimento.ProprietarioDocumento))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Documento do Proprietário");
            }

            if (AtualizarAtendimento.IdentificadorProprietarioTipoDocumento > 0)
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TipoDocumentoIdentificacao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w =>
                        w.TipoDocumentoIdentificacaoId == AtualizarAtendimento.IdentificadorProprietarioTipoDocumento, cancellationToken: ct);

                if (TipoDocumentoIdentificacao == null)
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Tipo do Documento do Proprietário inexistente: {AtualizarAtendimento.IdentificadorProprietarioTipoDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo != "CPF" && TipoDocumentoIdentificacao.Codigo != "CNPJ")
                {
                    ResultView.AvisosImpeditivos.Add("O Tipo do Documento do Proprietário precisa ser CPF ou CNPJ");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF"
                         && !string.IsNullOrWhiteSpace(AtualizarAtendimento.ProprietarioDocumento)
                         && !DocumentHelper.IsCPF(AtualizarAtendimento.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"O CPF do Proprietário inválido: {AtualizarAtendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ"
                         && !string.IsNullOrWhiteSpace(AtualizarAtendimento.ProprietarioDocumento)
                         && !DocumentHelper.IsCNPJ(AtualizarAtendimento.ProprietarioDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"O CNPJ do Proprietário inválido: {AtualizarAtendimento.ProprietarioDocumento}");
                }
            }

            #endregion Dados do Proprietário

            #region Nota Fiscal

            var permiteEdicaoNf = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == AtualizarAtendimento.IdentificadorUsuario
                               && (x.PerfilAcessoId == (int)PerfisDeAcessoEnum.NfeEditHomolog || x.PerfilAcessoId == (int)PerfisDeAcessoEnum.NfeEditProd)
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => (s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.NfeEditHomolog || s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.NfeEditProd)
                                             && s.IdSubModulo == (int)SubModuloEnum.EditarNfe), cancellationToken: ct);


            if (permiteEdicaoNf)
            {
                #region Receptor da Nota Fiscal

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalNome))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(AtualizarAtendimento.NotaFiscalDocumento) &&
                         !DocumentHelper.IsCNPJ(AtualizarAtendimento.NotaFiscalDocumento))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CPF ou CNPJ do Receptor da Nota Fiscal inválido: {AtualizarAtendimento.NotaFiscalDocumento}");
                }

                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(AtualizarAtendimento.NotaFiscalCEP))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"CEP do Receptor da Nota Fiscal inválido: {AtualizarAtendimento.NotaFiscalCEP}");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalEndereco))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalNumero))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalBairro))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalMunicipio))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalUF))
                {
                    ResultView.AvisosImpeditivos.Add("Informe a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(AtualizarAtendimento.NotaFiscalUF))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Unidade Federativa do Receptor da Nota Fiscal inválida: {AtualizarAtendimento.NotaFiscalUF}");
                }

                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(AtualizarAtendimento.NotaFiscalTelefone) &&
                         !ContactHelper.IsCellphone(AtualizarAtendimento.NotaFiscalTelefone))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtualizarAtendimento.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add("Informe o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(AtualizarAtendimento.NotaFiscalDDD))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal inválido: {AtualizarAtendimento.NotaFiscalDDD}");
                }

                if (!string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalEmail) &&
                    !EmailHelper.IsEmail(AtualizarAtendimento.NotaFiscalEmail))
                {
                    ResultView.AvisosImpeditivos.Add(
                        $"E-mail do Receptor da Nota Fiscal inválido: {AtualizarAtendimento.NotaFiscalEmail}");
                }

                #endregion Contatos do Receptor da Nota Fiscal

                #region Inscrição Municipal do Tomador do Serviço

                if (!string.IsNullOrWhiteSpace(AtualizarAtendimento.NotaFiscalDocumento) &&
                    DocumentHelper.IsCNPJ(AtualizarAtendimento.NotaFiscalDocumento))
                {
                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel FaturamentoRegra = await _context.FaturamentoRegra
                        .Include(x => x.FaturamentoRegraTipo)
                        .Where(x => x.ClienteId == Grv.ClienteId &&
                                    x.FaturamentoRegraTipo.Codigo ==
                                    FaturamentoRegraTipoEnum.ObrigatorioInformarInscricaoMunicipal)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken: ct);

                    if (FaturamentoRegra != null)
                    {
                        ResultView.AvisosImpeditivos.Add(
                            "Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }

                #endregion Inscrição Municipal do Tomador do Serviço
            }

            #endregion Nota Fiscal

            #region Forma de Pagamento

            // TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
            //     .AsNoTracking()
            //     .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == AtualizarAtendimento.IdentificadorTipoMeioCobranca);

            // if (TipoMeioCobranca == null)
            // {
            //     ResultView.AvisosImpeditivos.Add(
            //         $"Forma de Pagamento inexistente: {AtualizarAtendimento.IdentificadorTipoMeioCobranca}");
            // }
            //  if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico &&
            //          Grv.Cliente.FlagPossuiPixEstatico == "N")
            // {
            //     ResultView.AvisosImpeditivos.Add(
            //         "Este Cliente não está configurado para emitir a Forma de Pagamento PIX Estático");
            // }
            // else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico &&
            //          Grv.Cliente.FlagPossuiPixDinamico == "N")
            // {
            //     ResultView.AvisosImpeditivos.Add(
            //         "Este Cliente não está configurado para emitir a Forma de Pagamento PIX Dinâmico");
            // }

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

            if (grv.StatusOperacao.StatusOperacaoId != "L")
            {
                mensagem.AvisosImpeditivos.Add(
                    $"Status do Processo não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpperTrim()}");

                return mensagem;
            }

            #endregion Consultas

            return mensagem;
        }

        public async Task<AtendimentoCadastroDTO> CreateAtendimentoAsync(AtendimentoParameters AtendimentoInput, CancellationToken ct)
        {
            #region Consultas

            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Where(x => x.GrvId == AtendimentoInput.IdentificadorProcesso)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken: ct);

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);
            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == Grv.ClienteId && x.DepositoId == Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);

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

                ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento.Replace(".", "").Replace("/", "")
                    .Replace("-", ""),

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
                FormaLiberacao = null,
                FormaLiberacaoCNH = null,
                FormaLiberacaoCPF = null,
                FormaLiberacaoNome = null,
                FormaLiberacaoPlaca = null
            };

            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S" && permitirEmissao)
            {
                Atendimento.NotaFiscalNome = AtendimentoInput.NotaFiscalNome.ToUpperTrim();

                Atendimento.NotaFiscalDocumento = AtendimentoInput.NotaFiscalDocumento.Replace(".", "").Replace("/", "")
                    .Replace("-", "");

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

            bool flagRetroativa = AtendimentoInput.FlagPermissaoDataRetroativaFaturamento?.ToUpper() == "S";

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento =
                await ConfigParametrosCalculoFaturamentoAsync(Grv, AtendimentoInput.IdentificadorTipoMeioCobranca,
                    AtendimentoInput.IdentificadorUsuario, DataHoraPorDeposito, AtendimentoInput.Descontos,
                    flagRetroativa, AtendimentoInput.DataRetroativa);

            AtendimentoCadastroDTO ResultView = new();

            FaturamentoModel Faturamento = new();

            CalculoDiariasModel CalculoDiarias = new();

            await using (var transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.Atendimento.Add(Atendimento);

                    await _context.SaveChangesAsync(ct);

                    Faturamento = new FaturamentoService(_context)
                        .Faturar(ParametrosCalculoFaturamento, out CalculoDiarias);


                    CreateFotoResponsavel(Atendimento.AtendimentoId, AtendimentoInput.IdentificadorUsuario,
                        AtendimentoInput.ResponsavelFoto);

                    UpdateStatusERP(ParametrosCalculoFaturamento.ClienteDeposito, Faturamento, Atendimento);

                    CreateLiberacaoLeilao(ParametrosCalculoFaturamento);

                    UpdateGrv(ParametrosCalculoFaturamento);

                    await _context.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);

                    ResultView.IdentificadorAtendimento = Atendimento.AtendimentoId;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            ResultView.Faturamento = _mapper.Map<FaturamentoCadastroDTO>(Faturamento);

            ResultView.Faturamento.ListagemServico =
                _mapper.Map<List<FaturamentoCadastroComposicaoDTO>>(Faturamento.ListagemFaturamentoComposicao);

            FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

            foreach (var item in ResultView.Faturamento.ListagemServico)
            {
                FaturamentoServicoTipoVeiculo = _context.FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .AsNoTracking()
                    .FirstOrDefault(x =>
                        x.FaturamentoServicoTipoVeiculoId == item.IdentificadorFaturamentoServicoTipoVeiculo);

                item.DescricaoTipoServico = ListagemTipoCobranca.Where(x => x.ValorCadastro == item.TipoServico)
                    .FirstOrDefault().Descricao;

                item.NomeServico = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao;

                item.DataVigenciaInicial =
                    FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaInicial;

                item.DataVigenciaFinal = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaFinal;
            }

            // TODO:
            // GerarFormaPagamento(ParametrosCalculoFaturamento);

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();

            return ResultView;
        }

        public async Task<MensagemDTO> UpdateAtendimentoAsync(
            AtualizarAtendimentoParameters AtendimentoInput, CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            #region Consultas

            AtendimentoModel Atendimento = await _context.Atendimento
                .Include(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .Include(x => x.Grv)
                .ThenInclude(x => x.Deposito)
                .Where(x => x.AtendimentoId == AtendimentoInput.IdentificadorAtendimento)
                .AsTracking()
                .FirstOrDefaultAsync(cancellationToken: ct);

            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == Atendimento.Grv.ClienteId && x.DepositoId == Atendimento.Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);

            #endregion Consultas


            #region Dados do Atendimento

            Atendimento.GrvId = AtendimentoInput.IdentificadorProcesso;
            Atendimento.QualificacaoResponsavelId = AtendimentoInput.IdentificadorQualificacaoResponsavel;
            Atendimento.UsuarioAlteracaoId = AtendimentoInput.IdentificadorUsuario;
            Atendimento.ResponsavelNome = AtendimentoInput.ResponsavelNome.ToUpperTrim();
            Atendimento.ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento.Replace(".", "")
                .Replace("/", "")
                .Replace("-", "");
            Atendimento.ResponsavelCnh = AtendimentoInput.ResponsavelCNH;
            Atendimento.ResponsavelEndereco = AtendimentoInput.ResponsavelEndereco.ToUpperTrim();
            Atendimento.ResponsavelNumero = AtendimentoInput.ResponsavelNumero.ToUpperTrim();
            Atendimento.ResponsavelComplemento = AtendimentoInput.ResponsavelComplemento.ToUpperTrim();
            Atendimento.ResponsavelBairro = AtendimentoInput.ResponsavelBairro.ToUpperTrim();
            Atendimento.ResponsavelMunicipio = AtendimentoInput.ResponsavelMunicipio.ToUpperTrim();
            Atendimento.ResponsavelUF = AtendimentoInput.ResponsavelUF.ToUpperTrim();
            Atendimento.ResponsavelCEP = AtendimentoInput.ResponsavelCEP.Replace("-", "");
            Atendimento.ResponsavelDDD = AtendimentoInput.ResponsavelDDD ?? "";
            Atendimento.ResponsavelTelefone = AtendimentoInput.ResponsavelTelefone?.Replace("-", "") ?? "";
            Atendimento.ProprietarioNome = AtendimentoInput.ProprietarioNome.ToUpperTrim();
            Atendimento.ProprietarioTipoDocumentoId = AtendimentoInput.IdentificadorProprietarioTipoDocumento;
            Atendimento.ProprietarioDocumento = AtendimentoInput.ProprietarioDocumento;
            Atendimento.ProprietarioEndereco = AtendimentoInput.ProprietarioEndereco.ToUpperTrim();
            Atendimento.ProprietarioNumero = AtendimentoInput.ProprietarioNumero.ToUpperTrim();
            Atendimento.ProprietarioComplemento = AtendimentoInput.ProprietarioComplemento.ToUpperTrim();
            Atendimento.ProprietarioBairro = AtendimentoInput.ProprietarioBairro.ToUpperTrim();
            Atendimento.ProprietarioMunicipio = AtendimentoInput.ProprietarioMunicipio.ToUpperTrim();
            Atendimento.ProprietarioUF = AtendimentoInput.ProprietarioUF.ToUpperTrim();
            Atendimento.ProprietarioCEP = AtendimentoInput.ProprietarioCEP.Replace("-", "");
            Atendimento.ProprietarioDDD = AtendimentoInput.ProprietarioDDD;
            Atendimento.ProprietarioTelefone = AtendimentoInput.ProprietarioTelefone.Replace("-", "");
            Atendimento.FormaLiberacao = AtendimentoInput.FormaLiberacao.ToUpperTrim();
            Atendimento.FormaLiberacaoCNH = AtendimentoInput.FormaLiberacaoCNH;
            Atendimento.FormaLiberacaoCPF = AtendimentoInput.FormaLiberacaoCPF.Replace(".", "").Replace(";", "")
                .Replace("-", "");
            Atendimento.FormaLiberacaoNome = AtendimentoInput.FormaLiberacaoNome.ToUpperTrim();
            Atendimento.FormaLiberacaoPlaca = AtendimentoInput.FormaLiberacaoPlaca.Replace("-", "").ToUpperTrim();
            if (permitirEmissao)
            {
                Atendimento.NotaFiscalNome = AtendimentoInput.NotaFiscalNome.ToUpperTrim();
                Atendimento.NotaFiscalDocumento = AtendimentoInput.NotaFiscalDocumento.Replace(".", "")
                    .Replace("/", "")
                    .Replace("-", "");
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
                Atendimento.NotaFiscalInscricaoMunicipal =
                    AtendimentoInput.NotaFiscalInscricaoMunicipal.ToUpperTrim();
            }

            Atendimento.DataAlteracao = DateTime.Now;

            #endregion Dados do Atendimento

            await using (var transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    CreateFotoResponsavel(Atendimento.AtendimentoId, AtendimentoInput.IdentificadorUsuario,
                        AtendimentoInput.ResponsavelFoto);

                    if (AtendimentoInput.IdentificadorTipoMeioCobranca == 12)
                    {
                        await _provider.GetService<LiberacaoEspecialService>()
                            .UpdateLiberacaoEspecialAsync(AtendimentoInput.LiberacaoEspecial);
                    }

                    await _context.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);
                    ResultView = MensagemViewHelper.SetInternalServerError(ex);
                    return ResultView;
                }
            }

            ResultView = MensagemViewHelper.SetUpdateSuccess();
            return ResultView;
        }


        private void CreateFotoResponsavel(int AtendimentoId, int UsuarioId, byte[] ResponsavelFoto)
        {
            if (ResponsavelFoto != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel, AtendimentoId,
                        UsuarioId, ResponsavelFoto);
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

        private async Task<CalculoFaturamentoParametroModel> ConfigParametrosCalculoFaturamentoAsync(GrvModel Grv,
            int TipoMeioCobrancaId,
            int UsuarioCadastroId, DateTime DataHoraPorDeposito, List<DescontoParameters>? descontoParameters,
            bool FlagPermissaoDataRetroativaFaturamento = false, DateTime? DataRetroativa = null)
        {
            // Quando no cadastro do Cliente foi configurado o Tipo de Cobrança, este cadastro é o que será usado para o cadastro da Fatura.
            var TipoMeioCobranca = await _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId ==
                                          (Grv.Cliente.TipoMeioCobrancaId.HasValue &&
                                           Grv.Cliente.TipoMeioCobrancaId.Value > 0
                                              ? Grv.Cliente.TipoMeioCobrancaId.Value
                                              : TipoMeioCobrancaId));

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                DataHoraInicialParaCalculo = Grv.DataHoraGuarda.Value,

                DataHoraFinalParaCalculo = (FlagPermissaoDataRetroativaFaturamento && DataRetroativa.HasValue && DataRetroativa.Value > DateTime.MinValue)
                    ? DataRetroativa.Value
                    : DateTime.MinValue,

                DataHoraPorDeposito = DataHoraPorDeposito,
                FlagPermissaoDataRetroativaFaturamento = FlagPermissaoDataRetroativaFaturamento, 

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
                    .Contains(Grv.StatusOperacaoId)
                    ? Grv.StatusOperacaoId
                    : string.Empty,

                TipoMeioCobrancaId = TipoMeioCobranca.TipoMeioCobrancaId,

                ClienteDeposito = await _context.ClienteDeposito
                    .Include(x => x.Cliente)
                    .ThenInclude(x => x.Endereco)
                    .Include(x => x.Deposito)
                    .ThenInclude(x => x.Endereco)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == Grv.ClienteId && x.DepositoId == Grv.DepositoId),

                FaturamentoDescontos = descontoParameters?.Select(x => new CalculoFaturamentoDescontoModel
                {
                    FaturamentoServicoTipoVeiculoId = x.FaturamentoServicoTipoVeiculoId,
                    TipoComposicao = x.TipoComposicao,
                    FaturamentoTipoComposicaoId = x.FaturamentoTipoComposicaoId,
                    UsuarioDescontoId = x.UsuarioDescontoId,
                    TipoDesconto = x.TipoDesconto,
                    QuantidadeDesconto = x.QuantidadeDesconto,
                    ValorDesconto = x.ValorDesconto,
                    ObservacaoDesconto = x.ObservacaoDesconto
                }).ToList(),

                FaturamentoQuantidadesAlteradas = descontoParameters?
                    .Where(x => x.QuantidadeAjuste != 0)
                    .Select(x => new CalculoFaturamentoQuantidadeAlteradaModel
                    {
                        FaturamentoServicoTipoVeiculoId = x.FaturamentoServicoTipoVeiculoId,
                        TipoComposicao = x.TipoComposicao,
                        FaturamentoTipoComposicaoId = x.FaturamentoTipoComposicaoId,
                        UsuarioAlteracaoQuantidadeId = x.UsuarioDescontoId,
                        QuantidadeAjuste = x.QuantidadeAjuste,
                        QuantidadeAlterada = 0,
                    }).ToList() 
            };

            return ParametrosCalculoFaturamento;
        }

        public async Task<MensagemDTO> DeleteAtendimentoAsync(
            string NumeroProcesso,
            int UsuarioId,
            int ClienteId
        )
        {
            UsuarioPermissaoModel UsuarioPermissao = await _context.UsuarioPermissao
                .Include(x => x.TipoPermissao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId
                                          && x.TipoPermissao.TipoPermissaoId == 3);

            var permiteExclusao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == UsuarioId
                               && (x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog || x.PerfilAcessoId == (int)PerfisDeAcessoEnum.AtendimentoEditProd)
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => (s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditHomolog || s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.AtendimentoEditProd)
                                             && s.IdSubModulo == (int)SubModuloEnum.ExcluirAtendimento));

            if (UsuarioPermissao == null)
            {
                return MensagemViewHelper.SetUnauthorized("Usuário não possui permissão para excluir Processos");
            }
            
            if (!permiteExclusao)
            {
                return MensagemViewHelper.SetUnauthorized("Usuário não possui permissão para excluir Processos");
            }

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                return MensagemViewHelper.SetBadRequest("Precisa por o numero do processo");
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.ListagemCondutorDocumento)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.SaidaParaReparo)
                .Include(x => x.Liberacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NumeroFormularioGrv == NumeroProcesso && x.ClienteId == ClienteId);

            if (Grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            if (new[] { "M", "P", "G", "V", "1", "3", "7" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<FaturamentoModel> Faturamentos = null;
            bool SaidaParaReparo = false;
            if (Grv.Atendimento != null)
            {
                Faturamentos = _context.Faturamento
                    .Include(x => x.ListagemBoleto)
                    .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId)
                    .AsNoTracking()
                    .ToList();
            }

            if (Grv.Atendimento?.SaidaParaReparo is not null)
            {
                SaidaParaReparo = await _context.SaidaReparo
                    .AsNoTracking()
                    .AnyAsync(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId);
            }

            await using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (Grv.LiberacaoId != null || Grv.Liberacao != null)
                    {
                        await _context.Grv
                            .Where(x => x.GrvId == Grv.GrvId && x.LiberacaoId != null)
                            .ExecuteUpdateAsync(s => s.SetProperty(x => x.LiberacaoId, (int?)null));
                    }

                    if (Grv.Atendimento != null)
                    {
                        List<int> faturamentoIds = await _context.Faturamento
                            .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId)
                            .Select(x => x.FaturamentoId)
                            .ToListAsync();

                        if (faturamentoIds.Count > 0)
                        {
                            await _context.PixDinamicoSenhaConfirmacaoTranferencia
                                .Where(x => faturamentoIds.Contains(x.FaturamentoId))
                                .ExecuteDeleteAsync();

                            await _context.PixEstatico
                                .Where(x => faturamentoIds.Contains(x.FaturamentoId))
                                .ExecuteDeleteAsync();

                            await _context.PixDinamico
                                .Where(x => faturamentoIds.Contains(x.FaturamentoId))
                                .ExecuteDeleteAsync();

                            await _context.LiberacaoEspecial
                                .Where(x => faturamentoIds.Contains(x.IdFaturamento))
                                .ExecuteDeleteAsync();
                        }
                    }

                    await _context.LiberacaoEspecial
                        .Where(x => x.IdGrv == Grv.GrvId)
                        .ExecuteDeleteAsync();

                    if (SaidaParaReparo && Grv.Atendimento != null)
                    {
                        await _context.SaidaReparo
                            .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId)
                            .ExecuteDeleteAsync();
                    }

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC VoltarProcesso @numero_grv = @numero_grv, @id_cliente = @id_cliente",
                        new SqlParameter("@numero_grv", NumeroProcesso),
                        new SqlParameter("@id_cliente", ClienteId)
                    );
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, Grv.GrvId);

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV, Grv.GrvId);

            if (Grv.ListagemCondutorDocumento?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles(BucketNomeTabelaOrigemEnum.DocumentoCondutor, Grv.ListagemCondutorDocumento
                        .Select(x => x.CondutorDocumentoId)
                        .ToList());
            }

            if (Grv.Atendimento != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles(BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel, Grv.Atendimento.AtendimentoId);

                if (Faturamentos?.Count > 0)
                {
                    foreach (FaturamentoModel Faturamento in Faturamentos)
                    {
                        if (Faturamento.ListagemBoleto?.Count > 0)
                        {
                            foreach (BoletoModel FaturamentoBoleto in Faturamento.ListagemBoleto)
                            {
                                new BucketService(_context, _httpClientFactory)
                                    .DeleteFiles(BucketNomeTabelaOrigemEnum.Boleto,
                                        FaturamentoBoleto.FaturamentoBoletoId);
                            }
                        }
                    }
                }
            }

            return MensagemViewHelper.SetOk("Processo excluído com sucesso");
        }

        public async Task<AtendimentoDTO> GetByIdAsync(int AtendimentoId, int UsuarioId)
        {
            AtendimentoDTO ResultView = new();

            if (AtendimentoId <= 0)
            {
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorAtendimentoInvalido);

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

        public async Task<SaidaParaReparoDTO> CreateSaidaReparo(SaidaParaReparoParameters parameters,
            CancellationToken ct)
        {
            SaidaParaReparoDTO ResultView = new();
            ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(parameters.IdentificadorProcesso,
                parameters.IdentificadorUsuario);
            var Erros = new List<string>();

            #region Consultar

            var atendimento = await _context.Atendimento
                .Include(x => x.Grv)
                .Include(x => x.ListagemFaturamento.Where(x => x.Status != "C"))
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AtendimentoId == parameters.IdentificadorAtendimento,
                    cancellationToken: ct);

            List<FaturamentoModel> Faturamentos =
                atendimento.ListagemFaturamento.OrderByDescending(x => x.DataCadastro).ToList();

            if (Faturamentos == null || !Faturamentos.Any())
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
                return ResultView;
            }

            if (Faturamentos.Exists(x => x.Status == "N"))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Este Processo possui uma Fatura não paga");
                return ResultView;
            }

            var grv = atendimento.Grv;
            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == atendimento.Grv.ClienteId && x.DepositoId == atendimento.Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);


            FaturamentoModel UltimoFaturamento = atendimento.ListagemFaturamento?
                .FirstOrDefault();


            TipoLiberacaoModel TipoLiberacao = await _context
                .TipoLiberacao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoLiberacaoId == parameters.IdentificadorTipoLiberacao,
                    cancellationToken: ct);

            if (parameters.IdentificadorTipoLiberacao <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Precisa ter um tipo de liberação");
                return ResultView;
            }

            bool exists = await _context.SaidaReparo
                .AsNoTracking()
                .AnyAsync(x => x.AtendimentoId == parameters.IdentificadorAtendimento, cancellationToken: ct);

            #endregion


            if (TipoLiberacao is null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não existe esse tipo de liberação");
                return ResultView;
            }

            if (atendimento is null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Atendimento não identificado");
                return ResultView;
            }

            if (exists)
            {
                ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess("Já está cadastrado");
                return ResultView;
            }

            // if (parameters.DataSaida > atendimento.Grv.DataHoraGuarda)
            //     Erros.Add("A Data da Saída não pode ser maior do que a Data da guarda");

            if (parameters.DataSaida > parameters.DataPrevisaoRetorno)
                Erros.Add("A Data da Saída não pode ser maior do que a Data da Previão de Retorno");

            if (parameters.IdentificadorTipoLiberacao == 1)
            {
                if (parameters.FormaLiberacao is null)
                    Erros.Add("Forma de Liberação precisa ser preenchida");
                if (!string.IsNullOrWhiteSpace(parameters.FormaLiberacao?.FormaLiberacaoPlaca) &&
                    !parameters.FormaLiberacao.FormaLiberacaoPlaca.IsPlaca())
                    Erros.Add("Placa inválida");
                if (!string.IsNullOrWhiteSpace(parameters.FormaLiberacao?.FormaLiberacaoCnh) &&
                    !parameters.FormaLiberacao.FormaLiberacaoCnh.IsCNH())
                    Erros.Add("CNH inválido");
                if (!string.IsNullOrWhiteSpace(parameters.FormaLiberacao?.FormaLiberacaoCpf) &&
                    !parameters.FormaLiberacao.FormaLiberacaoCpf.IsCPF())
                    Erros.Add("CPF inválido");
            }

            if (parameters.IdentificadorTipoLiberacao == 2)
            {
                if (parameters.LiberacaoEspecial is null)
                    Erros.Add("Liberação Especial precisa ser preenchida");
            }

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                Erros.Add("Grv incorreto");

            if (Erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(Erros);
                return ResultView;
            }

            AtendimentoSaidaParaReparoModel saidaReparo = new()
            {
                AtendimentoId = parameters.IdentificadorAtendimento,
                DataSaida = parameters.DataSaida,
                DataPrevisaoRetorno = parameters.DataPrevisaoRetorno,
                MotivoSaida = parameters.MotivoSaida,
                IdUsuario = parameters.IdentificadorUsuario
            };


            await using IDbContextTransaction _transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                await _context.SaidaReparo.AddAsync(saidaReparo, ct);

                await AtualizarStatusGrvSaidaReparoAsync(parameters, ct);

                if (parameters.IdentificadorTipoLiberacao == 1)
                {
                    await AtualizarFormaLiberacaoAtendimentoAsync(parameters, ct);
                }

                if (parameters.IdentificadorTipoLiberacao == 2)
                {
                    await ProcessarLiberacaoEspecialSaidaReparoAsync(parameters, ct);
                }

                if (parameters.FlagFaturamentoAdiantado == "S")
                {
                    await GerarFaturamentoAdicionalSaidaReparoAsync(new GerarFaturamentoSaidaReparoParameters
                    {
                        Grv = grv,
                        UltimoFaturamento = UltimoFaturamento,
                        DataInicialParaCalculo = parameters.DataSaida,
                        DataFinalParaCalculo = parameters.DataPrevisaoRetorno,
                        IdentificadorUsuario = parameters.IdentificadorUsuario,
                        IsAtualizacaoPrevisao = false
                    }, ct);
                }

                await ProcessarEmissaoNfseSaidaReparoAsync(parameters, permitirEmissao, ct);

                await _context.SaveChangesAsync(ct);
                await _transaction.CommitAsync(ct);
                ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();
                return ResultView;
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync(ct);
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);
                return ResultView;
            }
        }

        public async Task<MensagemDTO> UpdateSaidaReparo(SaidaParaReparoUpdateParameters parameters,
            CancellationToken ct)
        {
            MensagemDTO ResultView = new();
            var errors = new List<string>();

            #region Consultar

            var atendimento = await _context.Atendimento
                .Include(x => x.Grv)
                .Include(x => x.ListagemFaturamento.OrderByDescending(x => x.DataCadastro).Where(x => x.Status != "C"))
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AtendimentoId == parameters.IdentificadorAtendimento,
                    cancellationToken: ct);
            var saidaReparo = await _context.SaidaReparo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == parameters.IdentificadorSaidaParaReparo, cancellationToken: ct);

            var ultimoFaturamento = atendimento.ListagemFaturamento.FirstOrDefault();
            var dataPrevisaoAntigo = saidaReparo.DataPrevisaoRetorno;

            #endregion

            if (atendimento is null)
                errors.Add("Atendimento não identificado");

            if (saidaReparo is null)
                errors.Add("Saida para reparo não encontrado");

            // if (saidaReparo.DataSaida > atendimento.Grv.DataHoraGuarda)
            //     errors.Add("A Data da Saída não pode ser maior do que a Data da guarda");

            if (saidaReparo.DataSaida > parameters.DataPrevisaoRetorno)
                errors.Add("A Data da Saída não pode ser maior do que a Data da Previão de Retorno");

            if (errors.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
                ResultView.AvisosImpeditivos.AddRange(errors);
                return ResultView;
            }


            await using (IDbContextTransaction _transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    await _context.SaidaReparo
                        .Where(x => x.Id == parameters.IdentificadorSaidaParaReparo)
                        .UpdateAsync(x => new AtendimentoSaidaParaReparoModel()
                        {
                            DataPrevisaoRetorno = parameters.DataPrevisaoRetorno
                        }, cancellationToken: ct);


                    if (parameters.FlagAtualizarFaturamentoAdiantado == "S")
                    {
                        await GerarFaturamentoAdicionalSaidaReparoAsync(new GerarFaturamentoSaidaReparoParameters
                        {
                            Grv = atendimento.Grv,
                            UltimoFaturamento = ultimoFaturamento,
                            DataInicialParaCalculo = saidaReparo.DataSaida,
                            DataFinalParaCalculo = parameters.DataPrevisaoRetorno,
                            IdentificadorUsuario = saidaReparo.IdUsuario!.Value,
                            IsAtualizacaoPrevisao = true,
                            DataPrevisaoAntiga = dataPrevisaoAntigo
                        }, ct);
                    }

                    await _context.SaveChangesAsync(ct);
                    await _transaction.CommitAsync(ct);
                    ResultView = MensagemViewHelper.SetUpdateSuccess("Atualização da data de previsão do retorno do veiculo");
                    return ResultView;
                }
                catch (Exception ex)
                {
                    await _transaction.RollbackAsync(ct);
                    ResultView = MensagemViewHelper.SetInternalServerError(ex);
                    return ResultView;
                }
            }
        }

        public async Task<AtendimentoDTO> GetByProcessoAsync(string NumeroProcesso, string CodigoProduto, int ClienteId,
            int DepositoId, int UsuarioId)
        {
            AtendimentoDTO ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidateInputGrv(NumeroProcesso, CodigoProduto, ClienteId,
                    DepositoId, UsuarioId)
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

        private void UpdateStatusERP(ClienteDepositoModel ClienteDeposito, FaturamentoModel Faturamento,
            AtendimentoModel Atendimento)
        {
            if (ClienteDeposito.Cliente.FlagEmissaoNotaFiscal == "S" &&
                !string.IsNullOrWhiteSpace(ClienteDeposito.CodigoERPOrdemVenda))
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

        private async Task AtualizarStatusGrvSaidaReparoAsync(SaidaParaReparoParameters parameters,
            CancellationToken ct)
        {
            await _context.Grv
                .Where(x => x.GrvId == parameters.IdentificadorProcesso)
                .UpdateAsync(x => new GrvModel
                {
                    StatusOperacaoId = "R",
                    DataAlteracao = DateTime.Now,
                    UsuarioAlteracaoId = parameters.IdentificadorUsuario
                }, cancellationToken: ct);
        }

        private async Task AtualizarFormaLiberacaoAtendimentoAsync(SaidaParaReparoParameters parameters,
            CancellationToken ct)
        {
            await _context.Atendimento
                .Where(x => x.AtendimentoId == parameters.IdentificadorAtendimento)
                .UpdateAsync(x => new AtendimentoModel()
                {
                    FormaLiberacao = parameters.FormaLiberacao.FormaLiberacao,
                    FormaLiberacaoNome = parameters.FormaLiberacao.FormaLiberacaoNome,
                    FormaLiberacaoCNH = parameters.FormaLiberacao.FormaLiberacaoCnh,
                    FormaLiberacaoCPF = parameters.FormaLiberacao.FormaLiberacaoCpf,
                    FormaLiberacaoPlaca = parameters.FormaLiberacao.FormaLiberacaoPlaca,
                    DataAlteracao = DateTime.Now
                }, cancellationToken: ct);
        }

        private async Task ProcessarLiberacaoEspecialSaidaReparoAsync(SaidaParaReparoParameters parameters,
            CancellationToken ct)
        {
            await _provider.GetService<LiberacaoEspecialService>()
                .CreateLiberacaoEspecialAsync(parameters.LiberacaoEspecial, new DateTime(1900, 1, 1), true, ct);
        }

        private async Task GerarFaturamentoAdicionalSaidaReparoAsync(
            GerarFaturamentoSaidaReparoParameters payload,
            CancellationToken ct)
        {
            if (payload.UltimoFaturamento is null)
                throw new ArgumentNullException(nameof(payload.UltimoFaturamento),
                    "Ultimo Faturamento não pode ser nulo");

            if (payload.IsAtualizacaoPrevisao)
            {
                await ProcessarFaturamentoAtualizacaoPrevisaoAsync(payload, ct);
                return;
            }

            await ExecutarFaturamentoAdicionalAsync(
                payload,
                payload.DataInicialParaCalculo,
                payload.DataFinalParaCalculo,
                ct);
        }

        private async Task ProcessarFaturamentoAtualizacaoPrevisaoAsync(
            GerarFaturamentoSaidaReparoParameters payload,
            CancellationToken ct)
        {
            if (payload.UltimoFaturamento.Status == "N")
            {
                await CancelarFaturamentoAsync(payload.UltimoFaturamento.FaturamentoId,
                    payload.IdentificadorUsuario, ct);
                await ExecutarFaturamentoAdicionalAsync(
                    payload,
                    payload.DataInicialParaCalculo,
                    payload.DataFinalParaCalculo,
                    ct);
            }
            else
            {
                DateTime dataInicial = payload.DataPrevisaoAntiga ?? payload.DataInicialParaCalculo;

                await ExecutarFaturamentoAdicionalAsync(
                    payload,
                    dataInicial,
                    payload.DataFinalParaCalculo,
                    ct);
            }
        }

        private async Task CancelarFaturamentoAsync(
            int faturamentoId,
            int usuarioAlteracaoId,
            CancellationToken ct)
        {
            await _context.Faturamento
                .Where(x => x.FaturamentoId == faturamentoId)
                .UpdateAsync(x => new FaturamentoModel
                {
                    Status = "C",
                    UsuarioAlteracaoId = usuarioAlteracaoId,
                    DataAlteracao = DateTime.Now
                }, ct);
        }

        private async Task ExecutarFaturamentoAdicionalAsync(
            GerarFaturamentoSaidaReparoParameters payload,
            DateTime dataInicial,
            DateTime dataFinal,
            CancellationToken ct)
        {
            CalculoFaturamentoParametroModel parametrosCalculo =
                await MontarParametrosCalculoFaturamentoAsync(payload, dataInicial, dataFinal, ct);

            FaturamentoModel novoFaturamento = _provider.GetService<FaturamentoService>()
                .Faturar(parametrosCalculo, out _);

            novoFaturamento.UsuarioCadastroId = payload.IdentificadorUsuario;

            await _context.Faturamento.AddAsync(novoFaturamento, ct);
        }

        private async Task<CalculoFaturamentoParametroModel> MontarParametrosCalculoFaturamentoAsync(
            GerarFaturamentoSaidaReparoParameters payload,
            DateTime dataInicial,
            DateTime dataFinal,
            CancellationToken ct)
        {
            var clienteDeposito = await _context.ClienteDeposito
                .Include(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.ClienteId == payload.Grv.ClienteId && x.DepositoId == payload.Grv.DepositoId,
                    cancellationToken: ct);

            return new CalculoFaturamentoParametroModel
            {
                DataHoraInicialParaCalculo = dataInicial,
                DataHoraFinalParaCalculo = dataFinal,
                DataHoraPorDeposito = dataFinal,
                FaturarSemGrv = false,
                IsSimulacao = false,
                IsComboio = false,
                StatusOperacaoId = payload.Grv.StatusOperacaoId,
                IsLeilaoStatus = new[] { "1", "3", "7" }.Contains(payload.Grv.StatusOperacaoId),
                FaturamentoProdutoId = payload.Grv.FaturamentoProdutoId,
                GrvId = payload.Grv.GrvId,
                NumeroFormularioGrv = payload.Grv.NumeroFormularioGrv,
                TipoVeiculoId = payload.Grv.TipoVeiculoId,
                FaturamentoAdicional = true,
                TipoMeioCobrancaId = payload.UltimoFaturamento.TipoMeioCobrancaId,
                ClienteDeposito = clienteDeposito
            };
        }

        private async Task ProcessarEmissaoNfseSaidaReparoAsync(SaidaParaReparoParameters parameters,
            bool permitirEmissao, CancellationToken ct)
        {
            if (_options.Value.Enable && permitirEmissao)
            {
                await _provider.GetService<WSNfseService>()
                    .CreateNfseAsync(parameters.IdentificadorProcesso, parameters.IdentificadorUsuario, ct);
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