using System.Collections.Generic;

namespace AhDai.Core.Models
{
	/// <summary>
	/// TokenData
	/// </summary>
	public class TokenData
	{
		/// <summary>
		/// 用户标识
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 平台
		/// </summary>
		public string Platform { get; set; }
		/// <summary>
		/// 扩展数据：存储键为 Ext-{key}
		/// </summary>
		public IDictionary<string, string> Extensions { get; set; }

	}
}
