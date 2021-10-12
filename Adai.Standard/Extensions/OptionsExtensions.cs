namespace Adai.Standard.Extensions
{
	/// <summary>
	/// OptionsExtensions
	/// </summary>
	public static class OptionsExtensions
	{
		/// <summary>
		/// AddConfig
		/// </summary>
		/// <param name="options"></param>
		/// <param name="dbType"></param>
		/// <param name="name"></param>
		/// <param name="connectionString"></param>
		public static void AddConfig(this Options.DbContextOptions options, DbContext.Config.DbType dbType, string name, string connectionString)
		{
			options.Configs.Add(new DbContext.Models.DbConfig() { DbType = dbType, Name = name, ConnectionString = connectionString });
		}
	}
}
