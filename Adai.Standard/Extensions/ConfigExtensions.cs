namespace Adai.Standard
{
	/// <summary>
	/// ConfigExt
	/// </summary>
	public static class ConfigExtensions
	{
		/// <summary>
		/// 获取说明
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetNote(this Config.SocketState state)
		{
			return state switch
			{
				Config.SocketState.Connecting => "连接中",
				Config.SocketState.Open => "已连接",
				Config.SocketState.Closing => "关闭中",
				Config.SocketState.Closed => "已关闭",
				_ => null,
			};
		}
	}
}
