using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Domain.Models;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
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

        public async Task<AvisoViewModel> ValidarInformacoesParaCadastro(AtendimentoViewModel Atendimento)
        {
            AvisoViewModel avisos = new();

            #region Consultas
            if (Atendimento.GrvId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Identificador do GRV");

                return avisos;
            }
            else if (Atendimento.UsuarioCadastroId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando o cadastro");

                return avisos;
            }

            if (avisos.Erros.Count > 0)
            {
                return avisos;
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
                avisos.Erros.Add("GRV sem permissão de acesso ou inexistente");

                return avisos;
            }

            if (Grv.StatusOperacao.StatusOperacaoId != "V" && Grv.StatusOperacao.StatusOperacaoId != "1")
            {
                avisos.Erros.Add($"Status do GRV não está apto para o cadastro do Atendimento: {Grv.StatusOperacao.Descricao.ToUpper()}");

                return avisos;
            }

            AtendimentoModel atendimentoConsulta = await GetById(Atendimento.GrvId, Atendimento.UsuarioCadastroId);

            if (atendimentoConsulta != null)
            {
                avisos.Erros.Add($"Este GRV já possui um Atendimento cadastrado: {atendimentoConsulta.AtendimentoId}");

                return avisos;
            }
            #endregion Consultas

            #region Leilão
            AvisoViewModel avisoLeilao = await new LeilaoService(_context)
                .GetAvisoLeilao(Grv.GrvId, Grv.StatusOperacaoId);

            if (avisoLeilao != null)
            {
                if (avisoLeilao.Avisos.Count > 0)
                {
                    avisos.Avisos.Add(avisoLeilao.Avisos[0]);
                }

                if (avisoLeilao.Erros.Count > 0)
                {
                    avisos.Erros.Add(avisoLeilao.Erros[0]);

                    return avisos;
                }
            }
            #endregion Leilão

            #region Dados do Responsável
            if (Atendimento.QualificacaoResponsavelId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNome))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                avisos.Erros.Add("Primeiro é necessário informar o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(Atendimento.ResponsavelDocumento))
            {
                avisos.Erros.Add($"CPF do Responsável é inválido: {Atendimento.ResponsavelDocumento}");
            }

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(Atendimento.ResponsavelCnh))
                {
                    avisos.Erros.Add($"CNH do Responsável é inválido: {Atendimento.ResponsavelCnh}");
                }
            }
            #endregion Dados do Responsável

            #region Endereço do Responsável
            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelCep))
            {
                avisos.Erros.Add("Primeiro é necessário informar o CEP do Responsável");
            }
            else if (!LocalizacaoHelper.IsCEP(Atendimento.ResponsavelCep))
            {
                avisos.Erros.Add($"CEP do Responsável inválido: {Atendimento.ResponsavelCep}");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelEndereco))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNumero))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelBairro))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelMunicipio))
            {
                avisos.Erros.Add("Primeiro é necessário informar Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelUf))
            {
                avisos.Erros.Add("Primeiro é necessário informar a Unidade Federativa do Responsável");
            }
            else if (!LocalizacaoHelper.IsUF(Atendimento.ResponsavelUf))
            {
                avisos.Erros.Add("Unidade Federativa do Responsável inválida");
            }
            #endregion Endereço do Responsável

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(Atendimento.ResponsavelTelefone) && !ContactHelper.IsCellphone(Atendimento.ResponsavelTelefone)))
                {
                    avisos.Erros.Add($"Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDdd))
                {
                    avisos.Erros.Add("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsDDD(Atendimento.ResponsavelDdd))
                {
                    avisos.Erros.Add($"DDD do Número do Telefone/Celular do Responsável é inválido: {Atendimento.ResponsavelDdd}");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Dados do Proprietário
            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioNome))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Nome do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento))
            {
                avisos.Erros.Add("Primeiro é necessário informar o Documento do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }
            else
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TiposDocumentosIdentificacao
                    .Where(w => w.TipoDocumentoIdentificacaoId == Atendimento.ProprietarioTipoDocumentoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    avisos.Erros.Add($"Tipo do Documento do Proprietário inexistente: {Atendimento.ProprietarioTipoDocumentoId}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CPF" && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    avisos.Erros.Add($"O CPF do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo == "CNPJ" && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    avisos.Erros.Add($"O CNPJ do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscal == "S")
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNome))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(Atendimento.NotaFiscalDocumento) || !DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    avisos.Erros.Add($"CPF ou CNPJ do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDocumento}");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalCep))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsCEP(Atendimento.NotaFiscalCep))
                {
                    avisos.Erros.Add($"CEP do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalCep}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEndereco))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNumero))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalBairro))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalMunicipio))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalUf))
                {
                    avisos.Erros.Add("Primeiro é necessário informar a UF do Receptor da Nota Fiscal");
                }
                else if (!LocalizacaoHelper.IsUF(Atendimento.NotaFiscalUf))
                {
                    avisos.Erros.Add($"Unidade Federativa do Receptor da Nota Fiscal inválida: {Atendimento.NotaFiscalUf}");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalTelefone))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(Atendimento.NotaFiscalTelefone) && !ContactHelper.IsCellphone(Atendimento.NotaFiscalTelefone))
                {
                    avisos.Erros.Add($"Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalTelefone}");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDdd))
                {
                    avisos.Erros.Add("Primeiro é necessário informar o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(Atendimento.NotaFiscalDdd))
                {
                    avisos.Erros.Add($"DDD do Número do Telefone/Celular do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalDdd}");
                }

                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEmail) && !EmailHelper.IsEmail(Atendimento.NotaFiscalEmail))
                {
                    avisos.Erros.Add($"E-mail do Receptor da Nota Fiscal é inválido: {Atendimento.NotaFiscalEmail}");
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
                        avisos.Erros.Add("Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }
                #endregion Inscrição Municipal do Tomador do Serviço
            }
            #endregion Nota Fiscal

            #region Faturamento
            if (Atendimento.TipoMeioCobrancaId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar a Forma de Pagamento");
            }
            else
            {
                TipoMeioCobrancaModel TipoMeioCobranca = await _context.TiposMeiosCobrancas
                    .Where(w => w.TipoMeioCobrancaId == Atendimento.TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoMeioCobranca == null)
                {
                    avisos.Erros.Add($"Forma de Pagamento inexistente: {Atendimento.TipoMeioCobrancaId}");
                }

                if ((TipoMeioCobranca.Alias == "PIX" || TipoMeioCobranca.Alias == "PIXEST") && Grv.Cliente.FlagPossuiPixEstatico == "N")
                {
                    avisos.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Estático");
                }
                else if (TipoMeioCobranca.Alias == "PIXDIN" && Grv.Cliente.FlagPossuiPixDinamico == "N")
                {
                    avisos.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Dinâmico");
                }
            }
            #endregion

            return avisos;
        }

        public async Task<AvisoViewModel> ValidarInformacoesParaAtualizacao(AtendimentoViewModel Atendimento)
        {
            AvisoViewModel avisos = new();

            #region Consultas
            if (Atendimento.GrvId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Identificador do GRV");
            }
            else if (Atendimento.AtendimentoId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Identificador do Atendimento");
            }
            else if (Atendimento.UsuarioAlteracaoId <= 0)
            {
                avisos.Erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando a atualização");
            }

            if (avisos.Erros.Count > 0)
            {
                return avisos;
            }

            GrvModel grv = await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Where(w => w.GrvId == Atendimento.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (grv == null)
            {
                avisos.Erros.Add("GRV sem permissão de acesso ou inexistente");

                return avisos;
            }

            AtendimentoModel atendimentoConsulta = await GetById(Atendimento.AtendimentoId, Atendimento.UsuarioAlteracaoId.Value);

            if (atendimentoConsulta == null)
            {
                avisos.Erros.Add("Atendimento sem permissão de acesso ou inexistente");

                return avisos;
            }

            // TODO: Verificar o Status para confirmação do Pagamento
            if (grv.StatusOperacao.StatusOperacaoId != "V" && grv.StatusOperacao.StatusOperacaoId != "1")
            {
                avisos.Erros.Add($"Status do GRV não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao.ToUpper()}");

                return avisos;
            }
            #endregion Consultas

            return avisos;
        }

        public async Task<CalculoFaturamentoModel> Cadastrar(AtendimentoViewModel AtendimentoInput)
        {
            GrvModel Grv = await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId == AtendimentoInput.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = await new DepositoService(_context)
                .SelecionarDataHoraPorDeposito(Grv.DepositoId);

            AtendimentoModel Atendimento = new()
            {
                GrvId = AtendimentoInput.GrvId,

                QualificacaoResponsavelId = AtendimentoInput.QualificacaoResponsavelId,

                UsuarioCadastroId = AtendimentoInput.UsuarioCadastroId,

                DataHoraInicioAtendimento = AtendimentoInput.DataHoraInicioAtendimento,

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

            List<TipoMeioCobrancaModel> TiposMeiosCobrancas = await _context.TiposMeiosCobrancas
                .AsNoTracking()
                .ToListAsync();

            GrvModel GrvToUpdate = await _context.Grvs
                .Where(w => w.GrvId == AtendimentoInput.GrvId)
                .FirstOrDefaultAsync();

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                UsuarioCadastroId = Atendimento.UsuarioCadastroId,

                Grv = Grv,

                Atendimento = Atendimento,

                DataHoraAtualPorDeposito = DataHoraPorDeposito,

                TipoMeioCobrancaId = Grv.Cliente.TipoMeioCobrancaId.HasValue && Grv.Cliente.TipoMeioCobrancaId.Value > 0 ? Grv.Cliente.TipoMeioCobrancaId.Value : AtendimentoInput.TipoMeioCobrancaId
            };

            ParametrosCalculoFaturamento.StatusOperacaoId = GrvToUpdate.StatusOperacaoId = await GetStatusOperacao(TiposMeiosCobrancas, ParametrosCalculoFaturamento.TipoMeioCobrancaId);

            CalculoFaturamentoModel CalculoFaturamento = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Atendimentos.AddAsync(Atendimento);

                    await _context.SaveChangesAsync();

                    ParametrosCalculoFaturamento.Atendimento = Atendimento;

                    CalculoFaturamento = await new FaturamentoService(_context)
                        .Faturar(ParametrosCalculoFaturamento);

                    _context.Grvs.Update(GrvToUpdate);

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    CalculoFaturamento.Mensagens.Erros.Add(ex.Message);
                }
            }

            return CalculoFaturamento;
        }

        private async Task<string> GetStatusOperacao(List<TipoMeioCobrancaModel> TiposMeiosCobrancas, int TipoMeioCobrancaId)
        {
            TipoMeioCobrancaModel TipoMeioCobranca = new();

            if (TiposMeiosCobrancas == null)
            {
                TipoMeioCobranca = await _context.TiposMeiosCobrancas
                    .Where(w => w.TipoMeioCobrancaId == TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            else
            {
                TipoMeioCobranca = TiposMeiosCobrancas
                    .Where(w => w.TipoMeioCobrancaId == TipoMeioCobrancaId)
                    .FirstOrDefault();
            }

            string StatusOperacaoId = "L"; // Aguardando Pagamento

            if (TipoMeioCobranca.Alias == "LIBESP")
            {
                StatusOperacaoId = "U"; // Aguardando Liberação Especial
            }

            return StatusOperacaoId;
        }
    }
}