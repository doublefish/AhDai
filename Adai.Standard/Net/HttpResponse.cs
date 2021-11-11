using System;
using System.Net;

namespace Adai.Standard.Net
{
	/// <summary>
	/// Http响应数据
	/// </summary>
	public class HttpResponse
	{
		/// <summary>
		/// StatusCode
		/// </summary>
		public HttpStatusCode StatusCode { get; private set; }
		/// <summary>
		/// ResponseUri
		/// </summary>
		public Uri ResponseUri { get; private set; }
		/// <summary>
		/// StatusDescription
		/// </summary>
		public string StatusDescription { get; private set; }
		/// <summary>
		/// ContentLength
		/// </summary>
		public long ContentLength { get; private set; }
		/// <summary>
		/// ContentEncoding
		/// </summary>
		public string ContentEncoding { get; private set; }
		/// <summary>
		/// CharacterSet
		/// </summary>
		public string CharacterSet { get; private set; }
		/// <summary>
		/// ContentType
		/// </summary>
		public string ContentType { get; private set; }
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="httpWebResponse"></param>
		/// <param name="content"></param>
		public HttpResponse(HttpWebResponse httpWebResponse, string content = null)
		{
			StatusCode = httpWebResponse.StatusCode;
			ResponseUri = httpWebResponse.ResponseUri;
			StatusDescription = httpWebResponse.StatusDescription;
			ContentLength = httpWebResponse.ContentLength;
			ContentEncoding = httpWebResponse.ContentEncoding;
			CharacterSet = httpWebResponse.CharacterSet;
			ContentType = httpWebResponse.ContentType;
			Content = content;
		}
	}
}
