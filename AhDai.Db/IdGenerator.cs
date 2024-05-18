using AhDai.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace AhDai.Db;

/// <summary>
/// IdGenerator
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class IdGenerator<TEntity> : ValueGenerator<long>
	where TEntity : class, IKeyEntity
{
	readonly MyIdGenerator<TEntity> _idGenerator;

	public IdGenerator()
	{
		_idGenerator = new MyIdGenerator<TEntity>();
	}

	public override bool GeneratesTemporaryValues => false;

	public override long Next(EntityEntry entry)
	{
		ArgumentNullException.ThrowIfNull(entry, nameof(entry));
		return _idGenerator.Next(entry.Context);
	}
}
