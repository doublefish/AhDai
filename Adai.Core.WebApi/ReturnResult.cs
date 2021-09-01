using Adai.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// 返回结果
	/// </summary>
	public class ReturnResult<T> : IActionResult<T>
	{
		/// <summary>
		/// 状态代码
		/// </summary>
		public int Code { get; set; }
		/// <summary>
		/// 消息
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// 结果
		/// </summary>
		public T Content { get; set; }
		/// <summary>
		/// 内容类型
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="content">内容</param>
		/// <param name="contentType">内容类型</param>
		public ReturnResult(T content = default, string contentType = HttpContentType.Json) : this(0, "success", content, contentType)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="code">结果代码</param>
		/// <param name="message">消息</param>
		/// <param name="content">内容</param>
		/// <param name="contentType">内容类型</param>
		public ReturnResult(int code, string message, T content = default, string contentType = HttpContentType.Json)
		{
			Code = code;
			Message = message;
			Content = content;
			ContentType = contentType;
		}

		/// <summary>
		/// ExecuteResultAsync
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public Task ExecuteResultAsync(ActionContext context)
		{
			var text = ContentType switch
			{
				HttpContentType.Xml or HttpContentType.TextXml => "待实现",
				_ => JsonHelper.SerializeObject(this),
			};
			//var bytes = Encoding.Default.GetBytes(text);
			context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
			context.HttpContext.Response.ContentType = ContentType;
			//context.HttpContext.Response.ContentLength = bytes.Length;
			context.HttpContext.Response.WriteAsync(text);
			return Task.CompletedTask;
		}
	}
}