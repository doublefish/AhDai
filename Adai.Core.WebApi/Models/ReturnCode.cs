namespace Adai.Core.WebApi.Models
{
	/// <summary>
	/// ReturnCode
	/// </summary>
	public enum ReturnCode
	{
		/// <summary>
		/// 正常
		/// </summary>
		OK = 0,
		/// <summary>
		/// 登录超时
		/// </summary>
		LoginTimeout = 99,
		/// <summary>
		/// 自定义异常
		/// </summary>
		CustomException = 100,
		/// <summary>
		/// 接口异常
		/// </summary>
		ApiException = 200,
		/// <summary>
		/// 系统异常
		/// </summary>
		SystemException = 300
	}
}
