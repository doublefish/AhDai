using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace AhDai.Core.Models
{
	/// <summary>
	/// Http响应数据
	/// </summary>
	public class HttpResponse
	{
		/// <summary>
		/// ResponseUri
		/// </summary>
		public Uri ResponseUri { get; set; }
		/// <summary>
		/// StatusCode
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }
		/// <summary>
		/// ReasonPhrase
		/// </summary>
		public string ReasonPhrase { get; set; }
		/// <summary>
		/// ContentType
		/// </summary>
		public MediaTypeHeaderValue ContentType { get; set; }
		/// <summary>
		/// ContentLength
		/// </summary>
		public long ContentLength { get; set; }
		/// <summary>
		/// ContentEncoding
		/// </summary>
		public ICollection<string> ContentEncoding { get; set; }
		/// <summary>
		/// ContentLanguage
		/// </summary>
		public ICollection<string> ContentLanguage { get; set; }
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }
	}
}
