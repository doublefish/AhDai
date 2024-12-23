using AhDai.Base.Extensions;
using AhDai.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Core.Middlewares;

/// <summary>
/// RequestMiddleware
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class RequestMiddleware(RequestDelegate next
    , ILogger<RequestMiddleware> logger)
{
    readonly RequestDelegate _next = next;
    readonly ILogger<RequestMiddleware> _logger = logger;

    /// <summary>
    /// InvokeAsync
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var data = TrayGetLoginData(httpContext);
        if (data != null)
        {
            //MyApp.SetLoginData(data);
        }
        //var ipAddress = MyApp.GetClientIpAddress() ?? "Unknown IP";
        //LogContext.PushProperty("IpAddress", ipAddress);
        //using (_logger.BeginScope(new { IpAddress = ipAddress + "-1" }))
        //{
        //    _logger.LogInformation("请求IP: {IpAddress}, Path: {Path}", ipAddress, httpContext.Request.Path);
        //    await _next(httpContext);
        //}
        await _next(httpContext);
    }

    static LoginData? TrayGetLoginData(HttpContext httpContext)
    {
        if (httpContext.User.Claims.Any())
        {
            var claims = httpContext.User.Claims.ToArray();
            var data = new LoginData();
            foreach (var c in claims)
            {
                switch (c.Type)
                {
                    case "Id": data.Id = c.Value.ToInt64(0); break;
                    case "Username": data.Username = c.Value; break;
                    default: break;
                }
            }
            return data;
        }

        var userId = httpContext.Request.Query["userId"].FirstOrDefault().ToInt64();
        var tenantId = httpContext.Request.Query["tenantId"].FirstOrDefault().ToInt64();
        if (userId > 0 && tenantId > 0)
        {
            //using var db = MyApp.GetMasterDbContextAsync().GetAwaiter().GetResult();
            //var loginData = Helpers.UserHelper.GetLoginDataAsync(db, userId, tenantId).GetAwaiter().GetResult();
            //return loginData;
        }
        else
        {
            //return null;
        }
        return null;
    }
}
