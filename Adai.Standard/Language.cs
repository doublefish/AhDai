namespace Adai.Standard
{
	/// <summary>
	/// Language
	/// </summary>
	public class Language : Adai.Model.Config<string>
	{
		/// <summary>
		/// 默认
		/// </summary>
		public const string Default = "zh-CN";
		/// <summary>
		/// ZH_CN
		/// </summary>
		public const string ZH_CN = "zh-CN";
		/// <summary>
		/// EN_US
		/// </summary>
		public const string EN_US = "en-US";

		/// <summary>
		/// 构造函数
		/// </summary>
		public Language()
		{
			Add(ZH_CN, "中文(简体)");
			Add(EN_US, "英文(美国)");
		}
	}
}
