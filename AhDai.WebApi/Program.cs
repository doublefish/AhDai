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

        // �����־�м��
        builder.Logging.AddLog4Net();
        // ���ת��ͷ�м��
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
        });

        // �����֤����
        builder.Services.AddMyAuthentication(builder.Configuration);
        builder.Services.AddMyAuthorization(builder.Configuration);

        // ����
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyAllowOrigins", builder =>
            {
                //messageBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                // �����κ���Դ���������ʣ�ָ������cookie
                builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddMySwaggerGen();

        // �������ݴ�С����
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

        // ���ҵ�����
        Startup.ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // �����ζ�ȡbody
        app.Use(next => context =>
        {
            context.Request.EnableBuffering();
            return next(context);
        });

        // ����ת��ͷ�м��
        app.UseForwardedHeaders();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseMySwaggerUI();
        }

        // �����쳣����
        app.UseMiddleware<AhDai.Core.Middlewares.RequestHandlerMiddleware>();
        // ����Https
        app.UseHttpsRedirection();
        // �ȿ�����֤
        app.UseAuthentication();
        // �ٿ�����Ȩ
        app.UseAuthorization();
        // ���ð汾����
        //app.UseApiVersioning();
        // ���ÿ�����
        app.MapControllers();
        // ���þ�̬��Դ
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
        });
        // ���ÿ���
        app.UseCors("MyAllowOrigins");

        // ������ʵ��
        Startup.Configure(app);

        // ע�����
        //Encoding.RegisterProvider(CodePagesEncodingProvider.Services);

        app.Run();
    }
}