namespace Adai.Core.Models
{
	/// <summary>
	/// 上传文件
	/// </summary>
	public class FileData
	{
		/// <summary>
		/// 唯一标识
		/// </summary>
		public string Guid { get; set; }
		/// <summary>
		/// 文件名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 后缀名
		/// </summary>
		public string Extension { get; set; }
		/// <summary>
		/// 大小
		/// </summary>
		public long Length { get; set; }
		/// <summary>
		/// 物理路径
		/// </summary>
		public string PhysicalPath { get; set; }
		/// <summary>
		/// 虚拟路径
		/// </summary>
		public string VirtualPath { get; set; }

		/// <summary>
		/// 扩展.文件全名
		/// </summary>
		public string FullName => $"{Guid}.{Extension}";

		/// <summary>
		/// 构造函数
		/// </summary>
		public FileData()
		{
		}
	}
}