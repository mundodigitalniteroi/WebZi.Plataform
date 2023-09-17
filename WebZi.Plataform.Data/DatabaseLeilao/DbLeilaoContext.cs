using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.ModelsLeilao;

namespace WebZi.Plataform.Data.DatabaseLeilao;

public partial class DbLeilaoContext : DbContext
{
    public DbLeilaoContext()
    {
    }

    public DbLeilaoContext(DbContextOptions<DbLeilaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GrupoListaContato> GrupoListaContatos { get; set; }

    public virtual DbSet<ImportacaoLote> ImportacaoLotes { get; set; }

    public virtual DbSet<LeilaoDetranpb0120Ipva> LeilaoDetranpb0120Ipvas { get; set; }

    public virtual DbSet<LeilaoDetranpb0120Multum> LeilaoDetranpb0120Multa { get; set; }

    public virtual DbSet<LeilaoDetranpb0120Restricao> LeilaoDetranpb0120Restricaos { get; set; }

    public virtual DbSet<LeilaoDetranpb0120Veiculo> LeilaoDetranpb0120Veiculos { get; set; }

    public virtual DbSet<LeilaoDetranpb0220Ipva> LeilaoDetranpb0220Ipvas { get; set; }

    public virtual DbSet<LeilaoDetranpb0220Multum> LeilaoDetranpb0220Multa { get; set; }

    public virtual DbSet<LeilaoDetranpb0220Restricao> LeilaoDetranpb0220Restricaos { get; set; }

    public virtual DbSet<LeilaoDetranpb0220Veiculo> LeilaoDetranpb0220Veiculos { get; set; }

    public virtual DbSet<ListaContato> ListaContatos { get; set; }

    public virtual DbSet<LotesImportar> LotesImportars { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<TbArrematantesFatura> TbArrematantesFaturas { get; set; }

    public virtual DbSet<TbArrematantesFaturaItem> TbArrematantesFaturaItems { get; set; }

    public virtual DbSet<TbComitente> TbComitentes { get; set; }

    public virtual DbSet<TbComitentesRegra> TbComitentesRegras { get; set; }

    public virtual DbSet<TbComitentesTaxa> TbComitentesTaxas { get; set; }

    public virtual DbSet<TbComitentesTipoImportacao> TbComitentesTipoImportacaos { get; set; }

    public virtual DbSet<TbComitentesTransacao> TbComitentesTransacaos { get; set; }

    public virtual DbSet<TbDetranBaFtpCodigoRetorno> TbDetranBaFtpCodigoRetornos { get; set; }

    public virtual DbSet<TbDetranBaFtpEnvio> TbDetranBaFtpEnvios { get; set; }

    public virtual DbSet<TbDetranBaFtpEnvioDetalhe> TbDetranBaFtpEnvioDetalhes { get; set; }

    public virtual DbSet<TbDetranBaFtpRetorno> TbDetranBaFtpRetornos { get; set; }

    public virtual DbSet<TbDetranBaFtpRetornoDetalhe> TbDetranBaFtpRetornoDetalhes { get; set; }

    public virtual DbSet<TbExpositore> TbExpositores { get; set; }

    public virtual DbSet<TbFaturaComposicaoCobranca> TbFaturaComposicaoCobrancas { get; set; }

    public virtual DbSet<TbFaturaFormaPagamento> TbFaturaFormaPagamentos { get; set; }

    public virtual DbSet<TbFaturaStatus> TbFaturaStatuses { get; set; }

    public virtual DbSet<TbFinanceira> TbFinanceiras { get; set; }

    public virtual DbSet<TbImportacaoTdi> TbImportacaoTdis { get; set; }

    public virtual DbSet<TbLeiaoLotesServico> TbLeiaoLotesServicos { get; set; }

    public virtual DbSet<TbLeiaoPrestacaoConta> TbLeiaoPrestacaoContas { get; set; }

    public virtual DbSet<TbLeiaoPrestacaoContasIten> TbLeiaoPrestacaoContasItens { get; set; }

    public virtual DbSet<TbLeilao> TbLeilaos { get; set; }

    public virtual DbSet<TbLeilaoAudit> TbLeilaoAudits { get; set; }

    public virtual DbSet<TbLeilaoDespesa> TbLeilaoDespesas { get; set; }

    public virtual DbSet<TbLeilaoEditai> TbLeilaoEditais { get; set; }

    public virtual DbSet<TbLeilaoImportacao> TbLeilaoImportacaos { get; set; }

    public virtual DbSet<TbLeilaoImportacaoResultado> TbLeilaoImportacaoResultados { get; set; }

    public virtual DbSet<TbLeilaoLote> TbLeilaoLotes { get; set; }

    public virtual DbSet<TbLeilaoLotesArrematante> TbLeilaoLotesArrematantes { get; set; }

    public virtual DbSet<TbLeilaoLotesArrematantesAudit> TbLeilaoLotesArrematantesAudits { get; set; }

    public virtual DbSet<TbLeilaoLotesArrematantesImportacao> TbLeilaoLotesArrematantesImportacaos { get; set; }

    public virtual DbSet<TbLeilaoLotesArrematantesImportacaoIten> TbLeilaoLotesArrematantesImportacaoItens { get; set; }

    public virtual DbSet<TbLeilaoLotesArrematantesImportacaoResultado> TbLeilaoLotesArrematantesImportacaoResultados { get; set; }

    public virtual DbSet<TbLeilaoLotesAudit> TbLeilaoLotesAudits { get; set; }

    public virtual DbSet<TbLeilaoLotesDespesa> TbLeilaoLotesDespesas { get; set; }

    public virtual DbSet<TbLeilaoLotesFoto> TbLeilaoLotesFotos { get; set; }

    public virtual DbSet<TbLeilaoLotesPericiaArquivo> TbLeilaoLotesPericiaArquivos { get; set; }

    public virtual DbSet<TbLeilaoLotesPericium> TbLeilaoLotesPericia { get; set; }

    public virtual DbSet<TbLeilaoLotesProprietario> TbLeilaoLotesProprietarios { get; set; }

    public virtual DbSet<TbLeilaoLotesRestrico> TbLeilaoLotesRestricoes { get; set; }

    public virtual DbSet<TbLeilaoLotesTiposTransacao> TbLeilaoLotesTiposTransacaos { get; set; }

    public virtual DbSet<TbLeilaoLotesTransaco> TbLeilaoLotesTransacoes { get; set; }

    public virtual DbSet<TbLeilaoRegrasPrestacaoConta> TbLeilaoRegrasPrestacaoContas { get; set; }

    public virtual DbSet<TbLeilaoSapStatus> TbLeilaoSapStatuses { get; set; }

    public virtual DbSet<TbLeilaoStatus> TbLeilaoStatuses { get; set; }

    public virtual DbSet<TbLeiloeiro> TbLeiloeiros { get; set; }

    public virtual DbSet<TbLogEmailArrematacaoBoleto> TbLogEmailArrematacaoBoletos { get; set; }

    public virtual DbSet<TbLogEmailNotificacao> TbLogEmailNotificacaos { get; set; }

    public virtual DbSet<TbLogEmailNotificacaoLote> TbLogEmailNotificacaoLotes { get; set; }

    public virtual DbSet<TbLogLeilaoLote> TbLogLeilaoLotes { get; set; }

    public virtual DbSet<TbLoteNumeracao> TbLoteNumeracaos { get; set; }

    public virtual DbSet<TbLotesStatus> TbLotesStatuses { get; set; }

    public virtual DbSet<TbLotesStatusGrupo> TbLotesStatusGrupos { get; set; }

    public virtual DbSet<TbNotificacaoFatura> TbNotificacaoFaturas { get; set; }

    public virtual DbSet<TbNotificaco> TbNotificacoes { get; set; }

    public virtual DbSet<TbNotificacoesStatus> TbNotificacoesStatuses { get; set; }

    public virtual DbSet<TbNotificacoesTipo> TbNotificacoesTipos { get; set; }

    public virtual DbSet<TbPericiaStatus> TbPericiaStatuses { get; set; }

    public virtual DbSet<TbPericiaStatusVeiculo> TbPericiaStatusVeiculos { get; set; }

    public virtual DbSet<TbRegra> TbRegras { get; set; }

    public virtual DbSet<TbSeqIdentificacaoLeilaoOrgao> TbSeqIdentificacaoLeilaoOrgaos { get; set; }

    public virtual DbSet<TbUsuariosEmpresa> TbUsuariosEmpresas { get; set; }

    public virtual DbSet<ViewDadosProprietariosParaEdital> ViewDadosProprietariosParaEditals { get; set; }

    public virtual DbSet<VwDetranBaArquivoFtpLeilaoAbertura> VwDetranBaArquivoFtpLeilaoAberturas { get; set; }

    public virtual DbSet<VwDetranBaArquivoFtpLeilaoResultado> VwDetranBaArquivoFtpLeilaoResultados { get; set; }

    public virtual DbSet<VwDetranBaArquivoFtpLeilaoResultadoTerc> VwDetranBaArquivoFtpLeilaoResultadoTercs { get; set; }

    public virtual DbSet<VwDetranBaArquivoFtpLeilaoSelecao> VwDetranBaArquivoFtpLeilaoSelecaos { get; set; }

    public virtual DbSet<VwDetranBaArquivoFtpLeilaoSelecaoTerc> VwDetranBaArquivoFtpLeilaoSelecaoTercs { get; set; }

    public virtual DbSet<VwEditalNotificacao> VwEditalNotificacaos { get; set; }

    public virtual DbSet<VwEmailArrematacaoBoletosPago> VwEmailArrematacaoBoletosPagos { get; set; }

    public virtual DbSet<VwTermoFtpSelecao> VwTermoFtpSelecaos { get; set; }

    public virtual DbSet<XmlLeilaoDetranpb0120> XmlLeilaoDetranpb0120s { get; set; }

    public virtual DbSet<XmlLeilaoDetranpb0220> XmlLeilaoDetranpb0220s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=177.39.16.6;Initial Catalog=dbLeilao;User ID=sa;Password=@Studio55Webzi;MultipleActiveResultSets=True;Persist Security Info=True;Transaction Binding=Implicit Unbind;Connection Timeout=60;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<GrupoListaContato>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<ImportacaoLote>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Ano)
                .HasMaxLength(255)
                .HasColumnName("ANO");
            entity.Property(e => e.Ano1)
                .HasMaxLength(255)
                .HasColumnName("ANO1");
            entity.Property(e => e.Apreensao)
                .HasMaxLength(255)
                .HasColumnName("APREENSAO");
            entity.Property(e => e.Chassi)
                .HasMaxLength(255)
                .HasColumnName("CHASSI");
            entity.Property(e => e.Chave)
                .HasMaxLength(255)
                .HasColumnName("CHAVE");
            entity.Property(e => e.Classificacao)
                .HasMaxLength(255)
                .HasColumnName("CLASSIFICACAO");
            entity.Property(e => e.Cliente)
                .HasMaxLength(255)
                .HasColumnName("CLIENTE");
            entity.Property(e => e.Combustivel)
                .HasMaxLength(255)
                .HasColumnName("COMBUSTIVEL");
            entity.Property(e => e.Cor)
                .HasMaxLength(255)
                .HasColumnName("COR");
            entity.Property(e => e.Código)
                .HasMaxLength(255)
                .HasColumnName("CÓDIGO");
            entity.Property(e => e.Deposito)
                .HasMaxLength(255)
                .HasColumnName("DEPOSITO");
            entity.Property(e => e.F14).HasMaxLength(255);
            entity.Property(e => e.Fipe)
                .HasColumnType("money")
                .HasColumnName("FIPE");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Leilao)
                .HasMaxLength(255)
                .HasColumnName("LEILAO");
            entity.Property(e => e.Lote)
                .HasMaxLength(255)
                .HasColumnName("LOTE");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(255)
                .HasColumnName("MARCA_MODELO");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(255)
                .HasColumnName("NUMERO_MOTOR");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(255)
                .HasColumnName("OBSERVACOES");
            entity.Property(e => e.Placa)
                .HasMaxLength(255)
                .HasColumnName("PLACA");
            entity.Property(e => e.Processo)
                .HasMaxLength(255)
                .HasColumnName("PROCESSO");
            entity.Property(e => e.Renavan)
                .HasMaxLength(255)
                .HasColumnName("RENAVAN");
            entity.Property(e => e.Setor)
                .HasMaxLength(255)
                .HasColumnName("SETOR");
            entity.Property(e => e.StatusLote)
                .HasMaxLength(255)
                .HasColumnName("STATUS_LOTE");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(255)
                .HasColumnName("TIPO_VEICULO");
            entity.Property(e => e.Uf)
                .HasMaxLength(255)
                .HasColumnName("UF");
            entity.Property(e => e.Vaga)
                .HasMaxLength(255)
                .HasColumnName("VAGA");
        });

        modelBuilder.Entity<LeilaoDetranpb0120Ipva>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0120_ipva");

            entity.Property(e => e.CodigoBarrasIpva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_BARRAS_IPVA");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_MUNICIPIO");
            entity.Property(e => e.DataVencimentoIpva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_VENCIMENTO_IPVA");
            entity.Property(e => e.Exercicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXERCICIO");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
        });

        modelBuilder.Entity<LeilaoDetranpb0120Multum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0120_multa");

            entity.Property(e => e.CodigoInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_INFRACAO");
            entity.Property(e => e.DataInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_INFRACAO");
            entity.Property(e => e.DataVencimentoMulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_VENCIMENTO_MULTA");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumeroAutoInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMERO_AUTO_INFRACAO");
            entity.Property(e => e.OrgaoEmissor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ORGAO_EMISSOR");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
            entity.Property(e => e.ValorAPagarMulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VALOR_A_PAGAR_MULTA");
        });

        modelBuilder.Entity<LeilaoDetranpb0120Restricao>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0120_restricao");

            entity.Property(e => e.DescRestricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESC_RESTRICAO");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.NmfinRestricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMFIN_RESTRICAO");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumeroRegistro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMERO_REGISTRO");
            entity.Property(e => e.ObservacaoRestricao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("OBSERVACAO_RESTRICAO");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
        });

        modelBuilder.Entity<LeilaoDetranpb0120Veiculo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0120_veiculo");

            entity.Property(e => e.Anofab).HasColumnName("ANOFAB");
            entity.Property(e => e.Anomod).HasColumnName("ANOMOD");
            entity.Property(e => e.Baifin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIFIN");
            entity.Property(e => e.BairroProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIRRO_PROP");
            entity.Property(e => e.Baivend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIVEND");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Cdocor).HasColumnName("CDOCOR");
            entity.Property(e => e.Cdpatio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CDPATIO");
            entity.Property(e => e.CepProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEP_PROP");
            entity.Property(e => e.Cepfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEPFIN");
            entity.Property(e => e.Cepvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEPVEND");
            entity.Property(e => e.Cnpjfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CNPJFIN");
            entity.Property(e => e.Combust)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMBUST");
            entity.Property(e => e.Compfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPFIN");
            entity.Property(e => e.ComplProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPL_PROP");
            entity.Property(e => e.Compvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPVEND");
            entity.Property(e => e.Cor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COR");
            entity.Property(e => e.DocProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOC_PROP");
            entity.Property(e => e.Docvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOCVEND");
            entity.Property(e => e.Dtent)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DTENT");
            entity.Property(e => e.EnderProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDER_PROP");
            entity.Property(e => e.Endfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDFIN");
            entity.Property(e => e.Endvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDVEND");
            entity.Property(e => e.Especie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESPECIE");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Marcamod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MARCAMOD");
            entity.Property(e => e.Msgcor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MSGCOR");
            entity.Property(e => e.Munfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNFIN");
            entity.Property(e => e.MuniciProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNICI_PROP");
            entity.Property(e => e.Munvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNVEND");
            entity.Property(e => e.NmProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NM_PROP");
            entity.Property(e => e.Nmfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMFIN");
            entity.Property(e => e.Nmvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMVEND");
            entity.Property(e => e.Nrgrv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRGRV");
            entity.Property(e => e.Nrmotor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRMOTOR");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumerProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMER_PROP");
            entity.Property(e => e.Numfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMFIN");
            entity.Property(e => e.Numvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMVEND");
            entity.Property(e => e.Orgaoapre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ORGAOAPRE");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Qtdiaria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("QTDIARIA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
            entity.Property(e => e.Tipveiculo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPVEICULO");
            entity.Property(e => e.UfProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UF_PROP");
            entity.Property(e => e.Ufemplac)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFEMPLAC");
            entity.Property(e => e.Uffin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFFIN");
            entity.Property(e => e.Ufvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFVEND");
            entity.Property(e => e.Vldiaria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLDIARIA");
            entity.Property(e => e.Vllicenc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLLICENC");
            entity.Property(e => e.Vlmulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLMULTA");
            entity.Property(e => e.Vlreboq)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLREBOQ");
        });

        modelBuilder.Entity<LeilaoDetranpb0220Ipva>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0220_ipva");

            entity.Property(e => e.CodigoBarrasIpva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_BARRAS_IPVA");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_MUNICIPIO");
            entity.Property(e => e.DataVencimentoIpva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_VENCIMENTO_IPVA");
            entity.Property(e => e.Exercicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXERCICIO");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
        });

        modelBuilder.Entity<LeilaoDetranpb0220Multum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0220_multa");

            entity.Property(e => e.CodigoInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGO_INFRACAO");
            entity.Property(e => e.DataInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_INFRACAO");
            entity.Property(e => e.DataVencimentoMulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATA_VENCIMENTO_MULTA");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumeroAutoInfracao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMERO_AUTO_INFRACAO");
            entity.Property(e => e.OrgaoEmissor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ORGAO_EMISSOR");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
            entity.Property(e => e.ValorAPagarMulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VALOR_A_PAGAR_MULTA");
        });

        modelBuilder.Entity<LeilaoDetranpb0220Restricao>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0220_restricao");

            entity.Property(e => e.DescRestricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESC_RESTRICAO");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.NmfinRestricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMFIN_RESTRICAO");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumeroRegistro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMERO_REGISTRO");
            entity.Property(e => e.ObservacaoRestricao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("OBSERVACAO_RESTRICAO");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
        });

        modelBuilder.Entity<LeilaoDetranpb0220Veiculo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leilao_DETRANPB0220_veiculo");

            entity.Property(e => e.Anofab).HasColumnName("ANOFAB");
            entity.Property(e => e.Anomod).HasColumnName("ANOMOD");
            entity.Property(e => e.Baifin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIFIN");
            entity.Property(e => e.BairroProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIRRO_PROP");
            entity.Property(e => e.Baivend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAIVEND");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Cdocor).HasColumnName("CDOCOR");
            entity.Property(e => e.Cdpatio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CDPATIO");
            entity.Property(e => e.CepProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEP_PROP");
            entity.Property(e => e.Cepfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEPFIN");
            entity.Property(e => e.Cepvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CEPVEND");
            entity.Property(e => e.Cnpjfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CNPJFIN");
            entity.Property(e => e.Combust)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMBUST");
            entity.Property(e => e.Compfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPFIN");
            entity.Property(e => e.ComplProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPL_PROP");
            entity.Property(e => e.Compvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMPVEND");
            entity.Property(e => e.Cor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COR");
            entity.Property(e => e.DocProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOC_PROP");
            entity.Property(e => e.Docvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOCVEND");
            entity.Property(e => e.Dtent)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DTENT");
            entity.Property(e => e.EnderProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDER_PROP");
            entity.Property(e => e.Endfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDFIN");
            entity.Property(e => e.Endvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDVEND");
            entity.Property(e => e.Especie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESPECIE");
            entity.Property(e => e.Gravame)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Marcamod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MARCAMOD");
            entity.Property(e => e.Msgcor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MSGCOR");
            entity.Property(e => e.Munfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNFIN");
            entity.Property(e => e.MuniciProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNICI_PROP");
            entity.Property(e => e.Munvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNVEND");
            entity.Property(e => e.NmProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NM_PROP");
            entity.Property(e => e.Nmfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMFIN");
            entity.Property(e => e.Nmvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NMVEND");
            entity.Property(e => e.Nrgrv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRGRV");
            entity.Property(e => e.Nrmotor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRMOTOR");
            entity.Property(e => e.Nrvin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NRVIN");
            entity.Property(e => e.NumerProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMER_PROP");
            entity.Property(e => e.Numfin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMFIN");
            entity.Property(e => e.Numvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMVEND");
            entity.Property(e => e.Orgaoapre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ORGAOAPRE");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Qtdiaria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("QTDIARIA");
            entity.Property(e => e.Renavam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RENAVAM");
            entity.Property(e => e.Sequencial).HasColumnName("SEQUENCIAL");
            entity.Property(e => e.Tipveiculo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPVEICULO");
            entity.Property(e => e.UfProp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UF_PROP");
            entity.Property(e => e.Ufemplac)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFEMPLAC");
            entity.Property(e => e.Uffin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFFIN");
            entity.Property(e => e.Ufvend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UFVEND");
            entity.Property(e => e.Vldiaria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLDIARIA");
            entity.Property(e => e.Vllicenc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLLICENC");
            entity.Property(e => e.Vlmulta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLMULTA");
            entity.Property(e => e.Vlreboq)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VLREBOQ");
        });

        modelBuilder.Entity<ListaContato>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.Fone1)
                .IsUnicode(false)
                .HasColumnName("fone_1");
            entity.Property(e => e.Fone2)
                .IsUnicode(false)
                .HasColumnName("fone_2");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdGrupoListaContatos).HasColumnName("idGrupoListaContatos");
            entity.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.PrimeiroNome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("primeiro_nome");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf");
            entity.Property(e => e.UltimoNome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ultimo_nome");
        });

        modelBuilder.Entity<LotesImportar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LotesImportar");

            entity.Property(e => e.Placa).HasMaxLength(255);
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.Model).IsRequired();
            entity.Property(e => e.ProductVersion)
                .IsRequired()
                .HasMaxLength(32);
        });

        modelBuilder.Entity<TbArrematantesFatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_arrem__3213E83FD470CC27");

            entity.ToTable("tb_arrematantes_fatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.DataCadastro)
                .HasColumnType("datetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.DataExpiracao)
                .HasColumnType("datetime")
                .HasColumnName("data_expiracao");
            entity.Property(e => e.DataPagamento)
                .HasColumnType("datetime")
                .HasColumnName("data_pagamento");
            entity.Property(e => e.DataVencimento)
                .HasColumnType("datetime")
                .HasColumnName("data_vencimento");
            entity.Property(e => e.IdConfiguracaoConta).HasColumnName("id_configuracao_conta");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdFormaPagamento).HasColumnName("id_forma_pagamento");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.JurosPerc)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("juros_perc");
            entity.Property(e => e.MultaPerc)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("multa_perc");
            entity.Property(e => e.Token)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.ValorDesconto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_desconto");
            entity.Property(e => e.ValorJurosPago)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_juros_pago");
            entity.Property(e => e.ValorMultaPago)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_multa_pago");
            entity.Property(e => e.ValorTaxaPaga)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_taxa_paga");
            entity.Property(e => e.ValorTotalPago)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_total_pago");

            entity.HasOne(d => d.IdFormaPagamentoNavigation).WithMany(p => p.TbArrematantesFaturas)
                .HasForeignKey(d => d.IdFormaPagamento)
                .HasConstraintName("FK__tb_arrema__id_fo__3D9E16F4");

            entity.HasOne(d => d.IdLeilaoNavigation).WithMany(p => p.TbArrematantesFaturas)
                .HasForeignKey(d => d.IdLeilao)
                .HasConstraintName("FK__tb_arrema__id_le__3F865F66");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.TbArrematantesFaturas)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK__tb_arrema__id_st__3E923B2D");
        });

        modelBuilder.Entity<TbArrematantesFaturaItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_arrem__3213E83F95FC6161");

            entity.ToTable("tb_arrematantes_fatura_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.IdArrematantes).HasColumnName("id_arrematantes");
            entity.Property(e => e.IdFatura).HasColumnName("id_fatura");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.ValorUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_unitario");

            entity.HasOne(d => d.IdFaturaNavigation).WithMany(p => p.TbArrematantesFaturaItems)
                .HasForeignKey(d => d.IdFatura)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tb_arrema__id_fa__4727812E");
        });

        modelBuilder.Entity<TbComitente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_comitentes");

            entity.ToTable("tb_comitentes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrato)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrato");
            entity.Property(e => e.Cor)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('NA')")
                .HasColumnName("cor");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.Iss)
                .HasDefaultValueSql("('0')")
                .HasColumnName("iss");
            entity.Property(e => e.MonitorTransacao)
                .HasDefaultValueSql("((0))")
                .HasColumnName("monitor_transacao");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.TipoImportacao).HasColumnName("tipo_importacao");

            entity.HasOne(d => d.TipoImportacaoNavigation).WithMany(p => p.TbComitentes)
                .HasForeignKey(d => d.TipoImportacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_comitentes_tb_comitentes_tipo_importacao");
        });

        modelBuilder.Entity<TbComitentesRegra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_comitentes_regras");

            entity.ToTable("tb_comitentes_regras");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataVigenciaFinal)
                .HasColumnType("date")
                .HasColumnName("data_vigencia_final");
            entity.Property(e => e.IdComitente).HasColumnName("id_comitente");
            entity.Property(e => e.IdRegra).HasColumnName("id_regra");
            entity.Property(e => e.Valor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdComitenteNavigation).WithMany(p => p.TbComitentesRegras)
                .HasForeignKey(d => d.IdComitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comitentes_regras_comitentes");

            entity.HasOne(d => d.IdRegraNavigation).WithMany(p => p.TbComitentesRegras)
                .HasForeignKey(d => d.IdRegra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comitentes_regras_regras");
        });

        modelBuilder.Entity<TbComitentesTaxa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_comitentes_taxas_1");

            entity.ToTable("tb_comitentes_taxas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.IdComitente).HasColumnName("id_comitente");
            entity.Property(e => e.Valor)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdComitenteNavigation).WithMany(p => p.TbComitentesTaxas)
                .HasForeignKey(d => d.IdComitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_comitentes_taxas_tb_comitentes");
        });

        modelBuilder.Entity<TbComitentesTipoImportacao>(entity =>
        {
            entity.ToTable("tb_comitentes_tipo_importacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbComitentesTransacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_comit__3213E83F4979DDF4");

            entity.ToTable("tb_comitentes_transacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdComitente).HasColumnName("id_comitente");
            entity.Property(e => e.IdTipoTransacao).HasColumnName("id_tipo_transacao");
            entity.Property(e => e.OrdemExecucao).HasColumnName("ordem_execucao");
            entity.Property(e => e.ProximaTransacao).HasColumnName("proxima_transacao");

            entity.HasOne(d => d.IdTipoTransacaoNavigation).WithMany(p => p.TbComitentesTransacaos)
                .HasForeignKey(d => d.IdTipoTransacao)
                .HasConstraintName("FK_comitentes_transacao_tipo_trancao");
        });

        modelBuilder.Entity<TbDetranBaFtpCodigoRetorno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_detran_ba_ftp_codigo_retorno");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbDetranBaFtpEnvio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_detran_ba_ftp_envio");

            entity.Property(e => e.Arquivo).HasColumnName("arquivo");
            entity.Property(e => e.CodigoOrgaoSolic).HasColumnName("codigo_orgao_solic");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.DataGeracaoArquivo)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_geracao_arquivo");
            entity.Property(e => e.ExtensaoArquivo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("extensao_arquivo");
            entity.Property(e => e.FlagLeilaoTerceiros)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_leilaoTerceiros");
            entity.Property(e => e.FlagRetornado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_retornado");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.NomeArquivo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_arquivo");
            entity.Property(e => e.QuantRegDetalhe).HasColumnName("quant_reg_detalhe");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.TamanhoArquivo).HasColumnName("tamanho_arquivo");
            entity.Property(e => e.TipoArquivo).HasColumnName("tipo_arquivo");
            entity.Property(e => e.TipoArquivoDescricao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo_arquivo_descricao");
        });

        modelBuilder.Entity<TbDetranBaFtpEnvioDetalhe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_detran_ba_ftp_envio_detalhe");

            entity.Property(e => e.CepArrematante).HasColumnName("cep_arrematante");
            entity.Property(e => e.Chassi)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Classificacao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("classificacao");
            entity.Property(e => e.CnpjCpfArrematante).HasColumnName("cnpj_cpf_arrematante");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.ComplemEnderecoArrematante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("complem_endereco_arrematante");
            entity.Property(e => e.DataApreensao).HasColumnName("data_apreensao");
            entity.Property(e => e.DataEditalLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_liberacao");
            entity.Property(e => e.DataEditalNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_notificacao");
            entity.Property(e => e.DataEncerramento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_encerramento");
            entity.Property(e => e.DataExecucao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_execucao");
            entity.Property(e => e.DataLimiteRetirada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_limite_retirada");
            entity.Property(e => e.DataNotaFiscal).HasColumnName("data_nota_fiscal");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_notificacao");
            entity.Property(e => e.EmailArrematante)
                .IsUnicode(false)
                .HasColumnName("email_arrematante");
            entity.Property(e => e.EnderecoArrematante)
                .IsUnicode(false)
                .HasColumnName("endereco_arrematante");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdFtpEnvioDetalhe)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_ftp_envio_detalhe");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLeilaoLote).HasColumnName("id_leilao_lote");
            entity.Property(e => e.MunicipioArrematante)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("municipio_arrematante");
            entity.Property(e => e.NomeArrematante)
                .IsUnicode(false)
                .HasColumnName("nome_arrematante");
            entity.Property(e => e.NumeroEnderecoArrematante)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_endereco_arrematante");
            entity.Property(e => e.NumeroNotaFiscal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_nota_fiscal");
            entity.Property(e => e.NumeroTermo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numero_termo");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.TipoAtualizacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_atualizacao");
            entity.Property(e => e.TipoDocArrematante).HasColumnName("tipo_doc_arrematante");
            entity.Property(e => e.UfArrematante)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf_arrematante");
            entity.Property(e => e.ValorEstimado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_estimado");
            entity.Property(e => e.ValorVenda)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_venda");
        });

        modelBuilder.Entity<TbDetranBaFtpRetorno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_detran_ba_ftp_retorno");

            entity.Property(e => e.Arquivo).HasColumnName("arquivo");
            entity.Property(e => e.CodigoOrgaoSolic).HasColumnName("codigo_orgao_solic");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.CodigoRetorno).HasColumnName("codigo_retorno");
            entity.Property(e => e.DataGeracaoArquivo).HasColumnName("data_geracao_arquivo");
            entity.Property(e => e.ExtensaoArquivo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("extensao_arquivo");
            entity.Property(e => e.HoraGeracaoArquivo).HasColumnName("hora_geracao_arquivo");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdFtpRetorno)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_ftp_retorno");
            entity.Property(e => e.NomeArquivo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_arquivo");
            entity.Property(e => e.QuantRegDetalhe).HasColumnName("quant_reg_detalhe");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.TamanhoArquivo).HasColumnName("tamanho_arquivo");
            entity.Property(e => e.TipoArquivo).HasColumnName("tipo_arquivo");
            entity.Property(e => e.TipoArquivoDescricao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo_arquivo_descricao");
            entity.Property(e => e.TipoRegistro).HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<TbDetranBaFtpRetornoDetalhe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_detran_ba_ftp_retorno_detalhe");

            entity.Property(e => e.CepArrematante).HasColumnName("cep_arrematante");
            entity.Property(e => e.Chassi)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Classificacao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("classificacao");
            entity.Property(e => e.CnpjCpfArrematante)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnpj_cpf_arrematante");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.CodigoRetorno).HasColumnName("codigo_retorno");
            entity.Property(e => e.ComplemEnderecoArrematante)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("complem_endereco_arrematante");
            entity.Property(e => e.DataApreensao).HasColumnName("data_apreensao");
            entity.Property(e => e.DataEditalLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_liberacao");
            entity.Property(e => e.DataEditalNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_notificacao");
            entity.Property(e => e.DataEncerramento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_encerramento");
            entity.Property(e => e.DataExecucao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_execucao");
            entity.Property(e => e.DataLimiteRetirada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_limite_retirada");
            entity.Property(e => e.DataNotaFiscal).HasColumnName("data_nota_fiscal");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_notificacao");
            entity.Property(e => e.EmailArrematante)
                .IsUnicode(false)
                .HasColumnName("email_arrematante");
            entity.Property(e => e.EnderecoArrematante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("endereco_arrematante");
            entity.Property(e => e.IdFtpRetorno).HasColumnName("id_ftp_retorno");
            entity.Property(e => e.IdFtpRetornoDetalhe)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_ftp_retorno_detalhe");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.MunicipioArrematante)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("municipio_arrematante");
            entity.Property(e => e.NomeArrematante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_arrematante");
            entity.Property(e => e.NumeroEnderecoArrematante)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("numero_endereco_arrematante");
            entity.Property(e => e.NumeroNotaFiscal).HasColumnName("numero_nota_fiscal");
            entity.Property(e => e.NumeroTermo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_termo");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.TipoAtualizacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_atualizacao");
            entity.Property(e => e.TipoDocArrematante).HasColumnName("tipo_doc_arrematante");
            entity.Property(e => e.UfArrematante)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf_arrematante");
            entity.Property(e => e.ValorEstimado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_estimado");
            entity.Property(e => e.ValorVenda)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_venda");
        });

        modelBuilder.Entity<TbExpositore>(entity =>
        {
            entity.ToTable("tb_expositores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.EmailNotificacao)
                .IsUnicode(false)
                .HasColumnName("email_notificacao");
        });

        modelBuilder.Entity<TbFaturaComposicaoCobranca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_fatur__3213E83F256EB227");

            entity.ToTable("tb_fatura_composicao_cobranca");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdFatura).HasColumnName("id_fatura");
            entity.Property(e => e.IdFormaPagamento).HasColumnName("id_forma_pagamento");
            entity.Property(e => e.IdReferencia)
                .IsUnicode(false)
                .HasColumnName("id_referencia");
            entity.Property(e => e.Qrcode)
                .IsUnicode(false)
                .HasColumnName("qrcode");
            entity.Property(e => e.Qrstring)
                .IsUnicode(false)
                .HasColumnName("qrstring");
            entity.Property(e => e.Valor)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");
            entity.Property(e => e.ValorTaxa)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_taxa");

            entity.HasOne(d => d.IdFaturaNavigation).WithMany(p => p.TbFaturaComposicaoCobrancas)
                .HasForeignKey(d => d.IdFatura)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tb_fatura__id_fa__51A50FA1");

            entity.HasOne(d => d.IdFormaPagamentoNavigation).WithMany(p => p.TbFaturaComposicaoCobrancas)
                .HasForeignKey(d => d.IdFormaPagamento)
                .HasConstraintName("FK__tb_fatura__id_fo__50B0EB68");
        });

        modelBuilder.Entity<TbFaturaFormaPagamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_fatur__3213E83FAB95218C");

            entity.ToTable("tb_fatura_forma_pagamento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbFaturaStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_fatur__3213E83F25008D78");

            entity.ToTable("tb_fatura_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbFinanceira>(entity =>
        {
            entity.ToTable("tb_financeiras");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cnpj");
            entity.Property(e => e.Complemento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Ddd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ddd");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Endereco)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.Fone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fone");
            entity.Property(e => e.Municipio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Segmento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("segmento");
            entity.Property(e => e.Site)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("site");
            entity.Property(e => e.Uf)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("uf");
        });

        modelBuilder.Entity<TbImportacaoTdi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_impor__3213E83F0FF5D190");

            entity.ToTable("tb_importacao_tdi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("datetime")
                .HasColumnName("data");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.JsonRetorno).HasColumnName("json_retorno");
            entity.Property(e => e.QtdDados).HasColumnName("qtd_dados");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .HasColumnName("senha");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbLeiaoLotesServico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leiao__3213E83F1A89E4E1");

            entity.ToTable("tb_leiao_lotes_servicos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Ordem).HasColumnName("ordem");
            entity.Property(e => e.Valor)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<TbLeiaoPrestacaoConta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leiao__3213E83F0777106D");

            entity.ToTable("tb_leiao_prestacao_contas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoeficienteLeilao)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("coeficiente_leilao");
            entity.Property(e => e.DiasPatio)
                .HasDefaultValueSql("((0))")
                .HasColumnName("dias_patio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.ValorArrematacao)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_arrematacao");
        });

        modelBuilder.Entity<TbLeiaoPrestacaoContasIten>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leiao__3213E83F0E240DFC");

            entity.ToTable("tb_leiao_prestacao_contas_itens");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Devido)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("devido");
            entity.Property(e => e.Grupo).HasColumnName("grupo");
            entity.Property(e => e.IdPrestacaoContas).HasColumnName("id_prestacao_contas");
            entity.Property(e => e.NaoPago)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("nao_pago");
            entity.Property(e => e.Pago)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("pago");
            entity.Property(e => e.Saldo)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldo");
        });

        modelBuilder.Entity<TbLeilao>(entity =>
        {
            entity.ToTable("tb_leilao", tb =>
                {
                    tb.HasTrigger("tb_leilao_delete");
                    tb.HasTrigger("tb_leilao_insert");
                    tb.HasTrigger("tb_leilao_insert_identificacao_orgao");
                    tb.HasTrigger("tb_leilao_update");
                });

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.DataAgendamento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_agendamento");
            entity.Property(e => e.DataEditalLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_liberacao");
            entity.Property(e => e.DataEncerramento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_encerramento");
            entity.Property(e => e.DataFimRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_fim_retirada");
            entity.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.DataHoraPublicacaoDiarioOficial)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_hora_publicacao_diario_oficial");
            entity.Property(e => e.DataInicioRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_inicio_retirada");
            entity.Property(e => e.DataLeilao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_leilao");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_notificacao");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.EmailNotificacao)
                .IsUnicode(false)
                .HasColumnName("email_notificacao");
            entity.Property(e => e.EndComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("end_complemento");
            entity.Property(e => e.EndNumero)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("end_numero");
            entity.Property(e => e.Endereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.HoraLeilao)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora_leilao");
            entity.Property(e => e.IdComitente).HasColumnName("id_comitente");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdExpositor).HasColumnName("id_expositor");
            entity.Property(e => e.IdLeiloeiro).HasColumnName("id_leiloeiro");
            entity.Property(e => e.IdRegraPrestacaoContas).HasColumnName("id_regra_prestacao_contas");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.IdentificacaoLeilaoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacao_leilao_orgao");
            entity.Property(e => e.LeilaoDsin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("leilao_dsin");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_local");
            entity.Property(e => e.NumeroDiarioOficial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_diario_oficial");
            
            entity.Property(e => e.OrdemInternaMatriz)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ordem_interna_matriz");
            entity.Property(e => e.Uf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uf");

            entity.HasOne(d => d.IdComitenteNavigation).WithMany(p => p.TbLeilaos)
                .HasForeignKey(d => d.IdComitente)
                .HasConstraintName("FK_tb_leilao_tb_comitentes");

            entity.HasOne(d => d.IdLeiloeiroNavigation).WithMany(p => p.TbLeilaos)
                .HasForeignKey(d => d.IdLeiloeiro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_leilao_tb_leiloeiros");

            entity.HasOne(d => d.IdRegraPrestacaoContasNavigation).WithMany(p => p.TbLeilaos)
                .HasForeignKey(d => d.IdRegraPrestacaoContas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_leilao_tb_leilao_regras_prestacao_contas");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.TbLeilaos)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_leilao_tb_leilao_status");
        });

        modelBuilder.Entity<TbLeilaoAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_audit");

            entity.Property(e => e.AuditDataState)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDataState");
            entity.Property(e => e.AuditDateTime)
                .HasColumnType("datetime")
                .HasColumnName("auditDateTime");
            entity.Property(e => e.AuditDmlaction)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDMLAction");
            entity.Property(e => e.AuditUser)
                .HasMaxLength(128)
                .HasColumnName("auditUser");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.DataAgendamento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_agendamento");
            entity.Property(e => e.DataEditalLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_liberacao");
            entity.Property(e => e.DataEncerramento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_encerramento");
            entity.Property(e => e.DataFimRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_fim_retirada");
            entity.Property(e => e.DataHoraCadastro)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.DataHoraPublicacaoDiarioOficial)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_hora_publicacao_diario_oficial");
            entity.Property(e => e.DataInicioRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_inicio_retirada");
            entity.Property(e => e.DataLeilao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_leilao");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_notificacao");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.EmailNotificacao)
                .IsUnicode(false)
                .HasColumnName("email_notificacao");
            entity.Property(e => e.EndComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("end_complemento");
            entity.Property(e => e.EndNumero)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("end_numero");
            entity.Property(e => e.Endereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.HoraLeilao)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora_leilao");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdComitente).HasColumnName("id_comitente");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdExpositor).HasColumnName("id_expositor");
            entity.Property(e => e.IdLeiloeiro).HasColumnName("id_leiloeiro");
            entity.Property(e => e.IdRegraPrestacaoContas).HasColumnName("id_regra_prestacao_contas");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.IdentificacaoLeilaoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacao_leilao_orgao");
            entity.Property(e => e.LeilaoDsin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("leilao_dsin");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_local");
            entity.Property(e => e.NumeroDiarioOficial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_diario_oficial");

            entity.Property(e => e.OrdemInternaMatriz)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ordem_interna_matriz");
            entity.Property(e => e.Uf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uf");
            entity.Property(e => e.UpdateColumns)
                .IsUnicode(false)
                .HasColumnName("updateColumns");
        });

        modelBuilder.Entity<TbLeilaoDespesa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_despesas");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdDespesa).HasColumnName("id_despesa");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<TbLeilaoEditai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_leilao_edital");

            entity.ToTable("tb_leilao_editais");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataGeracao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_geracao");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdUsuarioGeracao).HasColumnName("id_usuario_geracao");

            entity.HasOne(d => d.IdLeilaoNavigation).WithMany(p => p.TbLeilaoEditais)
                .HasForeignKey(d => d.IdLeilao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_leilao_edital_tb_leilao");
        });

        modelBuilder.Entity<TbLeilaoImportacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leila__3213E83FB85E2797");

            entity.ToTable("tb_leilao_importacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arquivo)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.DataImportacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_importacao");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Obs)
                .IsUnicode(false)
                .HasColumnName("obs");
            entity.Property(e => e.QtdErro).HasColumnName("qtd_erro");
            entity.Property(e => e.QtdItens).HasColumnName("qtd_itens");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TbLeilaoImportacaoResultado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leila__3213E83F1E90310A");

            entity.ToTable("tb_leilao_importacao_resultado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coluna).HasColumnName("coluna");
            entity.Property(e => e.IdImportacao).HasColumnName("id_importacao");
            entity.Property(e => e.Linha).HasColumnName("linha");
            entity.Property(e => e.MsgErro)
                .IsUnicode(false)
                .HasColumnName("msg_erro");
        });

        modelBuilder.Entity<TbLeilaoLote>(entity =>
        {
            entity.ToTable("tb_leilao_lotes", tb =>
                {
                    tb.HasTrigger("tb_leilao_lotes_delete");
                    tb.HasTrigger("tb_leilao_lotes_insert");
                    tb.HasTrigger("tb_leilao_lotes_update");
                    tb.HasTrigger("tr_log_upd_leilao_grv");
                    tb.HasTrigger("tr_log_upd_leilao_lotes");
                    tb.HasTrigger("tr_log_upd_leilao_processo_placa_chassi");
                });

            entity.HasIndex(e => e.IdGrv, "idx_tb_leilao_lotes1");

            entity.HasIndex(e => e.NumeroFormularioGrv, "idx_tb_leilao_lotes2");

            entity.HasIndex(e => e.IdLeilao, "idx_tb_leilao_lotes3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            entity.Property(e => e.ArCondicionado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ar_condicionado");
            entity.Property(e => e.Cambio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cambio");
            entity.Property(e => e.Chassi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Chave)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .HasColumnName("chave");
            entity.Property(e => e.ConferidoPatio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("conferido_patio");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.CorOstentada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor_ostentada");
            entity.Property(e => e.DataHoraAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_alteracao");
            entity.Property(e => e.DataHoraApreensao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_apreensao");
            entity.Property(e => e.DataHoraEntrada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_entrada");
            entity.Property(e => e.DataHoraInsercao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_insercao");
            entity.Property(e => e.DataHoraLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_liberacao");
            entity.Property(e => e.Direcao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direcao");
            entity.Property(e => e.FlagAgendado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_agendado");
            entity.Property(e => e.FlagAnaliseSobra)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_analise_sobra");
            entity.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_normalizado");
            entity.Property(e => e.FlagTransacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength()
                .HasColumnName("flag_transacao");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdStatusLote).HasColumnName("id_status_lote");
            entity.Property(e => e.IdStatusOperacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id_status_operacao");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioInclusao).HasColumnName("id_usuario_inclusao");
            entity.Property(e => e.InformacaoRoubo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            entity.Property(e => e.LanceMinimo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("lance_minimo");
            entity.Property(e => e.Localizacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localizacao");
            entity.Property(e => e.LogRecolhimento).HasColumnName("log_recolhimento");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca_modelo");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NumeroFormularioGrv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");
            entity.Property(e => e.NumeroItemLote)
                .HasDefaultValueSql("((0))")
                .HasColumnName("numero_item_lote");
            entity.Property(e => e.NumeroLote).HasColumnName("numero_lote");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            entity.Property(e => e.ObsTransacao)
                .IsUnicode(false)
                .HasColumnName("obs_transacao");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            entity.Property(e => e.Patio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patio");
            entity.Property(e => e.Periciado)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periciado");
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.PlacaOstentada)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa_ostentada");
            entity.Property(e => e.ProcedenciaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("procedencia_veiculo");
            entity.Property(e => e.Quilometragem).HasColumnName("quilometragem");
            entity.Property(e => e.Reboque)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reboque");
            entity.Property(e => e.Renavan)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("renavan");
            entity.Property(e => e.ResponsavelRemocao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_remocao");
            entity.Property(e => e.RestricaoEstelionato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao_estelionato");
            entity.Property(e => e.SituacaoChassi)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_chassi");
            entity.Property(e => e.SituacaoGnv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_gnv");
            entity.Property(e => e.SituacaoLote)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_lote");
            entity.Property(e => e.SituacaoPlaca)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_placa");
            entity.Property(e => e.SituacaoVeiculo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_veiculo");
            entity.Property(e => e.SituacaoVeiculoPericia).HasColumnName("situacao_veiculo_pericia");
            entity.Property(e => e.StatusPericia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_pericia");
            entity.Property(e => e.TipoCombustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_combustivel");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculo");
            entity.Property(e => e.TravaEletrica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trava_eletrica");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf");
            entity.Property(e => e.ValorAvaliacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("valor_avaliacao");
            entity.Property(e => e.VidroEletrico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vidro_eletrico");

            entity.HasOne(d => d.IdLeilaoNavigation).WithMany(p => p.TbLeilaoLotes)
                .HasForeignKey(d => d.IdLeilao)
                .HasConstraintName("FK_tb_leilao_lotes_tb_leilao");

            entity.HasOne(d => d.IdStatusLoteNavigation).WithMany(p => p.TbLeilaoLotes)
                .HasForeignKey(d => d.IdStatusLote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_leilao_lotes_tb_lotes_status");
        });

        modelBuilder.Entity<TbLeilaoLotesArrematante>(entity =>
        {
            entity.ToTable("tb_leilao_lotes_arrematantes", tb =>
                {
                    tb.HasTrigger("tb_leilao_lotes_arrematantes_delete");
                    tb.HasTrigger("tb_leilao_lotes_arrematantes_insert");
                    tb.HasTrigger("tb_leilao_lotes_arrematantes_update");
                });

            entity.HasIndex(e => e.NumeroProcesso, "IX_tb_leilao_lotes_arrematantes");

            entity.HasIndex(e => e.IdLote, "idx_id_lote");

            entity.HasIndex(e => e.IdGrv, "idx_tb_leilao_lotes_arrematantes1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arrematacao)
                .IsUnicode(false)
                .HasColumnName("arrematacao");
            entity.Property(e => e.Avaliacao)
                .IsUnicode(false)
                .HasColumnName("avaliacao");
            entity.Property(e => e.Bairro)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cartela)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("cartela");
            entity.Property(e => e.Cep)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Chassi)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Cidade)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Cnpj)
                .IsUnicode(false)
                .HasColumnName("cnpj");
            entity.Property(e => e.CodigoSapBanco)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_sap_banco");
            entity.Property(e => e.Comissao)
                .IsUnicode(false)
                .HasColumnName("comissao");
            entity.Property(e => e.Complemento)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .IsUnicode(false)
                .HasColumnName("cpf");
            entity.Property(e => e.DataEmissaoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_emissao_boleto");
            entity.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.DataNotaFiscal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_nota_fiscal");
            entity.Property(e => e.DataPagamentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_pagamento_boleto");
            entity.Property(e => e.DataVencimentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_vencimento_boleto");
            entity.Property(e => e.Descontos)
                .IsUnicode(false)
                .HasColumnName("descontos");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FlgConfirmado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flg_confirmado");
            entity.Property(e => e.Fone1)
                .IsUnicode(false)
                .HasColumnName("fone_1");
            entity.Property(e => e.Fone2)
                .IsUnicode(false)
                .HasColumnName("fone_2");
            entity.Property(e => e.IdDocumentoClienteSap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_documento_cliente_sap");
            entity.Property(e => e.IdDocumentoFb70Sap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_documento_fb70_sap");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Iss)
                .IsUnicode(false)
                .HasColumnName("iss");
            entity.Property(e => e.Leilao)
                .IsUnicode(false)
                .HasColumnName("leilao");
            entity.Property(e => e.LinhaDigitavel)
                .IsUnicode(false)
                .HasColumnName("linha_digitavel");
            entity.Property(e => e.Logradouro)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Lote)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.MsgErroFb70)
                .IsUnicode(false)
                .HasColumnName("msg_erro_fb_70");
            entity.Property(e => e.NomeArrematante)
                .IsUnicode(false)
                .HasColumnName("nome_arrematante");
            entity.Property(e => e.NotaArrematacao)
                .IsUnicode(false)
                .HasColumnName("nota_arrematacao");
            entity.Property(e => e.Numero)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.NumeroBoleto)
                .IsUnicode(false)
                .HasColumnName("numero_boleto");
            entity.Property(e => e.NumeroProcesso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_processo");
            entity.Property(e => e.OutrasTaxas)
                .IsUnicode(false)
                .HasColumnName("outras_taxas");
            entity.Property(e => e.Placa)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.StatusCadastroClienteSap)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status_cadastro_cliente_sap");
            entity.Property(e => e.StatusCadastroFb70Sap)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status_cadastro_fb70_sap");
            entity.Property(e => e.TarifaBancaria)
                .IsUnicode(false)
                .HasColumnName("tarifa_bancaria");
            entity.Property(e => e.TaxaAdministrativa)
                .IsUnicode(false)
                .HasColumnName("taxa_administrativa");
            entity.Property(e => e.ValorPago)
                .IsUnicode(false)
                .HasColumnName("valor_pago");
            entity.Property(e => e.ValorTotal)
                .IsUnicode(false)
                .HasColumnName("valor_total");
        });

        modelBuilder.Entity<TbLeilaoLotesArrematantesAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_lotes_arrematantes_audit");

            entity.Property(e => e.Arrematacao)
                .IsUnicode(false)
                .HasColumnName("arrematacao");
            entity.Property(e => e.AuditDataState)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDataState");
            entity.Property(e => e.AuditDateTime)
                .HasColumnType("datetime")
                .HasColumnName("auditDateTime");
            entity.Property(e => e.AuditDmlaction)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDMLAction");
            entity.Property(e => e.AuditUser)
                .HasMaxLength(128)
                .HasColumnName("auditUser");
            entity.Property(e => e.Avaliacao)
                .IsUnicode(false)
                .HasColumnName("avaliacao");
            entity.Property(e => e.Bairro)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cartela)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("cartela");
            entity.Property(e => e.Cep)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Chassi)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Cidade)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Cnpj)
                .IsUnicode(false)
                .HasColumnName("cnpj");
            entity.Property(e => e.CodigoSapBanco)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_sap_banco");
            entity.Property(e => e.Comissao)
                .IsUnicode(false)
                .HasColumnName("comissao");
            entity.Property(e => e.Complemento)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .IsUnicode(false)
                .HasColumnName("cpf");
            entity.Property(e => e.DataEmissaoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_emissao_boleto");
            entity.Property(e => e.DataHoraCadastro)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.DataNotaFiscal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_nota_fiscal");
            entity.Property(e => e.DataPagamentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_pagamento_boleto");
            entity.Property(e => e.DataVencimentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_vencimento_boleto");
            entity.Property(e => e.Descontos)
                .IsUnicode(false)
                .HasColumnName("descontos");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FlgConfirmado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flg_confirmado");
            entity.Property(e => e.Fone1)
                .IsUnicode(false)
                .HasColumnName("fone_1");
            entity.Property(e => e.Fone2)
                .IsUnicode(false)
                .HasColumnName("fone_2");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDocumentoClienteSap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_documento_cliente_sap");
            entity.Property(e => e.IdDocumentoFb70Sap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_documento_fb70_sap");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Iss)
                .IsUnicode(false)
                .HasColumnName("iss");
            entity.Property(e => e.Leilao)
                .IsUnicode(false)
                .HasColumnName("leilao");
            entity.Property(e => e.LinhaDigitavel)
                .IsUnicode(false)
                .HasColumnName("linha_digitavel");
            entity.Property(e => e.Logradouro)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Lote)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.MsgErroFb70)
                .IsUnicode(false)
                .HasColumnName("msg_erro_fb_70");
            entity.Property(e => e.NomeArrematante)
                .IsUnicode(false)
                .HasColumnName("nome_arrematante");
            entity.Property(e => e.NotaArrematacao).HasColumnName("nota_arrematacao");
            entity.Property(e => e.Numero)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.NumeroBoleto)
                .IsUnicode(false)
                .HasColumnName("numero_boleto");
            entity.Property(e => e.NumeroProcesso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_processo");
            entity.Property(e => e.OutrasTaxas)
                .IsUnicode(false)
                .HasColumnName("outras_taxas");
            entity.Property(e => e.Placa)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.StatusCadastroClienteSap)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status_cadastro_cliente_sap");
            entity.Property(e => e.StatusCadastroFb70Sap)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status_cadastro_fb70_sap");
            entity.Property(e => e.TarifaBancaria)
                .IsUnicode(false)
                .HasColumnName("tarifa_bancaria");
            entity.Property(e => e.TaxaAdministrativa)
                .IsUnicode(false)
                .HasColumnName("taxa_administrativa");
            entity.Property(e => e.UpdateColumns)
                .IsUnicode(false)
                .HasColumnName("updateColumns");
            entity.Property(e => e.ValorPago)
                .IsUnicode(false)
                .HasColumnName("valor_pago");
            entity.Property(e => e.ValorTotal)
                .IsUnicode(false)
                .HasColumnName("valor_total");
        });

        modelBuilder.Entity<TbLeilaoLotesArrematantesImportacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leila__3213E83F62458BBE");

            entity.ToTable("tb_leilao_lotes_arrematantes_importacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arquivo)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.DataImportacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_importacao");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.QtdErro).HasColumnName("qtd_erro");
            entity.Property(e => e.QtdItens).HasColumnName("qtd_itens");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TbLeilaoLotesArrematantesImportacaoIten>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_lotes_arrematantes_importacao_itens");

            entity.Property(e => e.Arrematacao)
                .IsUnicode(false)
                .HasColumnName("arrematacao");
            entity.Property(e => e.Avaliacao)
                .IsUnicode(false)
                .HasColumnName("avaliacao");
            entity.Property(e => e.Bairro)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cartela)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("cartela");
            entity.Property(e => e.Cep)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Chassi)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Cidade)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Cnpj)
                .IsUnicode(false)
                .HasColumnName("cnpj");
            entity.Property(e => e.Comissao)
                .IsUnicode(false)
                .HasColumnName("comissao");
            entity.Property(e => e.Complemento)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .IsUnicode(false)
                .HasColumnName("cpf");
            entity.Property(e => e.DataEmissaoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_emissao_boleto");
            entity.Property(e => e.DataNotaFiscal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_nota_fiscal");
            entity.Property(e => e.DataPagamentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_pagamento_boleto");
            entity.Property(e => e.DataVencimentoBoleto)
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("data_vencimento_boleto");
            entity.Property(e => e.Descontos)
                .IsUnicode(false)
                .HasColumnName("descontos");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fone1)
                .IsUnicode(false)
                .HasColumnName("fone_1");
            entity.Property(e => e.Fone2)
                .IsUnicode(false)
                .HasColumnName("fone_2");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdImportacao).HasColumnName("id_importacao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdentificacaoAgencia)
                .IsUnicode(false)
                .HasColumnName("identificacao_agencia");
            entity.Property(e => e.IdentificacaoBanco)
                .IsUnicode(false)
                .HasColumnName("identificacao_banco");
            entity.Property(e => e.IdentificacaoContaCorrente)
                .IsUnicode(false)
                .HasColumnName("identificacao_conta_corrente");
            entity.Property(e => e.Iss)
                .IsUnicode(false)
                .HasColumnName("iss");
            entity.Property(e => e.Leilao)
                .IsUnicode(false)
                .HasColumnName("leilao");
            entity.Property(e => e.LinhaDigitavel)
                .IsUnicode(false)
                .HasColumnName("linha_digitavel");
            entity.Property(e => e.Logradouro)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Lote)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.NomeArrematante)
                .IsUnicode(false)
                .HasColumnName("nome_arrematante");
            entity.Property(e => e.NotaArrematacao)
                .HasMaxLength(20)
                .HasColumnName("nota_arrematacao");
            entity.Property(e => e.Numero)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.NumeroBoleto)
                .IsUnicode(false)
                .HasColumnName("numero_boleto");
            entity.Property(e => e.NumeroProcesso)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_processo");
            entity.Property(e => e.OutrasTaxas)
                .IsUnicode(false)
                .HasColumnName("outras_taxas");
            entity.Property(e => e.Placa)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.TarifaBancaria)
                .IsUnicode(false)
                .HasColumnName("tarifa_bancaria");
            entity.Property(e => e.TaxaAdministrativa)
                .IsUnicode(false)
                .HasColumnName("taxa_administrativa");
            entity.Property(e => e.ValorPago)
                .IsUnicode(false)
                .HasColumnName("valor_pago");
            entity.Property(e => e.ValorTotal)
                .IsUnicode(false)
                .HasColumnName("valor_total");
        });

        modelBuilder.Entity<TbLeilaoLotesArrematantesImportacaoResultado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_leila__3213E83F67FE6514");

            entity.ToTable("tb_leilao_lotes_arrematantes_importacao_resultado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coluna).HasColumnName("coluna");
            entity.Property(e => e.IdImportacaoArrematantes).HasColumnName("id_importacao_arrematantes");
            entity.Property(e => e.Linha).HasColumnName("linha");
            entity.Property(e => e.MsgErro)
                .IsUnicode(false)
                .HasColumnName("msg_erro");
        });

        modelBuilder.Entity<TbLeilaoLotesAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_lotes_audit");

            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            entity.Property(e => e.ArCondicionado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ar_condicionado");
            entity.Property(e => e.AuditDataState)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDataState");
            entity.Property(e => e.AuditDateTime)
                .HasColumnType("datetime")
                .HasColumnName("auditDateTime");
            entity.Property(e => e.AuditDmlaction)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auditDMLAction");
            entity.Property(e => e.AuditUser)
                .HasMaxLength(128)
                .HasColumnName("auditUser");
            entity.Property(e => e.Cambio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cambio");
            entity.Property(e => e.Chassi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Chave)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("chave");
            entity.Property(e => e.ConferidoPatio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("conferido_patio");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.CorOstentada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor_ostentada");
            entity.Property(e => e.DataHoraAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_alteracao");
            entity.Property(e => e.DataHoraApreensao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_apreensao");
            entity.Property(e => e.DataHoraEntrada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_entrada");
            entity.Property(e => e.DataHoraInsercao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_insercao");
            entity.Property(e => e.DataHoraLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_liberacao");
            entity.Property(e => e.Direcao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direcao");
            entity.Property(e => e.FlagAgendado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_agendado");
            entity.Property(e => e.FlagAnaliseSobra)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_analise_sobra");
            entity.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_normalizado");
            entity.Property(e => e.FlagTransacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_transacao");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdStatusLote).HasColumnName("id_status_lote");
            entity.Property(e => e.IdStatusOperacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id_status_operacao");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioInclusao).HasColumnName("id_usuario_inclusao");
            entity.Property(e => e.InformacaoRoubo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            entity.Property(e => e.LanceMinimo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("lance_minimo");
            entity.Property(e => e.Localizacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localizacao");
            entity.Property(e => e.LogRecolhimento).HasColumnName("log_recolhimento");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca_modelo");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NumeroFormularioGrv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");
            entity.Property(e => e.NumeroItemLote).HasColumnName("numero_item_lote");
            entity.Property(e => e.NumeroLote).HasColumnName("numero_lote");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            entity.Property(e => e.ObsTransacao)
                .IsUnicode(false)
                .HasColumnName("obs_transacao");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            entity.Property(e => e.Patio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patio");
            entity.Property(e => e.Periciado).HasColumnName("periciado");
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.PlacaOstentada)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa_ostentada");
            entity.Property(e => e.ProcedenciaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("procedencia_veiculo");
            entity.Property(e => e.Quilometragem).HasColumnName("quilometragem");
            entity.Property(e => e.Reboque)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reboque");
            entity.Property(e => e.Renavan)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("renavan");
            entity.Property(e => e.ResponsavelRemocao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_remocao");
            entity.Property(e => e.RestricaoEstelionato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao_estelionato");
            entity.Property(e => e.SituacaoChassi)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_chassi");
            entity.Property(e => e.SituacaoGnv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_gnv");
            entity.Property(e => e.SituacaoLote)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_lote");
            entity.Property(e => e.SituacaoPlaca)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_placa");
            entity.Property(e => e.SituacaoVeiculo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_veiculo");
            entity.Property(e => e.SituacaoVeiculoPericia).HasColumnName("situacao_veiculo_pericia");
            entity.Property(e => e.StatusPericia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_pericia");
            entity.Property(e => e.TipoCombustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_combustivel");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculo");
            entity.Property(e => e.TravaEletrica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trava_eletrica");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf");
            entity.Property(e => e.UpdateColumns)
                .IsUnicode(false)
                .HasColumnName("updateColumns");
            entity.Property(e => e.ValorAvaliacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("valor_avaliacao");
            entity.Property(e => e.VidroEletrico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vidro_eletrico");
        });

        modelBuilder.Entity<TbLeilaoLotesDespesa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_lotes_despesas");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdDespesa).HasColumnName("id_despesa");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Referencia)
                .IsUnicode(false)
                .HasColumnName("referencia");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdLoteNavigation).WithMany()
                .HasForeignKey(d => d.IdLote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_debitos_lote");
        });

        modelBuilder.Entity<TbLeilaoLotesFoto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_leilao_lotes_fotos");

            entity.Property(e => e.Foto)
                .IsRequired()
                .HasColumnName("foto");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.NomeFoto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_foto");
            entity.Property(e => e.Observacao)
                .IsUnicode(false)
                .HasColumnName("observacao");
        });

        modelBuilder.Entity<TbLeilaoLotesPericiaArquivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_leilao_lotes_pericia_arquivos_1");

            entity.ToTable("tb_leilao_lotes_pericia_arquivos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path");
            entity.Property(e => e.Tamanho).HasColumnName("tamanho");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbLeilaoLotesPericium>(entity =>
        {
            entity.ToTable("tb_leilao_lotes_pericia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
        });

        modelBuilder.Entity<TbLeilaoLotesProprietario>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IdLote });

            entity.ToTable("tb_leilao_lotes_proprietarios");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            entity.Property(e => e.BairroComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("bairro_comunicado_venda");
            entity.Property(e => e.BairroFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("bairro_financiamento_efet");
            entity.Property(e => e.BairroProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("bairro_proprietario");
            entity.Property(e => e.CapacidadeCarga)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("capacidade_carga");
            entity.Property(e => e.CapacidadePassageiros)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("capacidade_passageiros");
            entity.Property(e => e.CepComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cep_comunicado_venda");
            entity.Property(e => e.CepEnderecoProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cep_endereco_proprietario");
            entity.Property(e => e.CepFinanciamentoEfet)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cep_financiamento_efet");
            entity.Property(e => e.Chassi)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.ChassiRemarcado)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("chassi_remarcado");
            entity.Property(e => e.ComplementoComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("complemento_comunicado_venda");
            entity.Property(e => e.ComplementoEnderecoProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("complemento_endereco_proprietario");
            entity.Property(e => e.ComplementoFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("complemento_financiamento_efet");
            entity.Property(e => e.CpfCnpjAgenteFinanceiro)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_agente_financeiro");
            entity.Property(e => e.CpfCnpjComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_comunicado_venda");
            entity.Property(e => e.CpfCnpjFinanciadoSng)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_financiado_sng");
            entity.Property(e => e.CpfCnpjFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_financiamento_efet");
            entity.Property(e => e.DataFinanciadoSng)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("data_financiado_sng");
            entity.Property(e => e.DataFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("data_financiamento_efet");
            entity.Property(e => e.DataLimiteRestricao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("data_limite_restricao");
            entity.Property(e => e.DataVendaComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("data_venda_comunicado_venda");
            entity.Property(e => e.DescricaoCategoria)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_categoria");
            entity.Property(e => e.DescricaoCombustivel)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_combustivel");
            entity.Property(e => e.DescricaoCor)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_cor");
            entity.Property(e => e.DescricaoEspecie)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_especie");
            entity.Property(e => e.DescricaoMarca)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_marca");
            entity.Property(e => e.DescricaoMunicipioEmplacamento)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_municipio_emplacamento");
            entity.Property(e => e.DescricaoMunicipioEndereco)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_municipio_endereco");
            entity.Property(e => e.DescricaoSerie)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_serie");
            entity.Property(e => e.DescricaoTipo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_tipo");
            entity.Property(e => e.DiaJuliano)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("dia_juliano");
            entity.Property(e => e.EnderecoComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("endereco_comunicado_venda");
            entity.Property(e => e.EnderecoFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("endereco_financiamento_efet");
            entity.Property(e => e.EnderecoProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("endereco_proprietario");
            entity.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_normalizado");
            entity.Property(e => e.FlagNotificarComunicado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_notificar_comunicado");
            entity.Property(e => e.FlagNotificarFinanceira)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_notificar_financeira");
            entity.Property(e => e.FlagNotificarProprietario)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_notificar_proprietario");
            entity.Property(e => e.HoraFinanciadoSng)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("hora_financiado_sng");
            entity.Property(e => e.HoraFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("hora_financiamento_efet");
            entity.Property(e => e.IndicacaoDividaAtiva)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("indicacao_divida_ativa");
            entity.Property(e => e.IndicacaoFinanciamento)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("indicacao_financiamento");
            entity.Property(e => e.IndicacaoMultasRenainf)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("indicacao_multas_renainf");
            entity.Property(e => e.IndicacaoRouboFurto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("indicacao_roubo_furto");
            entity.Property(e => e.IndicacaoVeiculoBaixado)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("indicacao_veiculo_baixado");
            entity.Property(e => e.MunicipioComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("municipio_comunicado_venda");
            entity.Property(e => e.MunicipioFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("municipio_financiamento_efet");
            entity.Property(e => e.NomeAgenteFinanceiro)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_agente_financeiro");
            entity.Property(e => e.NomeComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_comunicado_venda");
            entity.Property(e => e.NomeFinanciadoSng)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_financiado_sng");
            entity.Property(e => e.NomeFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_financiamento_efet");
            entity.Property(e => e.NomeProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_proprietario");
            entity.Property(e => e.NomeProprietarioAnterior)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_proprietario_anterior");
            entity.Property(e => e.NumeroComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("numero_comunicado_venda");
            entity.Property(e => e.NumeroCpfCnpj)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("numero_cpf_cnpj");
            entity.Property(e => e.NumeroEnderecoProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("numero_endereco_proprietario");
            entity.Property(e => e.NumeroFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("numero_financiamento_efet");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            entity.Property(e => e.PesoBrutoTotal)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("peso_bruto_total");
            entity.Property(e => e.Placa)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.PlacaAnterior)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("placa_anterior");
            entity.Property(e => e.PlacaNova)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("placa_nova");
            entity.Property(e => e.Renavam)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("renavam");
            entity.Property(e => e.Retorno)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("retorno");
            entity.Property(e => e.Sequencial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("sequencial");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
            entity.Property(e => e.TipoDocumentoAgenteFinanceiro)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tipo_documento_agente_financeiro");
            entity.Property(e => e.TipoDocumentoComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tipo_documento_comunicado_venda");
            entity.Property(e => e.TipoDocumentoFinanciadoSng)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tipo_documento_financiado_sng");
            entity.Property(e => e.TipoDocumentoFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tipo_documento_financiamento_efet");
            entity.Property(e => e.Transacao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("transacao");
            entity.Property(e => e.UfComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("uf_comunicado_venda");
            entity.Property(e => e.UfFinanciamentoEfet)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf_financiamento_efet");
            entity.Property(e => e.UfProprietario)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf_proprietario");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.TbLeilaoLotesProprietarios)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK_tb_leilao_lotes_proprietarios_tb_leilao_lotes_proprietarios");
        });

        modelBuilder.Entity<TbLeilaoLotesRestrico>(entity =>
        {
            entity.ToTable("tb_leilao_lotes_restricoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("('NA')")
                .HasColumnName("codigo");
            entity.Property(e => e.DataHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Observacoes)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            entity.Property(e => e.Origem)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("origem");
            entity.Property(e => e.Restricao)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("restricao");
            entity.Property(e => e.SubRestricao)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("sub_restricao");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.TbLeilaoLotesRestricos)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK_tb_leilao_lotes_restricoes_tb_leilao_lotes");
        });

        modelBuilder.Entity<TbLeilaoLotesTiposTransacao>(entity =>
        {
            entity.ToTable("tb_leilao_lotes_tipos_transacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Flag)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength()
                .HasColumnName("flag");
            entity.Property(e => e.Ordem).HasColumnName("ordem");
        });

        modelBuilder.Entity<TbLeilaoLotesTransaco>(entity =>
        {
            entity.ToTable("tb_leilao_lotes_transacoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Retorno)
                .IsUnicode(false)
                .HasColumnName("retorno");
            entity.Property(e => e.Transacao)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("transacao");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.TbLeilaoLotesTransacos)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK_tb_leilao_lotes_transacoes_tb_leilao_lotes");
        });

        modelBuilder.Entity<TbLeilaoRegrasPrestacaoConta>(entity =>
        {
            entity.ToTable("tb_leilao_regras_prestacao_contas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbLeilaoSapStatus>(entity =>
        {
            entity.HasKey(e => e.Flag).HasName("PK_tb_leilao_sap_status_1");

            entity.ToTable("tb_leilao_sap_status");

            entity.Property(e => e.Flag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<TbLeilaoStatus>(entity =>
        {
            entity.ToTable("tb_leilao_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ativo");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.ExibeMensagemConferencia)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("exibe_mensagem_conferencia");
            entity.Property(e => e.Sequencia).HasColumnName("sequencia");
        });

        modelBuilder.Entity<TbLeiloeiro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_leiloeiros");

            entity.ToTable("tb_leiloeiros");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comissao).HasColumnName("comissao");
            entity.Property(e => e.DataHoraCadastro)
                .HasComputedColumnSql("(getdate())", false)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            entity.Property(e => e.Documento)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("documento");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.OrgaoVinculado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orgao_vinculado");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<TbLogEmailArrematacaoBoleto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_log_e__3213E83F2D9CB955");

            entity.ToTable("tb_log_email_arrematacao_boleto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assunto)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("assunto");
            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_envio");
            entity.Property(e => e.Destinatario)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("destinatario");
            entity.Property(e => e.Erro)
                .HasDefaultValueSql("((0))")
                .HasColumnName("erro");
            entity.Property(e => e.IdBoleto).HasColumnName("id_boleto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Mensagem)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("mensagem");
            entity.Property(e => e.MsgErro)
                .IsUnicode(false)
                .HasColumnName("msg_erro");
        });

        modelBuilder.Entity<TbLogEmailNotificacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_log_e__3213E83F1F83A428");

            entity.ToTable("tb_log_email_notificacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_envio");
            entity.Property(e => e.Destinatario)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("destinatario");
            entity.Property(e => e.Erro)
                .HasDefaultValueSql("((0))")
                .HasColumnName("erro");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Mensagem)
                .HasColumnType("text")
                .HasColumnName("mensagem");
            entity.Property(e => e.MsgErro)
                .IsUnicode(false)
                .HasColumnName("msg_erro");
        });

        modelBuilder.Entity<TbLogEmailNotificacaoLote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_log_e__3213E83F1BB31344");

            entity.ToTable("tb_log_email_notificacao_lotes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdLogEmailNotificacao).HasColumnName("id_log_email_notificacao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
        });

        modelBuilder.Entity<TbLogLeilaoLote>(entity =>
        {
            entity.ToTable("tb_log_leilao_lotes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            entity.Property(e => e.ArCondicionado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ar_condicionado");
            entity.Property(e => e.Cambio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cambio");
            entity.Property(e => e.Chassi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Chave)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("chave");
            entity.Property(e => e.ConferidoPatio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("conferido_patio");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.CorOstentada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor_ostentada");
            entity.Property(e => e.DataHoraAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_alteracao");
            entity.Property(e => e.DataHoraApreensao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_apreensao");
            entity.Property(e => e.DataHoraEntrada)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_entrada");
            entity.Property(e => e.DataHoraInsercao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_insercao");
            entity.Property(e => e.DataHoraLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_hora_liberacao");
            entity.Property(e => e.DatahoraLog)
                .HasColumnType("smalldatetime")
                .HasColumnName("datahora_log");
            entity.Property(e => e.Direcao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direcao");
            entity.Property(e => e.FlagAgendado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_agendado");
            entity.Property(e => e.FlagAnaliseSobra)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_analise_sobra");
            entity.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_normalizado");
            entity.Property(e => e.FlagTransacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_transacao");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdStatusLote).HasColumnName("id_status_lote");
            entity.Property(e => e.IdStatusOperacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id_status_operacao");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioInclusao).HasColumnName("id_usuario_inclusao");
            entity.Property(e => e.InformacaoRoubo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            entity.Property(e => e.LanceMinimo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("lance_minimo");
            entity.Property(e => e.Localizacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localizacao");
            entity.Property(e => e.LogRecolhimento).HasColumnName("log_recolhimento");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca_modelo");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NumeroFormularioGrv)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");
            entity.Property(e => e.NumeroLote).HasColumnName("numero_lote");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            entity.Property(e => e.ObsTransacao)
                .IsUnicode(false)
                .HasColumnName("obs_transacao");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacoes");
            entity.Property(e => e.Patio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patio");
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.PlacaOstentada)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa_ostentada");
            entity.Property(e => e.ProcedenciaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("procedencia_veiculo");
            entity.Property(e => e.Quilometragem).HasColumnName("quilometragem");
            entity.Property(e => e.Reboque)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reboque");
            entity.Property(e => e.Renavan)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("renavan");
            entity.Property(e => e.ResponsavelRemocao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_remocao");
            entity.Property(e => e.RestricaoEstelionato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao_estelionato");
            entity.Property(e => e.SituacaoChassi)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_chassi");
            entity.Property(e => e.SituacaoGnv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_gnv");
            entity.Property(e => e.SituacaoLote)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_lote");
            entity.Property(e => e.SituacaoPlaca)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_placa");
            entity.Property(e => e.SituacaoVeiculo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("situacao_veiculo");
            entity.Property(e => e.SituacaoVeiculoPericia).HasColumnName("situacao_veiculo_pericia");
            entity.Property(e => e.StatusPericia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_pericia");
            entity.Property(e => e.TipoCombustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_combustivel");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculo");
            entity.Property(e => e.TravaEletrica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trava_eletrica");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf");
            entity.Property(e => e.ValorAvaliacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("valor_avaliacao");
            entity.Property(e => e.VidroEletrico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vidro_eletrico");
        });

        modelBuilder.Entity<TbLoteNumeracao>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_lote_numeracao");

            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.PrefixoLote).HasColumnName("prefixo_lote");
            entity.Property(e => e.UltimoLote).HasColumnName("ultimo_lote");
        });

        modelBuilder.Entity<TbLotesStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lotes_status");

            entity.ToTable("tb_lotes_status");

            entity.HasIndex(e => new { e.Codigo, e.CodigoGrupo }, "UQ__lotes_status__codigo__codigo_grupo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("ativo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.CodigoGrupo).HasColumnName("codigo_grupo");
            entity.Property(e => e.CorrelacaoDsin)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("correlacao_dsin");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.IdLeiloado).HasColumnName("id_leiloado");
            entity.Property(e => e.IdNaoLeiloado).HasColumnName("id_nao_leiloado");
            entity.Property(e => e.IdReaproveitavel).HasColumnName("id_reaproveitavel");
            entity.Property(e => e.PermiteAlteracao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("permite_alteracao");
            entity.Property(e => e.PrefixoLote)
                .HasDefaultValueSql("((0))")
                .HasColumnName("prefixo_lote");
            entity.Property(e => e.Reaproveitavel)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("reaproveitavel");
            entity.Property(e => e.ValidaLote)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("valida_lote");

            entity.HasOne(d => d.CodigoGrupoNavigation).WithMany(p => p.TbLotesStatuses)
                .HasForeignKey(d => d.CodigoGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__lotes_status__lotes_status_grupos");

            entity.HasOne(d => d.IdLeiloadoNavigation).WithMany(p => p.InverseIdLeiloadoNavigation)
                .HasForeignKey(d => d.IdLeiloado)
                .HasConstraintName("FK__tb_lotes_status__tb_lotes_status__id_leiloado");

            entity.HasOne(d => d.IdNaoLeiloadoNavigation).WithMany(p => p.InverseIdNaoLeiloadoNavigation)
                .HasForeignKey(d => d.IdNaoLeiloado)
                .HasConstraintName("FK__tb_lotes_status__tb_lotes_status__id_nao_leiloado");

            entity.HasOne(d => d.IdReaproveitavelNavigation).WithMany(p => p.InverseIdReaproveitavelNavigation)
                .HasForeignKey(d => d.IdReaproveitavel)
                .HasConstraintName("FK__tb_lotes_status__tb_lotes_status__id_reaproveitavel");
        });

        modelBuilder.Entity<TbLotesStatusGrupo>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__lotes_status_grupos");

            entity.ToTable("tb_lotes_status_grupos");

            entity.HasIndex(e => e.Descricao, "UQ__lotes_status_grupos__descricao").IsUnique();

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbNotificacaoFatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_notif__3213E83F04AB9942");

            entity.ToTable("tb_notificacao_fatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataEnvio)
                .HasColumnType("datetime")
                .HasColumnName("data_envio");
            entity.Property(e => e.Destinatario)
                .IsUnicode(false)
                .HasColumnName("destinatario");
            entity.Property(e => e.Enviado).HasColumnName("enviado");
            entity.Property(e => e.IdFatura).HasColumnName("id_fatura");
            entity.Property(e => e.MsgErro)
                .IsUnicode(false)
                .HasColumnName("msg_erro");

            entity.HasOne(d => d.IdFaturaNavigation).WithMany(p => p.TbNotificacaoFaturas)
                .HasForeignKey(d => d.IdFatura)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tb_notifi__id_fa__4AF81212");
        });

        modelBuilder.Entity<TbNotificaco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_notificacoes_1");

            entity.ToTable("tb_notificacoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdNotificacaoTipo).HasColumnName("id_notificacao_tipo");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdNotificacaoTipoNavigation).WithMany(p => p.TbNotificacos)
                .HasForeignKey(d => d.IdNotificacaoTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_notificacoes_tb_notificacoes_tipos");
        });

        modelBuilder.Entity<TbNotificacoesStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_status_notificacoes");

            entity.ToTable("tb_notificacoes_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagValido)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_valido");
        });

        modelBuilder.Entity<TbNotificacoesTipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_notificacoes");

            entity.ToTable("tb_notificacoes_tipos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlgStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flg_status");
        });

        modelBuilder.Entity<TbPericiaStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_pericia_status");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<TbPericiaStatusVeiculo>(entity =>
        {
            entity.ToTable("tb_pericia_status_veiculo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbRegra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_regras");

            entity.ToTable("tb_regras");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbSeqIdentificacaoLeilaoOrgao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_seq_i__3213E83F3F667A32");

            entity.ToTable("tb_seq_identificacao_leilao_orgao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ano)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ano");
            entity.Property(e => e.CodigoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_orgao");
            entity.Property(e => e.Seq).HasColumnName("seq");
            entity.Property(e => e.SeqMax).HasColumnName("seq_max");
            entity.Property(e => e.SeqMin).HasColumnName("seq_min");
            entity.Property(e => e.TipoLeilao).HasColumnName("tipo_leilao");
        });

        modelBuilder.Entity<TbUsuariosEmpresa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_usuarios_empresas");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
        });

        modelBuilder.Entity<ViewDadosProprietariosParaEdital>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_dados_proprietarios_para_edital");

            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ano_fabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ano_modelo");
            entity.Property(e => e.Chassi)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.DescricaoMarca)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao_marca");
            entity.Property(e => e.NomeComunicadoVenda)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_comunicado_venda");
            entity.Property(e => e.NomeFinanciamentoEfet)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_financiamento_efet");
            entity.Property(e => e.NomeProprietario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_proprietario");
            entity.Property(e => e.NomeProprietarioAnterior)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome_proprietario_anterior");
            entity.Property(e => e.Placa)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.UfProprietario)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("uf_proprietario");
        });

        modelBuilder.Entity<VwDetranBaArquivoFtpLeilaoAbertura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detran_ba_arquivo_ftp_leilao_abertura");

            entity.Property(e => e.Arquivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.SequencialRemessa).HasColumnName("sequencial_remessa");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<VwDetranBaArquivoFtpLeilaoResultado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detran_ba_arquivo_ftp_leilao_resultado");

            entity.Property(e => e.Arquivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.SequencialRemessa).HasColumnName("sequencial_remessa");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<VwDetranBaArquivoFtpLeilaoResultadoTerc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detran_ba_arquivo_ftp_leilao_resultado_terc");

            entity.Property(e => e.Arquivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.SequencialRemessa).HasColumnName("sequencial_remessa");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(19)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<VwDetranBaArquivoFtpLeilaoSelecao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detran_ba_arquivo_ftp_leilao_selecao");

            entity.Property(e => e.Arquivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.SequencialRemessa).HasColumnName("sequencial_remessa");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<VwDetranBaArquivoFtpLeilaoSelecaoTerc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detran_ba_arquivo_ftp_leilao_selecao_terc");

            entity.Property(e => e.Arquivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("arquivo");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDeposito).HasColumnName("id_deposito");
            entity.Property(e => e.IdFtpEnvio).HasColumnName("id_ftp_envio");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.SequencialRemessa).HasColumnName("sequencial_remessa");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoRegistro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_registro");
        });

        modelBuilder.Entity<VwEditalNotificacao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EDITAL_NOTIFICACAO");

            entity.Property(e => e.AnocarroAnofabri)
                .IsUnicode(false)
                .HasColumnName("ANOCARRO_ANOFABRI");
            entity.Property(e => e.Chassi)
                .IsUnicode(false)
                .HasColumnName("CHASSI");
            entity.Property(e => e.DescMarca)
                .IsUnicode(false)
                .HasColumnName("DESC_MARCA");
            entity.Property(e => e.Gravame)
                .IsUnicode(false)
                .HasColumnName("GRAVAME");
            entity.Property(e => e.Placa)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Proprietario)
                .IsUnicode(false)
                .HasColumnName("PROPRIETARIO");
        });

        modelBuilder.Entity<VwEmailArrematacaoBoletosPago>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_email_arrematacao_boletos_pagos");

            entity.Property(e => e.Assunto)
                .HasMaxLength(101)
                .IsUnicode(false)
                .HasColumnName("assunto");
            entity.Property(e => e.Corpo)
                .IsUnicode(false)
                .HasColumnName("corpo");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdBoleto).HasColumnName("id_boleto");
        });

        modelBuilder.Entity<VwTermoFtpSelecao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_termo_ftp_selecao");

            entity.Property(e => e.Chassi)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLeilaoLote).HasColumnName("id_leilao_lote");
            entity.Property(e => e.NumeroTermo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_termo");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.TipoAtualizacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_atualizacao");
        });

        modelBuilder.Entity<XmlLeilaoDetranpb0120>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__xml_leil__3214EC074B2D1C3C");

            entity.ToTable("xml_leilao_DETRANPB0120");

            entity.Property(e => e.LoadedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Xmldata)
                .HasColumnType("xml")
                .HasColumnName("XMLData");
        });

        modelBuilder.Entity<XmlLeilaoDetranpb0220>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__xml_leil__3214EC074EFDAD20");

            entity.ToTable("xml_leilao_DETRANPB0220");

            entity.Property(e => e.LoadedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Xmldata)
                .HasColumnType("xml")
                .HasColumnName("XMLData");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
