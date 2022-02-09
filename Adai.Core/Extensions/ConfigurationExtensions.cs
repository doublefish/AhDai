using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Adai.Core.Extensions
{
	/// <summary>
	/// ConfigurationExtensions
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// 获取数据库配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static ICollection<DbContext.Models.DbConfig> GetDbConfigs(this IConfiguration configuration)
		{
			var configs = new List<DbContext.Models.DbConfig>();
			var dbs = new string[] { Config.Db.Master, Config.Db.Product, Config.Db.Report, Config.Db.Digital, Config.Db.Foreign, Config.Db.Marketing };
			foreach (var db in dbs)
			{
				var config = new DbContext.Models.DbConfig()
				{
					DbType = DbContext.Config.DbType.MySQL,
					Name = db,
					ConnectionString = configuration.GetSection(db).Value
				};
				configs.Add(config);
			}
			return configs;
		}

		/// <summary>
		/// 获取邮件配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Models.MailConfig GetMailConfig(this IConfiguration configuration)
		{
			var config = new Models.MailConfig()
			{
				SmtpHost = configuration.GetSection(Config.Mail.SmtpHost).Value,
				SmtpPort = Convert.ToInt32(configuration.GetSection(Config.Mail.SmtpPort).Value),
				SmtpUsername = configuration.GetSection(Config.Mail.SmtpUsername).Value,
				SmtpPassword = configuration.GetSection(Config.Mail.SmtpPassword).Value
			};
			return config;
		}

		/// <summary>
		/// 获取Redis配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Models.RedisConfig GetRedisConfig(this IConfiguration configuration)
		{
			var config = new Models.RedisConfig()
			{
				Host = configuration.GetSection(Config.Redis.Host).Value,
				Port = Convert.ToInt32(configuration.GetSection(Config.Redis.Port).Value),
				Password = configuration.GetSection(Config.Redis.Password).Value
			};
			return config;
		}

		/// <summary>
		/// 获取RabbitMQ配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static RabbitMQ.Config GetRabbitMQConfig(this IConfiguration configuration)
		{
			var config = new RabbitMQ.Config()
			{
				Host = configuration.GetSection(Config.RabbitMQ.Host).Value,
				Port = Convert.ToInt32(configuration.GetSection(Config.RabbitMQ.Port).Value),
				VirtualHost = configuration.GetSection(Config.RabbitMQ.VirtualHost).Value,
				Username = configuration.GetSection(Config.RabbitMQ.Username).Value,
				Password = configuration.GetSection(Config.RabbitMQ.Password).Value
			};
			return config;
		}
	}
}
