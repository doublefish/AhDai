using System.Configuration;

namespace Adai.Standard.Config
{
	/// <summary>
	/// 数据库相关
	/// </summary>
	public static class Db
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		static Db()
		{
			var customize = ConfigurationManager.AppSettings["Db.Customize"];
			if (customize == "1")
			{
				// Db
				Master = ConfigurationManager.AppSettings["Db.Master"];
				Product = ConfigurationManager.AppSettings["Db.Product"];
				Report = ConfigurationManager.AppSettings["Db.Report"];
				Digital = ConfigurationManager.AppSettings["Db.Digital"];
				Foreign = ConfigurationManager.AppSettings["Db.Foreign"];
				Marketing = ConfigurationManager.AppSettings["Db.Marketing"];
			}
		}

		/// <summary>
		/// 主库
		/// </summary>
		public static string Master = "db:master";
		/// <summary>
		/// 产品库
		/// </summary>
		public static string Product = "db:product";
		/// <summary>
		/// 报表库
		/// </summary>
		public static string Report = "db:report";
		/// <summary>
		/// 数码库
		/// </summary>
		public static string Digital = "db:digital";
		/// <summary>
		/// 外码库
		/// </summary>
		public static string Foreign = "db:foreign";
		/// <summary>
		/// 营销库
		/// </summary>
		public static string Marketing = "db:marketing";
	}
}
