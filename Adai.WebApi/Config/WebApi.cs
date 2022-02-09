using System.Configuration;

namespace Adai.WebApi.Config
{
	/// <summary>
	/// WebApi
	/// </summary>
	public static class WebApi
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		static WebApi()
		{
			var customize = ConfigurationManager.AppSettings["WebApi.Customize"];
			if (customize == "1")
			{
				ContextPath = ConfigurationManager.AppSettings["WebApi.ContextPath"];
				JwtIssuer = ConfigurationManager.AppSettings["WebApi.Jwt.Issuer"];
				JwtAudience = ConfigurationManager.AppSettings["WebApi.Jwt.Audience"];
				JwtKey = ConfigurationManager.AppSettings["WebApi.Jwt.Key"];
			}
		}
		/// <summary>
		/// ContextPath
		/// </summary>
		public static readonly string ContextPath = "WebApi:ContextPath";
		/// <summary>
		/// JwtIssuer
		/// </summary>
		public static readonly string JwtIssuer = "WebApi:Jwt:Issuer";
		/// <summary>
		/// JwtAudience
		/// </summary>
		public static readonly string JwtAudience = "WebApi:Jwt:Audience";
		/// <summary>
		/// JwtKey
		/// </summary>
		public static readonly string JwtKey = "WebApi:Jwt:Key";
	}
}
