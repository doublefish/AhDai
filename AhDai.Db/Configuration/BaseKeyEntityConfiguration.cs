using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace AhDai.Db.Configuration;

/// <summary>
/// BaseKeyEntityConfiguration
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal abstract class BaseKeyEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, Entity.IKeyEntity
{
	static readonly string[] types0 = [
		typeof(Entity.Sys.RoleMenu).Name,
		typeof(Entity.Sys.UserRole).Name,
		typeof(Entity.Sys.TenantMenu).Name,
	];

	static readonly string[] types1 = [];

	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		var type = typeof(TEntity);
		builder.HasKey(e => e.Id);
		//builder.Property(e => e.Id);
		if (types0.Contains(type.Name))
		{
			//数据库自增
		}
		else if (types1.Contains(type.Name))
		{
			//Yitter
			//builder.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<IdGenerator>();
		}
		else
		{
			//redis自增
			builder.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<IdGenerator<TEntity>>();
		}
	}
}

