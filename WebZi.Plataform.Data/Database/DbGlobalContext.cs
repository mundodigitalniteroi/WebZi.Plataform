using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.ModelsGlobal;

namespace WebZi.Plataform.Data.Database;

public partial class DbGlobalContext : DbContext
{
    public DbGlobalContext()
    {
    }

    public DbGlobalContext(DbContextOptions<DbGlobalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbDetranBaFtpDiferenca> TbDetranBaFtpDiferencas { get; set; }

    public virtual DbSet<TbGloBanBanco> TbGloBanBancos { get; set; }

    public virtual DbSet<TbGloBanCarto> TbGloBanCartoes { get; set; }

    public virtual DbSet<TbGloBanPixTipoChave> TbGloBanPixTipoChaves { get; set; }

    public virtual DbSet<TbGloDocAutoridadesDiviso> TbGloDocAutoridadesDivisoes { get; set; }

    public virtual DbSet<TbGloDocOrgaosEmissore> TbGloDocOrgaosEmissores { get; set; }

    public virtual DbSet<TbGloDocTiposContato> TbGloDocTiposContatos { get; set; }

    public virtual DbSet<TbGloDocTiposDocumento> TbGloDocTiposDocumentos { get; set; }

    public virtual DbSet<TbGloDocTiposDocumentosIdentificacao> TbGloDocTiposDocumentosIdentificacaos { get; set; }

    public virtual DbSet<TbGloDocTiposPessoa> TbGloDocTiposPessoas { get; set; }

    public virtual DbSet<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; }

    public virtual DbSet<TbGloEmpEmpresasClassificacao> TbGloEmpEmpresasClassificacaos { get; set; }

    public virtual DbSet<TbGloLocBairro> TbGloLocBairros { get; set; }

    public virtual DbSet<TbGloLocCep> TbGloLocCeps { get; set; }

    public virtual DbSet<TbGloLocContinente> TbGloLocContinentes { get; set; }

    public virtual DbSet<TbGloLocEstado> TbGloLocEstados { get; set; }

    public virtual DbSet<TbGloLocFeriado> TbGloLocFeriados { get; set; }

    public virtual DbSet<TbGloLocMunicipio> TbGloLocMunicipios { get; set; }

    public virtual DbSet<TbGloLocPaise> TbGloLocPaises { get; set; }

    public virtual DbSet<TbGloLocRegio> TbGloLocRegioes { get; set; }

    public virtual DbSet<TbGloLocSiteCorreio> TbGloLocSiteCorreios { get; set; }

    public virtual DbSet<TbGloLocTiposLogradouro> TbGloLocTiposLogradouros { get; set; }

    public virtual DbSet<TbGloLocUtc> TbGloLocUtcs { get; set; }

    public virtual DbSet<TbGloLogBanBanco> TbGloLogBanBancos { get; set; }

    public virtual DbSet<TbGloPesPessoa> TbGloPesPessoas { get; set; }

    public virtual DbSet<TbGloPesPessoasDocumentosIdentificacao> TbGloPesPessoasDocumentosIdentificacaos { get; set; }

    public virtual DbSet<TbGloPesPessoasFoto> TbGloPesPessoasFotos { get; set; }

    public virtual DbSet<TbGloPesPessoasLogradouro> TbGloPesPessoasLogradouros { get; set; }

    public virtual DbSet<TbGloPesPessoasTiposContato> TbGloPesPessoasTiposContatos { get; set; }

    public virtual DbSet<TbGloPesTiposEstadoCivil> TbGloPesTiposEstadoCivils { get; set; }

    public virtual DbSet<TbGloPesTiposProfisso> TbGloPesTiposProfissoes { get; set; }

    public virtual DbSet<TbGloProprietario> TbGloProprietarios { get; set; }

    public virtual DbSet<TbGloSysCore> TbGloSysCores { get; set; }

    public virtual DbSet<TbGloSysIgnorarString> TbGloSysIgnorarStrings { get; set; }

    public virtual DbSet<TbGloSysPalavrasOfenciva> TbGloSysPalavrasOfencivas { get; set; }

    public virtual DbSet<TbGloVeiTiposCombustivei> TbGloVeiTiposCombustiveis { get; set; }

    public virtual DbSet<TbGloVeiculosRestrico> TbGloVeiculosRestricoes { get; set; }

    public virtual DbSet<TbGovCnae> TbGovCnaes { get; set; }

    public virtual DbSet<TbGovCnaeListaServico> TbGovCnaeListaServicos { get; set; }

    public virtual DbSet<TbGovListaServico> TbGovListaServicos { get; set; }

    public virtual DbSet<TbGovParametroMunicipio> TbGovParametroMunicipios { get; set; }

    public virtual DbSet<VwGloConsultarEndereco> VwGloConsultarEnderecos { get; set; }

    public virtual DbSet<VwGloConsultarEnderecoCompleto> VwGloConsultarEnderecoCompletos { get; set; }

    public virtual DbSet<VwGloConsultarEnderecoIncompleto> VwGloConsultarEnderecoIncompletos { get; set; }

    public virtual DbSet<VwGloConsultarMunicipioPorCep> VwGloConsultarMunicipioPorCeps { get; set; }

    public virtual DbSet<VwGloDetranBaDadosProprietario> VwGloDetranBaDadosProprietarios { get; set; }

    public virtual DbSet<VwGloPesPessoasDocumentosIdentificacao> VwGloPesPessoasDocumentosIdentificacaos { get; set; }

    public virtual DbSet<VwGloPesPessoasTiposContato> VwGloPesPessoasTiposContatos { get; set; }

    public virtual DbSet<VwGovCnaeListaServico> VwGovCnaeListaServicos { get; set; }

    public virtual DbSet<VwGovCnaeListaServicoParametroMunicipio> VwGovCnaeListaServicoParametroMunicipios { get; set; }

    public virtual DbSet<VwGovParametroMunicipio> VwGovParametroMunicipios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=177.39.16.6;Initial Catalog=db_global;User ID=sa;Password=@Studio55Webzi;MultipleActiveResultSets=True;Persist Security Info=True;Transaction Binding=Implicit Unbind;Connection Timeout=60;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbDetranBaFtpDiferenca>(entity =>
        {
            entity.HasKey(e => new { e.IdFtpRetornoDetalhe, e.NomeCampo }).HasName("PK_detran_ba_ftp_diferencas");

            entity.ToTable("tb_detran_ba_ftp_diferencas");

            entity.Property(e => e.IdFtpRetornoDetalhe).HasColumnName("id_ftp_retorno_detalhe");
            entity.Property(e => e.NomeCampo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_campo");
            entity.Property(e => e.ValorAnterior)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("valor_anterior");
            entity.Property(e => e.ValorAtual)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("valor_atual");
        });

        modelBuilder.Entity<TbGloBanBanco>(entity =>
        {
            entity.HasKey(e => e.IdBanco).HasName("pk_tb_glo_ban_bancos");

            entity.ToTable("tb_glo_ban_bancos", tb => tb.HasTrigger("tr_glo_log_upd_ban_bancos"));

            entity.HasIndex(e => e.CodigoFebraban, "idx_tb_glo_ban_bancos1").IsUnique();

            entity.HasIndex(e => e.IdUsuarioCadastro, "idx_tb_glo_ban_bancos2");

            entity.HasIndex(e => e.IdUsuarioAlteracao, "idx_tb_glo_ban_bancos3");

            entity.Property(e => e.IdBanco).HasColumnName("id_banco");
            entity.Property(e => e.CodigoFebraban)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_febraban");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbGloBanCarto>(entity =>
        {
            entity.HasKey(e => e.IdCartao).HasName("pk_tb_glo_ban_cartoes");

            entity.ToTable("tb_glo_ban_cartoes");

            entity.Property(e => e.IdCartao).HasColumnName("id_cartao");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbGloBanPixTipoChave>(entity =>
        {
            entity.HasKey(e => e.PixTipoChaveId).HasName("pk_tb_glo_ban_pix_tipo_chave");

            entity.ToTable("tb_glo_ban_pix_tipo_chave");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_ban_pix_tipo_chave1").IsUnique();

            entity.Property(e => e.PixTipoChaveId).ValueGeneratedOnAdd();
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
        });

        modelBuilder.Entity<TbGloDocAutoridadesDiviso>(entity =>
        {
            entity.HasKey(e => e.IdAutoridadeDivisao).HasName("pk_tb_glo_doc_autoridades_divisoes");

            entity.ToTable("tb_glo_doc_autoridades_divisoes");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_doc_autoridades_divisoes1").IsUnique();

            entity.Property(e => e.IdAutoridadeDivisao)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_autoridade_divisao");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloDocOrgaosEmissore>(entity =>
        {
            entity.HasKey(e => e.IdOrgaoEmissor).HasName("pk_tb_glo_doc_orgaos_emissores");

            entity.ToTable("tb_glo_doc_orgaos_emissores");

            entity.HasIndex(e => e.Sigla, "idx_tb_glo_doc_orgaos_emissores1").IsUnique();

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_doc_orgaos_emissores2").IsUnique();

            entity.HasIndex(e => e.Uf, "idx_tb_glo_doc_orgaos_emissores3");

            entity.Property(e => e.IdOrgaoEmissor).HasColumnName("id_orgao_emissor");
            entity.Property(e => e.CodigoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_orgao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.FlagAutoridadeResponsavel)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_autoridade_responsavel");
            entity.Property(e => e.FlagDetran)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_detran");
            entity.Property(e => e.Sigla)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sigla");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.UfNavigation).WithMany(p => p.TbGloDocOrgaosEmissores)
                .HasPrincipalKey(p => p.Uf)
                .HasForeignKey(d => d.Uf)
                .HasConstraintName("fk_tb_glo_doc_orgaos_emissores1");
        });

        modelBuilder.Entity<TbGloDocTiposContato>(entity =>
        {
            entity.HasKey(e => e.IdTipoContato).HasName("pk_tb_glo_doc_tipos_contatos");

            entity.ToTable("tb_glo_doc_tipos_contatos");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_doc_tipos_contatos1").IsUnique();

            entity.Property(e => e.IdTipoContato).HasColumnName("id_tipo_contato");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.Formato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("formato");
            entity.Property(e => e.OrdemApresentacao)
                .HasDefaultValueSql("((255))")
                .HasColumnName("ordem_apresentacao");
            entity.Property(e => e.TamanhoMaximo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("tamanho_maximo");
            entity.Property(e => e.TamanhoMinimo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("tamanho_minimo");
        });

        modelBuilder.Entity<TbGloDocTiposDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("pk_tb_glo_doc_tipos_documentos");

            entity.ToTable("tb_glo_doc_tipos_documentos");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_doc_tipos_documentos1").IsUnique();

            entity.Property(e => e.IdTipoDocumento)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_documento");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.Formato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("formato");
            entity.Property(e => e.OrdemApresentacao)
                .HasDefaultValueSql("((255))")
                .HasColumnName("ordem_apresentacao");
            entity.Property(e => e.TamanhoMaximo).HasColumnName("tamanho_maximo");
        });

        modelBuilder.Entity<TbGloDocTiposDocumentosIdentificacao>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumentoIdentificacao).HasName("pk_tb_glo_doc_tipos_documentos_identificacao");

            entity.ToTable("tb_glo_doc_tipos_documentos_identificacao");

            entity.HasIndex(e => e.Codigo, "idx_tb_glo_doc_tipos_documentos_identificacao1").IsUnique();

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_doc_tipos_documentos_identificacao2").IsUnique();

            entity.Property(e => e.IdTipoDocumentoIdentificacao)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_documento_identificacao");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.FlagPossuiComplemento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_complemento");
            entity.Property(e => e.FlagPossuiDataEmissao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_data_emissao");
            entity.Property(e => e.FlagPossuiDataValidade)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_data_validade");
            entity.Property(e => e.FlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_principal");
            entity.Property(e => e.Formato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("formato");
            entity.Property(e => e.OrdemApresentacao)
                .HasDefaultValueSql("((255))")
                .HasColumnName("ordem_apresentacao");
            entity.Property(e => e.TamanhoMaximo).HasColumnName("tamanho_maximo");
            entity.Property(e => e.TamanhoMinimo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("tamanho_minimo");
        });

        modelBuilder.Entity<TbGloDocTiposPessoa>(entity =>
        {
            entity.HasKey(e => e.IdTipoPessoa).HasName("pk_tb_glo_doc_tipos_pessoas");

            entity.ToTable("tb_glo_doc_tipos_pessoas");

            entity.Property(e => e.IdTipoPessoa)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_pessoa");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloEmpEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("pk_tb_glo_emp_empresas");

            entity.ToTable("tb_glo_emp_empresas");

            entity.HasIndex(e => e.IdEmpresaMatriz, "idx_tb_glo_emp_empresas1");

            entity.HasIndex(e => e.IdEmpresaClassificacao, "idx_tb_glo_emp_empresas2");

            entity.HasIndex(e => e.IdCep, "idx_tb_glo_emp_empresas3");

            entity.HasIndex(e => e.IdTipoLogradouro, "idx_tb_glo_emp_empresas4");

            entity.HasIndex(e => e.IdUsuarioCadastro, "idx_tb_glo_emp_empresas5");

            entity.HasIndex(e => e.IdUsuarioAlteracao, "idx_tb_glo_emp_empresas6");

            entity.HasIndex(e => e.CnaeId, "idx_tb_glo_emp_empresas7");

            entity.HasIndex(e => e.CnaeListaServicoId, "idx_tb_glo_emp_empresas8");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.Bairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.CnaeId).HasColumnName("CnaeID");
            entity.Property(e => e.CnaeListaServicoId).HasColumnName("CnaeListaServicoID");
            entity.Property(e => e.Cnpj)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cnpj");
            entity.Property(e => e.CodigoSap)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("codigo_sap");
            entity.Property(e => e.CodigoTributarioMunicipio).HasColumnName("codigo_tributario_municipio");
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.FlagIssRetido)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_iss_retido");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdEmpresaClassificacao).HasColumnName("id_empresa_classificacao");
            entity.Property(e => e.IdEmpresaMatriz).HasColumnName("id_empresa_matriz");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.InscricaoEstadual)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("inscricao_estadual");
            entity.Property(e => e.InscricaoMunicipal)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("inscricao_municipal");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Municipio)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomeFantasia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_fantasia");
            entity.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.OptanteSimplesNacional)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("optante_simples_nacional");
            entity.Property(e => e.Token)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.Cnae).WithMany(p => p.TbGloEmpEmpresas)
                .HasForeignKey(d => d.CnaeId)
                .HasConstraintName("fk_tb_glo_emp_empresas5");

            entity.HasOne(d => d.CnaeListaServico).WithMany(p => p.TbGloEmpEmpresas)
                .HasForeignKey(d => d.CnaeListaServicoId)
                .HasConstraintName("fk_tb_glo_emp_empresas6");

            entity.HasOne(d => d.IdCepNavigation).WithMany(p => p.TbGloEmpEmpresas)
                .HasForeignKey(d => d.IdCep)
                .HasConstraintName("fk_tb_glo_emp_empresas3");

            entity.HasOne(d => d.IdEmpresaClassificacaoNavigation).WithMany(p => p.TbGloEmpEmpresas)
                .HasForeignKey(d => d.IdEmpresaClassificacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_emp_empresas2");

            entity.HasOne(d => d.IdEmpresaMatrizNavigation).WithMany(p => p.InverseIdEmpresaMatrizNavigation)
                .HasForeignKey(d => d.IdEmpresaMatriz)
                .HasConstraintName("fk_tb_glo_emp_empresas1");

            entity.HasOne(d => d.IdTipoLogradouroNavigation).WithMany(p => p.TbGloEmpEmpresas)
                .HasForeignKey(d => d.IdTipoLogradouro)
                .HasConstraintName("fk_tb_glo_emp_empresas4");
        });

        modelBuilder.Entity<TbGloEmpEmpresasClassificacao>(entity =>
        {
            entity.HasKey(e => e.IdEmpresaClassificacao).HasName("pk_tb_glo_emp_empresas_classificacao");

            entity.ToTable("tb_glo_emp_empresas_classificacao");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_emp_empresas_classificacao1").IsUnique();

            entity.Property(e => e.IdEmpresaClassificacao)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_empresa_classificacao");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagMatriz)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_matriz");
        });

        modelBuilder.Entity<TbGloLocBairro>(entity =>
        {
            entity.HasKey(e => e.IdBairro).HasName("pk_tb_glo_loc_bairros");

            entity.ToTable("tb_glo_loc_bairros");

            entity.HasIndex(e => e.Nome, "idx_tb_glo_loc_bairros1");

            entity.Property(e => e.IdBairro).HasColumnName("id_bairro");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.TbGloLocBairros)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_loc_bairros1");
        });

        modelBuilder.Entity<TbGloLocCep>(entity =>
        {
            entity.HasKey(e => e.IdCep).HasName("pk_tb_glo_loc_cep");

            entity.ToTable("tb_glo_loc_cep");

            entity.HasIndex(e => e.Cep, "idx_tb_glo_loc_cep1");

            entity.HasIndex(e => e.Logradouro, "idx_tb_glo_loc_cep2");

            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("cep");
            entity.Property(e => e.FlagSanitizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_sanitizado");
            entity.Property(e => e.IdBairro).HasColumnName("id_bairro");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            entity.HasOne(d => d.IdBairroNavigation).WithMany(p => p.TbGloLocCeps)
                .HasForeignKey(d => d.IdBairro)
                .HasConstraintName("fk_tb_glo_loc_cep2");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.TbGloLocCeps)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_loc_cep1");

            entity.HasOne(d => d.IdTipoLogradouroNavigation).WithMany(p => p.TbGloLocCeps)
                .HasForeignKey(d => d.IdTipoLogradouro)
                .HasConstraintName("fk_tb_glo_loc_cep3");
        });

        modelBuilder.Entity<TbGloLocContinente>(entity =>
        {
            entity.HasKey(e => e.Continente).HasName("pk_tb_glo_loc_continentes");

            entity.ToTable("tb_glo_loc_continentes");

            entity.Property(e => e.Continente)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("continente");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
        });

        modelBuilder.Entity<TbGloLocEstado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("pk_tb_glo_loc_estados");

            entity.ToTable("tb_glo_loc_estados");

            entity.HasIndex(e => e.Uf, "idx_tb_glo_loc_estados1").IsUnique();

            entity.HasIndex(e => e.Nome, "idx_tb_glo_loc_estados2").IsUnique();

            entity.Property(e => e.Capital)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("capital");
            entity.Property(e => e.IdUtc).HasColumnName("id_utc");
            entity.Property(e => e.IdUtcVerao).HasColumnName("id_utc_verao");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
            entity.Property(e => e.PaisNumcode)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pais_numcode");
            entity.Property(e => e.Regiao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.PaisNumcodeNavigation).WithMany(p => p.TbGloLocEstados)
                .HasForeignKey(d => d.PaisNumcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_loc_estados1");

            entity.HasOne(d => d.RegiaoNavigation).WithMany(p => p.TbGloLocEstados)
                .HasForeignKey(d => d.Regiao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_loc_estados2");
        });

        modelBuilder.Entity<TbGloLocFeriado>(entity =>
        {
            entity.HasKey(e => e.IdFeriado).HasName("pk_tb_glo_loc_feriados1");

            entity.ToTable("tb_glo_loc_feriados");

            entity.Property(e => e.IdFeriado).HasColumnName("id_feriado");
            entity.Property(e => e.Ano)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("ano");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Dia)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("dia");
            entity.Property(e => e.FlagFeriadoEstadual)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_feriado_estadual");
            entity.Property(e => e.FlagFeriadoNacional)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_feriado_nacional");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.Mes)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("mes");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.TbGloLocFeriados)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("fk_tb_glo_loc_feriados2");

            entity.HasOne(d => d.UfNavigation).WithMany(p => p.TbGloLocFeriados)
                .HasPrincipalKey(p => p.Uf)
                .HasForeignKey(d => d.Uf)
                .HasConstraintName("fk_tb_glo_loc_feriados1");
        });

        modelBuilder.Entity<TbGloLocMunicipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("pk_tb_glo_loc_municipios");

            entity.ToTable("tb_glo_loc_municipios");

            entity.HasIndex(e => e.Nome, "idx_pk_tb_glo_loc_municipios1");

            entity.HasIndex(e => e.NomePtbr, "idx_pk_tb_glo_loc_municipios2");

            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");
            entity.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio_ibge");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.Estado).WithMany(p => p.TbGloLocMunicipios)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("fk_tb_glo_loc_municipios1");
        });

        modelBuilder.Entity<TbGloLocPaise>(entity =>
        {
            entity.HasKey(e => e.PaisNumcode).HasName("pk_tb_glo_loc_paises");

            entity.ToTable("tb_glo_loc_paises");

            entity.HasIndex(e => e.Nome, "idx_tb_glo_loc_paises1");

            entity.Property(e => e.PaisNumcode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pais_numcode");
            entity.Property(e => e.Continente)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("continente");
            entity.Property(e => e.Iso)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso");
            entity.Property(e => e.Iso3)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso3");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");

            entity.HasOne(d => d.ContinenteNavigation).WithMany(p => p.TbGloLocPaises)
                .HasForeignKey(d => d.Continente)
                .HasConstraintName("fk_tb_glo_loc_paises1");
        });

        modelBuilder.Entity<TbGloLocRegio>(entity =>
        {
            entity.HasKey(e => e.Regiao).HasName("pk_tb_glo_loc_regioes");

            entity.ToTable("tb_glo_loc_regioes");

            entity.HasIndex(e => e.Regiao, "idx_tb_glo_loc_regioes1").IsUnique();

            entity.HasIndex(e => e.Nome, "idx_tb_glo_loc_regioes2");

            entity.Property(e => e.Regiao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbGloLocSiteCorreio>(entity =>
        {
            entity.HasKey(e => e.IdSiteCorreios).HasName("pk_tb_glo_loc_site_correios");

            entity.ToTable("tb_glo_loc_site_correios");

            entity.Property(e => e.IdSiteCorreios)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_site_correios");
            entity.Property(e => e.ButtonName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("button_name");
            entity.Property(e => e.ButtonValue)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("button_value");
            entity.Property(e => e.Site)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("site");
            entity.Property(e => e.TextboxName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("textbox_name");
        });

        modelBuilder.Entity<TbGloLocTiposLogradouro>(entity =>
        {
            entity.HasKey(e => e.IdTipoLogradouro).HasName("pk_tb_glo_loc_tipos_logradouros1");

            entity.ToTable("tb_glo_loc_tipos_logradouros");

            entity.HasIndex(e => e.Codigo, "idx_tb_glo_loc_tipos_logradouros1").IsUnique();

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_loc_tipos_logradouros2").IsUnique();

            entity.Property(e => e.IdTipoLogradouro)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloLocUtc>(entity =>
        {
            entity.HasKey(e => e.IdUtc).HasName("pk_tb_glo_loc_utc");

            entity.ToTable("tb_glo_loc_utc");

            entity.Property(e => e.IdUtc)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_utc");
            entity.Property(e => e.Utc)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("utc");
        });

        modelBuilder.Entity<TbGloLogBanBanco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_tb_glo_log_ban_bancos");

            entity.ToTable("tb_glo_log_ban_bancos");

            entity.HasIndex(e => e.IdBanco, "idx_tb_glo_log_ban_bancos1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoFebraban)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_febraban");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCadastro)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.FlagAtivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.IdBanco).HasColumnName("id_banco");
            entity.Property(e => e.IdUsuarioAlteracao).HasColumnName("id_usuario_alteracao");
            entity.Property(e => e.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbGloPesPessoa>(entity =>
        {
            entity.HasKey(e => e.IdPessoa).HasName("pk_tb_glo_pes_pessoas");

            entity.ToTable("tb_glo_pes_pessoas");

            entity.HasIndex(e => new { e.Nome, e.NomeMeio, e.Sobrenome }, "idx_tb_glo_pes_pessoas1");

            entity.HasIndex(e => e.IdTipoEstadoCivil, "idx_tb_glo_pes_pessoas2");

            entity.HasIndex(e => e.IdTipoProfissao, "idx_tb_glo_pes_pessoas3");

            entity.HasIndex(e => e.Nome, "idx_tb_glo_pes_pessoas4");

            entity.HasIndex(e => e.NomeMeio, "idx_tb_glo_pes_pessoas5");

            entity.HasIndex(e => e.Sobrenome, "idx_tb_glo_pes_pessoas6");

            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.CpfUsuarioAlteracao)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cpf_usuario_alteracao");
            entity.Property(e => e.CpfUsuarioCadastro)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cpf_usuario_cadastro");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("data_nascimento");
            entity.Property(e => e.EmpresaUsuarioAlteracao)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("empresa_usuario_alteracao");
            entity.Property(e => e.EmpresaUsuarioCadastro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("empresa_usuario_cadastro");
            entity.Property(e => e.IdPessoaMae).HasColumnName("id_pessoa_mae");
            entity.Property(e => e.IdPessoaPai).HasColumnName("id_pessoa_pai");
            entity.Property(e => e.IdTipoEstadoCivil).HasColumnName("id_tipo_estado_civil");
            entity.Property(e => e.IdTipoProfissao)
                .HasDefaultValueSql("((1))")
                .HasColumnName("id_tipo_profissao");
            entity.Property(e => e.IpUsuarioAlteracao)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ip_usuario_alteracao");
            entity.Property(e => e.IpUsuarioCadastro)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ip_usuario_cadastro");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NomeMeio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_meio");
            entity.Property(e => e.NomeUsuarioAlteracao)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_usuario_alteracao");
            entity.Property(e => e.NomeUsuarioCadastro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_usuario_cadastro");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Sobrenome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sobrenome");

            entity.HasOne(d => d.IdTipoEstadoCivilNavigation).WithMany(p => p.TbGloPesPessoas)
                .HasForeignKey(d => d.IdTipoEstadoCivil)
                .HasConstraintName("fk_tb_glo_pes_pessoas1");

            entity.HasOne(d => d.IdTipoProfissaoNavigation).WithMany(p => p.TbGloPesPessoas)
                .HasForeignKey(d => d.IdTipoProfissao)
                .HasConstraintName("fk_tb_glo_pes_pessoas2");
        });

        modelBuilder.Entity<TbGloPesPessoasDocumentosIdentificacao>(entity =>
        {
            entity.HasKey(e => e.IdPessoaDocumentoIdentificacao).HasName("pk_tb_glo_pes_pessoas_documentos_identificacao");

            entity.ToTable("tb_glo_pes_pessoas_documentos_identificacao");

            entity.HasIndex(e => e.IdPessoa, "idx_tb_glo_pes_pessoas_documentos_identificacao1");

            entity.HasIndex(e => e.IdOrgaoEmissor, "idx_tb_glo_pes_pessoas_documentos_identificacao2");

            entity.HasIndex(e => e.IdTipoDocumentoIdentificacao, "idx_tb_glo_pes_pessoas_documentos_identificacao3");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_pes_pessoas_documentos_identificacao4");

            entity.Property(e => e.IdPessoaDocumentoIdentificacao).HasColumnName("id_pessoa_documento_identificacao");
            entity.Property(e => e.Complemento)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.DataEmissao)
                .HasColumnType("date")
                .HasColumnName("data_emissao");
            entity.Property(e => e.DataValidade)
                .HasColumnType("date")
                .HasColumnName("data_validade");
            entity.Property(e => e.Descricao)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.IdOrgaoEmissor).HasColumnName("id_orgao_emissor");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdTipoDocumentoIdentificacao).HasColumnName("id_tipo_documento_identificacao");

            entity.HasOne(d => d.IdOrgaoEmissorNavigation).WithMany(p => p.TbGloPesPessoasDocumentosIdentificacaos)
                .HasForeignKey(d => d.IdOrgaoEmissor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_documentos_identificacao2");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.TbGloPesPessoasDocumentosIdentificacaos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_documentos_identificacao1");

            entity.HasOne(d => d.IdTipoDocumentoIdentificacaoNavigation).WithMany(p => p.TbGloPesPessoasDocumentosIdentificacaos)
                .HasForeignKey(d => d.IdTipoDocumentoIdentificacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_documentos_identificacao3");
        });

        modelBuilder.Entity<TbGloPesPessoasFoto>(entity =>
        {
            entity.HasKey(e => e.IdPessoaFoto).HasName("pk_tb_glo_pes_pessoas_fotos");

            entity.ToTable("tb_glo_pes_pessoas_fotos");

            entity.HasIndex(e => e.IdPessoa, "idx_tb_glo_pes_pessoas_fotos1");

            entity.Property(e => e.IdPessoaFoto).HasColumnName("id_pessoa_foto");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.TbGloPesPessoasFotos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_fotos1");
        });

        modelBuilder.Entity<TbGloPesPessoasLogradouro>(entity =>
        {
            entity.HasKey(e => e.IdPessoaLogradouro).HasName("pk_tb_glo_pes_pessoas_logradouros");

            entity.ToTable("tb_glo_pes_pessoas_logradouros");

            entity.HasIndex(e => e.IdPessoa, "idx_tb_glo_pes_pessoas_logradouros1");

            entity.HasIndex(e => e.IdCep, "idx_tb_glo_pes_pessoas_logradouros2");

            entity.HasIndex(e => e.IdTipoLogradouro, "idx_tb_glo_pes_pessoas_logradouros3");

            entity.Property(e => e.IdPessoaLogradouro).HasColumnName("id_pessoa_logradouro");
            entity.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.FlagLogradouroPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_logradouro_principal");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.TipoLogradouro)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_logradouro");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.TbGloPesPessoasLogradouros)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_logradouros1");

            entity.HasOne(d => d.IdTipoLogradouroNavigation).WithMany(p => p.TbGloPesPessoasLogradouros)
                .HasForeignKey(d => d.IdTipoLogradouro)
                .HasConstraintName("fk_tb_glo_pes_pessoas_logradouros2");
        });

        modelBuilder.Entity<TbGloPesPessoasTiposContato>(entity =>
        {
            entity.HasKey(e => e.IdPessoaTipoContato).HasName("pk_tb_glo_pes_pessoas_tipos_contatos");

            entity.ToTable("tb_glo_pes_pessoas_tipos_contatos");

            entity.HasIndex(e => e.IdPessoa, "idx_tb_glo_pes_pessoas_tipos_contatos1");

            entity.HasIndex(e => e.IdTipoContato, "idx_tb_glo_pes_pessoas_tipos_contatos2");

            entity.Property(e => e.IdPessoaTipoContato).HasColumnName("id_pessoa_tipo_contato");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagContatoPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_contato_principal");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdTipoContato).HasColumnName("id_tipo_contato");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.TbGloPesPessoasTiposContatos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_tipos_contatos1");

            entity.HasOne(d => d.IdTipoContatoNavigation).WithMany(p => p.TbGloPesPessoasTiposContatos)
                .HasForeignKey(d => d.IdTipoContato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_glo_pes_pessoas_tipos_contatos2");
        });

        modelBuilder.Entity<TbGloPesTiposEstadoCivil>(entity =>
        {
            entity.HasKey(e => e.IdTipoEstadoCivil).HasName("pk_tb_glo_pes_tipos_estado_civil");

            entity.ToTable("tb_glo_pes_tipos_estado_civil");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_pes_tipos_estado_civil1").IsUnique();

            entity.Property(e => e.IdTipoEstadoCivil)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_estado_civil");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        });

        modelBuilder.Entity<TbGloPesTiposProfisso>(entity =>
        {
            entity.HasKey(e => e.IdTipoProfissao).HasName("pk_tb_glo_pes_tipos_profissoes");

            entity.ToTable("tb_glo_pes_tipos_profissoes");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_pes_tipos_profissoes1").IsUnique();

            entity.Property(e => e.IdTipoProfissao).HasColumnName("id_tipo_profissao");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagAtivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        });

        modelBuilder.Entity<TbGloProprietario>(entity =>
        {
            entity.HasKey(e => e.IdProprietario).HasName("PK_glo_proprietarios");

            entity.ToTable("tb_glo_proprietarios");

            entity.Property(e => e.IdProprietario).HasColumnName("idProprietario");
            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("anoFabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("anoModelo");
            entity.Property(e => e.CapacidadeCarga)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("capacidadeCarga");
            entity.Property(e => e.CapacidadePassag)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("capacidadePassag");
            entity.Property(e => e.Carroceria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("carroceria");
            entity.Property(e => e.CategoriaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoriaVeiculo");
            entity.Property(e => e.CepProprietario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cep_proprietario");
            entity.Property(e => e.Cilindrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cilindrada");
            entity.Property(e => e.CodigoCarroceria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCarroceria");
            entity.Property(e => e.CodigoCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCategoria");
            entity.Property(e => e.CodigoCombustive)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCombustive");
            entity.Property(e => e.CodigoCor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCor");
            entity.Property(e => e.CodigoEspecie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoEspecie");
            entity.Property(e => e.CodigoMarcaMod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoMarcaMod");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoMunicipio");
            entity.Property(e => e.CodigoOcoExec).HasColumnName("codigoOcoExec");
            entity.Property(e => e.CodigoOperacao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigoOperacao");
            entity.Property(e => e.CodigoPatio).HasColumnName("codigo_patio");
            entity.Property(e => e.CodigoRetExec).HasColumnName("codigoRetExec");
            entity.Property(e => e.CodigoTipVeic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoTipVeic");
            entity.Property(e => e.CodigoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigoUsuario");
            entity.Property(e => e.Combustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("combustivel");
            entity.Property(e => e.ComplemEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("complemEndereco");
            entity.Property(e => e.CompradorBairro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comprador_bairro");
            entity.Property(e => e.CompradorCep).HasColumnName("comprador_cep");
            entity.Property(e => e.CompradorComplemEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comprador_complem_endereco");
            entity.Property(e => e.CompradorDataVenda)
                .HasColumnType("smalldatetime")
                .HasColumnName("comprador_data_venda");
            entity.Property(e => e.CompradorEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comprador_endereco");
            entity.Property(e => e.CompradorMunicipio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comprador_municipio");
            entity.Property(e => e.CompradorNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comprador_nome");
            entity.Property(e => e.CompradorNumeroDoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("comprador_numero_doc");
            entity.Property(e => e.CompradorNumeroEndereco)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("comprador_numero_endereco");
            entity.Property(e => e.CompradorUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("comprador_uf");
            entity.Property(e => e.ComunicacaoVenda)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comunicacaoVenda");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.CpfCnpjAgenteFinanceiro)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_agente_financeiro");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("dataAlteracao");
            entity.Property(e => e.DataApreensao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_apreensao");
            entity.Property(e => e.DataAtualizacao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("dataAtualizacao");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("dataCadastro");
            entity.Property(e => e.DataLimiteRestricao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_limite_restricao");
            entity.Property(e => e.DescricaoEspecie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoEspecie");
            entity.Property(e => e.DescricaoMunicipioEmplacamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao_municipio_emplacamento");
            entity.Property(e => e.DescricaoSerie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao_serie");
            entity.Property(e => e.DiaJuliano)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dia_juliano");
            entity.Property(e => e.Endereco)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.FinanceiraBairro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_bairro");
            entity.Property(e => e.FinanceiraCep).HasColumnName("financeira_cep");
            entity.Property(e => e.FinanceiraCnpj)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("financeira_cnpj");
            entity.Property(e => e.FinanceiraCnpjSng)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("financeira_cnpj_sng");
            entity.Property(e => e.FinanceiraComplemEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_complem_endereco");
            entity.Property(e => e.FinanceiraData)
                .HasColumnType("smalldatetime")
                .HasColumnName("financeira_data");
            entity.Property(e => e.FinanceiraDataSng).HasColumnName("financeira_data_sng");
            entity.Property(e => e.FinanceiraEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_endereco");
            entity.Property(e => e.FinanceiraHora).HasColumnName("financeira_hora");
            entity.Property(e => e.FinanceiraHoraSng).HasColumnName("financeira_hora_sng");
            entity.Property(e => e.FinanceiraMunicipio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_municipio");
            entity.Property(e => e.FinanceiraNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_nome");
            entity.Property(e => e.FinanceiraNumeroEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("financeira_numero_endereco");
            entity.Property(e => e.FinanceiraTipoDocumento).HasColumnName("financeira_tipo_documento");
            entity.Property(e => e.FinanceiraTipoDocumentoSng).HasColumnName("financeira_tipo_documento_sng");
            entity.Property(e => e.FinanceiraUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("financeira_uf");
            entity.Property(e => e.IdLeilao).HasColumnName("id_leilao");
            entity.Property(e => e.IdLeilaoAnterior).HasColumnName("id_leilao_anterior");
            entity.Property(e => e.IndRouboFurto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("indRouboFurto");
            entity.Property(e => e.IndicacaoDividaAtiva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("indicacao_divida_ativa");
            entity.Property(e => e.IndicacaoFinanciamento).HasColumnName("indicacao_financiamento");
            entity.Property(e => e.IndicacaoMultasRenainf)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("indicacao_multas_renainf");
            entity.Property(e => e.IndicacaoVeiculoBaixado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("indicacao_veiculo_baixado");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marcaModelo");
            entity.Property(e => e.MotorDifAcesso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motorDifAcesso");
            entity.Property(e => e.NomeAgenteFinanceiro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_agente_financeiro");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomeBairro");
            entity.Property(e => e.NomeFinanciadoSng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_financiado_sng");
            entity.Property(e => e.NomeMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomeMunicipio");
            entity.Property(e => e.NomeProprietario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomeProprietario");
            entity.Property(e => e.NomeProprietarioAnterior)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_proprietario_anterior");
            entity.Property(e => e.NomeUf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomeUf");
            entity.Property(e => e.NotifFiscalSefaz)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("notif_fiscal_sefaz");
            entity.Property(e => e.NumeroCep)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numeroCep");
            entity.Property(e => e.NumeroCpfCgc)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroCpfCgc");
            entity.Property(e => e.NumeroDocProprietario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_doc_proprietario");
            entity.Property(e => e.NumeroEixos)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numeroEixos");
            entity.Property(e => e.NumeroEndereco)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numeroEndereco");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroMotor");
            entity.Property(e => e.NumeroTelefone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroTelefone");
            entity.Property(e => e.NumeroTermo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_termo");
            entity.Property(e => e.NumeroTrave).HasColumnName("numero_trave");
            entity.Property(e => e.Observacao)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacao");
            entity.Property(e => e.ParamChassi)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("paramChassi");
            entity.Property(e => e.ParamPlaca)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("paramPlaca");
            entity.Property(e => e.ParamRenavam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paramRenavam");
            entity.Property(e => e.ParamUf)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("paramUF");
            entity.Property(e => e.PesoBrutoTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("pesoBrutoTotal");
            entity.Property(e => e.PlacaAnterior)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa_anterior");
            entity.Property(e => e.PlacaNova)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa_nova");
            entity.Property(e => e.Potencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("potencia");
            entity.Property(e => e.Procedencia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("procedencia");
            entity.Property(e => e.QuantidadeDiaria).HasColumnName("quantidade_diaria");
            entity.Property(e => e.RegravChassi)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("regravChassi");
            entity.Property(e => e.Restricao1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao1");
            entity.Property(e => e.Restricao2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao2");
            entity.Property(e => e.Restricao3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao3");
            entity.Property(e => e.Restricao4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao4");
            entity.Property(e => e.Restricao5)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao5");
            entity.Property(e => e.Restricao6)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao6");
            entity.Property(e => e.SenhaUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senhaUsuario");
            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.Situacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("situacao");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StatusApreensao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_apreensao");
            entity.Property(e => e.TipoAtualizacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_atualizacao");
            entity.Property(e => e.TipoDocAgenteFinanceiro).HasColumnName("tipo_doc_agente_financeiro");
            entity.Property(e => e.TipoDocComunicVenda).HasColumnName("tipo_doc_comunic_venda");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoRegistro).HasColumnName("tipo_registro");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoVeiculo");
            entity.Property(e => e.TracaoMax)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tracaoMax");
            entity.Property(e => e.Transacao).HasColumnName("transacao");
            entity.Property(e => e.UnidadeFederacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unidadeFederacao");
            entity.Property(e => e.ValorDebDpvat)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebDpvat");
            entity.Property(e => e.ValorDebInfTrami)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebInfTrami");
            entity.Property(e => e.ValorDebIpva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebIpva");
            entity.Property(e => e.ValorDebLicenc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebLicenc");
            entity.Property(e => e.ValorDebMulta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebMulta");
            entity.Property(e => e.ValorDiaria)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_diaria");
            entity.Property(e => e.ValorReboque)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor_reboque");
            entity.Property(e => e.WebService)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("webService");
        });

        modelBuilder.Entity<TbGloSysCore>(entity =>
        {
            entity.HasKey(e => e.IdCor).HasName("pk_tb_glo_sys_cores");

            entity.ToTable("tb_glo_sys_cores");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_sys_cores1").IsUnique();

            entity.Property(e => e.IdCor).HasColumnName("id_cor");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.DescricaoSecundaria)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao_secundaria");
            entity.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            entity.Property(e => e.FlagCorPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cor_principal");
        });

        modelBuilder.Entity<TbGloSysIgnorarString>(entity =>
        {
            entity.HasKey(e => e.IdIgnorarString).HasName("pk_tb_glo_sys_ignorar_strings");

            entity.ToTable("tb_glo_sys_ignorar_strings");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_sys_ignorar_strings1").IsUnique();

            entity.Property(e => e.IdIgnorarString).HasColumnName("id_ignorar_string");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloSysPalavrasOfenciva>(entity =>
        {
            entity.HasKey(e => e.IdPalavraOfenciva).HasName("pk_tb_glo_sys_palavras_ofencivas");

            entity.ToTable("tb_glo_sys_palavras_ofencivas");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_sys_palavras_ofencivas1");

            entity.Property(e => e.IdPalavraOfenciva).HasColumnName("id_palavra_ofenciva");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloVeiTiposCombustivei>(entity =>
        {
            entity.HasKey(e => e.IdTipoCombustivel).HasName("pk_tb_glo_vei_tipos_combustiveis1");

            entity.ToTable("tb_glo_vei_tipos_combustiveis");

            entity.HasIndex(e => e.Descricao, "idx_tb_glo_vei_tipos_combustiveis1");

            entity.Property(e => e.IdTipoCombustivel)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_combustivel");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TbGloVeiculosRestrico>(entity =>
        {
            entity.HasKey(e => new { e.Sequencial, e.Placa, e.Chassi }).HasName("PK_glo_veiculos_restricoes");

            entity.ToTable("tb_glo_veiculos_restricoes");

            entity.Property(e => e.Sequencial).HasColumnName("sequencial");
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Chassi)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.DataCadastro)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FlagOrigem)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("flag_origem");
            entity.Property(e => e.IdProprietario).HasColumnName("id_proprietario");

            entity.HasOne(d => d.IdProprietarioNavigation).WithMany(p => p.TbGloVeiculosRestricos)
                .HasForeignKey(d => d.IdProprietario)
                .HasConstraintName("FK_glo_veiculos_restricoes_glo_proprietarios");
        });

        modelBuilder.Entity<TbGovCnae>(entity =>
        {
            entity.HasKey(e => e.CnaeId);

            entity.ToTable("tb_gov_cnae");

            entity.HasIndex(e => e.Codigo, "IDX_tb_gov_cnae1").IsUnique();

            entity.Property(e => e.CnaeId).HasColumnName("CnaeID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CodigoFormatado)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DataAlteracao).HasColumnType("smalldatetime");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
        });

        modelBuilder.Entity<TbGovCnaeListaServico>(entity =>
        {
            entity.HasKey(e => e.CnaeListaServicoId);

            entity.ToTable("tb_gov_cnae_lista_servico");

            entity.HasIndex(e => new { e.CnaeId, e.ListaServicoId }, "IDX_tb_gov_cnae_lista_servico1").IsUnique();

            entity.Property(e => e.CnaeListaServicoId).HasColumnName("CnaeListaServicoID");
            entity.Property(e => e.CnaeId).HasColumnName("CnaeID");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.ListaServicoId).HasColumnName("ListaServicoID");

            entity.HasOne(d => d.Cnae).WithMany(p => p.TbGovCnaeListaServicos)
                .HasForeignKey(d => d.CnaeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_gov_cnae_lista_servico1");

            entity.HasOne(d => d.ListaServico).WithMany(p => p.TbGovCnaeListaServicos)
                .HasForeignKey(d => d.ListaServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_gov_cnae_lista_servico2");
        });

        modelBuilder.Entity<TbGovListaServico>(entity =>
        {
            entity.HasKey(e => e.ListaServicoId);

            entity.ToTable("tb_gov_lista_servico");

            entity.HasIndex(e => new { e.ItemLista, e.AliquotaIss }, "IDX_tb_gov_lista_servico1").IsUnique();

            entity.Property(e => e.ListaServicoId).HasColumnName("ListaServicoID");
            entity.Property(e => e.AliquotaIss).HasColumnType("smallmoney");
            entity.Property(e => e.DataAlteracao).HasColumnType("smalldatetime");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ItemLista)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TbGovParametroMunicipio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_gov_parametro_municipio");

            entity.Property(e => e.CodigoCnae)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodigoTributarioMunicipio)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DataAlteracao).HasColumnType("smalldatetime");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.ItemListaServico)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ParametroMunicipioId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CnaeListaServico).WithMany()
                .HasForeignKey(d => d.CnaeListaServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_gov_parametro_municipio1");

            entity.HasOne(d => d.Municipio).WithMany()
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_gov_parametro_municipio2");
        });

        modelBuilder.Entity<VwGloConsultarEndereco>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_consultar_endereco");

            entity.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.BairroPtbr)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro_ptbr");
            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CEP");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.EstadoPtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_ptbr");
            entity.Property(e => e.IdBairro).HasColumnName("id_bairro");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.MunicipioPtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio_ptbr");
            entity.Property(e => e.Regiao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");
            entity.Property(e => e.RegiaoNome)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("regiao_nome");
            entity.Property(e => e.TipoLogradouro)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_logradouro");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        });

        modelBuilder.Entity<VwGloConsultarEnderecoCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_consultar_endereco_completo");

            entity.Property(e => e.Bairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.BairroPtbr)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro_ptbr");
            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("CEP");
            entity.Property(e => e.CodigoLogradouro)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("codigo_logradouro");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");
            entity.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio_ibge");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.EstadoPtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_ptbr");
            entity.Property(e => e.FlagSanitizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_sanitizado");
            entity.Property(e => e.IdBairro).HasColumnName("id_bairro");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.MunicipioPtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio_ptbr");
            entity.Property(e => e.Regiao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");
            entity.Property(e => e.RegiaoNome)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("regiao_nome");
            entity.Property(e => e.TipoLogradouro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_logradouro");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        });

        modelBuilder.Entity<VwGloConsultarEnderecoIncompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_consultar_endereco_incompleto");

            entity.Property(e => e.Bairro).HasColumnName("bairro");
            entity.Property(e => e.BairroPtbr).HasColumnName("bairro_ptbr");
            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CEP");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.EstadoPtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_ptbr");
            entity.Property(e => e.FlagSanitizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_sanitizado");
            entity.Property(e => e.IdBairro).HasColumnName("id_bairro");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdTipoLogradouro).HasColumnName("id_tipo_logradouro");
            entity.Property(e => e.Logradouro).HasColumnName("logradouro");
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.MunicipioPtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio_ptbr");
            entity.Property(e => e.Regiao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");
            entity.Property(e => e.RegiaoNome)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("regiao_nome");
            entity.Property(e => e.TipoLogradouro).HasColumnName("tipo_logradouro");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        });

        modelBuilder.Entity<VwGloConsultarMunicipioPorCep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_consultar_municipio_por_cep");

            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CEP");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");
            entity.Property(e => e.IdCep).HasColumnName("id_cep");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.MunicipioPtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio_ptbr");
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UF");
        });

        modelBuilder.Entity<VwGloDetranBaDadosProprietario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_detran_ba_dados_proprietario");

            entity.Property(e => e.AnoFabricacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("anoFabricacao");
            entity.Property(e => e.AnoModelo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("anoModelo");
            entity.Property(e => e.CapacidadeCarga)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("capacidadeCarga");
            entity.Property(e => e.CapacidadePassag)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("capacidadePassag");
            entity.Property(e => e.Carroceria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("carroceria");
            entity.Property(e => e.CategoriaVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoriaVeiculo");
            entity.Property(e => e.Cilindrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cilindrada");
            entity.Property(e => e.CodigoCarroceria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCarroceria");
            entity.Property(e => e.CodigoCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCategoria");
            entity.Property(e => e.CodigoCombustive)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCombustive");
            entity.Property(e => e.CodigoCor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoCor");
            entity.Property(e => e.CodigoEspecie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoEspecie");
            entity.Property(e => e.CodigoMarcaMod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoMarcaMod");
            entity.Property(e => e.CodigoMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoMunicipio");
            entity.Property(e => e.CodigoOperacao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigoOperacao");
            entity.Property(e => e.CodigoRetExec)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigoRetExec");
            entity.Property(e => e.CodigoTipVeic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoTipVeic");
            entity.Property(e => e.CodigoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigoUsuario");
            entity.Property(e => e.Combustivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("combustivel");
            entity.Property(e => e.ComplemEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("complemEndereco");
            entity.Property(e => e.ComunicacaoVenda)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comunicacaoVenda");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("dataAlteracao");
            entity.Property(e => e.DataAtualizacao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("dataAtualizacao");
            entity.Property(e => e.DataCadastro)
                .HasColumnType("smalldatetime")
                .HasColumnName("dataCadastro");
            entity.Property(e => e.DescricaoEspecie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoEspecie");
            entity.Property(e => e.Endereco)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.IdGrv).HasColumnName("id_grv");
            entity.Property(e => e.IdProprietario).HasColumnName("idProprietario");
            entity.Property(e => e.IndRouboFurto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("indRouboFurto");
            entity.Property(e => e.MarcaModelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marcaModelo");
            entity.Property(e => e.MotorDifAcesso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motorDifAcesso");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomeBairro");
            entity.Property(e => e.NomeMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomeMunicipio");
            entity.Property(e => e.NomeProprietario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomeProprietario");
            entity.Property(e => e.NomeUf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomeUf");
            entity.Property(e => e.NumeroCep)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numeroCep");
            entity.Property(e => e.NumeroCpfCgc)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroCpfCgc");
            entity.Property(e => e.NumeroEixos)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numeroEixos");
            entity.Property(e => e.NumeroEndereco)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numeroEndereco");
            entity.Property(e => e.NumeroGrv)
                .HasMaxLength(9)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("numero_grv");
            entity.Property(e => e.NumeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroMotor");
            entity.Property(e => e.NumeroTelefone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroTelefone");
            entity.Property(e => e.ParamChassi)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("paramChassi");
            entity.Property(e => e.ParamPlaca)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("paramPlaca");
            entity.Property(e => e.ParamRenavam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paramRenavam");
            entity.Property(e => e.ParamUf)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("paramUF");
            entity.Property(e => e.PesoBrutoTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("pesoBrutoTotal");
            entity.Property(e => e.Potencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("potencia");
            entity.Property(e => e.Procedencia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("procedencia");
            entity.Property(e => e.RegravChassi)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("regravChassi");
            entity.Property(e => e.Restricao1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao1");
            entity.Property(e => e.Restricao2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao2");
            entity.Property(e => e.Restricao3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao3");
            entity.Property(e => e.Restricao4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao4");
            entity.Property(e => e.Restricao5)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao5");
            entity.Property(e => e.Restricao6)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restricao6");
            entity.Property(e => e.SenhaUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senhaUsuario");
            entity.Property(e => e.Situacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("situacao");
            entity.Property(e => e.StatusOperacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("status_operacao");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoDocumento");
            entity.Property(e => e.TipoVeiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoVeiculo");
            entity.Property(e => e.TracaoMax)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tracaoMax");
            entity.Property(e => e.UnidadeFederacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unidadeFederacao");
            entity.Property(e => e.ValorDebDpvat)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebDpvat");
            entity.Property(e => e.ValorDebInfTrami)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebInfTrami");
            entity.Property(e => e.ValorDebIpva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebIpva");
            entity.Property(e => e.ValorDebLicenc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebLicenc");
            entity.Property(e => e.ValorDebMulta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valorDebMulta");
            entity.Property(e => e.WebService)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("webService");
        });

        modelBuilder.Entity<VwGloPesPessoasDocumentosIdentificacao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_pes_pessoas_documentos_identificacao");

            entity.Property(e => e.IdOrgaoEmissor).HasColumnName("id_orgao_emissor");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdPessoaDocumentoIdentificacao).HasColumnName("id_pessoa_documento_identificacao");
            entity.Property(e => e.IdTipoDocumentoIdentificacao).HasColumnName("id_tipo_documento_identificacao");
            entity.Property(e => e.OrgaosEmissoresDescricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("orgaos_emissores_descricao");
            entity.Property(e => e.OrgaosEmissoresFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("orgaos_emissores_flag_ativo");
            entity.Property(e => e.OrgaosEmissoresFlagAutoridadeResponsavel)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("orgaos_emissores_flag_autoridade_responsavel");
            entity.Property(e => e.OrgaosEmissoresSigla)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("orgaos_emissores_sigla");
            entity.Property(e => e.OrgaosEmissoresUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("orgaos_emissores_uf");
            entity.Property(e => e.PessoasDocumentosIdentificacaoComplemento)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("pessoas_documentos_identificacao_complemento");
            entity.Property(e => e.PessoasDocumentosIdentificacaoDataEmissao)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("pessoas_documentos_identificacao_data_emissao");
            entity.Property(e => e.PessoasDocumentosIdentificacaoDataValidade)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("pessoas_documentos_identificacao_data_validade");
            entity.Property(e => e.PessoasDocumentosIdentificacaoDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pessoas_documentos_identificacao_documento");
            entity.Property(e => e.TiposDocumentosIdentificacaoCodigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipos_documentos_identificacao_codigo");
            entity.Property(e => e.TiposDocumentosIdentificacaoDescricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipos_documentos_identificacao_descricao");
            entity.Property(e => e.TiposDocumentosIdentificacaoFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_documentos_identificacao_flag_ativo");
            entity.Property(e => e.TiposDocumentosIdentificacaoFlagPossuiComplemento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_documentos_identificacao_flag_possui_complemento");
            entity.Property(e => e.TiposDocumentosIdentificacaoFlagPossuiDataEmissao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_documentos_identificacao_flag_possui_data_emissao");
            entity.Property(e => e.TiposDocumentosIdentificacaoFlagPossuiDataValidade)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_documentos_identificacao_flag_possui_data_validade");
            entity.Property(e => e.TiposDocumentosIdentificacaoFlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_documentos_identificacao_flag_principal");
            entity.Property(e => e.TiposDocumentosIdentificacaoFormato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipos_documentos_identificacao_formato");
            entity.Property(e => e.TiposDocumentosIdentificacaoOrdemApresentacao).HasColumnName("tipos_documentos_identificacao_ordem_apresentacao");
            entity.Property(e => e.TiposDocumentosIdentificacaoTamanhoMaximo).HasColumnName("tipos_documentos_identificacao_tamanho_maximo");
            entity.Property(e => e.TiposDocumentosIdentificacaoTamanhoMinimo).HasColumnName("tipos_documentos_identificacao_tamanho_minimo");
        });

        modelBuilder.Entity<VwGloPesPessoasTiposContato>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_glo_pes_pessoas_tipos_contatos");

            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdPessoaTipoContato).HasColumnName("id_pessoa_tipo_contato");
            entity.Property(e => e.IdTipoContato).HasColumnName("id_tipo_contato");
            entity.Property(e => e.PessoasTiposContatosDescricao)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("pessoas_tipos_contatos_descricao");
            entity.Property(e => e.PessoasTiposContatosFlagContatoPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pessoas_tipos_contatos_flag_contato_principal");
            entity.Property(e => e.TiposContatosDescricao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipos_contatos_descricao");
            entity.Property(e => e.TiposContatosFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_contatos_flag_ativo");
            entity.Property(e => e.TiposContatosFormato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipos_contatos_formato");
            entity.Property(e => e.TiposContatosOrdemApresentacao).HasColumnName("tipos_contatos_ordem_apresentacao");
            entity.Property(e => e.TiposContatosTamanhoMaximo).HasColumnName("tipos_contatos_tamanho_maximo");
            entity.Property(e => e.TiposContatosTamanhoMinimo).HasColumnName("tipos_contatos_tamanho_minimo");
        });

        modelBuilder.Entity<VwGovCnaeListaServico>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_gov_cnae_lista_servico");

            entity.Property(e => e.AliquotaIss).HasColumnType("smallmoney");
            entity.Property(e => e.CnaeCodigo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeCodigoFormatado)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeDescricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CnaeId).HasColumnName("CnaeID");
            entity.Property(e => e.CnaeListaServicoId).HasColumnName("CnaeListaServicoID");
            entity.Property(e => e.ListaServico)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ListaServicoDescricao)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ListaServicoId).HasColumnName("ListaServicoID");
        });

        modelBuilder.Entity<VwGovCnaeListaServicoParametroMunicipio>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_gov_cnae_lista_servico_parametro_municipio");

            entity.Property(e => e.AliquotaIss).HasColumnType("smallmoney");
            entity.Property(e => e.CnaeCodigo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeCodigoFormatado)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeDescricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodigoTributarioMunicipio)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ListaServico)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ListaServicoDescricao)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<VwGovParametroMunicipio>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_gov_parametro_municipio");

            entity.Property(e => e.AliquotaIss).HasColumnType("smallmoney");
            entity.Property(e => e.CnaeCodigo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeCodigoFormatado)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CnaeDescricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CnaeFlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodigoTributarioMunicipio)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DataAlteracao).HasColumnType("smalldatetime");
            entity.Property(e => e.DataCadastro).HasColumnType("smalldatetime");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ListaServicoDescricao)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ListaServicoItem)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
