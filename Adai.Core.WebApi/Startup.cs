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
using Adai.Base.Extensions;
using Adai.Standard.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Threading.Tasks;

namespace Adai.WebApi
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Configuration
		/// </summary>
		public IConfiguration Configuration { get; }
		/// <summary>
		/// 跨域
		/// </summary>
		readonly string MyAllowOrigins = "MyAllowOrigins";

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			// 添加本地化
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			// 添加版本控制
			services.AddApiVersioning(options =>
			{
				// 响应标头中返回支持的版本信息
				options.ReportApiVersions = true;
				// 启用未指明版本API，指向默认版本
				options.AssumeDefaultVersionWhenUnspecified = true;
				// 默认版本，支持时间或数字版本号
				options.DefaultApiVersion = new ApiVersion(1, 0);
				// 支持 MediaType、Header、QueryString 设置版本号；默认为QueryString、UrlSegment设置版本号
				options.ApiVersionReader = new HeaderApiVersionReader("api-version");
			});
			services.AddVersionedApiExplorer(options =>
			{
				// api组名格式
				options.GroupNameFormat = "'v'VVVV";
				// 是否提供API版本服务
				options.AssumeDefaultVersionWhenUnspecified = true;
			});

			// 添加转接头中间件
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("zh-CN") };
				options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			// 添加单例服务
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			// 添加数据库服务
			//services.AddSingleton<Standard.Interfaces.IDbService, Standard.Services.DbService>();
			// 添加Redis服务
			//services.AddSingleton<Standard.Interfaces.IRedisService, Standard.Services.RedisService>();

			//services.AddAuthorization();
			// 添加认证
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var issuer = Configuration.GetSection("jwt:issuer").Value;
				var audience = Configuration.GetSection("jwt:audience").Value;
				var key = Configuration.GetSection("jwt:key").Value;
				var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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
					IssuerSigningKey = signingKey
				};
				options.Events = new JwtBearerEvents
				{
					// 此处为权限验证失败后触发的事件
					OnChallenge = context =>
					{
						// 此处代码为终止.Net Core默认的返回类型和数据结果
						context.HandleResponse();

						// 自定义返回内容
						var requestId = context.Request.Headers[Const.RequestId];
						var result = new Standard.Models.ActionResult<string>(requestId, StatusCodes.Status401Unauthorized, "Unauthorized");

						context.Response.WriteObjectAsync(result);
						return Task.FromResult(0);
					}
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

			// 注册DbContext
			services.AddDbContext(AddDbContextConfig);
			// 注册Redis
			services.AddRedis(AddRedisConfig);
			// 注册RabbitMQ
			services.AddRabbitMQ(AddRabbitMqConfig);

			// 注册Swagger生成器，定义一个和多个Swagger 文档
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

				AddSwaggerGen(options);

				// 此处替换成所生成的XML的文件名
				//var basePath = AppDomain.CurrentDomain.BaseDirectory;
				var types = new Type[] { GetType()/*, typeof(Startup), typeof(Core.Const)*/ };
				foreach (var type in types)
				{
					var xmlPath = type.Assembly.Location.Replace(".dll", ".xml");
					options.IncludeXmlComments(xmlPath);
				}

				options.OperationFilter<Filters.SwaggerOperationFilter>();
				options.DocumentFilter<Filters.SwaggerDocumentFilter>();
			});

			// 另存服务实例
			//Standard.Utils.ServiceHelper.Init(services);
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// 另存服务实例
			Standard.Utils.ServiceHelper.Init(app.ApplicationServices);

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
			app.UseMiddleware<Standard.Middlewares.ExceptionHandlerMiddleware>();

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
			// 启用版本控制
			app.UseApiVersioning();

			// 启用DbContext
			//app.UseDbContext();
			// 启用Redis
			//app.UseRedis();
			// 启用RabbitMQ
			//app.UseRabbitMQ();
			// 启用邮件
			//app.UseMail();

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

			// 启用Mvc
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default_api", "api/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				// 运行状况检查
				//endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
			});

			//注册编码
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		/// <summary>
		/// AddDbContextConfig
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddDbContextConfig(Standard.Options.DbContextOptions options)
		{
			var dbs = new string[] { "db0", "db1" };
			foreach (var db in dbs)
			{
				options.AddConfig(DbContext.Config.DbType.MySQL, db, Configuration.GetSection($"db:{db}").Value);
			}
		}

		/// <summary>
		/// AddRedisConfig
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddRedisConfig(Standard.Options.RedisOptions options)
		{
			options.Config = new Standard.Models.RedisConfig()
			{
				Host = Configuration.GetSection("redis:host").Value,
				Port = Configuration.GetSection("redis:port").Value.ToInt32(),
				Password = Configuration.GetSection("redis:password").Value
			};
			options.Config.ConfigurationChangedBroadcast = StartupRedis.ConfigurationChangedBroadcast;
			options.Config.ConfigurationChanged = StartupRedis.ConfigurationChanged;
			options.Config.HashSlotMoved = StartupRedis.HashSlotMoved;
			options.Config.ErrorMessage = StartupRedis.ErrorMessage;
			options.Config.InternalError = StartupRedis.InternalError;
			options.Config.ConnectionFailed = StartupRedis.ConnectionFailed;
			options.Config.ConnectionRestored = StartupRedis.ConnectionRestored;
		}

		/// <summary>
		/// AddRabbitMqConfig
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddRabbitMqConfig(Standard.Options.RabbitMQOptions1 options)
		{
			options.Config = new Standard.Models.RabbitMQConfig()
			{
				Host = Configuration.GetSection("rabbitmq:host").Value,
				VirtualHost = Configuration.GetSection("rabbitmq:vhost").Value,
				Port = Configuration.GetSection("rabbitmq:port").Value.ToInt32(),
				Username = Configuration.GetSection("rabbitmq:username").Value,
				Password = Configuration.GetSection("rabbitmq:password").Value
			};
		}

		/// <summary>
		/// AddMailConfig
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddMailConfig(Standard.Options.MailOptions options)
		{
			options.Config = new Standard.Models.MailConfig()
			{
				SmtpHost = Configuration.GetSection("mail:smtp:host").Value,
				SmtpPort = Configuration.GetSection("mail:smtp:port").Value.ToInt32(),
				SmtpUsername = Configuration.GetSection("mail:smtp:username").Value,
				SmtpPassword = Configuration.GetSection("mail:smtp:password").Value
			};
		}

		/// <summary>
		/// AddSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc("common", new OpenApiInfo { Title = "Common v1.0", Version = "1.0", Description = "By CCN" });
			var provider = Standard.Utils.ServiceHelper.GetRequiredService<IApiVersionDescriptionProvider>();
			// add a swagger document for each discovered API version
			// note: you might choose to skip or document deprecated API versions differently
			foreach (var description in provider.ApiVersionDescriptions)
			{
				var info = new OpenApiInfo()
				{
					//Title = $"{description.GroupName} v{description.ApiVersion}",
					Title = $"v{description.ApiVersion}",
					Version = description.ApiVersion.ToString(),
					Description = "多版本管理（点右上角版本切换）<br/>",
					Contact = new OpenApiContact() { Name = "test", Email = "test@yesno.com" }
				};
				if (description.IsDeprecated)
				{
					info.Description += "<br/><b>即将废弃</b>";
				}
				//options.SwaggerDoc(description.GroupName, info);
			}
		}

		/// <summary>
		/// ConfigureSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		/// <param name="root"></param>
		public virtual void UseSwaggerUI(SwaggerUIOptions options, string root = null)
		{
			options.SwaggerEndpoint($"{root}/swagger/common/swagger.json", "Common v1.0");
			var provider = Standard.Utils.ServiceHelper.GetRequiredService<IApiVersionDescriptionProvider>();
			foreach (var description in provider.ApiVersionDescriptions)
			{
				//options.SwaggerEndpoint($"{root}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
			}
		}

	}
}
