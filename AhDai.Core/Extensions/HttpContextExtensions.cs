using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AhDai.Core.Extensions;

/// <summary>
/// HttpContextExtensions
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns></returns>
    public static string? GetClientIpAddress(this HttpContext httpContext, bool toIPv6 = false)
    {
        var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
        {
            var ip = forwardedHeader.Split(',').First();
            if (System.Net.IPAddress.TryParse(ip, out var ipAddress))
            {
                return (toIPv6 ? ipAddress.MapToIPv6() : ipAddress.MapToIPv4()).ToString();
            }
        }

        var realIpHeader = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIpHeader))
        {
            if (System.Net.IPAddress.TryParse(realIpHeader, out var ipAddress))
            {
                return (toIPv6 ? ipAddress.MapToIPv6() : ipAddress.MapToIPv4()).ToString();
            }
        }

        var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
        if (remoteIpAddress != null)
        {
            return (toIPv6 ? remoteIpAddress.MapToIPv6() : remoteIpAddress.MapToIPv4()).ToString();
        }
        return null;
    }
}
