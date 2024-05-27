using AhDai.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <exception cref="System.NotImplementedException"></exception>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //var fileParameters = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile)).ToArray();
        //if (fileParameters.Length > 0)
        //{
        //    operation.Parameters.Clear();
        //    foreach (var parameter in fileParameters)
        //    {
        //        if (parameter.Name == null) continue;
        //        operation.RequestBody = new OpenApiRequestBody()
        //        {
        //            Content = new Dictionary<string, OpenApiMediaType>()
        //            {
        //                ["multipart/form-data"] = new OpenApiMediaType()
        //                {
        //                    Schema = new OpenApiSchema()
        //                    {
        //                        Type = "object",
        //                        Properties = new Dictionary<string, OpenApiSchema>()
        //                        {
        //                            [parameter.Name] = new OpenApiSchema()
        //                            {
        //                                Type = "string",
        //                                Format = "binary"
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        };
        //    }
        //}

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
        if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
        {
            Type typeFromHandle = typeof(ApiAuthorizeAttribute);
            if (methodInfo.GetCustomAttributes(typeFromHandle, inherit: true).FirstOrDefault() is ApiAuthorizeAttribute)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Platform",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("Web")
                    },
                    Required = false,
                    Description = "平台标识，Web/Wap/App"
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Mac",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    },
                    Required = false,
                    Description = "Mac地址"
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Timestamp",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        //Default = new OpenApiDouble(DateTimeUtil.TimestampOfMilliseconds)
                    },
                    Required = false,
                    Description = "时间戳（UTC），当前时间距1970-01-01的毫秒数"
                });
            }
        }
    }

}
