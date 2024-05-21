using AhDai.Core;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using AhDai.Service;
using AhDai.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace AhDai.WebApi;

/// <summary>
/// Program
/// </summary>
public class Program
{
    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            //options.Filters.Add<HttpResponseExceptionFilter>();
            options.ValueProviderFactories.Add(new JQueryQueryStringValueProviderFactory());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.ConfigInvalidModelStateResponse();
        }).AddJsonOptions(options =>
        {
            //options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter(Const.DateTimeFormat));
        });
        builder.Services.AddRouting(options =>
        {
            //options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        // 添加日志中间件
        builder.Logging.AddLog4Net();
        // 添加转接头中间件
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
        });

        // 添加认证服务
        builder.Services.AddMyAuthentication(builder.Configuration);
        builder.Services.AddMyAuthorization(builder.Configuration);

        // 跨域
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyAllowOrigins", builder =>
            {
                //messageBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                // 允许任何来源的主机访问，指定处理cookie
                builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddMySwaggerGen();

        // 请求内容大小限制
        var limitsConfig = builder.Configuration.GetSection("ServerLimits").Get<Configs.ServerLimitsConfig>();
        builder.WebHost.ConfigureKestrel(u =>
        {
            //u.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(130);
            //u.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
            if (limitsConfig != null && limitsConfig.MaxRequestBodySize.HasValue)
            {
                u.Limits.MaxRequestBodySize = limitsConfig.MaxRequestBodySize.Value;
            }
        });

        // 添加业务服务
        Startup.ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // 允许多次读取body
        app.Use(next => context =>
        {
            context.Request.EnableBuffering();
            return next(context);
        });

        // 启用转接头中间件
        app.UseForwardedHeaders();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseMySwaggerUI();
        }

        // 启用异常处理
        app.UseMiddleware<AhDai.Core.Middlewares.RequestHandlerMiddleware>();
        // 启用Https
        app.UseHttpsRedirection();
        // 先开启认证
        app.UseAuthentication();
        // 再开启授权
        app.UseAuthorization();
        // 启用版本控制
        //app.UseApiVersioning();
        // 启用控制器
        app.MapControllers();
        // 启用静态资源
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
        });
        // 启用跨域
        app.UseCors("MyAllowOrigins");

        // 另存服务实例
        Startup.Configure(app);

        // 注册编码
        //Encoding.RegisterProvider(CodePagesEncodingProvider.Services);

        app.Run();
    }
}