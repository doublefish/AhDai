using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AhDai.WebApi.Filters;

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
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		//base.Apply(swaggerDoc, context);
	}
}
