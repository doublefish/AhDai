using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AhDai.Core.Filters;

/// <summary>
/// ActionFilter
/// </summary>
/// <param name="logger"></param>
public class ActionFilter(ILogger<ActionFilter> logger) : IActionFilter, IOrderedFilter
{
    /// <summary>
    /// Order
    /// </summary>
    public int Order => int.MaxValue - 10;
    readonly ILogger<ActionFilter> _logger = logger;

    /// <summary>
    /// OnActionExecuting
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context) { }

    /// <summary>
    /// OnActionExecuted
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, "{Message}", context.Exception.Message);
            context.ExceptionHandled = true;
            ApiResult<string> result;
            if (context.Exception is ApiException apiException)
            {
                result = ApiResult.Error(apiException.Code, apiException.Message, apiException.Data);
                context.HttpContext.Response.StatusCode = apiException.Code switch
                {
                    StatusCodes.Status401Unauthorized => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status200OK,
                };
            }
            else
            {
                result = ApiResult.Error(StatusCodes.Status500InternalServerError, context.Exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            result.TraceId = context.HttpContext.TraceIdentifier;
            context.Result = new ObjectResult(result);
        }
        else if (context.Result is ObjectResult objResult && objResult.Value is IApiResult apiResult)
        {
            apiResult.TraceId = context.HttpContext.TraceIdentifier;
        }
    }
}