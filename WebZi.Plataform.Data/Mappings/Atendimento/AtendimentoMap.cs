using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Data.Mappings.Atendimento
{
    public class AtendimentoMap : IEntityTypeConfiguration<AtendimentoModel>
    {
        public void Configure(EntityTypeBuilder<AtendimentoModel> builder)
        {
            builder
                .ToTable("tb_dep_atendimento", "dbo", tb =>
                {
                    tb.HasTrigger("tr_log_del_atendimento");
                    tb.HasTrigger("tr_log_upd_atendimento");
                })
                .HasKey(e => e.AtendimentoId);

            builder.Property(e => e.AtendimentoId)
                .HasColumnName("id_atendimento")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.DataHoraInicioAtendimento)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_inicio_atendimento");

            builder.Property(e => e.DataImpressao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_impressao");

            builder.Property(e => e.FlagAtendimentoWs)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_atendimento_ws");

            builder.Property(e => e.FlagPagamentoFinanciado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_pagamento_financiado");

            builder.Property(e => e.FormaLiberacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("forma_liberacao");

            builder.Property(e => e.FormaLiberacaoCnh)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("forma_liberacao_cnh");

            builder.Property(e => e.FormaLiberacaoCpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("forma_liberacao_cpf");

            builder.Property(e => e.FormaLiberacaoNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("forma_liberacao_nome");

            builder.Property(e => e.FormaLiberacaoPlaca)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("forma_liberacao_placa");

            builder.Property(e => e.DocumentoSapId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id_documento_sap");

            builder.Property(e => e.EmpresaFaturamentoId)
                .HasColumnName("id_empresa_faturamento");

            builder.Property(e => e.PessoaFaturamentoId)
                .HasColumnName("id_pessoa_faturamento");

            builder.Property(e => e.QualificacaoResponsavelId)
                .HasColumnName("id_qualificacao_responsavel");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.NotaFiscalBairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_bairro");

            builder.Property(e => e.NotaFiscalCep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nota_fiscal_cep");

            builder.Property(e => e.NotaFiscalComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_complemento");

            builder.Property(e => e.NotaFiscalDocumento)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_cpf");

            builder.Property(e => e.NotaFiscalDdd)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nota_fiscal_ddd");

            builder.Property(e => e.NotaFiscalEmail)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_email");

            builder.Property(e => e.NotaFiscalEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_endereco");

            builder.Property(e => e.NotaFiscalInscricaoMunicipal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_inscricao_municipal");

            builder.Property(e => e.NotaFiscalMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_municipio");

            builder.Property(e => e.NotaFiscalNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_nome");

            builder.Property(e => e.NotaFiscalNumero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_numero");

            builder.Property(e => e.NotaFiscalTelefone)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("nota_fiscal_telefone");

            builder.Property(e => e.NotaFiscalUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nota_fiscal_uf");

            builder.Property(e => e.ProprietarioBairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proprietario_bairro");

            builder.Property(e => e.ProprietarioCep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("proprietario_cep");

            builder.Property(e => e.ProprietarioComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proprietario_complemento");

            builder.Property(e => e.ProprietarioDdd)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("proprietario_ddd");

            builder.Property(e => e.ProprietarioDocumento)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("proprietario_documento");

            builder.Property(e => e.ProprietarioEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("proprietario_endereco");

            builder.Property(e => e.ProprietarioTipoDocumentoId).HasColumnName("proprietario_id_tipo_documento");

            builder.Property(e => e.ProprietarioMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proprietario_municipio");

            builder.Property(e => e.ProprietarioNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("proprietario_nome");

            builder.Property(e => e.ProprietarioNumero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("proprietario_numero");

            builder.Property(e => e.ProprietarioTelefone)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("proprietario_telefone");

            builder.Property(e => e.ProprietarioUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("proprietario_uf");

            builder.Property(e => e.ResponsavelBairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("responsavel_bairro");

            builder.Property(e => e.ResponsavelCep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("responsavel_cep");

            builder.Property(e => e.ResponsavelCnh)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("responsavel_cnh");

            builder.Property(e => e.ResponsavelComplemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("responsavel_complemento");

            builder.Property(e => e.ResponsavelDdd)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("responsavel_ddd");

            builder.Property(e => e.ResponsavelDocumento)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("responsavel_documento");

            builder.Property(e => e.ResponsavelEndereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_endereco");

            builder.Property(e => e.ResponsavelMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("responsavel_municipio");

            builder.Property(e => e.ResponsavelNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsavel_nome");

            builder.Property(e => e.ResponsavelNumero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("responsavel_numero");

            builder.Property(e => e.ResponsavelTelefone)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("responsavel_telefone");

            builder.Property(e => e.ResponsavelUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("responsavel_uf");

            builder.Property(e => e.StatusCadastroOrdensVendaERP)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status_cadastro_ordens_venda_sap");

            builder.Property(e => e.StatusCadastroERP)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status_cadastro_sap");

            builder.Property(e => e.TotalImpressoes)
                .HasColumnName("total_impressoes");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.UsuarioCadastroAtendimentos)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioAlteracao).WithMany(p => p.UsuarioAlteracaoAtendimentos)
                .HasForeignKey(d => d.UsuarioAlteracaoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}