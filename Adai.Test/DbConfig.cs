using Adai.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Test
{
	/// <summary>
	/// DbConfig
	/// </summary>
	public class DbConfig
	{
		public static string Basic = JsonConfigHelper.Get<string>("Database[0].Name");
		public static string Basic0 = JsonConfigHelper.Get<string>("Database[1].Name");
	}
}
