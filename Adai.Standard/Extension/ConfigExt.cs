using System;
using System.Collections.Generic;
using System.Text;

namespace Adai.Standard.Extension
{
	/// <summary>
	/// ConfigExt
	/// </summary>
	public static class ConfigExt
	{
		/// <summary>
		/// 获取说明
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetNote(this Config.SocketState state)
		{
			switch (state)
			{
				case Config.SocketState.Connecting: return "连接中";
				case Config.SocketState.Open: return "已连接";
				case Config.SocketState.Closing: return "关闭中";
				case Config.SocketState.Closed: return "已关闭";
				default: return null;
			}
		}
	}
}
