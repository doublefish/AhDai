﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;

namespace AhDai.Core.Extensions;

/// <summary>
/// BuilderExtensions
/// </summary>
public static class BuilderExtensions
{
    #region UseRedis
    /// <summary>
    /// UseRedis
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseRedis(this IApplicationBuilder app, Options.RedisOptions options)
    {
        if (options != null)
        {
            //Utils.RedisHelper.Init(options.Config);
        }
        return app;
    }

    /// <summary>
    /// UseRedis
    /// </summary>
    /// <param name="app"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseRedis(this IApplicationBuilder app, Action<Options.RedisOptions>? setupAction = null)
    {
        Options.RedisOptions options;
        using (var scope = app.ApplicationServices.CreateScope())
        {
            // 这里才会执行添加配置时传入的action
            options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.RedisOptions>>().Value;
            setupAction?.Invoke(options);
        }
        return app.UseRedis(options);
    }
    #endregion

    #region UseMail
    /// <summary>
    /// UseMail
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMail(this IApplicationBuilder app, Options.MailOptions options)
    {
        if (options != null)
        {
            //Services.MailService.Init(options.Config);
        }
        return app;
    }

    /// <summary>
    /// UseMail
    /// </summary>
    /// <param name="app"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMail(this IApplicationBuilder app, Action<Options.MailOptions>? setupAction = null)
    {
        Options.MailOptions options;
        using (var scope = app.ApplicationServices.CreateScope())
        {
            // 这里才会执行添加配置时传入的action
            options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.MailOptions>>().Value;
            setupAction?.Invoke(options);
        }
        return app.UseMail(options);
    }
    #endregion

    /// <summary>
    /// AddLoggerProvider
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="onExecuted"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddLoggerProvider(this ILoggingBuilder builder, Action<LogLevel, string, Exception?>? onExecuted = null)
    {
        // 删除所有 ILoggerProvider 实例
        builder.ClearProviders();
        builder.AddProvider(new Providers.LoggerProvider(onExecuted));
        return builder;
    }


    /// <summary>
    /// AddLog4Net
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configFile"></param>
    /// <param name="repository"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string configFile = "log4net.config", string repository = "log4net")
    {
        return builder.AddProvider(new Providers.Log4NetProvider(configFile, repository));
    }

    /// <summary>
    /// 配置实体验证错误返回结果
    /// </summary>
    /// <param name="options"></param>
    public static void ConfigInvalidModelStateResponse(this ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(ms => ms.Value != null && ms.Value.Errors.Count > 0).ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            var res = ApiResult.Error((int)HttpStatusCode.BadRequest, "实体验证失败", errors);
            res.TraceId = context.HttpContext.TraceIdentifier;
            return new BadRequestObjectResult(res);
        };
    }

}
