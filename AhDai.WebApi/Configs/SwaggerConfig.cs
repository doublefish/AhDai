using AhDai.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;

namespace AhDai.WebApi.Configs;

/// <summary>
/// SwaggerConfig
/// </summary>
public static class SwaggerConfig
{
	/// <summary>
	/// 认证
	/// </summary>
	public const string Auth = "auth";
	/// <summary>
	/// 系统
	/// </summary>
	public const string System = "system";
	/// <summary>
	/// 工作流
	/// </summary>
	public const string Workflow = "workflow";
	/// <summary>
	/// 基础
	/// </summary>
	public const string Basic = "basic";
	/// <summary>
	/// 业务
	/// </summary>
	public const string Business = "business";

	/// <summary>
	/// AddMySwaggerGen
	/// </summary>
	/// <param name="services"></param>
	/// <param name="setupAction"></param>
	/// <returns></returns>
	public static IServiceCollection AddMySwaggerGen(this IServiceCollection services, Action<SwaggerGenOptions>? setupAction = null)
    {
        services.AddSwaggerGen(options =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                { securityScheme, Array.Empty<string>() }
            });

            var terms = new Uri("https://example.com/terms");
            var contact = new OpenApiContact
            {
                Name = "联系人",
                Url = new Uri("https://example.com/contact"),
                Email = "example@live.com"
            };
            var license = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            };
            options.SwaggerDoc(Business, new OpenApiInfo { Title = "业务", Description = "by AhDai", Version = "v1" });
            options.SwaggerDoc(System, new OpenApiInfo { Title = "系统", Description = "by AhDai", Version = "v1" });
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                return true;
            });

            var baseDirectory = AppContext.BaseDirectory;
            var xmlPaths = new string[] {
                Path.Combine(baseDirectory, "AhDai.Core.xml"),
                Path.Combine(baseDirectory, "AhDai.Service.xml")
            };
            foreach (var xmlPath in xmlPaths)
            {
                if (!Path.Exists(xmlPath)) continue;
                options.IncludeXmlComments(xmlPath);
            }
            options.IncludeXmlComments(Path.Combine(baseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml"), true);

            options.OperationFilter<SwaggerOperationFilter>();
            options.DocumentFilter<SwaggerDocumentFilter>();

            setupAction?.Invoke(options);
        });

        return services;
    }

    /// <summary>
    /// UseMySwaggerUI
    /// </summary>
    /// <param name="app"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMySwaggerUI(this IApplicationBuilder app, Action<SwaggerUIOptions>? setupAction = null)
    {
        app.UseSwaggerUI(options =>
        {
            var root = "";
            options.SwaggerEndpoint($"{root}/swagger/{Business}/swagger.json", "业务");
            options.SwaggerEndpoint($"{root}/swagger/{System}/swagger.json", "系统");
            options.ShowExtensions();
            //options.RoutePrefix = string.Empty;

            setupAction?.Invoke(options);
        });

        return app;
    }

}
