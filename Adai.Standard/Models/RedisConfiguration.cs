namespace Adai.Standard.Models
{
	/// <summary>
	/// RedisConfiguration
	/// </summary>
	public class RedisConfiguration
	{
		/// <summary>
		/// Host
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// Port
		/// </summary>
		public int Port { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// DbNumber
		/// </summary>
		public int Database { get; set; }
	}
}
