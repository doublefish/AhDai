using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration.Sys;

internal class OperationLogConfiguration : BaseEntityConfiguration<OperationLog>
{
    public override void Configure(EntityTypeBuilder<OperationLog> builder)
    {
        builder.ToTable("Sys_OperationLog");
        base.Configure(builder);
    }
}
