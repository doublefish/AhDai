using Adai.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Adai.Core.WebApi.Models;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// MvcController
	/// </summary>
	public class MvcController : Controller
	{
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
				DirectoryName = lastName.Substring(1);
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
				//重写输出内容
				var result = new ReturnResult<object>(ReturnCode.OK, null, value);
				context.Result = new ContentResult()
				{
					StatusCode = StatusCodes.Status200OK,
					Content = JsonHelper.SerializeObject(result),
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