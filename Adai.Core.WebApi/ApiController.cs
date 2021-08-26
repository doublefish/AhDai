using Adai.Extension;
using Adai.Standard;
using Adai.Standard.Extension;
using Adai.Standard.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Adai.Core.WebApi.Models;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ApiController
	/// </summary>
	[ApiController]
	public abstract class ApiController : ControllerBase
	{
		string _Token;
		int _Platform;
		string _Mac;
		string _Ip;
		Token<TokenData> _LoginInfo;

		/// <summary>
		/// 构造函数
		/// </summary>
		public ApiController() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		public ApiController(IStringLocalizerFactory factory) : this(factory, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		/// <param name="webHostEnvironment"></param>
		public ApiController(IStringLocalizerFactory factory, IWebHostEnvironment webHostEnvironment)
		{
			var type = GetType();
			if (factory != null)
			{
				Localizer = factory.Create(type);
			}
			WebHostEnvironment = webHostEnvironment;
		}

		/// <summary>
		/// HostingEnvironment
		/// </summary>
		protected readonly IWebHostEnvironment WebHostEnvironment;
		/// <summary>
		/// Localizer
		/// </summary>
		protected readonly IStringLocalizer Localizer;

		/// <summary>
		/// Token
		/// </summary>
		public string Token
		{
			get
			{
				if (_Token == null)
				{
					_Token = Request.Headers.GetValue("X-Token");
				}
				return _Token;
			}
		}

		/// <summary>
		/// 平台标识
		/// </summary>
		public int Platform
		{
			get
			{
				if (_Platform == 0)
				{
					var value = Request.Headers.GetValue("X-Platform");
					_Platform = value != null ? value.ToInt32(0) : 0;
				}
				return _Platform;
			}
		}

		/// <summary>
		/// Mac地址
		/// </summary>
		public string Mac
		{
			get
			{
				if (_Mac == null)
				{
					_Mac = Request.Headers.GetValue("X-Mac");
				}
				return _Mac;
			}
		}

		/// <summary>
		/// Ip地址
		/// </summary>
		public string Ip
		{
			get
			{
				if (_Ip == null)
				{
					_Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
				}
				return _Ip;
			}
		}

		/// <summary>
		/// 登录信息
		/// </summary>
		public Token<TokenData> LoginInfo
		{
			get
			{
				if (_LoginInfo == null && !string.IsNullOrEmpty(Token))
				{
					_LoginInfo = GetLogin(Token);
				}
				return _LoginInfo;
			}
		}

		/// <summary>
		/// 读取登录信息
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		protected abstract Token<TokenData> GetLogin(string token);

		/// <summary>
		/// Ok
		/// </summary>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected ReturnResult<string> Ok(string message = null)
		{
			return Content("", HttpContentType.Json, message);
		}

		/// <summary>
		/// Json
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content">内容</param>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected ReturnResult<T> Json<T>(T content, string message = null)
		{
			return Content(content, HttpContentType.Json, message);
		}

		/// <summary>
		/// Content
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content">内容</param>
		/// <param name="contentType">内容类型</param>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected ReturnResult<T> Content<T>(T content, string contentType, string message = "success")
		{
			if (string.IsNullOrEmpty(message))
			{
				message = "success";
			}
			var code = message == "success" ? ReturnCode.OK : ReturnCode.CustomException;
			return new ReturnResult<T>(code, message, content, contentType);
		}
	}
}