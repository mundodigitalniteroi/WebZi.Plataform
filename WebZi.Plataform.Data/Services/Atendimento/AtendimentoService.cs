using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Domain.Models;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento.ViewModel;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.Models.Pagamento.ViewModel;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;
        private readonly IServiceProvider _provider;

        public AtendimentoService(AppDbContext context, IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<AtendimentoModel> GetById(int AtendimentoId, int UsuarioId)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .Where(w => w.AtendimentoId == AtendimentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return atendimento;
        }

        public async Task<AtendimentoModel> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .Include(i => i.Grv)
                .Where(w => w.Grv.NumeroFormularioGrv == NumeroProcesso && w.Grv.ClienteId == ClienteId && w.Grv.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return atendimento;
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(AtendimentoCadastroViewModel Atendimento)
        {
            MensagemViewModel mensagem = new();

            #region Consultas
            if (Atendimento.GrvId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Identificador do GRV");

                return mensagem;
            }
            else if (Atendimento.UsuarioId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando o cadastro");

                return mensagem;
            }

            if (mensagem.Erros.Count > 0)
            {
                return mensagem;
            }

            GrvModel Grv = await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Where(w => w.GrvId == Atendimento.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                mensagem.Erros.Add("GRV sem permissão de acesso ou inexistente");

                return mensagem;
            }

            if (Grv.StatusOperacao.StatusOperacaoId != "V" && Grv.StatusOperacao.StatusOperacaoId != "1")
            {
                mensagem.Erros.Add($"Status do GRV não está apto para o cadastro do Atendimento: {Grv.StatusOperacao.Descricao.ToUpper()}");

                return mensagem;
            }

            AtendimentoModel atendimentoConsulta = await GetById(Atendimento.GrvId, Atendimento.UsuarioId);

            if (atendimentoConsulta != null)
            {
                mensagem.Erros.Add($"Este GRV já possui um Atendimento cadastrado: {atendimentoConsulta.AtendimentoId}");

                return mensagem;
            }
            #endregion Consultas

            #region Leilão
            MensagemViewModel mensagemLeilao = await new LeilaoService(_context)
                .GetAvisoLeilao(Grv.GrvId, Grv.StatusOperacaoId);

            if (mensagemLeilao != null)
            {
                if (mensagemLeilao.Avisos.Count > 0)
                {
                    mensagem.Avisos.Add(mensagemLeilao.Avisos[0]);
                }

                if (mensagemLeilao.Erros.Count > 0)
                {
                    mensagem.Erros.Add(mensagemLeilao.Erros[0]);

                    return mensagem;
                }
            }
            #endregion Leilão

            #region Dados do Responsável
            if (Atendimento.QualificacaoResponsavelId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNome))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(Atendimento.ResponsavelDocumento))
            {
                mensagem.Erros.Add($"CPF do Responsável é inválido: {Atendimento.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(Atendimento.ResponsavelCnh))
                {
                    mensagem.Erros.Add($"CNH do Responsável é inválido: {Atendimento.ResponsavelCnh}");
                }
            }
            #endregion Dados do Responsável

            #region Endereço do Responsável
            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelCep))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(Atendimento.ResponsavelCep))
            {
                mensagem.Erros.Add($"CEP do Responsável inválido: {Atendimento.ResponsavelCep}");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelEndereco))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNumero))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelBairro))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelMunicipio))
            {
                mensagem.Erros.Add("Primeiro é necessário informar Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelUf))
            {
                mensagem.Erros.Add("Primeiro é necessário informar a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(Atendimento.ResponsavelUf))
            {
                mensagem.Erros.Add("Unidade Federativa do Responsável inválida");
            }
            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(Atendimento.ResponsavelTelefone) && !ContactHelper.IsCellphone(Atendimento.ResponsavelTelefone)))
                {
                    mensagem.Erros.Add($"Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDdd))
                {
                    mensagem.Erros.Add("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(Atendimento.ResponsavelDdd))
                {
                    mensagem.Erros.Add($"DDD do Número do Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelDdd}");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário
            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioNome))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Nome do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento))
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Documento do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }
            else
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TiposDocumentosIdentificacao
                    .Where(w => w.TipoDocumentoIdentificacaoId == Atendimento.ProprietarioTipoDocumentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    mensagem.Erros.Add($"Tipo do Documento do Proprietário inexistente: {Atendimento.ProprietarioTipoDocumentoId}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF" && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    mensagem.Erros.Add($"O CPF do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ" && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    mensagem.Erros.Add($"O CNPJ do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNome))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(Atendimento.NotaFiscalDocumento) || !DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    mensagem.Erros.Add($"CPF ou CNPJ do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDocumento}");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalCep))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(Atendimento.NotaFiscalCep))
                {
                    mensagem.Erros.Add($"CEP do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalCep}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEndereco))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNumero))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalBairro))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalMunicipio))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalUf))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(Atendimento.NotaFiscalUf))
                {
                    mensagem.Erros.Add($"Unidade Federativa do Receptor da Nota Fiscal inválida: {Atendimento.NotaFiscalUf}");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalTelefone))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(Atendimento.NotaFiscalTelefone) && !ContactHelper.IsCellphone(Atendimento.NotaFiscalTelefone))
                {
                    mensagem.Erros.Add($"Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDdd))
                {
                    mensagem.Erros.Add("Primeiro é necessário informar o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(Atendimento.NotaFiscalDdd))
                {
                    mensagem.Erros.Add($"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDdd}");
                }

                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEmail) && !EmailHelper.IsEmail(Atendimento.NotaFiscalEmail))
                {
                    mensagem.Erros.Add($"E-mail do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalEmail}");
                }
                #endregion Contatos do Receptor da Nota Fiscal

                #region Inscrição Municipal do Tomador do Serviço
                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento) && DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel faturamentoRegra = await _context.FaturamentoRegras
                        .Include(i => i.FaturamentoRegraTipo)
                        .Where(w => w.ClienteId == Grv.ClienteId &&
                                    w.FaturamentoRegraTipo.Codigo == "ATENDINSCRICMUNIC")
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (faturamentoRegra != null)
                    {
                        mensagem.Erros.Add("Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }
                #endregion Inscrição Municipal do Tomador do Serviço
            }
            #endregion Nota Fiscal

            #region Faturamento
            if (Atendimento.TipoMeioCobrancaId <= 0)
            {
                mensagem.Erros.Add("Primeiro é necessário informar a Forma de Pagamento");
            }
            else
            {
                TipoMeioCobrancaModel TipoMeioCobranca = await _context.TiposMeiosCobrancas
                    .Where(w => w.TipoMeioCobrancaId == Atendimento.TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoMeioCobranca == null)
                {
                    mensagem.Erros.Add($"Forma de Pagamento inexistente: {Atendimento.TipoMeioCobrancaId}");
                }

                if ((TipoMeioCobranca.Alias == "PIX" || TipoMeioCobranca.Alias == "PIXEST") && Grv.Cliente.FlagPossuiPixEstatico == "N")
                {
                    mensagem.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Estático");
                }
                else if (TipoMeioCobranca.Alias == "PIXDIN" && Grv.Cliente.FlagPossuiPixDinamico == "N")
                {
                    mensagem.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Dinâmico");
                }
            }
            #endregion

            return mensagem;
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

            GrvModel grv = await _context.Grvs
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
            GrvModel Grv = await GetGrv(AtendimentoInput.GrvId);

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
                    await _context.Atendimentos.AddAsync(Atendimento);

                    await _context.SaveChangesAsync();

                    ParametrosCalculoFaturamento.Atendimento = Atendimento;

                    ParametrosCalculoFaturamento.Faturamento = await new FaturamentoService(_context).Faturar(ParametrosCalculoFaturamento);

                    CadastrarFoto(Atendimento.AtendimentoId, AtendimentoInput);

                    AtualizarStatusERP(ParametrosCalculoFaturamento);

                    CadastrarLiberacaoLeilao(ParametrosCalculoFaturamento);

                    AtualizarGrv(ParametrosCalculoFaturamento);

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    AtendimentoCadastroResultView.AtendimentoId = Atendimento.AtendimentoId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.Message);
                    AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.InnerException.Message);
                }
            }

            GerarFormaPagamento(ParametrosCalculoFaturamento);

            return AtendimentoCadastroResultView;
        }

        private async Task<GrvModel> GetGrv(int GrvId)
        {
            return await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        private async Task<DateTime> GetDataHoraPorDeposito(int DepositoId)
        {
            return await _provider
                .GetService<DepositoService>()
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

                ClienteDeposito = await _context.ClientesDepositos
                    .Where(w => w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(),

                Atendimento = Atendimento,

                DataHoraPorDeposito = DataHoraPorDeposito,

                // Esta funcionalidade altera o GRV com Status de Leilão para Status de Atendimento
                // para que o fluxo do Atendimento/Faturamento/Liberação funcionem.
                StatusOperacaoLeilaoId = new[] { "1", "2", "4" }.Contains(Grv.StatusOperacaoId) ? Grv.StatusOperacaoId : string.Empty,

                TiposMeiosCobrancas = await _context.TiposMeiosCobrancas
                    .AsNoTracking()
                    .ToListAsync(),

                FaturamentoRegras = await _context.FaturamentoRegras
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

        private async void CadastrarFoto(int AtendimentoId, AtendimentoCadastroViewModel AtendimentoInput)
        {
            if (AtendimentoInput.ResponsavelFoto != null)
            {
                await _context.AtendimentosFotosResponsaveis.AddAsync(new()
                {
                    AtendimentoId = AtendimentoId,

                    Foto = AtendimentoInput.ResponsavelFoto
                });
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

                _context.Atendimentos.Update(ParametrosCalculoFaturamento.Atendimento);
            }
        }

        private async void CadastrarLiberacaoLeilao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (new[] { "1", "2", "3" }.Contains(ParametrosCalculoFaturamento.StatusOperacaoLeilaoId))
            {
                await _context.LiberacaoLeilao.AddAsync(new()
                {
                    GrvId = ParametrosCalculoFaturamento.Grv.GrvId,

                    StatusOperacaoLeilaoId = ParametrosCalculoFaturamento.StatusOperacaoLeilaoId,

                    UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId
                });
            }
        }

        private async void AtualizarGrv(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            GrvModel Grv = await _context.Grvs
                .Where(w => w.GrvId == ParametrosCalculoFaturamento.Grv.GrvId)
                .FirstOrDefaultAsync();

            Grv.StatusOperacaoId = ParametrosCalculoFaturamento.StatusOperacaoId;

            _context.Grvs.Update(Grv);
        }

        private async void GerarFormaPagamento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao != "N" || ParametrosCalculoFaturamento.Faturamento.ValorFaturado <= 0)
            {
                return;
            }

            // BOLETO
            if (ParametrosCalculoFaturamento.TipoMeioCobranca.CodigoERP == "D")
            {

            }

            return;
        }
    }
}