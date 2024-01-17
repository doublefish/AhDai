using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class MenuConfiguration : BaseModelConfiguration<Menu>
{
    public override void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_MENU_ID");
        builder.ToTable("MENU");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.ParentId).HasPrecision(10).HasColumnName("PARENT_ID");
        builder.Property(e => e.Type).HasPrecision(10).HasColumnName("TYPE");
        builder.Property(e => e.Path).HasMaxLength(256).IsUnicode(false).HasColumnName("PATH");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
