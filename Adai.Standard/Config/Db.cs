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
		/// 主库 默认：Db:Master
		/// </summary>
		public static readonly string Master = "Db:Master";
		/// <summary>
		/// 产品库 默认：Db:Product
		/// </summary>
		public static readonly string Product = "Db:Product";
		/// <summary>
		/// 报表库 默认：Db:Report
		/// </summary>
		public static readonly string Report = "Db:Report";
		/// <summary>
		/// 数码库 默认：Db:Digital
		/// </summary>
		public static readonly string Digital = "Db:Digital";
		/// <summary>
		/// 外码库 默认：Db:Foreign
		/// </summary>
		public static readonly string Foreign = "Db:Foreign";
		/// <summary>
		/// 营销库 默认：Db:Marketing
		/// </summary>
		public static readonly string Marketing = "Db:Marketing";
	}
}
