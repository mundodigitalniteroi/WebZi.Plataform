using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.PIX;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Database;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Governo;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Pessoa;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.Views.Faturamento;
using WebZi.Plataform.Domain.Views.Localizacao;
using WebZi.Plataform.Domain.Views.Report;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Database
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adiciona automaticamente todas as configurações criadas por TypeConfiguration encontradas no Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder
                .Entity<StoreProcedurePrimaryKeyModel>()
                .HasNoKey();

            modelBuilder
                .Entity<StoreProcedureForeingKeyModel>()
                .HasNoKey();

            modelBuilder
                .Entity<GenericIntModel>()
                .HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(AppSettingsHelper.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(120))
                .LogTo(Console.WriteLine, LogLevel.Information) // Exibe as queries executadas no BD pelo EF
                .EnableSensitiveDataLogging(); // Configura o EF para exibir os dados sensíveis
        }

        public string GetConnectionString()
        {
            return AppSettingsHelper.GetConnectionString("DefaultConnection");
        }

        public void SetUserContextInfo(int UsuarioId)
        {
            Database.ExecuteSqlRaw($"EXECUTE dbo.sp_set_contextinfo {UsuarioId}");
        }

        [DbFunction(Name = "sp_fkeys", Schema = "dbo", IsBuiltIn = true)]
        public string GetForeingKeys(string tableName)
        {
            throw new NotImplementedException();
        }

        #region DbSets public DbSet<Model> Name { get; set; }

        public DbSet<GenericIntModel> GenericInt { get; set; }

        #region Banco
        public DbSet<AgenciaBancariaModel> AgenciaBancaria { get; set; }

        public DbSet<BancoModel> Banco { get; set; }

        public DbSet<PixEstaticoModel> PixEstatico { get; set; }

        public DbSet<PixDinamicoModel> PixDinamico { get; set; }

        public DbSet<PixDinamicoConfiguracaoModel> PixDinamicoConfiguracao { get; set; }

        public DbSet<PixDinamicoConsultaModel> PixDinamicoConsulta { get; set; }

        public DbSet<PixDinamicoSenhaConfirmacaoTranferenciaModel> PixDinamicoSenhaConfirmacaoTranferencia { get; set; }

        public DbSet<PixDinamicoTipoStatusGeracaoModel> PixDinamicoTipoStatusGeracao { get; set; }

        public DbSet<PixDinamicoUrlModel> PixDinamicoUrl { get; set; }
        #endregion Banco

        #region Depósito Público
        public DbSet<AtendimentoModel> Atendimento { get; set; }

        public DbSet<AtendimentoFotoResponsavelModel> AtendimentoFotoResponsavel { get; set; }

        public DbSet<AutoridadeResponsavelModel> AutoridadeResponsavel { get; set; }

        public DbSet<BucketArquivoModel> BucketArquivo { get; set; }

        public DbSet<BucketNomeTabelaOrigemModel> BucketNomeTabelaOrigem { get; set; }

        public DbSet<ClienteModel> Cliente { get; set; }

        public DbSet<ClienteCodigoIdentificacaoModel> ClienteCodigoIdentificacao { get; set; }

        public DbSet<ClienteDepositoModel> ClienteDeposito { get; set; }

        public DbSet<ClienteDepositoTipoVeiculoModel> ClienteDepositoTipoVeiculo { get; set; }

        public DbSet<ClienteRegraModel> ClienteRegra { get; set; }

        public DbSet<ClienteRegraTipoModel> ClienteRegraTipo { get; set; }

        public DbSet<CondutorModel> Condutor { get; set; }

        public DbSet<CondutorDocumentoModel> CondutorDocumento { get; set; }

        public DbSet<CondutorEquipamentoOpcionalModel> CondutorEquipamentoOpcional { get; set; }

        public DbSet<CondutorEquipamentoOpcionalNaoConformidadeModel> CondutorEquipamentoOpcionalNaoConformidade { get; set; }

        public DbSet<ConfiguracaoModel> Configuracao { get; set; }

        public DbSet<ConfiguracaoLogoModel> ConfiguracaoLogo { get; set; }

        public DbSet<DepositoModel> Deposito { get; set; }

        public DbSet<EnquadramentoInfracaoModel> EnquadramentoInfracao { get; set; }

        public DbSet<EnquadramentoInfracaoGrvModel> EnquadramentoInfracaoGrv { get; set; }

        public DbSet<EquipamentoOpcionalModel> EquipamentoOpcional { get; set; }

        public DbSet<MotivoApreensaoModel> MotivoApreensao { get; set; }

        public DbSet<QualificacaoResponsavelModel> QualificacaoResponsavel { get; set; }

        public DbSet<GrvModel> Grv { get; set; }

        public DbSet<LacreModel> Lacre { get; set; }

        public DbSet<MarcaModeloModel> MarcaModelo { get; set; }

        public DbSet<ReboqueModel> Reboque { get; set; }

        public DbSet<ReboquistaModel> Reboquista { get; set; }

        public DbSet<StatusOperacaoModel> StatusOperacao { get; set; }

        public DbSet<TabelaGenericaModel> TabelaGenerica { get; set; }

        public DbSet<TipoAvariaModel> TipoAvaria { get; set; }

        public DbSet<TipoVeiculoClassificacaoModel> TipoVeiculoClassificacao { get; set; }

        public DbSet<TipoVeiculoClassificacaoNomeModel> TipoVeiculoClassificacaoNome { get; set; }

        public DbSet<TipoVeiculoEquipamentoAssociacaoModel> TipoVeiculoEquipamentoAssociacao { get; set; }

        public DbSet<TipoMeioCobrancaModel> TipoMeioCobranca { get; set; }

        public DbSet<TipoVeiculoModel> TipoVeiculo { get; set; }

        public DbSet<UsuarioModel> Usuario { get; set; }

        public DbSet<UsuarioClienteModel> UsuarioCliente { get; set; }

        public DbSet<UsuarioDepositoModel> UsuarioDeposito { get; set; }

        public DbSet<UsuarioTipoPermissaoModel> UsuarioTipoPermissao { get; set; }

        public DbSet<UsuarioPermissaoModel> UsuarioPermissao { get; set; }

        public DbSet<WebServiceUrlModel> WebServiceUrl { get; set; }
        #endregion Depósito Público

        #region Endereço
        public DbSet<BairroModel> Bairro { get; set; }

        public DbSet<CEPModel> CEP { get; set; }

        public DbSet<ContinenteModel> Continente { get; set; }

        public DbSet<EstadoModel> Estado { get; set; }

        public DbSet<FeriadoModel> Feriado { get; set; }

        public DbSet<MunicipioModel> Municipio { get; set; }

        public DbSet<PaisModel> Pais { get; set; }

        public DbSet<RegiaoModel> Regiao { get; set; }

        public DbSet<TipoLogradouroModel> TipoLogradouro { get; set; }

        public DbSet<UTCModel> UTC { get; set; }
        #endregion Endereço

        #region Faturamento
        public DbSet<FaturamentoModel> Faturamento { get; set; }

        public DbSet<FaturamentoBoletoModel> FaturamentoBoleto { get; set; }

        public DbSet<FaturamentoBoletoImagemModel> FaturamentoBoletoImagem { get; set; }

        public DbSet<FaturamentoCartaoModel> FaturamentoCartao { get; set; }

        public DbSet<FaturamentoCodigoAutorizacaoCartaoModel> FaturamentoCodigoAutorizacaoCartao { get; set; }

        public DbSet<FaturamentoComposicaoModel> FaturamentoComposicao { get; set; }

        public DbSet<FaturamentoComposicaoNotaFiscalModel> FaturamentoComposicaoNotaFiscal { get; set; }

        public DbSet<FaturamentoCondicaoPagamentoModel> FaturamentoCondicaoPagamento { get; set; }

        public DbSet<FaturamentoProdutoModel> FaturamentoProduto { get; set; }

        public DbSet<FaturamentoRegraModel> FaturamentoRegra { get; set; }

        public DbSet<FaturamentoRegraTipoModel> FaturamentoRegraTipo { get; set; }

        public DbSet<FaturamentoServicoAssociadoModel> FaturamentoServicoAssociado { get; set; }

        public DbSet<FaturamentoServicoGrvModel> FaturamentoServicoGrv { get; set; }

        public DbSet<FaturamentoServicoTipoModel> FaturamentoServicoTipo { get; set; }

        public DbSet<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculo { get; set; }

        public DbSet<FaturamentoTipoComposicaoModel> FaturamentoTipoComposicao { get; set; }
        #endregion Faturamento

        #region Global
        public DbSet<CorModel> Cor { get; set; }

        public DbSet<EmpresaClassificacaoModel> EmpresaClassificacao { get; set; }

        public DbSet<EmpresaModel> Empresa { get; set; }
        #endregion Global

        #region Governo
        public DbSet<AssociacaoCnaeListaServicoModel> AssociacaoCnaeListaServico { get; set; }

        public DbSet<CnaeModel> Cnae { get; set; }

        public DbSet<ListaServicoModel> ListaServico { get; set; }

        public DbSet<ParametroMunicipioModel> ParametroMunicipio { get; set; }

        #endregion Governo

        #region Leilão
        public DbSet<LeilaoModel> Leilao { get; set; }

        public DbSet<LeilaoLoteModel> LeilaoLote { get; set; }

        public DbSet<LeilaoLoteStatusModel> LeilaoLoteStatus { get; set; }

        public DbSet<LeilaoStatusModel> LeilaoStatus { get; set; }

        public DbSet<LiberacaoLeilaoModel> LiberacaoLeilao { get; set; }
        #endregion

        #region Liberação
        public DbSet<CobrancaLegalModel> CobrancaLegal { get; set; }

        public DbSet<LiberacaoModel> Liberacao { get; set; }

        public DbSet<TipoCobrancaLegalModel> TipoCobrancaLegal { get; set; }

        public DbSet<TipoLiberacaoModel> TipoLiberacao { get; set; }
        #endregion Liberação

        #region Pessoa
        public DbSet<OrgaoEmissorModel> OrgaoEmissor { get; set; }

        public DbSet<PessoaModel> Pessoa { get; set; }

        public DbSet<TipoDocumentoIdentificacaoModel> TipoDocumentoIdentificacao { get; set; }
        #endregion Pessoa

        #region Vistoria
        public DbSet<VistoriaModel> Vistoria { get; set; }

        public DbSet<VistoriaSituacaoChassiModel> VistoriaSituacaoChassi { get; set; }

        public DbSet<VistoriaStatusModel> VistoriaStatus { get; set; }
        #endregion Vistoria

        #region Views
        public DbSet<ViewFaturamentoBoletoModel> ViewFaturamentoBoleto { get; set; }

        public DbSet<ViewFaturamentoServicoAssociadoVeiculoModel> ViewFaturamentoServicoAssociadoVeiculo { get; set; }

        public DbSet<ViewFaturamentoServicoGrvModel> ViewFaturamentoServicoGrv { get; set; }

        public DbSet<ViewGuiaPagamentoReboqueEstadiaModel> ViewGuiaPagamentoReboqueEstadia { get; set; }

        public DbSet<ViewUsuarioClienteDepositoGrvModel> ViewUsuarioClienteDepositoGrv { get; set; }

        public DbSet<ViewUsuarioClienteDepositoReboqueModel> ViewUsuarioClienteDepositoReboque { get; set; }

        public DbSet<ViewUsuarioClienteDepositoReboquistaModel> ViewUsuarioClienteDepositoReboquista { get; set; }

        public DbSet<ViewUsuarioClienteDepositoModel> ViewUsuarioClienteDeposito { get; set; }

        public DbSet<ViewEnderecoCompletoModel> EnderecoCompleto { get; set; }
        #endregion Views

        #endregion DbSets
    }
}