using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class UserConfiguration : BaseModelConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_USER_ID");
        builder.ToTable("USER");
        builder.HasIndex(e => e.Username, "UNQ_USER_USERNAME").IsUnique();
        builder.Property(e => e.Username).HasMaxLength(128).IsUnicode(false).HasColumnName("USERNAME");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
