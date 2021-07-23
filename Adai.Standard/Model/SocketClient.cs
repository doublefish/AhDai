namespace Adai.Standard.Model
{
	/// <summary>
	/// SocketClient
	/// </summary>
	public class SocketClient : BaseSocket
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="name"></param>
		/// <param name="log"></param>
		public SocketClient(string host = null, int port = 0, string name = null, bool log = false)
			: base(host, port, name, log)
		{
			// 连接到服务器
			Connect(IPEndPoint);
			LogHelper.Info($"连接服务端【{RemoteEndPoint}】成功");

			// 启动监听接收消息
			ListenReceive();
		}
	}
}
