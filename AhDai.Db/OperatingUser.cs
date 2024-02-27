using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Db;

/// <summary>
/// 操作用户
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="id"></param>
/// <param name="username"></param>
public class OperatingUser(long id, string username)
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; } = id;
	/// <summary>
	/// 用户名
	/// </summary>
	public string Username { get; set; } = username;
	/// <summary>
	/// 租户Id
	/// </summary>
	public long TenantId { get; set; }
}