using AhDai.Base.Extensions;
using AhDai.Core;
using AhDai.Core.Services;
using AhDai.Core.Utils;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// AuthServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class AuthServiceImpl(ILogger<AuthServiceImpl> logger
	, IBaseJwtService jwtService
	, IBaseRedisService redisService
	, IUserService userService
	, IEmployeeService employeeService)
	: BaseServiceImpl(logger)
	, IAuthService
{
	readonly IBaseJwtService _jwtService = jwtService;
	readonly IBaseRedisService _redisService = redisService;
	readonly IUserService _userService = userService;
	readonly IEmployeeService _employeeService = employeeService;

	/// <summary>
	/// 生成验证码
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<CaptchaOutput> GenerateCaptchaAsync()
	{
		await Task.Run(() =>
		{

		});
		//var result = await GenerateCaptchaAsync();
		return new CaptchaOutput()
		{
			Id = "",
			Image = ""
		};
	}

	/// <summary>
	/// 注册
	/// </summary>
	/// <param name="input">入参</param>
	/// <returns></returns>
	public async Task RegisterAsync(SignupInput input)
	{
		await Task.Run(() =>
		{

		});
	}

	/// <summary>
	/// 登录
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	public async Task<LoginResult> LoginAsync(LoginInput input)
	{
		var user = await _userService.GetByUsernameAsync(input.Username, input.Password);
		var employees = await _employeeService.GetAsync(new Service.Sys.Models.EmployeeQueryInput()
		{
			UserId = user.Id,
			DataDepth = 0,
			WithDataPermission = false,
			PageSize = 1,
		});
		var employee = employees.FirstOrDefault();
		var claims = new List<Claim>()
		{
			new(MyConst.Claim.ID, user.Id.ToString()),
			new(MyConst.Claim.USERNAME, user.Username),
			new(MyConst.Claim.NAME, user.Name),
			new(MyConst.Claim.ROLE_CODES, Utils.ModelUtils.ToString(user.RoleCodes)),
			new(MyConst.Claim.EMPLOYEE_ID, (employee?.Id ?? 0).ToString()),
			new(MyConst.Claim.ORG_ID, (employee?.OrgId ?? 0).ToString()),
			new(MyConst.Claim.TENANT_ID, user.TenantId.ToString()),
			new(MyConst.Claim.TENANT_NAME, user.TenantName),
			new(MyConst.Claim.TENANT_TYPE, ((int)user.TenantType).ToString()),
		};
		var res = await _jwtService.GenerateTokenAsync([.. claims]);
		var rdb = _redisService.GetDatabase();
		await rdb.HashSetAsync(MyConst.Redis.GenerateKey("LoginUser"), user.Id, JsonUtil.Serialize(user));
		return new LoginResult(res);
	}

	/// <summary>
	/// 登出
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public async Task<bool> LogoutAsync()
	{
		var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
		if (httpContext == null || httpContext.User == null) return false;
		var token = httpContext.Request.Headers[Const.Authorization].FirstOrDefault();
		if (string.IsNullOrEmpty(token)) return false;
		try
		{
			var data = await GetLoginAsync();
			await RemoveTokenAsync(token);

			var rdb = _redisService.GetDatabase();
			await rdb.HashDeleteAsync(MyConst.Redis.GenerateKey("LoginUser"), data.Id);
			return true;
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "登出发生异常=>{Message}", ex.Message);
			return false;
		}
	}

	/// <summary>
	/// 获取登录信息
	/// </summary>
	/// <returns></returns>
	public Task<LoginData> GetLoginAsync()
	{
		var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
		if (httpContext == null || httpContext.User == null)
		{
			throw new Exception("未读取到登录信息");
		}
		var claims = httpContext.User.Claims.ToArray();
		//var tokenData = _jwtService.ToTokenData(claims);
		var loginData = new LoginData();
		foreach (var c in claims)
		{
			switch (c.Type)
			{
				case MyConst.Claim.ID: loginData.Id = c.Value.ToInt64(0); break;
				case MyConst.Claim.USERNAME: loginData.Username = c.Value; break;
				case MyConst.Claim.NAME: loginData.Name = c.Value; break;
				case MyConst.Claim.ROLE_CODES: loginData.RoleCodes = Utils.ModelUtils.ToArray(c.Value); break;
				case MyConst.Claim.EMPLOYEE_ID: loginData.EmployeeId = c.Value.ToInt64(0); break;
				case MyConst.Claim.ORG_ID: loginData.OrgId = c.Value.ToInt64(0); break;
				case MyConst.Claim.TENANT_ID: loginData.TenantId = c.Value.ToInt64(0); break;
				case MyConst.Claim.TENANT_NAME: loginData.TenantName = c.Value; break;
				case MyConst.Claim.TENANT_TYPE: loginData.TenantType = (Shared.Enums.TenantType)c.Value.ToInt32(0); break;
				default: break;
			}
		}
		return Task.FromResult(loginData);
	}

	/// <summary>
	/// 刷新Token
	/// </summary>
	/// <returns></returns>
	public async Task<LoginResult> RefreshTokenAsync()
	{
		var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
		if (httpContext == null || httpContext.User == null)
		{
			throw new Exception("未读取到登录信息");
		}
		var token = httpContext.Request.Headers[Const.Authorization].FirstOrDefault();
		var res = await _jwtService.RefreshTokenAsync(token);
		return new LoginResult(res);
	}

	/// <summary>
	/// 移除Token
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public async Task<bool> RemoveTokenAsync(string token)
	{
		return await _jwtService.RemoveTokenAsync(token);
	}

}
