using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Adai.DbContext.MySql
{
	/// <summary>
	/// 启动
	/// </summary>
	public static class Startup
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="connectionStrings">数据库标识和连接字符串</param>
		/// <param name="beforeExecute">执行之前执行，可用于记录SQL，第一个参数是初始化时传入的EventId</param>
		/// <returns></returns>
		public static bool Init(IDictionary<string, string> connectionStrings, Action<string, IDbCommand> beforeExecute = null)
		{
			// 指定配置数据库别名的appSettings的key
			MySqlDbContext.BeforeExecuteAction = beforeExecute;
			return Adai.DbContext.Startup.Init(connectionStrings);
		}
	}
}
