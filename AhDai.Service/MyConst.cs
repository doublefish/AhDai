namespace AhDai.Service;

internal class MyConst
{
	/// <summary>
	/// Redis
	/// </summary>
	public class Redis
	{
		/// <summary>
		/// ROOT
		/// </summary>
		public const string ROOT = "AhDai";
	}

	/// <summary>
	/// 字典编码
	/// </summary>
	public class DictCode
	{
		/// <summary>
		/// 问题类别
		/// </summary>
		public const string PROBLEM_TYPE = "problem_type";
		/// <summary>
		/// 问题状态
		/// </summary>
		public const string PROBLEM_STATUS = "problem_status";
		/// <summary>
		/// 登录白名单
		/// </summary>
		public const string LOGIN_WHITELIST = "login_whitelist";
		/// <summary>
		/// 登录黑名单
		/// </summary>
		public const string LOGIN_BLACKLIST = "login_blacklist";
	}
}
