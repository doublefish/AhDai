using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class DictConfiguration : BaseModelConfiguration<Dict>
{
    public override void Configure(EntityTypeBuilder<Dict> builder)
    {
        Sequence = "SEQ_DICT_ID";
        builder.HasKey(e => e.Id).HasName("PK_DICT_ID");
        builder.ToTable("DICT");
        builder.Property(e => e.Code).HasMaxLength(128).IsUnicode(false).HasColumnName("CODE");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
