namespace Adai.Standard.Config
{
	/// <summary>
	/// SocketState
	/// </summary>
	public enum SocketState
	{
		/// <summary>
		/// 连接中
		/// </summary>
		Connecting = 0,
		/// <summary>
		/// 已连接
		/// </summary>
		Open = 1,
		/// <summary>
		/// 关闭中
		/// </summary>
		Closing = 2,
		/// <summary>
		/// 已关闭
		/// </summary>
		Closed = 3
	}
}
