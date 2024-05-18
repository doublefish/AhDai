using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models
{
    /// <summary>
    /// 用户所属组织
    /// </summary>
    public class UserOrgInput
	{
		/// <summary>
		/// 组织Id
		/// </summary>
		[Required]
		public long OrgId { get; set; }
		/// <summary>
		/// 数据权限
		/// </summary>
		[Required]
		public Shared.Enums.DataPermission DataPermission { get; set; }
		/// <summary>
		/// 是否默认值
		/// </summary>
		public bool IsDefault { get; set; }
	}
}
