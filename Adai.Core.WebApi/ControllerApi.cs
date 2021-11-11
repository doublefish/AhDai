using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Resources;

namespace Adai.WebApi
{
	/// <summary>
	/// ControllerApi
	/// </summary>
	[ApiController]
	public class ControllerApi : ControllerBase
	{
		string _UserId;
		string _RequestId;
		ResourceManager _SharedLocalizer;

		/// <summary>
		/// HostingEnvironment
		/// </summary>
		protected readonly IWebHostEnvironment WebHostEnvironment;

		/// <summary>
		/// 共享本地语言
		/// </summary>
		protected ResourceManager SharedLocalizer
		{
			get
			{
				if (_SharedLocalizer == null)
				{
					var type = GetType();
					_SharedLocalizer = new ResourceManager(type.FullName, type.Assembly);
				}
				return _SharedLocalizer;
			}
		}

		/// <summary>
		/// 请求Id
		/// </summary>
		protected string RequestId
		{
			get
			{
				if (string.IsNullOrEmpty(_RequestId))
				{
					_RequestId = HttpContext.Request.Headers[Const.RequestId];
				}
				return _RequestId;
			}
		}

		/// <summary>
		/// 用户Id
		/// </summary>
		protected string UserId
		{
			get
			{
				if (string.IsNullOrEmpty(_UserId))
				{
					_UserId = Utils.JwtHelper.GetClaimValue(HttpContext.Request, "open-id");
				}
				return _UserId;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public ControllerApi() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="webHostEnvironment"></param>
		public ControllerApi(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
		}

		/// <summary>
		/// Json
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content">内容</param>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected Standard.Interfaces.IActionResult<T> Json<T>(T content = default, string message = null)
		{
			return Content(content, Standard.HttpContentType.Json, message);
		}

		/// <summary>
		/// Content
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content">内容</param>
		/// <param name="contentType">内容类型</param>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected Standard.Interfaces.IActionResult<T> Content<T>(T content, string contentType, string message = "success")
		{
			if (string.IsNullOrEmpty(message))
			{
				message = "success";
			}
			var code = message == "success" ? 0 : 1;
			return new Standard.Models.ActionResult<T>(RequestId, code, message, content, contentType);
		}
	}
}