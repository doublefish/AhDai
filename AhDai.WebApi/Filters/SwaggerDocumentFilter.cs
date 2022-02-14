using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AhDai.WebApi.Filters
{
	/// <summary>
	/// 添加控制器模块说明
	/// </summary>
	public class SwaggerDocumentFilter : IDocumentFilter
	{
		/// <summary>
		/// Apply
		/// </summary>
		/// <param name="swaggerDoc"></param>
		/// <param name="context"></param>
		public virtual void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			//swaggerDoc.Tags.Add(new OpenApiTag() { Name = "Home", Description = "Home" });
			//swaggerDoc.Tags.Add(new OpenApiTag() { Name = "VerifyCode", Description = "验证码" });
		}
	}
}
