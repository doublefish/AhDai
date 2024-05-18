using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class UserFunctionConfiguration : BaseEntityConfiguration<UserFunction>
{
    public override void Configure(EntityTypeBuilder<UserFunction> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Sys_UuserFunction");
        base.Configure(builder);
    }
}
