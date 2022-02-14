using AhDai.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Resources;

namespace AhDai.WebApi
{
	/// <summary>
	/// ControllerMvc
	/// </summary>
	public class ControllerMvc : Controller
	{
		string _UserId;
		string _RequestId;
		ResourceManager _SharedLocalizer;

		/// <summary>
		/// 目录名称
		/// </summary>
		public string DirectoryName { get; set; }
		/// <summary>
		/// Controller
		/// </summary>
		public string ControllerName { get; set; }
		/// <summary>
		/// Action
		/// </summary>
		public string ActionName { get; set; }

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
		/// Action 执行前执行
		/// </summary>
		/// <param name="context"></param>
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			//读取 Controller 的目录
			var namespaceNames = context.Controller.GetType().Namespace.Split('.');
			var lastName = namespaceNames[^1];
			if (lastName != "Controllers")
			{
				DirectoryName = lastName[1..];
			}
			ControllerName = RouteData.Values["controller"].ToString();
			ActionName = RouteData.Values["action"].ToString();
			base.OnActionExecuting(context);
		}

		/// <summary>
		/// Action 执行后执行
		/// </summary>
		/// <param name="context"></param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Result is ObjectResult)
			{
				var value = (context.Result as ObjectResult).Value;
				// 重写输出内容
				var result = new Core.Models.ActionResult<object>(RequestId, 0, null, value);
				context.Result = new ContentResult()
				{
					StatusCode = StatusCodes.Status200OK,
					Content = Core.Utils.JsonHelper.SerializeObject(result),
					ContentType = HttpContentType.Json
				};
			}
			else
			{

			}
			base.OnActionExecuted(context);
		}

		/// <summary>
		/// 查找视图
		/// </summary>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public override ViewResult View(string viewName, object model)
		{
			//查找视图 默认命名空间以后的部分
			var dirName = string.Empty;//IsMobile ? "Mobile/" : string.Empty;
			if (!string.IsNullOrEmpty(DirectoryName))
			{
				dirName += DirectoryName + "/";
			}
			if (string.IsNullOrEmpty(viewName))
			{
				viewName = ActionName;
			}
			viewName = string.Format("~/Views/{0}{1}/{2}.cshtml", dirName, ControllerName, viewName);
			return base.View(viewName, model);
		}
	}
}