using System.Threading;

namespace Adai.Standard.Models
{
	/// <summary>
	/// SocketService
	/// </summary>
	public class SocketService : BaseSocket
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="name"></param>
		/// <param name="log"></param>
		public SocketService(string host = null, int port = 0, string name = null, bool log = false)
			: base(host, port, name, log)
		{
			// 绑定IP地址和端口
			Bind(IPEndPoint);
			// 设定最多10个排队连接请求
			Listen(10);
			Utils.Log4netHelper.Info($"服务启动成功=>{LocalEndPoint}");

			// 监听客户端消息
			var thread = new Thread(() =>
			{
				while (true)
				{
					var remote = Accept();
					Open(remote);
					// 启动监听接收消息
					ListenReceive(remote);
					Remotes.Add(remote);
				}
			});
			thread.Start();
		}

	}
}
