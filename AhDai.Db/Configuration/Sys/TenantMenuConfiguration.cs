using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class TenantMenuConfiguration : BaseKeyEntityConfiguration<TenantMenu>
{
	public override void Configure(EntityTypeBuilder<TenantMenu> builder)
	{
		builder.ToTable("Sys_TenantMenu");
		base.Configure(builder);
	}
}
