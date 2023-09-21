using Castle.Core.Resource;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Localizacao.View;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;

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

        #region DbSets

        #region Depósito Público
        public DbSet<AtendimentoModel> Atendimentos { get; set; }

        public DbSet<AtendimentoFotoResponsavelModel> AtendimentosFotosResponsaveis { get; set; }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<DepositoModel> Depositos { get; set; }

        public DbSet<EnquadramentoInfracaoModel> EnquadramentosInfracoes { get; set; }

        public DbSet<FaturamentoModel> Faturamentos { get; set; }

        public DbSet<FaturamentoBoletoModel> FaturamentoBoletos { get; set; }

        public DbSet<FaturamentoBoletoImagemModel> FaturamentoBoletoImagens { get; set; }

        public DbSet<FaturamentoCartaoModel> FaturamentoCartoes { get; set; }

        public DbSet<FaturamentoCodigoAutorizacaoCartaoModel> FaturamentoCodigosAutorizacoesCartoes { get; set; }

        public DbSet<FaturamentoComposicaoModel> FaturamentoComposicoes { get; set; }

        public DbSet<FaturamentoComposicaoNotaFiscalModel> FaturamentoComposicaoNotasFiscais { get; set; }

        public DbSet<FaturamentoCondicaoPagamentoModel> FaturamentoCondicoesPagamentos { get; set; }

        public DbSet<FaturamentoProdutoModel> FaturamentoProdutos { get; set; }

        public DbSet<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public DbSet<FaturamentoRegraTipoModel> FaturamentoRegraTipos { get; set; }

        public DbSet<FaturamentoServicoAssociadoModel> FaturamentoServicosAssociados { get; set; }

        public DbSet<FaturamentoServicoGrvModel> FaturamentoServicosGrvs { get; set; }

        public DbSet<FaturamentoServicoTipoModel> FaturamentoServicosTipos { get; set; }

        public DbSet<FaturamentoServicoTipoVeiculoModel> FaturamentoServicosTiposVeiculos { get; set; }

        public DbSet<FaturamentoTipoComposicaoModel> FaturamentoTiposComposicoes { get; set; }

        public DbSet<QualificacaoResponsavelModel> QualificacoesResponsaveis { get; set; }

        public DbSet<GrvModel> Grvs { get; set; }

        public DbSet<ReboqueModel> Reboques { get; set; }

        public DbSet<ReboquistaModel> Reboquistas { get; set; }

        public DbSet<StatusOperacaoModel> StatusOperacoes { get; set; }

        public DbSet<TipoDocumentoIdentificacaoModel> TiposDocumentosIdentificacao { get; set; }

        public DbSet<TipoMeioCobrancaModel> TiposMeiosCobrancas { get; set; }

        public DbSet<TipoVeiculoModel> TiposVeiculos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<UsuarioClienteModel> UsuariosClientes { get; set; }

        public DbSet<UsuarioDepositoModel> UsuariosDepositos { get; set; }

        public DbSet<UsuarioTipoPermissaoModel> UsuariosTiposPermissoes { get; set; }

        public DbSet<UsuarioPermissaoModel> UsuariosPermissoes { get; set; }
        #endregion Depósito Público

        #region Views
        public DbSet<ViewFaturamentoServicoGrvModel> ViewFaturamentoServicosGrvs { get; set; }

        public DbSet<ViewFaturamentoServicoAssociadoVeiculoModel> ViewFaturamentoServicosAssociadosVeiculos { get; set; }

        public DbSet<ViewEnderecoCompletoModel> ViewEnderecosCompletos { get; set; }
        #endregion Views

        #region Leilão
        public DbSet<LeilaoModel> Leiloes { get; set; }

        public DbSet<LeilaoLoteModel> LeilaoLotes { get; set; }

        public DbSet<LeilaoLoteStatusModel> LeilaoLotesStatus { get; set; }

        public DbSet<LeilaoStatusModel> LeilaoStatus { get; set; }
        #endregion

        #region Localização
        public DbSet<BairroModel> Bairros { get; set; }

        public DbSet<CEPModel> CEPs { get; set; }

        public DbSet<ConfiguracaoModel> Configuracao { get; set; }

        public DbSet<ContinenteModel> Continentes { get; set; }

        public DbSet<EstadoModel> Estados { get; set; }

        public DbSet<FeriadoModel> Feriados { get; set; }

        public DbSet<MunicipioModel> Municipios { get; set; }

        public DbSet<PaisModel> Paises { get; set; }

        public DbSet<RegiaoModel> Regioes { get; set; }

        public DbSet<TipoLogradouroModel> TiposLogradouros { get; set; }

        public DbSet<UTCModel> UTCs { get; set; }
        #endregion Localização

        #endregion DbSets
    }
}