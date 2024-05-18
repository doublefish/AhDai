using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class MenuConfiguration : BaseEntityConfiguration<Menu>
{
    public override void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Sys_Menu");
        base.Configure(builder);
    }
}
