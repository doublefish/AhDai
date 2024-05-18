using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class RoleMenuConfiguration : BaseKeyEntityConfiguration<RoleMenu>
{
	public override void Configure(EntityTypeBuilder<RoleMenu> builder)
	{
		builder.ToTable("Sys_RoleMenu");
		base.Configure(builder);
	}
}
