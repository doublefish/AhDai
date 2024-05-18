namespace AhDai.Db;

/// <summary>
/// 操作用户
/// </summary>
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
	/// 姓名
	/// </summary>
	public string Name { get; set; } = default!;
	/// <summary>
	/// 租户Id
	/// </summary>
	public long TenantId { get; set; }
	/// <summary>
	/// 租户名称
	/// </summary>
	public string TenantName { get; set; } = "";
	/// <summary>
	/// 租户类型
	/// </summary>
	public int TenantType { get; set; }
}
