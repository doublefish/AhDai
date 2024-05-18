using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class EmployeeConfiguration : BaseEntityConfiguration<Employee>
{
	public override void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.ToTable("Sys_Employee");
		base.Configure(builder);
	}
}
