using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AhDai.Core.Extensions
{
    /// <summary>
    /// ConfigurationExtensions
    /// </summary>
    public static class ConfigurationExtensions
	{
		/// <summary>
		/// 获取Jwt配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Configs.JwtConfig GetJwtConfig(this IConfiguration configuration)
		{
			return configuration.GetSection("Jwt").Get<Configs.JwtConfig>();
		}

		/// <summary>
		/// 获取数据库配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static ICollection<DbContext.Models.DbConfig> GetDbConfigs(this IConfiguration configuration)
		{
			var configs = new List<DbContext.Models.DbConfig>();
			var children = configuration.GetSection("Database").GetChildren();
			foreach (var child in children)
			{
				var config = child.Get<DbContext.Models.DbConfig>();
				if (string.IsNullOrEmpty(config.Name) || string.IsNullOrEmpty(config.ConnectionString))
				{
					throw new Exception("数据库连接字符串或名称不能为空");
				}
				configs.Add(config);
			}
			return configs;
		}

		/// <summary>
		/// 获取邮件配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Configs.MailConfig GetMailConfig(this IConfiguration configuration)
		{
			return configuration.GetSection("Mail").Get<Configs.MailConfig>();
		}

		/// <summary>
		/// 获取Redis配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Configs.RedisConfig GetRedisConfig(this IConfiguration configuration)
		{
			return configuration.GetSection("Redis").Get<Configs.RedisConfig>();
		}

		/// <summary>
		/// 获取File配置
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static Configs.FileConfig GetFileConfig(this IConfiguration configuration)
		{
			return configuration.GetSection("File").Get<Configs.FileConfig>();
		}
	}
}
