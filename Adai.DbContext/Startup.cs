using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Adai.DbContext
{
	/// <summary>
	/// 启动
	/// </summary>
	public static class Startup
	{
		/// <summary>
		/// 初始化（建议在程序启动时执行此方法）
		/// </summary>
		/// <param name="connectionStrings">数据库别名-连接字符串</param>
		/// <param name="beforeExecute">执行之前执行，可用于记录SQL，第一个参数是初始化时传入的EventId</param>
		/// <returns></returns>
		public static bool Init(IDictionary<string, string> connectionStrings, Action<string, IDbCommand> beforeExecute = null)
		{
			DbHelper.ConnectionStrings = connectionStrings;
			BeforeExecuteAction = beforeExecute;
			return DbHelper.Initialized;
		}

		/// <summary>
		/// 执行之前
		/// </summary>
		public static Action<string, IDbCommand> BeforeExecuteAction { get; private set; }
	}
}
