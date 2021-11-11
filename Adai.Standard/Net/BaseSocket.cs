using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Adai.Standard.Net
{
	/// <summary>
	/// BaseSocket
	/// </summary>
	public class BaseSocket : Socket
	{
		/// <summary>
		/// 编码
		/// </summary>
		public Encoding Encoding { get; protected set; }
		/// <summary>
		/// 主机
		/// </summary>
		public string Host { get; protected set; }
		/// <summary>
		/// 端口
		/// </summary>
		public int Port { get; protected set; }
		/// <summary>
		/// IPAddress
		/// </summary>
		public IPAddress IPAddress { get; protected set; }
		/// <summary>
		/// IPEndPoint
		/// </summary>
		public IPEndPoint IPEndPoint { get; protected set; }
		/// <summary>
		/// 远程
		/// </summary>
		public ICollection<Socket> Remotes { get; private set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; protected set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int State { get; private set; }
		/// <summary>
		/// 是否记录日志
		/// </summary>
		public bool Log { get; private set; }

		/// <summary>
		/// 接收消息委托
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="message"></param>
		public delegate void MessageHandler(Socket remote, string message);
		/// <summary>
		/// 连接状态改变委托
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="state"></param>
		/// <param name="message"></param>
		public delegate void StateChangeHandler(Socket remote, Config.SocketState state, string message);

		/// <summary>
		/// 接收消息事件
		/// </summary>
		public event MessageHandler MessageEvent;
		/// <summary>
		/// 连接状态改变事件
		/// </summary>
		public event StateChangeHandler StateChangeEvent;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="name"></param>
		/// <param name="log"></param>
		public BaseSocket(string host, int port = 8080, string name = null, bool log = false)
			: base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
		{
			Encoding = Encoding.UTF8;
			Host = host;
			Port = port;
			Name = name;
			Log = log;

			IPAddress = IPAddress.Parse(host);
			IPEndPoint = new IPEndPoint(IPAddress, port);
			Remotes = new HashSet<Socket>();
		}

		/// <summary>
		/// 开启连接
		/// </summary>
		/// <param name="remote"></param>
		protected void Open(Socket remote)
		{
			ChangeState(remote, Config.SocketState.Open, "开启连接");
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="reason"></param>
		protected void Close(Socket remote, string reason = "close")
		{
			ChangeState(remote, Config.SocketState.Closed, reason);
			remote.Shutdown(SocketShutdown.Both);
			if (remote.Connected)
			{
				remote.Close();
			}
		}

		/// <summary>
		/// 接收消息
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="bytes"></param>
		/// <param name="size"></param>
		protected void Receive(Socket remote, byte[] bytes, int size)
		{
			var message = Encoding.GetString(bytes, 0, size);
			Utils.Log4netHelper.Info($"接收来自【{remote.RemoteEndPoint}】的消息=>{message}");
			MessageEvent?.Invoke(remote, message);
		}

		/// <summary>
		/// 客户端发送消息给服务端
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public int Send(string message)
		{
			if (!Connected && !IsBound)
			{
				return 0;
			}
			Utils.Log4netHelper.Info($"【{LocalEndPoint}】发送消息给【{RemoteEndPoint}】=>{message}");
			return Send(Encoding.GetBytes(message));
		}

		/// <summary>
		/// 服务端发送消息给客户端
		/// </summary>
		/// <param name="message"></param>
		/// <param name="remotes"></param>
		/// <returns></returns>
		public int Send(string message, params Socket[] remotes)
		{
			var i = 0;
			foreach (var remote in remotes)
			{
				Utils.Log4netHelper.Info($"【{remote.LocalEndPoint}】发送消息给【{remote.RemoteEndPoint}】=>{message}");
				i += remote.Send(Encoding.GetBytes(message));
			}
			return i;
		}

		/// <summary>
		/// 客户端发送消息给服务端
		/// </summary>
		/// <param name="message"></param>
		/// <param name="remote"></param>
		/// <returns></returns>
		public int Send(string message, Socket remote)
		{
			if (!Connected && !IsBound)
			{
				return 0;
			}
			Utils.Log4netHelper.Info($"【{remote.LocalEndPoint}】发送消息给【{remote.RemoteEndPoint}】=>{message}");
			return remote.Send(Encoding.GetBytes(message));
		}

		/// <summary>
		/// 改变状态
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="state"></param>
		/// <param name="message"></param>
		protected void ChangeState(Socket remote, Config.SocketState state, string message)
		{
			Send(message, remote);
			Utils.Log4netHelper.Info($"【{remote.RemoteEndPoint}】的连接状态变为=>[{state}],{message}");
			StateChangeEvent?.Invoke(remote, state, message);
		}

		/// <summary>
		/// 监听消息
		/// </summary>
		/// <param name="client"></param>
		protected void ListenReceive(Socket client = null)
		{
			var thread = new Thread((obj) =>
			{
				var client = (Socket)obj;
				while (true)
				{
					try
					{
						var bytes = new byte[1024];
						var size = client.Receive(bytes, bytes.Length, SocketFlags.None);
						if (size == 0)
						{
							continue;
						}
						Receive(client, bytes, size);
					}
					catch (Exception ex)
					{
						Utils.Log4netHelper.Info($"接收来自【{client.RemoteEndPoint}】的消息发生异常=>{ex.Message}");
						Utils.Log4netHelper.Error($"接收消息发生异常=>{ex}");
						Close(client, ex.Message);
						break;
					}
				}
			})
			{
				Name = Name
			};
			thread.Start(client ?? (this));
		}
	}
}
