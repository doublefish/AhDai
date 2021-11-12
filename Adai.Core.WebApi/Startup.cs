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
		/// ����
		/// </summary>
		readonly string MyAllowOrigins = "MyAllowOrigins";

		/// <summary>
		/// ���캯��
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
			// ��ӱ��ػ�
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			// ��Ӱ汾����
			services.AddApiVersioning(options =>
			{
				// ��Ӧ��ͷ�з���֧�ֵİ汾��Ϣ
				options.ReportApiVersions = true;
				// ����δָ���汾API��ָ��Ĭ�ϰ汾
				options.AssumeDefaultVersionWhenUnspecified = true;
				// Ĭ�ϰ汾��֧��ʱ������ְ汾��
				options.DefaultApiVersion = new ApiVersion(1, 0);
				// ֧�� MediaType��Header��QueryString ���ð汾�ţ�Ĭ��ΪQueryString��UrlSegment���ð汾��
				options.ApiVersionReader = new HeaderApiVersionReader("api-version");
			});
			services.AddVersionedApiExplorer(options =>
			{
				// api������ʽ
				options.GroupNameFormat = "'v'VVVV";
				// �Ƿ��ṩAPI�汾����
				options.AssumeDefaultVersionWhenUnspecified = true;
			});

			// ���ת��ͷ�м��
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

			// ��ӵ�������
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			// ������ݿ����
			//services.AddSingleton<Standard.Interfaces.IDbService, Standard.Services.DbService>();
			// ���Redis����
			//services.AddSingleton<Standard.Interfaces.IRedisService, Standard.Services.RedisService>();

			//services.AddAuthorization();
			// �����֤
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
					// �Ƿ���֤ǩ����
					ValidateIssuer = true,
					// ǩ����
					ValidIssuer = issuer,
					// �Ƿ���֤����
					ValidateAudience = true,
					// ����
					ValidAudience = audience,
					// �Ƿ���֤�������ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
					ValidateLifetime = true,
					// ����ʱ�䣬�Ƿ�Ҫ��Token��Claims�б������Expires
					RequireExpirationTime = false,
					// ���������ʱ��ƫ����
					ClockSkew = TimeSpan.FromSeconds(30),
					// �Ƿ���֤��Կ
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = signingKey
				};
				options.Events = new JwtBearerEvents
				{
					// �˴�ΪȨ����֤ʧ�ܺ󴥷����¼�
					OnChallenge = context =>
					{
						// �˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ��
						context.HandleResponse();

						// �Զ��巵������
						var requestId = context.Request.Headers[Const.RequestId];
						var result = new Standard.Models.ActionResult<string>(requestId, StatusCodes.Status401Unauthorized, "Unauthorized");

						context.Response.WriteObjectAsync(result);
						return Task.FromResult(0);
					}
				};
			});

			//����
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowOrigins, builder =>
				{
					//builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
					// �����κ���Դ���������ʣ�ָ������cookie
					builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
				});
			});

			// ע��DbContext
			services.AddDbContext(AddDbContextConfig);
			// ע��Redis
			services.AddRedis(AddRedisConfig);
			// ע��RabbitMQ
			services.AddRabbitMQ(AddRabbitMqConfig);

			// ע��Swagger������������һ���Ͷ��Swagger �ĵ�
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

				// �˴��滻�������ɵ�XML���ļ���
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

			// ������ʵ��
			//Standard.Utils.ServiceHelper.Init(services);
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// ������ʵ��
			Standard.Utils.ServiceHelper.Init(app.ApplicationServices);

			// �����ζ�ȡbody
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

			//����ת��ͷ�м��
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

			// �����쳣����
			app.UseExceptionHandler();

			//����Https
			app.UseHttpsRedirection();
			//����Session
			//app.UseSession();
			//����Routing
			app.UseRouting();
			//���ÿ���
			app.UseCors(MyAllowOrigins);
			//����StaticFiles
			app.UseStaticFiles();
			//����CookiePolicy
			//app.UseCookiePolicy();

			// �ȿ�����֤
			app.UseAuthentication();
			// �ٿ�����Ȩ
			app.UseAuthorization();
			// ���ð汾����
			app.UseApiVersioning();

			// ����DbContext
			//app.UseDbContext();
			// ����Redis
			//app.UseRedis();
			// ����RabbitMQ
			//app.UseRabbitMQ();
			// �����ʼ�
			//app.UseMail();

			// ʹ�м����������Swagger��ΪJSON�˵�
			app.UseSwagger();
			// �����м�����ṩ�û����棨HTML��js��CSS�ȣ����ر���ָ��JSON�˵�
			app.UseSwaggerUI(options =>
			{
				UseSwaggerUI(options);
				options.ShowExtensions();
				//options.RoutePrefix = string.Empty;
			});

			//���ñ��ػ�
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

			// ����Mvc
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default_api", "api/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				// ����״�����
				//endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
			});

			//ע�����
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
					Description = "��汾���������Ͻǰ汾�л���<br/>",
					Contact = new OpenApiContact() { Name = "test", Email = "test@yesno.com" }
				};
				if (description.IsDeprecated)
				{
					info.Description += "<br/><b>��������</b>";
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
