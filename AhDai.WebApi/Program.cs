using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.HttpLogging;
using AhDai.Base;

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

        // ��������ļ�
        builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: false, reloadOnChange: true);

        // Add services to the container.

        // Http��־
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
        // ת��ͷ�м��
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
        });
        // ��Ӧѹ��
        builder.Services.AddResponseCompression();
        // ·��
        builder.Services.AddRouting(options =>
        {
            //options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        // ����
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
        builder.Services.AddAuthentication().AddJwtAuthentication(config);

        // ������
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
            options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
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
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseMySwaggerUI();
        }

        // �����ζ�ȡbody
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next(context);
        });
        // ����Http��־
        app.UseHttpLogging();
        // ����ת��ͷ�м��
        app.UseForwardedHeaders();
        // ����ǿ��Https
        app.UseHsts();
        // ����Httpsת��
        app.UseHttpsRedirection();
        // ������Ӧѹ��
        app.UseResponseCompression();
        // ���þ�̬��Դ
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
        });
        // ����·��
        app.UseRouting();
        // ���ÿ���
        app.UseCors("MyCors");
        // �ȿ�����֤
        app.UseAuthentication();
        // �ٿ�����Ȩ
        app.UseAuthorization();
        // ���ÿ�����
        app.MapControllers();

        // ������ʵ��
        ServiceUtil.Init(app.Services, app.Services.GetRequiredService<IConfiguration>());

        app.Run();
    }
}
       
