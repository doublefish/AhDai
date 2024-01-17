using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class UserFunctionConfiguration : BaseModelConfiguration<UserFunction>
{
    public override void Configure(EntityTypeBuilder<UserFunction> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_USER_FUNCTION_ID");
        builder.ToTable("USER_FUNCTION");
        builder.Property(e => e.UserId).HasPrecision(10).HasColumnName("USER_ID");
        builder.Property(e => e.MenuId).HasPrecision(10).HasColumnName("MENU_ID");
        builder.Property(e => e.FunctionId).HasPrecision(10).HasColumnName("FUNCTION_ID");
        base.Configure(builder);
    }
}
