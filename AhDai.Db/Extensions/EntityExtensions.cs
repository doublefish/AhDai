using AhDai.Entity;
using System;

namespace AhDai.Db.Extensions;

/// <summary>
/// EntityExtensions
/// </summary>
public static class EntityExtensions
{
	/// <summary>
	/// 设置创建信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <param name="user"></param>
	public static void SetCreateInfo<T>(this T entity, OperatingUser user) where T : class, IBaseEntity
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(user);
		entity.CreationTime = DateTime.Now;
		entity.CreatorId = user.Id;
		entity.CreatorUsername = user.Username;
		if (entity is ITenantEntity entity1)
		{
			if (entity1.TenantId == 0)
			{
				entity1.TenantId = user.TenantId;
			}
		}
	}

	/// <summary>
	/// 设置修改信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <param name="user"></param>
	public static void SetModifyInfo<T>(this T entity, OperatingUser user) where T : class, IBaseEntity
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(user);
		entity.Version += 1;
		entity.ModificationTime = DateTime.Now;
		entity.ModifierId = user.Id;
		entity.ModifierUsername = user.Username;
	}

	/// <summary>
	/// 设置删除信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <param name="user"></param>
	public static void SetDeleteInfo<T>(this T entity, OperatingUser user) where T : class, IBaseEntity
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(user);
		entity.Version += 1;
		entity.IsDeleted = true;
		entity.DeleterId = user.Id;
		entity.DeleterUsername = user.Username;
		entity.DeletionTime = DateTime.Now;
	}
}
