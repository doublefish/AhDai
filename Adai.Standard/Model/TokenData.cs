using System.Collections.Generic;

namespace Adai.Standard.Model
{
	/// <summary>
	/// TokenData
	/// </summary>
	public class TokenData
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="type">类型</param>
		public TokenData(string type = null)
		{
			Type = type;
		}

		/// <summary>
		/// 用户标识
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 头像
		/// </summary>
		public string Avatar { get; set; }
		/// <summary>
		/// 名
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// 姓
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 角色Id
		/// </summary>
		public ICollection<int> RoleIds { get; set; }
		/// <summary>
		/// 代理商Id
		/// </summary>
		public int AgentId { get; set; }
		/// <summary>
		/// 是否代理商管理员
		/// </summary>
		public bool IsAdminOfAgent { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 平台
		/// </summary>
		public string Platform { get; set; }
	}
}
