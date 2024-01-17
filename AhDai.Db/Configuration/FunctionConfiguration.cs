using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class FunctionConfiguration : BaseModelConfiguration<Function>
{
    public override void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_FUNCTION_ID");
        builder.ToTable("FUNCTION");
        builder.Property(e => e.MenuId).HasPrecision(10).HasColumnName("MENU_ID");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
