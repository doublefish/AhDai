namespace Adai.DbContext.Models
{
	/// <summary>
	/// 数据库配置
	/// </summary>
	public class DbConfig
	{
		/// <summary>
		/// 数据库类型
		/// </summary>
		public Config.DbType DbType { get; set; }
		/// <summary>
		/// 别名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 连接字符串
		/// </summary>
		public string ConnectionString { get; set; }
	}
}
