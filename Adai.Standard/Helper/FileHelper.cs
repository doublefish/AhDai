using Adai.Standard.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Adai.Standard
{
	/// <summary>
	/// FileHelper
	/// </summary>
	public static class FileHelper
	{
		/// <summary>
		/// configuration
		/// </summary>
		static FileConfig configuration;

		/// <summary>
		/// Config
		/// </summary>
		public static FileConfig Configuration
		{
			get
			{
				if (configuration == null)
				{
					var config = JsonConfigHelper.Get("Upload");
					if (config != null)
					{
						configuration = new FileConfig()
						{
							MaxSize = config.Value<long>("MaxSize"),
							Extensions = new Dictionary<string, string[]>()
						};
						var extensions = config.Value<JToken>("Extensions");
						var keys = new string[] { "Image", "File" };
						foreach (var key in keys)
						{
							var value = extensions.Value<string>(key);
							configuration.Extensions.Add(key, value.Split(","));
						}
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return configuration;
			}
		}
		/// <summary>
		/// 上传根目录
		/// </summary>
		public const string UploadDirectory = "upload";
		/// <summary>
		/// 下载根目录
		/// </summary>
		public const string DownloadDirectory = "download";

		/// <summary>
		/// 上传
		/// </summary>
		/// <param name="rootPath"></param>
		/// <param name="formFiles"></param>
		/// <returns></returns>
		public static async Task<ICollection<FileData>> UploadAsync(string rootPath, params IFormFile[] formFiles)
		{
			formFiles = formFiles.Where(o => o != null).ToArray();
			//If the request is correct, the binary data will be extracted from content and IIS stores files in specified location.
			if (formFiles.Length == 0)
			{
				throw new Exception("没有需要上传的文件");
			}

			//虚拟目录
			var dir = DateTime.Now.ToString("yyyy-MM-dd");
			var virtualDir = string.Format("{0}/{1}", UploadDirectory, dir);
			//物理路径
			var physicalPath = Path.Combine(rootPath, UploadDirectory, dir);
			if (!Directory.Exists(physicalPath))
			{
				Directory.CreateDirectory(physicalPath);
			}
			var datas = new HashSet<FileData>();
			foreach (var formFile in formFiles)
			{
				var data = new FileData()
				{
					Guid = Guid.NewGuid().ToString(),
					Name = Path.GetFileNameWithoutExtension(formFile.FileName),
					Extension = Path.GetExtension(formFile.FileName).Substring(1),
					Length = formFile.Length
				};
				foreach (var kv in Configuration.Extensions)
				{
					if (kv.Value.Contains(data.Extension))
					{
						break;
					}
					throw new Exception("不支持的文件类型");
				}
				if (formFile.Length > Configuration.MaxSize)
				{
					throw new Exception(string.Format("超出文件大小限制：{0}", Configuration.MaxSizeNote));
				}
				//物理路径
				data.PhysicalPath = Path.Combine(physicalPath, data.FullName);
				//虚拟路径
				data.VirtualPath = string.Format("{0}/{1}", virtualDir.Replace("\\", "/"), data.FullName);
				using (var stream = new FileStream(data.PhysicalPath, FileMode.Create))
				{
					await formFile.CopyToAsync(stream).ConfigureAwait(false);
				}
				datas.Add(data);
			}
			return datas;
		}

		/// <summary>
		/// 批量压缩
		/// </summary>
		/// <param name="rootPath"></param>
		/// <param name="files"></param>
		/// <returns></returns>
		public static string Compress(string rootPath, IDictionary<string, string> files)
		{
			if (files == null)
			{
				throw new ArgumentNullException("参数不能为空。");
			}
			var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

			//文件目录
			var folderPath = Path.Combine(rootPath, DownloadDirectory);
			//临时文件夹路径
			var tempFolderPath = Path.Combine(folderPath, timestamp);
			//创建临时文件夹
			Directory.CreateDirectory(tempFolderPath);

			foreach (var kv in files)
			{
				var sourceFileName = kv.Value;
				if (!File.Exists(sourceFileName))
				{
					continue;
				}
				var destFileName = Path.Combine(tempFolderPath, kv.Key);
				File.Copy(sourceFileName, destFileName);
			}

			//生成RAR文件
			var fileName = string.Format("{0}.rar", timestamp);
			RARHelper.Compress(tempFolderPath, folderPath, fileName);
			//清空临时文件夹
			Directory.Delete(tempFolderPath, true);
			var filePath = Path.Combine(folderPath, fileName);
			return filePath;
		}

		/// <summary>
		/// 输出图片
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static HttpResponseMessage OutputImage(string path)
		{
			if (!File.Exists(path))
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}
			var bytes = File.ReadAllBytes(path);
			var extension = Path.GetExtension(path);
			var type = string.Format("image/{0}", extension.Substring(1));
			var name = string.Format("{0}{1}", Guid.NewGuid().ToString(), extension);
			return Output(bytes, type, name);
		}

		/// <summary>
		/// 输出文件
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static HttpResponseMessage Output(byte[] bytes, string type, string name)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("参数不能为空。");
			}
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(bytes)
			};
			response.Content.Headers.ContentLength = bytes.Length;
			response.Content.Headers.ContentType = new MediaTypeHeaderValue(type);
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = HttpUtility.UrlEncode(name, Encoding.UTF8)
			};
			return response;
		}

		/// <summary>
		/// 输出文件
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static HttpResponseMessage Output(Stream stream, string type, string name)
		{
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(stream)
			};
			//response.Content.Headers.ContentLength = stream.Length;
			response.Content.Headers.ContentType = new MediaTypeHeaderValue(type);
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = HttpUtility.UrlEncode(name, Encoding.UTF8)
			};
			return response;
		}

		/// <summary>
		/// 转换为物理路径
		/// </summary>
		/// <param name="path"></param>
		/// <param name="separator"></param>
		/// <returns></returns>
		public static string ToPhysicalPath(string path, char separator = '/')
		{
			var paths = path.Split(separator);
			return Path.Combine(paths);
		}

		/// <summary>
		/// 换算文件大小
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string GetFileSize(long b)
		{
			var size = (double)b;
			var units = new string[] { "B", "KB", "MB", "GB", "TB" };
			var unit = units[0];
			var i = 0;
			while (size > 100 && i < units.Length)
			{
				size /= 1024;
				unit = units[i + 1];
				i++;
			}
			size = Math.Round(size, 2);
			return size + unit;
		}
	}
}
