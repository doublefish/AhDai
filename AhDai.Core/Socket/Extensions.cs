namespace AhDai.Core.Socket1
{
	/// <summary>
	/// ConfigExt
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// 获取说明
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetNote(this SocketState state)
		{
			return state switch
			{
				SocketState.Connecting => "连接中",
				SocketState.Open => "已连接",
				SocketState.Closing => "关闭中",
				SocketState.Closed => "已关闭",
				_ => null,
			};
		}
	}
}
