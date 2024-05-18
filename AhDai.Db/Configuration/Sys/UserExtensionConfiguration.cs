using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class UserExtensionConfiguration : BaseEntityConfiguration<UserExtension>
{
	public override void Configure(EntityTypeBuilder<UserExtension> builder)
	{
		builder.ToTable("Sys_UserExtension");
		base.Configure(builder);
	}
}
