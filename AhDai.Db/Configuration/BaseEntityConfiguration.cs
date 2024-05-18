using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

/// <summary>
/// BaseEntityConfiguration
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal abstract class BaseEntityConfiguration<TEntity> : BaseKeyEntityConfiguration<TEntity> where TEntity : class, Entity.IBaseEntity
{

	public override void Configure(EntityTypeBuilder<TEntity> builder)
	{
		base.Configure(builder);
	}
}

