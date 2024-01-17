using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class InterfaceConfiguration : BaseModelConfiguration<Interface>
{
    public override void Configure(EntityTypeBuilder<Interface> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_INTERFACE_ID");
        builder.ToTable("INTERFACE");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Method).HasMaxLength(8).IsUnicode(false).HasColumnName("METHOD");
        builder.Property(e => e.Url).HasMaxLength(256).IsUnicode(false).HasColumnName("URL");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
