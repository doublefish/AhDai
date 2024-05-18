using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class DictConfiguration : BaseEntityConfiguration<Dict>
{
	public override void Configure(EntityTypeBuilder<Dict> builder)
	{
		builder.ToTable("Sys_Dict");
		base.Configure(builder);
	}
}
