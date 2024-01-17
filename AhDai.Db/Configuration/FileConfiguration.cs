using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class FileConfiguration : BaseModelConfiguration<File>
{
	public override void Configure(EntityTypeBuilder<File> builder)
	{
		builder.ToTable("File", tb => tb.HasComment("文件"));
		builder.Property(e => e.Name).HasMaxLength(256).HasColumnName("Name").HasComment("名称");
		builder.Property(e => e.Extension).HasMaxLength(32).IsUnicode(false).HasColumnName("Extension").HasComment("扩展名");
		builder.Property(e => e.Type).HasMaxLength(32).IsUnicode(false).HasColumnName("Type").HasComment("类型");
		builder.Property(e => e.Length).HasColumnName("Length").HasComment("大小");
		builder.Property(e => e.Path).HasMaxLength(512).IsUnicode(false).HasColumnName("Path").HasComment("路径");
		builder.Property(e => e.Hash).HasMaxLength(128).IsUnicode(false).HasColumnName("Hash").HasComment("哈希");
		base.Configure(builder);
	}
}
