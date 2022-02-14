using System;

namespace AhDai.Core.Models
{
	/// <summary>
	/// ApiException
	/// </summary>
	public class ApiException : Exception
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ApiException() : this(string.Empty, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message"></param>
		public ApiException(string message) : this(message, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public ApiException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
