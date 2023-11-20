using System;

namespace AhDai.Core.Attributes
{
	/// <summary>
	/// ErrorCodeAttribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ErrorCodeAttribute : Attribute
	{
		/// <summary>
		/// 消息
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">消息</param>
		public ErrorCodeAttribute(string message)
		{
			Message = message;
		}
	}
}
