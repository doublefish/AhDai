﻿using System.Collections.Generic;

namespace Adai.Standard.Models
{
	/// <summary>
	/// 文件配置
	/// </summary>
	public class FileConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public FileConfig()
		{
			UploadDirectory = "upload";
			DownloadDirectory = "download";
		}

		/// <summary>
		/// 上传根目录
		/// </summary>
		public string UploadDirectory { get; set; }
		/// <summary>
		/// 下载根目录
		/// </summary>
		public string DownloadDirectory { get; set; }
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
		public string MaxSizeNote => Utils.FileHelper.GetFileSize(MaxSize);
	}
}