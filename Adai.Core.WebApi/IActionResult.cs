namespace Adai.Core.WebApi
{
	/// <summary>
	/// IActionResult
	/// </summary>
	public interface IActionResult<T> : Microsoft.AspNetCore.Mvc.IActionResult
	{
		/// <summary>
		/// 状态代码
		/// </summary>
		public int Code { get; set; }
		/// <summary>
		/// 消息
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// 结果
		/// </summary>
		public T Content { get; set; }
		/// <summary>
		/// 内容类型
		/// </summary>
		public string ContentType { get; set; }
	}
}