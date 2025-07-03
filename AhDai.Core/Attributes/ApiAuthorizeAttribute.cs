using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AhDai.Core.Attributes;

/// <summary>
/// ApiAuthorizeAttribute
/// </summary>
/// <param name="logger"></param>
public abstract class ApiAuthorizeAttribute(ILogger<ApiAuthorizeAttribute> logger) : ActionFilterAttribute
{
    /// <summary>
    /// _logger
    /// </summary>
    readonly ILogger<ApiAuthorizeAttribute> _logger = logger;
    /// <summary>
    /// 访问频率
    /// </summary>
    public double Frequency { get; set; }
    /// <summary>
    /// 是否验证Token
    /// </summary>
    public bool VerifyToken { get; set; } = true;
    /// <summary>
    /// 是否验证权限
    /// </summary>
    public bool VerifyRight { get; set; }

    /// <summary>
    /// OnActionExecutionAsync
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //请求地址
        var path = context.HttpContext.Request.GetPath();

        //限制请求频率
        if (Frequency > 0D)
        {
            context.HttpContext.Request.VerifyRequestFrequencyLimit(Frequency, path);
        }

        if (VerifyToken)
        {
            var token = context.HttpContext.Request.Headers["X-Token"].FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                throw new ApiException((int)HttpStatusCode.Unauthorized, "未认证");
            }
            if (!VerifyLogin(token, out var login))
            {
                throw new ApiException((int)HttpStatusCode.Unauthorized, "未认证");
            }
            if (VerifyRight && !VerifyRequestRight(login.Id, path))
            {
                throw new ApiException((int)HttpStatusCode.Forbidden, "没有访问权限");
            }
        }
        try
        {
            WriteLog(context.HttpContext);
        }
        catch
        {
            throw;
        }
        return base.OnActionExecutionAsync(context, next);
    }

    /// <summary>
    /// 验证登录
    /// </summary>
    /// <param name="token"></param>
    /// <param name="login"></param>
    /// <returns></returns>
    protected abstract bool VerifyLogin(string token, out Models.TokenResult login);

    /// <summary>
    /// 验证权限
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    protected virtual bool VerifyRequestRight(string userId, string path)
    {
        return false;
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="httpContext"></param>
    protected virtual void WriteLog(HttpContext httpContext)
    {
        _logger.LogInformation("接收请求=>RemoteIpAddress={RemoteIpAddress},RemotePort={RemotePort},{Method} {Path}"
            , httpContext.Connection.RemoteIpAddress
            , httpContext.Connection.RemotePort
            , httpContext.Request.Method
            , httpContext.Request.Path.Value);
    }
}
