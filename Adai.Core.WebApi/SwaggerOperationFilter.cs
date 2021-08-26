using Adai;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// 操作过过滤器 添加通用参数等
	/// </summary>
	public class SwaggerOperationFilter : IOperationFilter
	{
		/// <summary>
		/// Apply
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="context"></param>
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			//operation.Parameters ??= new List<IParameter>();
			operation.Parameters.Add(new OpenApiParameter()
			{
				Name = "Accept-Language",
				In = ParameterLocation.Header,
				Required = true,
				Schema = new OpenApiSchema() { Type = "string", Default = new OpenApiString("zh-CN") },
				Description = "接受语言"
			});
			operation.Parameters.Add(new OpenApiParameter()
			{
				Name = "X-Version",
				In = ParameterLocation.Header,
				Required = true,
				Schema = new OpenApiSchema() { Type = "string", Default = new OpenApiString("1.0") },
				Description = "接口版本"
			});

			if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
			{
				var type = typeof(ApiAuthorizeAttribute);
				if (methodInfo.GetCustomAttributes(type, true).FirstOrDefault() is ApiAuthorizeAttribute author)
				{
					//选项
					//var options = new List<IOpenApiAny>() { new OpenApiInteger(1), new OpenApiInteger(2), new OpenApiInteger(3), new OpenApiInteger(4) };
					operation.Parameters.Add(new OpenApiParameter()
					{
						Name = "X-Platform",
						In = ParameterLocation.Header,
						Schema = new OpenApiSchema() { Type = "integer", Default = new OpenApiInteger(1) },
						Required = true,
						Description = "平台标识，1：Web，2：PC，3：Wap，4：App"
					});
					operation.Parameters.Add(new OpenApiParameter()
					{
						Name = "X-Mac",
						In = ParameterLocation.Header,
						Schema = new OpenApiSchema() { Type = "string" },
						Required = false,
						Description = "Mac地址"
					});
					operation.Parameters.Add(new OpenApiParameter()
					{
						Name = "X-Timestamp",
						In = ParameterLocation.Header,
						Schema = new OpenApiSchema() { Type = "string", Default = new OpenApiDouble(DateTimeHelper.TimestampOfMilliseconds) },
						Required = true,
						Description = "时间戳（UTC），当前时间距1970-01-01的毫秒数"
					});
					operation.Parameters.Add(new OpenApiParameter()
					{
						Name = "X-Token",
						In = ParameterLocation.Header,
						Schema = new OpenApiSchema() { Type = "string" },
						Required = author.VerifyToken,
						Description = "身份验证票据"
					});
					if (author.VerifyCode)
					{
						operation.Parameters.Add(new OpenApiParameter()
						{
							Name = "X-VGuid",
							In = ParameterLocation.Header,
							Schema = new OpenApiSchema() { Type = "string" },
							Required = true,
							Description = "图片验证码标识"
						});
						operation.Parameters.Add(new OpenApiParameter()
						{
							Name = "X-VCode",
							In = ParameterLocation.Header,
							Schema = new OpenApiSchema() { Type = "string" },
							Required = true,
							Description = "图片验证码"
						});
					}
				}
			}
			Init(operation, context);
		}

		/// <summary>
		/// Init
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="context"></param>
		public virtual void Init(OpenApiOperation operation, OperationFilterContext context)
		{

		}
	}
}
