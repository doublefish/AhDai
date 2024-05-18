using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 认证
/// </summary>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.Auth)]
[Route("api/v1/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
    readonly IAuthService _service = service;

    /// <summary>
    /// 注册
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IApiResult<string>> RegisterAsync([FromBody] SignupInput input)
    {
        await _service.RegisterAsync(input);
        return ApiResult.Success();
    }

    /// <summary>
    /// 获取登录信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("login")]
    public async Task<IApiResult<LoginData>> GetLoginAsync()
    {
        var res = await _service.GetLoginAsync();
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input">入参</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IApiResult<LoginResult>> LoginAsync([FromBody] LoginInput input)
    {
        var token = await _service.LoginAsync(input);
        return ApiResult.Success(token);
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    [HttpDelete("logout")]
    public async Task<IApiResult<bool>> LogoutAsync()
    {
        var res = await _service.LogoutAsync();
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <returns></returns>
    [HttpPut("refreshToken")]
    public async Task<IApiResult<LoginResult>> RefreshTokenAsync()
    {
        var res = await _service.RefreshTokenAsync();
        return ApiResult.Success(res);
    }
}
