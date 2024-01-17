using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class DictDatumConfiguration : BaseModelConfiguration<DictDatum>
{
    public override void Configure(EntityTypeBuilder<DictDatum> builder)
    {
        builder.ToTable("DictDatum", tb => tb.HasComment("字典数据"));
        builder.Property(e => e.DictId).HasColumnName("DictId").HasComment("字典Id");
        builder.Property(e => e.Code).HasMaxLength(64).HasColumnName("Code").HasComment("编码");
        builder.Property(e => e.Name).HasMaxLength(64).HasColumnName("Name").HasComment("名称");
        builder.Property(e => e.Value).HasMaxLength(512).HasColumnName("Value").HasComment("值");
        builder.Property(e => e.Remark).HasMaxLength(512).HasColumnName("Remark").HasComment("备注");
        builder.Property(e => e.Sort).HasColumnName("Sort").HasComment("顺序");
        builder.Property(e => e.Status).HasColumnName("Status").HasComment("状态");
        base.Configure(builder);
    }
}
