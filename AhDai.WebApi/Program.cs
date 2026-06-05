using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Text.Json;

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
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .CreateBootstrapLogger();

        var builder = WebApplication.CreateBuilder(args);

        // 添加配置文件
        builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: false, reloadOnChange: true);

        // Add services to the container.
        // 日志中间件
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
        //builder.Services.AddSerilog((services, configuration) =>
        //{
        //    configuration.ReadFrom.Configuration(builder.Configuration);
        //});
        // Http日志
        var loggingRequestHeaders = builder.Configuration.GetSection("LoggingRequestHeaders").Get<string[]>();
        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.All & ~HttpLoggingFields.ResponseBody;
            options.RequestBodyLogLimit = 1024 * 256;
            options.ResponseBodyLogLimit = 1024 * 32;
            options.CombineLogs = true;
            if (loggingRequestHeaders != null)
            {
                foreach (var header in loggingRequestHeaders)
                {
                    options.RequestHeaders.Add(header);
                }
            }
        });
        // 转接头中间件
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
        });
        // 响应压缩
        builder.Services.AddResponseCompression(options =>
        {
            options.MimeTypes = [
                "application/json",
                "text/plain",
                "text/css",
                "application/javascript"
            ];
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
        // 路由
        builder.Services.AddRouting(options =>
        {
            //options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        // 跨域
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCors", builder =>
            {
                //builder.WithOrigins(AllowedOrigins ?? []).AllowAnyMethod().AllowAnyHeader();
                builder.SetIsOriginAllowed((origin) => true).AllowAnyMethod().AllowAnyHeader();
            });
        });

        var config = builder.Configuration.GetJwtConfig();

        // 认证和授权
        builder.Services.AddAuthentication().AddJwtAuthentication(config);
        // 控制器
        builder.Services.AddControllers(options =>
        {
            //options.Filters.Add<HttpResponseExceptionFilter>();
            options.Filters.Add(new AuthorizeFilter());
            options.ValueProviderFactories.Add(new JQueryQueryStringValueProviderFactory());
            options.Filters.Add<Core.Filters.AsyncActionFilter>();
            options.ModelBinderProviders.Insert(0, new Configs.CommaSeparatedArrayModelBinderProvider());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.ConfigInvalidModelStateResponse();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddMySwaggerGen();

        builder.Services.AddRedisService();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddJwtService();
        builder.Services.AddFileService();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        // 启用转接头中间件
        app.UseForwardedHeaders();
        // 启用响应压缩
        app.UseResponseCompression();
        // 启用强制Https
        app.UseHsts();
        // 启用Https转发
        app.UseHttpsRedirection();
        // 启用静态资源
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
        });
        // 允许多次读取body
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next(context);
        });
        // 启用Http日志
        app.UseHttpLogging();
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                //diagnosticContext.Set("RequestHeaders2", httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
                //diagnosticContext.Set("RequestBody", ReadRequestBody(httpContext.Request));
                //diagnosticContext.Set("ResponseBody", ReadResponseBody(httpContext.Response));
            };
        });

        // 认证
        app.UseAuthentication();
        // Hangfire Dashboard
        //app.UseHangfireDashboard("/hangfire", new DashboardOptions
        //{
        //    AsyncAuthorization = new[] { new Filters.MyHangfireAuthorizationFilter("admin", "ahsanle") }
        //});
        // 路由
        app.UseRouting();
        // 跨域
        app.UseCors("MyCors");
        // 授权
        app.UseAuthorization();
        // 端点映射
        app.MapControllers();
        // 版本控制
        //app.UseApiVersioning();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseMySwaggerUI();
        }

        // 另存服务实例
        ServiceUtil.Init(app.Services, app.Services.GetRequiredService<IConfiguration>());

        app.Run();
    }
}

