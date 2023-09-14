using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Local;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;

        public AtendimentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AtendimentoModel> GetById(int AtendimentoId, int UsuarioId)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .Where(w => w.AtendimentoId.Equals(AtendimentoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return atendimento;
        }

        public async Task<AtendimentoModel> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .Include(i => i.Grv)
                .Where(w => w.Grv.NumeroFormularioGrv.Equals(NumeroProcesso) && w.Grv.ClienteId.Equals(ClienteId) && w.Grv.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return atendimento;
        }

        public async Task<AtendimentoAvisoViewModel> ValidarInformacoesParaCadastro(AtendimentoViewModel Atendimento)
        {
            AtendimentoAvisoViewModel avisos = new();

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
                .Where(w => w.GrvId.Equals(Atendimento.GrvId))
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
            else if (!LocalHelper.IsCEP(Atendimento.ResponsavelCep))
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
            else if (!LocalHelper.IsUF(Atendimento.ResponsavelUf))
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
                    .Where(w => w.TipoDocumentoIdentificacaoId.Equals(Atendimento.ProprietarioTipoDocumentoId))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoDocumentoIdentificacao == null)
                {
                    avisos.Erros.Add($"Tipo do Documento do Proprietário inexistente: {Atendimento.ProprietarioTipoDocumentoId}");
                }
                else if (TipoDocumentoIdentificacao.Codigo.Equals("CPF") && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    avisos.Erros.Add($"O CPF do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
                else if (TipoDocumentoIdentificacao.Codigo.Equals("CNPJ") && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    avisos.Erros.Add($"O CNPJ do Proprietário é inválido: {Atendimento.ProprietarioDocumento}");
                }
            }
            #endregion Dados do Proprietário

            #region Nota Fiscal
            if (Grv.Cliente.FlagEmissaoNotaFiscalSap.Equals("S"))
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
                else if (!LocalHelper.IsCEP(Atendimento.NotaFiscalCep))
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
                else if (!LocalHelper.IsUF(Atendimento.NotaFiscalUf))
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
                        .Where(w => w.ClienteId.Equals(Grv.ClienteId) &&
                                    w.FaturamentoRegraTipo.Codigo.Equals("ATENDINSCRICMUNIC"))
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
                    .Where(w => w.TipoMeioCobrancaId.Equals(Atendimento.TipoMeioCobrancaId))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (TipoMeioCobranca == null)
                {
                    avisos.Erros.Add($"Forma de Pagamento inexistente: {Atendimento.TipoMeioCobrancaId}");
                }

                if ((TipoMeioCobranca.Alias.Equals("PIX") || TipoMeioCobranca.Alias.Equals("PIXEST")) && Grv.Cliente.FlagPossuiPixEstatico == "N")
                {
                    avisos.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Estático");
                }
                else if (TipoMeioCobranca.Alias.Equals("PIXDIN") && Grv.Cliente.FlagPossuiPixDinamico == "N")
                {
                    avisos.Erros.Add("Este Cliente não está configurado para permitir a Forma de Pagamento PIX Dinâmico");
                }
            }
            #endregion

            return avisos;
        }

        public async Task<AtendimentoAvisoViewModel> ValidarInformacoesParaAtualizacao(AtendimentoViewModel Atendimento)
        {
            AtendimentoAvisoViewModel avisos = new();

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
                .Where(w => w.GrvId.Equals(Atendimento.GrvId))
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

        public async Task<string> Cadastrar(AtendimentoViewModel AtendimentoInput)
        {
            GrvModel Grv = await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Where(w => w.GrvId.Equals(AtendimentoInput.GrvId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            AtendimentoModel Atendimento = new()
            {
                GrvId = AtendimentoInput.GrvId,

                QualificacaoResponsavelId = AtendimentoInput.QualificacaoResponsavelId,

                UsuarioCadastroId = AtendimentoInput.UsuarioCadastroId,

                DataHoraInicioAtendimento = AtendimentoInput.DataHoraInicioAtendimento,

                DataCadastro = DateTime.Now, //ConfiguracoesController.SelecionarDataHoraPorDeposito(ClientesDepositosModel.DepositoId).ToString("yyyyMMdd HH:mm");

                DataImpressao = DateTime.Now, //ConfiguracoesController.SelecionarDataHoraPorDeposito(ClientesDepositosModel.DepositoId).ToString("yyyyMMdd HH:mm");

                TotalImpressoes = 1,

                ResponsavelNome = AtendimentoInput.ResponsavelNome,

                ResponsavelDocumento = AtendimentoInput.ResponsavelDocumento,

                ResponsavelCnh = AtendimentoInput.ResponsavelCnh,

                ResponsavelEndereco = AtendimentoInput.ResponsavelEndereco,

                ResponsavelNumero = AtendimentoInput.ResponsavelNumero,

                ResponsavelComplemento = AtendimentoInput.ResponsavelComplemento,

                ResponsavelBairro = AtendimentoInput.ResponsavelBairro,

                ResponsavelMunicipio = AtendimentoInput.ResponsavelMunicipio,

                ResponsavelUf = AtendimentoInput.ResponsavelUf,

                ResponsavelCep = AtendimentoInput.ResponsavelCep,

                ResponsavelDdd = AtendimentoInput.ResponsavelDdd,

                ResponsavelTelefone = AtendimentoInput.ResponsavelTelefone,

                ProprietarioNome = AtendimentoInput.ProprietarioNome,

                ProprietarioTipoDocumentoId = AtendimentoInput.ProprietarioTipoDocumentoId,

                ProprietarioDocumento = AtendimentoInput.ProprietarioDocumento,

                ProprietarioEndereco = AtendimentoInput.ProprietarioEndereco,

                ProprietarioNumero = AtendimentoInput.ProprietarioNumero,

                ProprietarioComplemento = AtendimentoInput.ProprietarioComplemento,

                ProprietarioBairro = AtendimentoInput.ProprietarioBairro,

                ProprietarioMunicipio = AtendimentoInput.ProprietarioMunicipio,

                ProprietarioUf = AtendimentoInput.ProprietarioUf,

                ProprietarioCep = AtendimentoInput.ProprietarioCep,

                ProprietarioDdd = AtendimentoInput.ProprietarioDdd,

                ProprietarioTelefone = AtendimentoInput.ProprietarioTelefone
            };

            if (Grv.Cliente.FlagEmissaoNotaFiscalSap.Equals("S"))
            {
                Atendimento.NotaFiscalNome = AtendimentoInput.NotaFiscalNome;

                Atendimento.NotaFiscalDocumento = AtendimentoInput.NotaFiscalDocumento;

                Atendimento.NotaFiscalEndereco = AtendimentoInput.NotaFiscalEndereco;

                Atendimento.NotaFiscalNumero = AtendimentoInput.NotaFiscalNumero;

                Atendimento.NotaFiscalComplemento = AtendimentoInput.NotaFiscalComplemento;

                Atendimento.NotaFiscalBairro = AtendimentoInput.NotaFiscalBairro;

                Atendimento.NotaFiscalMunicipio = AtendimentoInput.NotaFiscalMunicipio;

                Atendimento.NotaFiscalUf = AtendimentoInput.NotaFiscalUf;

                Atendimento.NotaFiscalCep = AtendimentoInput.NotaFiscalCep;

                Atendimento.NotaFiscalDdd = AtendimentoInput.NotaFiscalDdd;

                Atendimento.NotaFiscalTelefone = AtendimentoInput.NotaFiscalTelefone;

                Atendimento.NotaFiscalEmail = AtendimentoInput.NotaFiscalEmail;

                Atendimento.NotaFiscalInscricaoMunicipal = AtendimentoInput.NotaFiscalInscricaoMunicipal;
            }

            FaturamentoModel Faturamento = new();

            if (Grv.Cliente.TipoMeioCobrancaId.Value.Equals(0))
            {
                Faturamento.TipoMeioCobrancaId = AtendimentoInput.TipoMeioCobrancaId;
            }
            else
            {
                Faturamento.TipoMeioCobrancaId = Grv.Cliente.TipoMeioCobrancaId.Value;
            }

            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TiposMeiosCobrancas
                .Where(w => w.TipoMeioCobrancaId.Equals(Faturamento.TipoMeioCobrancaId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            string novoStatusOperacaoId = "L"; // Aguardando Pagamento

            if (TipoMeioCobranca.Alias.Equals("LIBESP"))
            {
                novoStatusOperacaoId = "U"; // Aguardando Liberação Especial
            }

            GrvModel GrvUpdate = await _context.Grvs
                .Where(w => w.GrvId.Equals(AtendimentoInput.GrvId))
                .FirstOrDefaultAsync();

            GrvUpdate.StatusOperacaoId = novoStatusOperacaoId;

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Atendimentos.Add(Atendimento);

                _context.Grvs.Update(GrvUpdate);

                _context.SaveChanges();

                transaction.Commit();
            }

            if (true)
            {

            }

            return null;
        }
    }
}