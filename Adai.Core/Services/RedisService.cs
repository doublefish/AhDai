using Adai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Adai.Core.Services
{
	/// <summary>
	/// Redis服务
	/// </summary>
	public class RedisService : Interfaces.IRedisService
	{
		/// <summary>
		/// 配置
		/// </summary>
		public Models.RedisConfig Config { get; private set; }
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
			Config = configuration.GetRedisConfig();
			Logger = logger;
			Config.ConfigurationChangedBroadcast += ConfigurationChangedBroadcast;
			Config.ConfigurationChanged += ConfigurationChanged;
			Config.HashSlotMoved += HashSlotMoved;
			Config.ErrorMessage += ErrorMessage;
			Config.InternalError += InternalError;
			Config.ConnectionFailed += ConnectionFailed;
			Config.ConnectionRestored += ConnectionRestored;
			Utils.RedisHelper.Init(Config);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="config"></param>
		public RedisService(Models.RedisConfig config)
		{
			Config = config;
			Utils.RedisHelper.Init(Config);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public IDatabase GetDatabase(int db = -1, object asyncState = null)
		{
			return Utils.RedisHelper.GetDatabase(db, asyncState, Config);
		}

		#region 接收通知事件
		/// <summary>
		/// 当节点被明确请求通过广播重新配置时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
		{
			Logger.Debug("", $"ConfigurationChangedBroadcast=>EndPoint={e.EndPoint}");
		}

		/// <summary>
		/// 检测到配置更改时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ConfigurationChanged(object sender, EndPointEventArgs e)
		{
			Logger.Debug("", $"ConfigurationChanged=>EndPoint={e.EndPoint}");
		}

		/// <summary>
		/// 当哈希槽被重新定位时/更改集群
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void HashSlotMoved(object sender, HashSlotMovedEventArgs e)
		{
			Logger.Debug("", $"HashSlotMoved=>NewEndPoint={e.NewEndPoint},OldEndPoint={e.OldEndPoint}");
		}

		/// <summary>
		/// 发生错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ErrorMessage(object sender, RedisErrorEventArgs e)
		{
			Logger.Debug("", $"ErrorMessage=>{e.Message}");
		}

		/// <summary>
		/// 发生内部错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void InternalError(object sender, InternalErrorEventArgs e)
		{
			Logger.Debug("", e.Exception, $"InternalError=>{e.Exception.Message}");
		}

		/// <summary>
		/// 连接失败，如果重新连接成功将不会收到这个通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ConnectionFailed(object sender, ConnectionFailedEventArgs e)
		{
			Logger.Debug("", e.Exception, $"ConnectionFailed=>EndPoint={e.EndPoint},FailureType={e.FailureType},Message={e.Exception?.Message}");
		}

		/// <summary>
		/// 建立物理连接时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ConnectionRestored(object sender, ConnectionFailedEventArgs e)
		{
			Logger.Debug("", $"ConnectionRestored=>EndPoint={e.EndPoint}");
		}
		#endregion
	}
}
