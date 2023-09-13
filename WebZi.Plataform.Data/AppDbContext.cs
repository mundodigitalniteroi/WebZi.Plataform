using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data
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

        #region DbSets
        public DbSet<AtendimentoModel> Atendimentos { get; set; }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<DepositoModel> Depositos { get; set; }

        public DbSet<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public DbSet<FaturamentoRegraTipoModel> FaturamentoRegraTipos { get; set; }

        public DbSet<GrvModel> Grvs { get; set; }

        public DbSet<StatusOperacaoModel> StatusOperacoes { get; set; }

        public DbSet<TipoDocumentoIdentificacaoModel> TiposDocumentosIdentificacao { get; set; }

        public DbSet<TipoVeiculoModel> TiposVeiculos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        #endregion DbSets
    }
}