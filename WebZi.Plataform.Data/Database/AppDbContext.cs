using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Views.Faturamento;
using WebZi.Plataform.Domain.Views.Localizacao;
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(new AppSettingsHelper().GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(120))
                .LogTo(Console.WriteLine, LogLevel.Information) // Exibe as queries executadas no BD pelo EF
                .EnableSensitiveDataLogging(); // Configura o EF para exibir os dados
        }

        public void SetUserContextInfo(int UsuarioId)
        {
            Database.ExecuteSqlRaw($"EXECUTE dbo.sp_set_contextinfo {UsuarioId}");
        }

        #region DbSets public DbSet<Model> Name { get; set; }

        #region Depósito Público
        public DbSet<AgenciaBancariaModel> AgenciaBancaria { get; set; }

        public DbSet<AtendimentoModel> Atendimento { get; set; }

        public DbSet<AtendimentoFotoResponsavelModel> AtendimentoFotoResponsavel { get; set; }

        public DbSet<BancoModel> Banco { get; set; }

        public DbSet<BucketArquivoModel> BucketArquivo { get; set; }

        public DbSet<BucketNomeTabelaOrigemModel> BucketNomeTabelaOrigem { get; set; }

        public DbSet<ClienteModel> Cliente { get; set; }

        public DbSet<ClienteDepositoModel> ClienteDeposito { get; set; }

        public DbSet<ConfiguracaoModel> Configuracao { get; set; }

        public DbSet<DepositoModel> Deposito { get; set; }

        public DbSet<EnquadramentoInfracaoModel> EnquadramentoInfracoe { get; set; }

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

        public DbSet<MotivoApreensaoModel> MotivoApreensao { get; set; }

        public DbSet<QualificacaoResponsavelModel> QualificacaoResponsavel { get; set; }

        public DbSet<GrvModel> Grv { get; set; }

        public DbSet<LacreModel> Lacre { get; set; }

        public DbSet<ReboqueModel> Reboque { get; set; }

        public DbSet<ReboquistaModel> Reboquista { get; set; }

        public DbSet<StatusOperacaoModel> StatusOperacao { get; set; }

        public DbSet<TipoDocumentoIdentificacaoModel> TipoDocumentoIdentificacao { get; set; }

        public DbSet<TipoMeioCobrancaModel> TipoMeioCobranca { get; set; }

        public DbSet<TipoVeiculoModel> TipoVeiculo { get; set; }

        public DbSet<UsuarioModel> Usuario { get; set; }

        public DbSet<UsuarioClienteModel> UsuarioCliente { get; set; }

        public DbSet<UsuarioDepositoModel> UsuarioDeposito { get; set; }

        public DbSet<UsuarioTipoPermissaoModel> UsuarioTipoPermissao { get; set; }

        public DbSet<UsuarioPermissaoModel> UsuarioPermissao { get; set; }

        public DbSet<WebServiceUrlModel> WebServiceUrl { get; set; }
        #endregion Depósito Público

        #region Views

        public DbSet<ViewUsuarioClienteDepositoModel> ViewUsuarioClienteDeposito { get; set; }

        public DbSet<ViewFaturamentoBoletoModel> ViewFaturamentoBoleto { get; set; }

        public DbSet<ViewFaturamentoServicoGrvModel> ViewFaturamentoServicoGrv { get; set; }

        public DbSet<ViewFaturamentoServicoAssociadoVeiculoModel> ViewFaturamentoServicoAssociadoVeiculo { get; set; }

        public DbSet<ViewEnderecoCompletoModel> ViewEnderecoCompleto { get; set; }
        #endregion Views

        #region Leilão
        public DbSet<LeilaoModel> Leilao { get; set; }

        public DbSet<LeilaoLoteModel> LeilaoLote { get; set; }

        public DbSet<LeilaoLoteStatusModel> LeilaoLoteStatus { get; set; }

        public DbSet<LeilaoStatusModel> LeilaoStatus { get; set; }

        public DbSet<LiberacaoLeilaoModel> LiberacaoLeilao { get; set; }
        #endregion

        #region Localização
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
        #endregion Localização

        #endregion DbSets
    }
}