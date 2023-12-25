using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class RoleConfiguration : BaseModelConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        Sequence = "SEQ_ROLE_ID";
        builder.HasKey(e => e.Id).HasName("PK_ROLE_ID");
        builder.ToTable("ROLE");
        builder.Property(e => e.Code).HasMaxLength(128).IsUnicode(false).HasColumnName("CODE");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
