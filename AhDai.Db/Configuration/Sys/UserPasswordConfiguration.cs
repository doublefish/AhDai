using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class UserPasswordConfiguration : BaseEntityConfiguration<UserPassword>
{
    public override void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        builder.ToTable("Sys_UserPassword");
        base.Configure(builder);
    }
}
