using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class InterfaceConfiguration : BaseEntityConfiguration<Interface>
{
    public override void Configure(EntityTypeBuilder<Interface> builder)
    {
        builder.ToTable("Sys_Interface");
        base.Configure(builder);
    }
}
