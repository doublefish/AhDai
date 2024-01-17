using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class OperationLogConfiguration : BaseModelConfiguration<OperationLog>
{
    public override void Configure(EntityTypeBuilder<OperationLog> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_OPERATION_LOG_ID");
        builder.ToTable("OPERATION_LOG");
        builder.Property(e => e.MenuName).HasMaxLength(128).IsUnicode(false).HasColumnName("MENU_NAME");
        builder.Property(e => e.FunctionName).HasMaxLength(128).IsUnicode(false).HasColumnName("FUNCTION_NAME");
        builder.Property(e => e.ApiUrl).HasMaxLength(512).IsUnicode(false).HasColumnName("API_URL");
        builder.Property(e => e.Type).HasMaxLength(64).IsUnicode(false).HasColumnName("TYPE");
        builder.Property(e => e.Content).HasColumnType("LONG").HasColumnName("CONTENT");
        base.Configure(builder);
    }
}
