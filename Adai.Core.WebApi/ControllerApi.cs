using Adai.Extension;
using Adai.Standard;
using Adai.Standard.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Adai.Core.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ControllerApi
	/// </summary>
	[ApiController]
	public abstract class ControllerApi : ControllerBase
	{
		string _UserId;
		string _Token;
		int _Platform;
		string _Mac;
		string _Ip;
		Token<TokenData> _LoginInfo;

		/// <summary>
		/// 构造函数
		/// </summary>
		public ControllerApi() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		public ControllerApi(IStringLocalizerFactory factory) : this(factory, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		/// <param name="webHostEnvironment"></param>
		public ControllerApi(IStringLocalizerFactory factory, IWebHostEnvironment webHostEnvironment)
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
		/// 用户Id
		/// </summary>
		protected string UserId
		{
			get
			{
				if (string.IsNullOrEmpty(_UserId))
				{
					var userValues = HttpContext.Request.Headers["Authorization"];
					if (userValues.Count > 0)
					{
						var token = userValues[0];
						if (!string.IsNullOrEmpty(token))
						{
							// token.Substring(7, token.Length - 7);
							token = token[7..];
						}

						if (!string.IsNullOrEmpty(token))
						{
							var jwt = new JwtSecurityTokenHandler();
							var jt = jwt.ReadJwtToken(token);

							using var claims = jt.Claims.GetEnumerator();
							while (claims.MoveNext())
							{
								var chain = claims.Current;
								if (chain != null && chain.Type == "open-id")
								{
									_UserId = chain.Value;
									break;
								}
							}
						}
					}
				}
				return _UserId;
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
		/// Json
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content">内容</param>
		/// <param name="message">消息</param>
		/// <returns></returns>
		protected static IActionResult<T> Json<T>(T content = default, string message = null)
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
		protected static IActionResult<T> Content<T>(T content, string contentType, string message = "success")
		{
			if (string.IsNullOrEmpty(message))
			{
				message = "success";
			}
			var code = message == "success" ? 0 : 1;
			return new ReturnResult<T>(code, message, content, contentType);
		}
	}
}