using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class FileConfiguration : BaseModelConfiguration<File>
{
    public override void Configure(EntityTypeBuilder<File> builder)
    {
        Sequence = "SEQ_FILE_ID";
        builder.HasKey(e => e.Id).HasName("PK_FILE_ID");
        builder.ToTable("FILE");
        builder.Property(e => e.Name).HasMaxLength(256).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Extension).HasMaxLength(64).IsUnicode(false).HasColumnName("EXTENSION");
        builder.Property(e => e.Type).HasMaxLength(64).IsUnicode(false).HasColumnName("TYPE");
        builder.Property(e => e.Length).HasPrecision(20).HasColumnName("LENGTH");
        builder.Property(e => e.Path).HasMaxLength(512).IsUnicode(false).HasColumnName("PATH");
        builder.Property(e => e.Hash).HasMaxLength(128).IsUnicode(false).HasColumnName("HASH");
        base.Configure(builder);
    }
}
