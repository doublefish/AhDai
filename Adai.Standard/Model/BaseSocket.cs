using Adai.Config;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Adai.Standard.Model
{
	/// <summary>
	/// BaseSocket
	/// </summary>
	public class BaseSocket : Socket
	{
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
		/// <param name="state"></param>
		/// <param name="message"></param>
		public delegate void StateChangeHandler(Socket remote, int state, string message);

		/// <summary>
		/// 接收消息事件
		/// </summary>
		public event MessageHandler MessageEvent;
		/// <summary>
		/// 连接状态改变事件
		/// </summary>
		public event StateChangeHandler StateChangeEvent;

		/// <summary>
		/// 开启连接
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="state"></param>
		protected void Open(Socket remote, int state = SocketState.Open)
		{
			ChangeState(remote, state, "开启连接");
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="state"></param>
		/// <param name="reason"></param>
		protected void Close(Socket remote, int state = SocketState.Closed, string reason = "close")
		{
			ChangeState(remote, state, reason);
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
			InfoFormat("接收【{0}】的消息=>{1}", remote.RemoteEndPoint.ToString(), message);
			MessageEvent?.Invoke(remote, message);
		}

		/// <summary>
		/// 发送消息（客户端）
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public int Send(string message)
		{
			if (!Connected && !IsBound)
			{
				return 0;
			}
			InfoFormat("【{0}】发送消息给【{1}】=>{2}", LocalEndPoint, RemoteEndPoint, message);
			return Send(Encoding.GetBytes(message));
		}

		/// <summary>
		/// 发送消息（服务端）
		/// </summary>
		/// <param name="message"></param>
		/// <param name="remotes"></param>
		/// <returns></returns>
		public int Send(string message, params Socket[] remotes)
		{
			var i = 0;
			foreach (var remote in remotes)
			{
				InfoFormat("【{0}】发送消息给【{1}】=>{2}", remote.LocalEndPoint, remote.RemoteEndPoint, message);
				i += remote.Send(Encoding.GetBytes(message));
			}
			return i;
		}

		/// <summary>
		/// 发送消息（服务端）
		/// </summary>
		/// <param name="message"></param>
		/// <param name="remotes"></param>
		/// <returns></returns>
		public int Send(string message, Socket remote)
		{
			if (!Connected && !IsBound)
			{
				return 0;
			}
			InfoFormat("【{0}】发送消息给【{1}】=>{2}", remote.LocalEndPoint, remote.RemoteEndPoint, message);
			return remote.Send(Encoding.GetBytes(message));
		}

		/// <summary>
		/// 改变状态
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="state"></param>
		/// <param name="message"></param>
		protected void ChangeState(Socket remote, int state, string message)
		{
			Send(message, remote);
			var note = ConfigIntHelper<SocketState>.GetValue(state);
			InfoFormat("【{0}】的连接状态变为=>[{1}]{2}，{3}", remote.RemoteEndPoint.ToString(), state, note, message);
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
						InfoFormat("接收远程【{0}】消息报错=>{1}", client.RemoteEndPoint.ToString(), ex.Message);
						Error("接收消息", ex);
						Close(client, SocketState.Closed, ex.Message);
						break;
					}
				}
			})
			{
				Name = Name
			};
			thread.Start(client ?? (this));
		}

		/// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		protected void InfoFormat(string format, params object[] args)
		{
			Log4netHelper.InfoFormat(format, args);
		}

		/// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		protected void ErrorFormat(string format, params object[] args)
		{
			Log4netHelper.ErrorFormat(format, args);
		}

		/// <summary>
		/// InfoFormat
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		protected static void Info(string message, Exception exception = null)
		{
			Log4netHelper.Info(message, exception);
		}

		/// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		protected void Error(string message, Exception exception = null)
		{
			Log4netHelper.Error(message, exception);
		}
	}
}
