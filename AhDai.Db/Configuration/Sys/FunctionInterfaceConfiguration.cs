using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class FunctionInterfaceConfiguration : BaseEntityConfiguration<FunctionInterface>
{
    public override void Configure(EntityTypeBuilder<FunctionInterface> builder)
    {
        builder.ToTable("Sys_FunctionInterface");
        base.Configure(builder);
    }
}
