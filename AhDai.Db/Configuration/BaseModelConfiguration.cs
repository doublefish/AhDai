using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

/// <summary>
/// BaseModelConfiguration
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal abstract class BaseModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
		builder.Property(e => e.Id).ValueGeneratedNever().HasColumnName("Id").HasComment("ID");
		builder.Property(e => e.RowVersion).HasColumnName("RowVersion").HasComment("版本");
        builder.Property(e => e.RowCreateUser).HasColumnName("RowCreateUser").HasComment("创建用户");
        builder.Property(e => e.RowCreateUsername).IsRequired().HasMaxLength(64).HasColumnName("RowCreateUsername").HasComment("创建用户名");
        builder.Property(e => e.RowCreateTime).HasColumnType("datetime").HasColumnName("RowCreateTime").HasComment("创建时间");
        builder.Property(e => e.RowUpdateUser).HasColumnName("RowUpdateUser").HasComment("更新用户");
        builder.Property(e => e.RowUpdateUsername).IsRequired().HasMaxLength(64).HasColumnName("RowUpdateUsername").HasComment("更新用户名");
        builder.Property(e => e.RowUpdateTime).HasColumnType("datetime").HasColumnName("RowUpdateTime").HasComment("更新时间");
        builder.Property(e => e.RowDeleted).HasColumnName("RowDeleted").HasComment("删除标识");
    }
}
