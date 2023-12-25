using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class DictDatumConfiguration : BaseModelConfiguration<DictDatum>
{
    public override void Configure(EntityTypeBuilder<DictDatum> builder)
    {
        Sequence = "SEQ_DICT_DATUM_ID";
        builder.HasKey(e => e.Id).HasName("PK_DICT_DATUM_ID");
        builder.ToTable("DICT_DATUM");
        builder.Property(e => e.DictId).HasPrecision(10).HasColumnName("DICT_ID");
        builder.Property(e => e.Code).HasMaxLength(128).IsUnicode(false).HasColumnName("CODE");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Value).HasMaxLength(256).IsUnicode(false).HasColumnName("VALUE");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Sort).HasPrecision(10).HasColumnName("SORT");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
