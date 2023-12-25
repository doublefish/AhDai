using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AhDai.Db.Configuration;

/// <summary>
/// BaseModelConfiguration
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal abstract class BaseModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
{
    public string Schema { get; private set; }
    public string Sequence { get; protected set; }

    public BaseModelConfiguration(string schema = "")
    {
        Schema = schema;
    }


    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        Schema = builder.Metadata.GetDefaultSchema();
		//builder.Property(e => e.Id).HasPrecision(10).HasColumnName("ID").ValueGeneratedOnAdd()
		//          .HasValueGenerator((e, t) => new SequenceValueGenerator(Schema, Sequence));
		builder.Property(e => e.Id).HasPrecision(10).HasColumnName("ID");
		builder.Property(e => e.RowVersion).HasPrecision(10).HasColumnName("ROW_VERSION");
        builder.Property(e => e.RowCreateUser).HasPrecision(10).HasColumnName("ROW_CREATE_USER");
        builder.Property(e => e.RowCreateUsername).HasMaxLength(64).IsUnicode(false).HasColumnName("ROW_CREATE_USERNAME");
        builder.Property(e => e.RowCreateTime).HasPrecision(6).HasColumnName("ROW_CREATE_TIME");
        builder.Property(e => e.RowUpdateUser).HasPrecision(10).HasColumnName("ROW_UPDATE_USER");
        builder.Property(e => e.RowUpdateUsername).HasMaxLength(64).IsUnicode(false).HasColumnName("ROW_UPDATE_USERNAME");
        builder.Property(e => e.RowUpdateTime).HasPrecision(6).HasColumnName("ROW_UPDATE_TIME");
        //entity.Property(e => e.RowDeleted).HasMaxLength(1).IsUnicode(false).IsFixedLength().HasColumnName("ROW_DELETED");
        builder.Property(e => e.RowDeleted).HasConversion(p => p ? '1' : '0', c => c == '1').HasColumnName("ROW_DELETED");
    }
}
