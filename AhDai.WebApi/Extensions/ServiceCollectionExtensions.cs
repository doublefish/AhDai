using AhDai.Core.Extensions;
using AhDai.Core.Requirements;
using AhDai.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace AhDai.WebApi.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
internal static class ServiceCollectionExtensions
{

    /// <summary>
    /// 添加Jwt认证服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("Jwt").Get<Core.Options.JwtOptions>() ?? throw new InvalidOperationException("未配置Jwt");
        services.AddAuthentication(options =>
        {
            //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtAuthentication(jwtOptions).AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie 有效期
            options.SlidingExpiration = true; // 在有效期内用户有活动时，延长 Cookie 有效期
            options.Cookie.HttpOnly = true; // 推荐设置为 true，防止 XSS 攻击获取 Cookie
            options.Cookie.IsEssential = true; // 标记 Cookie 是网站功能必需的，影响 GDPR 等法规
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // 如果你的网站使用 HTTPS，强烈建议设置为 Always
            options.Cookie.Name = "Logistics"; // 可选，自定义 Cookie 名称
        });
        return services;
    }

    /// <summary>
    /// AddMyAuthorization
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddMyAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPermissionAuthorization(new PermissionRequirement("IsVerified", "1", "用户未认证"));
        return services;
    }

    /// <summary>
    /// AddMySwaggerGen
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMySwaggerGen(this IServiceCollection services)
    {
        var baseDirectory = AppContext.BaseDirectory;
        var xmlFiles = new string[] {
            "AhDai.Core.xml",
            "AhDai.Integration.xml",
            "AhDai.Shared.xml",
            "AhDai.Service.xml",
            "AhDai.WebApi.xml"
        };

        services.AddSwaggerGen(options =>
        {
            // XML 注释
            foreach (var xmlFile in xmlFiles)
            {
                var path = Path.Combine(baseDirectory, xmlFile);
                try
                {
                    options.IncludeXmlComments(path, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("加载Xml发生异常：{0}", ex.Message);
                }
            }

            // 动态分组
            var groups = ApiGroupName.Get();
            foreach (var group in groups)
            {
                options.SwaggerDoc(group.Key, new OpenApiInfo()
                {
                    Title = group.Value,
                    Version = "v1"
                });
            }

            // 分组过滤
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                return apiDesc.GroupName == docName;
            });

            // 定义安全方案
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT 认证请求头。直接输入你的 Token，不需要在前面加 'Bearer '（系统会自动加上）。",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            // 应用安全要求
            options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", doc),
                    new List<string>()
                }
            });

            options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
            options.SchemaFilter<SwaggerSchemaFilter>();
        });

        return services;
    }
}
