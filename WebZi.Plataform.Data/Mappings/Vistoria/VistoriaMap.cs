using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Vistoria;

namespace WebZi.Plataform.Data.Mappings.Vistoria
{
    public class VistoriaMap : IEntityTypeConfiguration<VistoriaModel>
    {
        public void Configure(EntityTypeBuilder<VistoriaModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_vistoria", "dbo")
                .HasKey(e => e.VistoriaId);

            builder.Property(e => e.VistoriaId)
                .HasColumnName("id_grv_vistoria")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.EstadoGeralVeiculo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("B: BOM;\r\nE: EXCELENTE;\r\nP: PÉSSIMO;\r\nR: RUIM")
                .HasColumnName("estado_geral_veiculo");
            
            builder.Property(e => e.EmpresaVistoriaId)
                .HasComment("Faz referência à Tabela db_global.dbo.tb_glo_emp_empresas")
                .HasColumnName("id_empresa_vistoria");
            
            builder.Property(e => e.VistoriaSituacaoChassiId)
                .HasColumnName("id_grv_vistoria_situacao_chassi");
            
            builder.Property(e => e.VistoriaStatusId)
                .HasColumnName("id_grv_vistoria_status");
            
            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");
            
            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");
            
            builder.Property(e => e.MotivoNaoRealizacaoVistoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("motivo_nao_realizacao_vistoria");
            
            builder.Property(e => e.NomeVistoriador)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("nome_vistoriador");
            
            builder.Property(e => e.NumeroMotor)
                .HasMaxLength(22)
                .IsUnicode(false)
                .HasColumnName("numero_motor");
            
            builder.Property(e => e.NumeroVistoria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_vistoria");
            
            builder.Property(e => e.ResumoVistoria)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("resumo_vistoria");

            builder.Property(e => e.TipoDirecao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("M: MANUAL;\r\nE: ELETRO HIDRÁULICA;\r\nH: HIDRÁULICA.")
                .HasColumnName("tipo_direcao");

            builder.Property(e => e.DataVistoria)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vistoria");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagPossuiPlaca)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_placa");

            builder.Property(e => e.FlagPossuiRestricoes)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_restricoes");

            builder.Property(e => e.FlagPossuiTravaEletrica)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_trava_eletrica");

            builder.Property(e => e.FlagPossuiVidroEletrico)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_vidro_eletrico");
        }
    }
}