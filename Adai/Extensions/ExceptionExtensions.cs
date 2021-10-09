using System;

namespace Adai.Extensions
{
	/// <summary>
	/// ExceptionExt
	/// </summary>
	public static class ExceptionExtensions
	{
		/// <summary>
		/// 获取内部Exception
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static Exception GetInner(this Exception ex)
		{
			return ex.InnerException == null ? ex : ex.InnerException.GetInner();
		}
	}
}
