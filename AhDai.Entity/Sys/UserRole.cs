namespace AhDai.Entity.Sys;

/// <summary>
/// 用户角色
/// </summary>
public class UserRole : IKeyEntity
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 用户Id
	/// </summary>
	public long UserId { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long RoleId { get; set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	public UserRole() { }

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="userId"></param>
	/// <param name="roleId"></param>
	public UserRole(long userId, long roleId)
	{
		UserId = userId;
		RoleId = roleId;
	}
}
