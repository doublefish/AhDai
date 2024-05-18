using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class FileConfiguration : BaseEntityConfiguration<File>
{
    public override void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("Sys_File");
        base.Configure(builder);
    }
}
