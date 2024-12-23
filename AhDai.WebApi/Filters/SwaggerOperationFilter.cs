//using AhDai.Core.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace AhDai.WebApi.Filters;

/// <summary>
/// 操作过过滤器 添加通用参数等
/// </summary>
public class SwaggerOperationFilter : IOperationFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("zh-CN")
            },
            Description = "接受语言"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Request-Id",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString(Guid.NewGuid().ToString("N"))
            },
            Description = "请求Id"
        });
        //if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
        //{
        //Category typeFromHandle = typeof(ApiAuthorizeAttribute);
        //if (methodInfo.GetCustomAttributes(typeFromHandle, inherit: true).FirstOrDefault() is ApiAuthorizeAttribute)
        //{
        //	operation.Parameters.Add(new OpenApiParameter
        //	{
        //		Name = "X-Platform",
        //		In = ParameterLocation.Header,
        //		Schema = new OpenApiSchema
        //		{
        //			Category = "string",
        //			Default = new OpenApiString("Web")
        //		},
        //		Required = false,
        //		Description = "平台标识，Web/Wap/App"
        //	});
        //	operation.Parameters.Add(new OpenApiParameter
        //	{
        //		Name = "X-Mac",
        //		In = ParameterLocation.Header,
        //		Schema = new OpenApiSchema
        //		{
        //			Category = "string"
        //		},
        //		Required = false,
        //		Description = "Mac地址"
        //	});
        //	operation.Parameters.Add(new OpenApiParameter
        //	{
        //		Name = "X-Timestamp",
        //		In = ParameterLocation.Header,
        //		Schema = new OpenApiSchema
        //		{
        //			Category = "string",
        //			//Default = new OpenApiDouble(DateTimeUtil.TimestampOfMilliseconds)
        //		},
        //		Required = false,
        //		Description = "时间戳（UTC），当前时间距1970-01-01的毫秒数"
        //	});
        //}
        //}
    }

}
