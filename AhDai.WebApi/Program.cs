using AhDai.Core;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using AhDai.WebApi.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AhDai.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers(options =>
		{
			//options.Filters.Add<HttpResponseExceptionFilter>();
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
			//options.LowercaseQueryStrings = true;
		});

		// ������־�м��
		builder.Logging.AddLog4Net();
		// ����ת��ͷ�м��
		builder.Services.Configure<ForwardedHeadersOptions>(options =>
		{
			options.ForwardedHeaders = ForwardedHeaders.All;
		});

		// ������֤����
		builder.Services.AddJwtAuthentication(builder.Configuration.GetJwtConfig());

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

		var app = builder.Build();

		// ������ζ�ȡbody
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
			//RequestPath = new PathString("/" + fileConfig.UploadDirectory)
		});
		// ���ÿ���
		app.UseCors("MyAllowOrigins");

		// �������ʵ��
		ServiceUtil.Init(app.Services, app.Configuration);

		// ע�����
		//Encoding.RegisterProvider(CodePagesEncodingProvider.Services);

		app.Run();
	}
}