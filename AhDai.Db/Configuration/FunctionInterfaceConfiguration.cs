using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class FunctionInterfaceConfiguration : BaseModelConfiguration<FunctionInterface>
{
    public override void Configure(EntityTypeBuilder<FunctionInterface> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_FUNCTION_INTERFACE_ID");
        builder.ToTable("FUNCTION_INTERFACE");
        builder.Property(e => e.FunctionId).HasPrecision(10).HasColumnName("FUNCTION_ID");
        builder.Property(e => e.InterfaceId).HasPrecision(10).HasColumnName("INTERFACE_ID");
        base.Configure(builder);
    }
}
