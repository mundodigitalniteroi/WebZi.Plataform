using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Data.Services.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    internal class WebServiceUrlMap : IEntityTypeConfiguration<WebServiceUrlModel>
    {
        public void Configure(EntityTypeBuilder<WebServiceUrlModel> builder)
        {
            builder
                .ToTable("tb_dep_ws_urls", "dbo")
                .HasKey(e => e.WsUrlId);

            builder.Property(e => e.WsUrlId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.WsName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.WsUrl)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.WsUsername)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.WsPassword)
                .HasMaxLength(32)
                .IsUnicode(false);
        }
    }
}