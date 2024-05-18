using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class RoleFunctionConfiguration : BaseEntityConfiguration<RoleFunction>
{
    public override void Configure(EntityTypeBuilder<RoleFunction> builder)
    {
        builder.ToTable("Sys_RoleFunction");
        base.Configure(builder);
    }
}
