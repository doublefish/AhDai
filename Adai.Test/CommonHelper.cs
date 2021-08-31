using Adai.Standard;
using Adai.Standard.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Test
{
	/// <summary>
	/// CommonHelper
	/// </summary>
	public static class CommonHelper
	{
		public static void InitDbConfig()
		{
			// 读取数据库配置
			//var dbConfigs = JsonConfigHelper.Get<ICollection<DbConfiguration>>("Database");
			////var dict = dbConfigs.ToDictionary(o => o.Name);
			//var dict = dbConfigs.ToDictionary(o => o.Name, o => o.Value);
			//Adai.DbContext.Startup.Init(dict, BeforeExecute);
		}

		/// <summary>
		/// 执行前执行
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="command"></param>
		static void BeforeExecute(string eventId, IDbCommand command)
		{
			var message = $@"记录SQL=>{command.CommandText};Paras=>";
			foreach (IDbDataParameter para in command.Parameters)
			{
				message += $"{para.ParameterName}={para.Value},";
			}
			Log4netHelper.Debug(eventId, message);
		}
	}
}
