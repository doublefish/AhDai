using Adai.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Core.WebApi.Models
{
	/// <summary>
	/// 返回结果
	/// </summary>
	public class ReturnResult<T> : IActionResult
	{
		/// <summary>
		/// 状态代码
		/// </summary>
		public ReturnCode Code { get; set; }
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
		private string ContentType { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="code">结果代码</param>
		/// <param name="message">消息</param>
		/// <param name="content">内容</param>
		/// <param name="contentType">内容类型</param>
		public ReturnResult(ReturnCode code = ReturnCode.OK, string message = "success", T content = default, string contentType = HttpContentType.Json)
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
			var json = JsonHelper.SerializeObject(this);
			var bytes = Encoding.Default.GetBytes(json);
			context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
			context.HttpContext.Response.ContentType = ContentType;
			context.HttpContext.Response.ContentLength = bytes.Length;
			context.HttpContext.Response.WriteAsync(json);
			return Task.CompletedTask;
		}
	}
}