using System.Collections.Generic;

namespace Adai.Standard.Net
{
	/// <summary>
	/// Http请求数据
	/// </summary>
	public class HttpRequest
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="contentType"></param>
		public HttpRequest(string url = "", string method = HttpMethod.Get, string contentType = HttpContentType.Url)
		{
			Url = url;
			Method = method;
			ContentType = contentType;
		}

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// 方法
		/// </summary>
		public string Method { get; set; }
		/// <summary>
		/// 内容类型
		/// </summary>
		public string ContentType { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 请求头
		/// </summary>
		public IDictionary<string, string> Headers { get; set; }
		/// <summary>
		/// 内容参数
		/// </summary>
		public IDictionary<string, string> Body { get; set; }
	}
}
