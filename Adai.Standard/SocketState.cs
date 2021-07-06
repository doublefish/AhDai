namespace Adai.Standard
{
	public class SocketState : Adai.Model.Config
	{
		/// <summary>
		/// 连接中
		/// </summary>
		public const int Connecting = 0;
		/// <summary>
		/// 已连接
		/// </summary>
		public const int Open = 1;
		/// <summary>
		/// 关闭中
		/// </summary>
		public const int Closing = 2;
		/// <summary>
		/// 已关闭
		/// </summary>
		public const int Closed = 3;

		/// <summary>
		/// 构造函数
		/// </summary>
		public SocketState()
		{
			Add(Connecting, "连接中");
			Add(Open, "已连接");
			Add(Closing, "关闭中");
			Add(Closed, "已关闭");
		}
	}
}
