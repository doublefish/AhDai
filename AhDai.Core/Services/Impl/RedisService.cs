using AhDai.Core.Configs;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections.Generic;

namespace AhDai.Core.Services.Impl
{
    /// <summary>
    /// Redis服务
    /// </summary>
    public class RedisService : IRedisService
	{
		readonly IDictionary<string, IConnectionMultiplexer> ConnectionMultiplexers;
		readonly object Locker;

		/// <summary>
		/// 配置
		/// </summary>
		public RedisConfig Config { get; private set; }
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger<RedisService> Logger { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public RedisService(IConfiguration configuration, ILogger<RedisService> logger)
		{
			ConnectionMultiplexers = new Dictionary<string, IConnectionMultiplexer>();
			Locker = new object();

			Config = configuration.GetRedisConfig();
			Logger = logger;
			Config.ConfigurationChangedBroadcast += ConfigurationChangedBroadcast;
			Config.ConfigurationChanged += ConfigurationChanged;
			Config.HashSlotMoved += HashSlotMoved;
			Config.ErrorMessage += ErrorMessage;
			Config.InternalError += InternalError;
			Config.ConnectionFailed += ConnectionFailed;
			Config.ConnectionRestored += ConnectionRestored;
			Logger.Debug("", $"Init=>Config={JsonUtil.Serialize(Config)}");
		}

		/// <summary>
		/// 创建连接配置
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="password"></param>
		/// <param name="abortConnect"></param>
		/// <returns></returns>
		public string CreateConfiguration(string host, int port = 6379, string password = null, bool abortConnect = false)
		{
			return $"{host}:{port},password={password},abortConnect={abortConnect}";
		}

		/// <summary>
		/// 创建连接配置
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public string CreateConfiguration(RedisConfig config = null)
		{
			var c = config ?? Config;
			return CreateConfiguration(c.Host, c.Port, c.Password, c.AbortConnect);
		}

		/// <summary>
		/// 创建连接器
		/// </summary>
		/// <param name="configuration">配置</param>
		/// <returns></returns>
		public IConnectionMultiplexer CreateConnectionMultiplexer(string configuration)
		{
			var options = ConfigurationOptions.Parse(configuration);
			var multiplexer = ConnectionMultiplexer.Connect(options);
			// 注册事件
			multiplexer.ConfigurationChangedBroadcast += ConfigurationChangedBroadcast;
			multiplexer.ConfigurationChanged += ConfigurationChanged;
			multiplexer.HashSlotMoved += HashSlotMoved;
			multiplexer.ErrorMessage += ErrorMessage;
			multiplexer.InternalError += InternalError;
			multiplexer.ConnectionFailed += ConnectionFailed;
			multiplexer.ConnectionRestored += ConnectionRestored;
			return multiplexer;
		}

		/// <summary>
		/// 创建连接器
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public IConnectionMultiplexer CreateConnectionMultiplexer(RedisConfig config = null)
		{
			var c = config ?? Config;
			var configString = CreateConfiguration(c);
			return CreateConnectionMultiplexer(configString);
		}

		/// <summary>
		/// 获取连接实例
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public IConnectionMultiplexer GetConnectionMultiplexer(RedisConfig config = null)
		{
			var c = config ?? Config;
			var configString = CreateConfiguration(c);
			if (!ConnectionMultiplexers.TryGetValue(configString, out var multiplexer) || !multiplexer.IsConnected)
			{
				lock (Locker)
				{
					multiplexer = CreateConnectionMultiplexer(configString);
					ConnectionMultiplexers[configString] = multiplexer;
				}
			}
			return multiplexer;
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public IDatabase GetDatabase(int db = -1, object asyncState = null, RedisConfig config = null)
		{
			var multiplexer = GetConnectionMultiplexer(config);
			return multiplexer.GetDatabase(db, asyncState);
		}

		#region 接收通知事件
		/// <summary>
		/// 当节点被明确请求通过广播重新配置时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Logger.Debug("", $"ConfigurationChangedBroadcast=>EndPoint={e.EndPoint}");
        }

		/// <summary>
		/// 检测到配置更改时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Logger.Debug("", $"ConfigurationChanged=>EndPoint={e.EndPoint}");
        }

		/// <summary>
		/// 当哈希槽被重新定位时/更改集群
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Logger.Debug("", $"HashSlotMoved=>NewEndPoint={e.NewEndPoint},OldEndPoint={e.OldEndPoint}");
        }

		/// <summary>
		/// 发生错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Logger.Debug("", $"ErrorMessage=>{e.Message}");
        }

		/// <summary>
		/// 发生内部错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void InternalError(object sender, InternalErrorEventArgs e)
        {
            Logger.Debug("", e.Exception, $"InternalError=>{e.Exception.Message}");
        }

		/// <summary>
		/// 连接失败，如果重新连接成功将不会收到这个通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Logger.Debug("", e.Exception, $"ConnectionFailed=>EndPoint={e.EndPoint},FailureType={e.FailureType},Message={e.Exception?.Message}");
        }

		/// <summary>
		/// 建立物理连接时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Logger.Debug("", $"ConnectionRestored=>EndPoint={e.EndPoint}");
        }
        #endregion
    }
}
