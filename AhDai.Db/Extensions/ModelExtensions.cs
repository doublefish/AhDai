using AhDai.Core.Models;
using AhDai.Db.Models;
using System;
using System.Data;

namespace AhDai.Db.Extensions;

public static class ModelExtensions
{
	/// <summary>
	/// 设置创建信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="model"></param>
	public static void SetCreateInfo<T>(this T model) where T : BaseModel
	{
		// TODO
		//SetCreateInfo(model, MyApp.GetCurrentTokenData());
	}

	/// <summary>
	/// 设置创建信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="model"></param>
	/// <param name="token"></param>
	public static void SetCreateInfo<T>(this T model, TokenData token) where T : BaseModel
	{
		var time = DateTime.Now;
		model.RowCreateUser = 0;
		model.RowCreateUsername = token.Username;
		model.RowCreateTime = time;
		model.RowUpdateUser = 0;
		model.RowUpdateUsername = token.Username;
		model.RowUpdateTime = time;
	}

	/// <summary>
	/// 设置更新信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="model"></param>
	public static void SetUpdateInfo<T>(this T model) where T : BaseModel
	{
		//SetUpdateInfo(model, MyApp.GetCurrentTokenData());
	}

	/// <summary>
	/// 设置更新信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="model"></param>
	/// <param name="token"></param>
	public static void SetUpdateInfo<T>(this T model, TokenData token) where T : BaseModel
	{
		if (token == null)
		{
			return;
		}
		var time = DateTime.Now;
		model.RowVersion += 1;
		model.RowUpdateUser = 0;
		model.RowUpdateUsername = token.Username;
		model.RowUpdateTime = time;
	}

	/// <summary>
	/// 设置基础信息
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="model"></param>
	/// <param name="reader"></param>
	/// <param name="i"></param>
	/// <returns></returns>
	public static int SetBaseInfo<T>(this T model, IDataReader reader, int i) where T : BaseModel
	{
		model.Id = reader.GetInt32(0);
		model.RowVersion = reader.GetInt32(i++);
		model.RowCreateUser = reader.GetInt32(i++);
		model.RowCreateUsername = reader.GetString(i++);
		model.RowCreateTime = reader.GetDateTime(i++);
		model.RowUpdateUser = reader.GetInt32(i++);
		model.RowUpdateUsername = reader.GetString(i++);
		model.RowUpdateTime = reader.GetDateTime(i++);
		model.RowDeleted = reader.GetString(i++) == "1";
		return i;
	}
}
