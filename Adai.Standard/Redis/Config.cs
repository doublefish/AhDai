﻿using StackExchange.Redis;
using System;

namespace Adai.Standard.Redis
{
	/// <summary>
	/// Config
	/// </summary>
	public class Config
	{
		/// <summary>
		/// Host
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// Port
		/// </summary>
		public int Port { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// Database
		/// </summary>
		public int Database { get; set; }

		#region 注册事件
		/// <summary>
		/// 当节点被明确请求通过广播重新配置时
		/// </summary>
		public EventHandler<EndPointEventArgs> ConfigurationChangedBroadcast;
		/// <summary>
		/// 检测到配置更改时
		/// </summary>
		public EventHandler<EndPointEventArgs> ConfigurationChanged;
		/// <summary>
		/// 当哈希槽被重新定位时/更改集群
		/// </summary>
		public EventHandler<HashSlotMovedEventArgs> HashSlotMoved;
		/// <summary>
		/// 发生错误时
		/// </summary>
		public EventHandler<RedisErrorEventArgs> ErrorMessage;
		/// <summary>
		/// 发生内部错误时
		/// </summary>
		public EventHandler<InternalErrorEventArgs> InternalError;
		/// <summary>
		/// 连接失败，如果重新连接成功将不会收到这个通知
		/// </summary>
		public EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
		/// <summary>
		/// 建立物理连接时
		/// </summary>
		public EventHandler<ConnectionFailedEventArgs> ConnectionRestored;
		#endregion
	}
}
