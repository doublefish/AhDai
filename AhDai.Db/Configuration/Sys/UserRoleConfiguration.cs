using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class UserRoleConfiguration : BaseKeyEntityConfiguration<UserRole>
{
	public override void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("Sys_UserRole");
		base.Configure(builder);
	}
}
