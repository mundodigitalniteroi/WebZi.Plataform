using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario;

public class AuthMfaCodesMap : IEntityTypeConfiguration<AuthMfaCodesModel>
{
    public void Configure(EntityTypeBuilder<AuthMfaCodesModel> builder)
    {
        builder.ToTable("auth_mfa_codes")
            .HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CodeHash)
            .HasColumnName("code_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.Property(x => x.Attempts)
            .HasColumnName("attempts")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder.Property(x => x.Validated)
            .HasColumnName("validated")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();
     
        builder.Property(x => x.UsuarioId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}