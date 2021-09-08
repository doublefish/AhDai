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
		/// ���캯��
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
		/// ����
		/// </summary>
		readonly string MyAllowOrigins = "MyAllowOrigins";

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			ServiceHelper.Init(services);

			//��ӱ��ػ�
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			// ��Ӱ汾
			services.AddApiVersioning(option =>
			{
				option.ReportApiVersions = true;
				option.DefaultApiVersion = new ApiVersion(1, 0);
				option.AssumeDefaultVersionWhenUnspecified = true;
			});

			//���ת��ͷ�м��
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
			// �����֤
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
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
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

			//ע��Swagger������������һ���Ͷ��Swagger �ĵ�
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
				//�˴��滻�������ɵ�XML���ļ���
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
			app.UseMiddleware<ExceptionHandlerMiddleware>();

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

			// ����DbContext
			app.UseDbContext(Configuration);
			// ����Redis����Ϊ�������Ŀ�ò���Redis��������Ŀ����
			//app.UseRedis(Configuration);
			// �����ʼ�
			//app.UseMail(Configuration);

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

			//����Mvc
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default_api", "api/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				//����״�����
				//endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
			});

			//ע�����
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		/// <summary>
		/// AddSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc("public", new OpenApiInfo { Title = "����", Version = "v1", Description = "By Adai" });
		}

		/// <summary>
		/// ConfigureSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void UseSwaggerUI(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint("/swagger/public/swagger.json", "����-v1");
		}
	}
}
