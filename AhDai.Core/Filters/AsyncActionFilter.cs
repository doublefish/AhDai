using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AhDai.Core.Filters;

/// <summary>
/// AsyncActionFilter
/// </summary>
/// <param name="logger"></param>
public class AsyncActionFilter(ILogger<AsyncActionFilter> logger) : IAsyncActionFilter, IOrderedFilter
{
    /// <summary>
    /// Order
    /// </summary>
    public int Order => int.MaxValue - 10;
    readonly ILogger<AsyncActionFilter> _logger = logger;

    /// <summary>
    /// OnActionExecutionAsync
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();
        if (executedContext.Exception != null)
        {
            _logger.LogError(executedContext.Exception, "{Message}", executedContext.Exception.Message);
            executedContext.ExceptionHandled = true;
            ApiResult<string> result;
            if (executedContext.Exception is ApiException apiException)
            {
                result = ApiResult.Error(apiException.Code, apiException.Message, apiException.ExtraData);
                context.HttpContext.Response.StatusCode = apiException.Code switch
                {
                    StatusCodes.Status401Unauthorized => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status200OK,
                };
            }
            else
            {
                var ex = executedContext.Exception.InnerException ?? executedContext.Exception;
                result = ApiResult.Error(StatusCodes.Status500InternalServerError, ex.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            result.TraceId = context.HttpContext.TraceIdentifier;
            //context.Result = new ObjectResult(result);
            await context.HttpContext.Response.WriteAsJsonAsync(result);
        }
        else if (executedContext.Result is ObjectResult objResult && objResult.Value is IApiResult apiResult)
        {
            apiResult.TraceId = context.HttpContext.TraceIdentifier;
        }
    }
}