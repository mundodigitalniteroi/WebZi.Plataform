﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class CondutorEquipamentoOpcionalMap : IEntityTypeConfiguration<CondutorEquipamentoOpcionalModel>
    {
        public void Configure(EntityTypeBuilder<CondutorEquipamentoOpcionalModel> builder)
        {
            builder
                .ToTable("tb_dep_condutor_equipamentos_opcionais", "dbo", tb => tb.HasTrigger("tr_log_upd_condutor_equipamentos_opcionais"))
                .HasKey(x => x.CondutorEquipamentoOpcionalId);

            builder.Property(e => e.CondutorEquipamentoOpcionalId)
                .HasColumnName("id_condutor_equipamento_opcional")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_atualizacao");

            builder.Property(e => e.FlagEquipamentoAvariado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("avariado");

            builder.Property(e => e.CodigoAvaria).HasColumnName("cod_avaria");

            builder.Property(e => e.DataAtualizacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_atualizacao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.FlagPossuiEquipamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_possui_equipamento");

            builder.Property(e => e.EquipamentoOpcionalId)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("id_equipamento_opcional");
        }
    }
}