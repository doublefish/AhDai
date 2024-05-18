using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class FunctionConfiguration : BaseEntityConfiguration<Function>
{
    public override void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.ToTable("Sys_Function");
        base.Configure(builder);
    }
}
