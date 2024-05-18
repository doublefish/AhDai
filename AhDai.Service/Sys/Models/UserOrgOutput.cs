using System.Text.Json.Serialization;

namespace AhDai.Service.Sys.Models
{
	/// <summary>
	/// 用户所属组织
	/// </summary>
	public class UserOrgOutput
	{
		/// <summary>
		/// 用户Id
		/// </summary>
		[JsonIgnore]
		public long UserId { get; set; }
		/// <summary>
		/// 组织Id
		/// </summary>
		public long OrgId { get; set; }
		/// <summary>
		/// 组织名称
		/// </summary>
		public string? OrgName { get; set; }
		/// <summary>
		/// 数据权限
		/// </summary>
		public Shared.Enums.DataPermission DataPermission { get; set; }
		/// <summary>
		/// 是否默认值
		/// </summary>
		public bool IsDefault { get; set; }
	}
}
