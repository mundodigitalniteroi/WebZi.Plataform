using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    internal class WebServiceUrlMap : IEntityTypeConfiguration<WebServiceUrlModel>
    {
        public void Configure(EntityTypeBuilder<WebServiceUrlModel> builder)
        {
            builder
                .ToTable("tb_dep_ws_urls", "dbo")
                .HasKey(x => x.UrlId);

            builder.Property(e => e.UrlId)
                .ValueGeneratedOnAdd()
                .HasColumnName("WsUrlId");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("WsName");

            builder.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("WsUrl");

            builder.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("WsUsername");

            builder.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("WsPassword");
        }
    }
}