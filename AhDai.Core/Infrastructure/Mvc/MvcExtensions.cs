using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AhDai.Core.Infrastructure.Mvc;

/// <summary>
/// MvcExtensions
/// </summary>
public static class MvcExtensions
{
    /// <summary>
    /// 配置实体验证错误返回结果
    /// </summary>
    /// <param name="options"></param>
    public static void ConfigInvalidModelStateResponse(this ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(kvp => kvp.Value != null && kvp.Value.Errors.Count > 0)
            .SelectMany(kvp => kvp.Value?.Errors.Select(error => new Models.ValidationError
            {
                Field = kvp.Key,
                Message = error.ErrorMessage,
            }) ?? []);
            var res = Models.ApiResult.Error(StatusCodes.Status400BadRequest, "实体验证失败", errors);
            res.TraceId = context.HttpContext.TraceIdentifier;
            return new BadRequestObjectResult(res);
        };
    }
}
