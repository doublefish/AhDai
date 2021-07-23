using System;
using System.Collections.Generic;
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
		/// <param name="connectionStrings"></param>
		/// <returns></returns>
		public static bool Init(IDictionary<string, string> connectionStrings)
		{
			DbHelper.ConnectionStrings = connectionStrings;
			DbHelper.Initialized = connectionStrings != null && connectionStrings.Count > 0;
			return DbHelper.Initialized;
		}
	}
}
