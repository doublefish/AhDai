using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Configuration
		/// </summary>
		public IConfiguration Configuration { get; }
		/// <summary>
		/// 跨域
		/// </summary>
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			//添加本地化
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			//添加转接头中间件
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("en-US"),
						new CultureInfo("zh-CN")
					};

				options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddSession(options =>
			//{
			//	// Set a short timeout for easy testing.
			//	options.IdleTimeout = TimeSpan.FromSeconds(10);
			//	options.Cookie.Name = ".AdventureWorks.Session";
			//	options.Cookie.HttpOnly = true;
			//	// Make the session cookie essential
			//	options.Cookie.IsEssential = true;
			//});
			//services.AddAuthorization();

			//跨域
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins, builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			//注册Swagger生成器，定义一个和多个Swagger 文档
			services.AddSwaggerGen(options =>
			{
				AddSwaggerGen(options);

				var type = GetType();
				var currentDirectory = Path.GetDirectoryName(type.Assembly.Location);

				var xmlPath = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, type.Namespace);
				//此处替换成所生成的XML的文件名
				options.IncludeXmlComments(xmlPath);
				options.OperationFilter<SwaggerOperationFilter>();
				options.DocumentFilter<SwaggerDocumentFilter>();
			});
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//允许读取多次body
			app.Use(next => context =>
			{
				context.Request.EnableBuffering();
				return next(context);
			});

			// WebRootPath == null workaround.
			if (env != null && string.IsNullOrEmpty(env.WebRootPath))
			{
				env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			}

			//启用转接头中间件
			app.UseForwardedHeaders();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseMiddleware<ExceptionHandlerMiddleware>();

			//启用Https
			//app.UseHttpsRedirection();
			//启用Session
			//app.UseSession();
			//启用Routing
			app.UseRouting();
			//启用跨域
			app.UseCors(MyAllowSpecificOrigins);
			//启用StaticFiles
			app.UseStaticFiles();
			//启用CookiePolicy
			//app.UseCookiePolicy();

			//使中间件服务生成Swagger作为JSON端点
			app.UseSwagger();
			//启用中间件以提供用户界面（HTML、js、CSS等），特别是指定JSON端点
			app.UseSwaggerUI(options =>
			{
				UseSwaggerUI(options);
				options.ShowExtensions();
				//options.RoutePrefix = string.Empty;
			});

			//启用本地化
			var supportedCultures = new[]
			{
				new CultureInfo("en-US"),
				new CultureInfo("zh-CN"),
			};

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
				// Formatting numbers, dates, etc.
				SupportedCultures = supportedCultures,
				// UI strings that we have localized.
				SupportedUICultures = supportedCultures
			});

			//启用Mvc
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default_api", "api/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				//运行状况检查
				//endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
			});

			//注册编码
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		/// <summary>
		/// AddSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc("public", new OpenApiInfo { Title = "公共", Version = "v1", Description = "By Adai" });
		}

		/// <summary>
		/// ConfigureSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void UseSwaggerUI(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint("/swagger/public/swagger.json", "接口文档-公共");
		}
	}
}
