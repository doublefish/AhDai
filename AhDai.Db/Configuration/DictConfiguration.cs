using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class DictConfiguration : BaseModelConfiguration<Dict>
{
    public override void Configure(EntityTypeBuilder<Dict> builder)
    {
        builder.ToTable("Dict", tb => tb.HasComment("字典"));
        builder.Property(e => e.Code).IsRequired().HasMaxLength(64).HasColumnName("Code").HasComment("编码");
        builder.Property(e => e.Name).IsRequired().HasMaxLength(64).HasColumnName("Name").HasComment("名称");
        builder.Property(e => e.Remark).HasMaxLength(512).HasColumnName("Remark").HasComment("备注");
        builder.Property(e => e.Status).HasColumnName("Status").HasComment("状态");
        base.Configure(builder);
    }
}
