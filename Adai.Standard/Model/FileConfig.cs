using System.Collections.Generic;

namespace Adai.Standard.Model
{
	/// <summary>
	/// 文件配置
	/// </summary>
	public class FileConfig
	{
		/// <summary>
		/// 大小限制
		/// </summary>
		public long MaxSize { get; set; }
		/// <summary>
		/// 扩展名
		/// </summary>
		public IDictionary<string, string[]> Extensions { get; set; }

		/// <summary>
		/// 大小限制说明
		/// </summary>
		public string MaxSizeNote => FileHelper.GetFileSize(MaxSize);
	}
}
