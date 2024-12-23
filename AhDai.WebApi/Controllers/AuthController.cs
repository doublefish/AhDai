using AhDai.Base.Extensions;
using AhDai.Core;
using AhDai.Core.Models;
using AhDai.Core.Services;
using AhDai.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
/// <param name="jwtService"></param>
[ApiExplorerSettings(GroupName = SwaggerGroupName.Account)]
public class AuthController(IBaseJwtService jwtService) : BaseEmptyController
{
    readonly IBaseJwtService _jwtService = jwtService;

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input">入参</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IApiResult<LoginOutput>> LoginAsync([FromBody] LoginInput input)
    {
        var claims = new List<Claim>()
        {
            new("Id", "10000"),
            new("Username", input.Username),
        };
        var res = await _jwtService.GenerateTokenAsync([.. claims], 600);
        var output = new LoginOutput()
        {
            Token = res.Token,
            Expiration = res.Expiration,
            Type = res.Type,
        };
        return ApiResult.Success(output);
    }

    /// <summary>
    /// 获取登录信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("login")]
    public async Task<IApiResult<LoginData>> GetLoginAsync()
    {
        await _jwtService.ValidateTokenAsync(HttpContext.Request.Headers.Authorization.FirstOrDefault() ?? "");

        var data = new LoginData();
        if (HttpContext.User.Claims.Any())
        {
            var claims = HttpContext.User.Claims.ToArray();
            foreach (var c in claims)
            {
                switch (c.Type)
                {
                    case "Id": data.Id = c.Value.ToInt64(0); break;
                    case "Username": data.Username = c.Value; break;
                    default: break;
                }
            }
        }
        return ApiResult.Success(data);
    }
}
