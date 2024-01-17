using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class OrganizationConfiguration : BaseModelConfiguration<Organization>
{
    public override void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_ORGANIZATION_ID");
        builder.ToTable("ORGANIZATION");
        builder.Property(e => e.Code).HasMaxLength(128).IsUnicode(false).HasColumnName("CODE");
        builder.Property(e => e.Name).HasMaxLength(128).IsUnicode(false).HasColumnName("NAME");
        builder.Property(e => e.ParentId).HasPrecision(10).HasColumnName("PARENT_ID");
        builder.Property(e => e.Level).HasPrecision(10).HasColumnName("LEVEL");
        builder.Property(e => e.Type).HasPrecision(10).HasColumnName("TYPE");
        builder.Property(e => e.Remark).HasMaxLength(512).IsUnicode(false).HasColumnName("REMARK");
        builder.Property(e => e.Status).HasPrecision(10).HasColumnName("STATUS");
        base.Configure(builder);
    }
}
