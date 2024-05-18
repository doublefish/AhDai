using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

/// <summary>
/// BaseEntityConfiguration
/// </summary>
/// <typeparam name="TMainKey"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <param name="mainIdColumnName"></param>
internal abstract class BaseExtensionEntityConfiguration<TEntity>(string mainIdColumnName = "MainId") : BaseEntityConfiguration<TEntity>
	where TEntity : class, Entity.IBaseExtensionEntity
{
	public override void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.Property(e => e.MainId).HasColumnName(mainIdColumnName);
		builder.Property(e => e.Type);
		builder.Property(e => e.DigitalData);
		builder.Property(e => e.DigitalData1);
		builder.Property(e => e.DigitalData2);
		builder.Property(e => e.TextData);
		builder.Property(e => e.TextData1);
		builder.Property(e => e.TextData2);
		base.Configure(builder);
	}
}

