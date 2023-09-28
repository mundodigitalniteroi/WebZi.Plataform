﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario.View;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    internal class ViewUsuarioClienteDepositoMap : IEntityTypeConfiguration<ViewUsuarioClienteDepositoModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioClienteDepositoModel> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_dep_usuarios_clientes_depositos");

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");
            
            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.UsuarioClienteId)
                .HasColumnName("id_usuario_cliente");

            builder.Property(e => e.UsuarioDepositoId)
                .HasColumnName("id_usuario_deposito");
            
            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");

            builder.Property(e => e.Senha1)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha1");

            builder.Property(e => e.UsuarioFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("usuario_flag_ativo");
        }
    }
}