using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
		readonly string MyAllowOrigins = "MyAllowOrigins";

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			ServiceHelper.Init(services);

			//添加本地化
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			// 添加版本
			services.AddApiVersioning(option =>
			{
				option.ReportApiVersions = true;
				option.DefaultApiVersion = new ApiVersion(1, 0);
				option.AssumeDefaultVersionWhenUnspecified = true;
			});

			//添加转接头中间件
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("zh-CN") };

				options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			// HttpContextAccessor
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//services.AddAuthorization();
			// 添加认证
			var issuer = Configuration.GetSection("jwt:issuer").Value;
			var audience = Configuration.GetSection("jwt:audience").Value;
			var signingKey = Configuration.GetSection("jwt:key").Value;
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// 是否验证签发人
					ValidateIssuer = true,
					// 签发人
					ValidIssuer = issuer,
					// 是否验证受众
					ValidateAudience = true,
					// 受众
					ValidAudience = audience,
					// 是否验证生命周期，使用当前时间与Token的Claims中的NotBefore和Expires对比
					ValidateLifetime = true,
					// 过期时间，是否要求Token的Claims中必须包含Expires
					RequireExpirationTime = false,
					// 允许服务器时间偏移量
					ClockSkew = TimeSpan.FromSeconds(30),
					// 是否验证密钥
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
				};
			});

			//跨域
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowOrigins, builder =>
				{
					//builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
					// 允许任何来源的主机访问，指定处理cookie
					builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
				});
			});

			//注册Swagger生成器，定义一个和多个Swagger 文档
			services.AddSwaggerGen(options =>
			{
				AddSwaggerGen(options);

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
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{ securityScheme, Array.Empty<string>() }
				});

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
			// 允许多次读取body
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

			// 启用异常处理
			app.UseExceptionHandler();

			//启用Https
			app.UseHttpsRedirection();
			//启用Session
			//app.UseSession();
			//启用Routing
			app.UseRouting();
			//启用跨域
			app.UseCors(MyAllowOrigins);
			//启用StaticFiles
			app.UseStaticFiles();
			//启用CookiePolicy
			//app.UseCookiePolicy();

			// 先开启认证
			app.UseAuthentication();
			// 再开启授权
			app.UseAuthorization();

			// 启用DbContext
			app.UseDbContext(Configuration);
			// 启用Redis，因为大多数项目用不到Redis，交由项目启用
			//app.UseRedis(Configuration);
			// 启用邮件
			//app.UseMail(Configuration);

			// 使中间件服务生成Swagger作为JSON端点
			app.UseSwagger();
			// 启用中间件以提供用户界面（HTML、js、CSS等），特别是指定JSON端点
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
			options.SwaggerEndpoint("/swagger/public/swagger.json", "公共-v1");
		}
	}
}
