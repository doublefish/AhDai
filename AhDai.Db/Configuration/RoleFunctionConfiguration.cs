using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class RoleFunctionConfiguration : BaseModelConfiguration<RoleFunction>
{
    public override void Configure(EntityTypeBuilder<RoleFunction> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_ROLE_FUNCTION_ID");
        builder.ToTable("ROLE_FUNCTION");
        builder.Property(e => e.RoleId).HasPrecision(10).HasColumnName("ROLE_ID");
        builder.Property(e => e.MenuId).HasPrecision(10).HasColumnName("MENU_ID");
        builder.Property(e => e.FunctionId).HasPrecision(10).HasColumnName("FUNCTION_ID");
        base.Configure(builder);
    }
}
