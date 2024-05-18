using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class UserOrgConfiguration : BaseEntityConfiguration<UserOrg>
{
	public override void Configure(EntityTypeBuilder<UserOrg> builder)
	{
		builder.ToTable("Sys_UserOrg");
		base.Configure(builder);
	}
}
