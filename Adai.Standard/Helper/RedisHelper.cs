using StackExchange.Redis;
using System.Collections.Generic;

namespace Adai.Standard
{
	/// <summary>
	/// RedisHelper
	/// </summary>
	public static class RedisHelper
	{
		static readonly object Locker = new object();

		/// <summary>
		/// DbCount
		/// </summary>
		public const int DbCount = 16;

		/// <summary>
		/// 实例
		/// </summary>
		public static IDictionary<string, IConnectionMultiplexer> Instances { get; private set; }

		/// <summary>
		/// Configuration
		/// </summary>
		public static Model.RedisConfiguration Configuration { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configuration"></param>
		public static void Init(Model.RedisConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(int db = -1, object asyncState = null)
		{
			return GetConnectionMultiplexer().GetDatabase(db, asyncState);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(Model.RedisConfiguration configuration = null)
		{
			var multiplexer = GetConnectionMultiplexer(configuration);
			return multiplexer.GetDatabase(configuration.Database);
		}

		/// <summary>
		/// GetConnectionMultiplexer
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IConnectionMultiplexer GetConnectionMultiplexer(Model.RedisConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}
			var str = CreateConfiguration(configuration.Host, configuration.Port, configuration.Password);
			lock (Locker)
			{
				if (Instances == null)
				{
					Instances = new Dictionary<string, IConnectionMultiplexer>();
				}
				if (!Instances.TryGetValue(str, out var instance) || !instance.IsConnected)
				{
					instance = ConnectionMultiplexer.Connect(str);

					// 注册事件
					instance.ConfigurationChangedBroadcast += MuxerConfigurationChangedBroadcast;
					instance.HashSlotMoved += MuxerHashSlotMoved;
					instance.ErrorMessage += MuxerErrorMessage;
					instance.ConfigurationChanged += MuxerConfigurationChanged;
					instance.ConnectionFailed += MuxerConnectionFailed;
					instance.InternalError += MuxerInternalError;
					instance.ConnectionRestored += MuxerConnectionRestored;
				}
				return instance;
			}
		}

		/// <summary>
		/// CreateConfiguration
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static string CreateConfiguration(string host = "127.0.0.1", int port = 6379, string password = null)
		{
			return $"{host}:{port},password={password}";
		}

		#region 接收通知事件
		/// <summary>
		/// 当节点被明确请求通过广播重新配置时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
		{
			Log4netHelper.Debug($"ConfigurationChangedBroadcast=>=>EndPoint={e.EndPoint}");
		}

		/// <summary>
		/// 当哈希槽被重新定位时/更改集群
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
		{
			Log4netHelper.Debug($"HashSlotMoved=>NewEndPoint={e.NewEndPoint},OldEndPoint={e.OldEndPoint}");
		}

		/// <summary>
		/// 发生错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
		{
			Log4netHelper.Debug($"ErrorMessage=>{e.Message}");
		}

		/// <summary>
		/// 检测到配置更改时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
		{
			Log4netHelper.Debug($"ConfigurationChanged=>EndPoint=e.EndPoint");
		}

		/// <summary>
		/// 连接失败，如果重新连接成功将不会收到这个通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
		{
			Log4netHelper.Debug($"ConnectionFailed=>EndPoint={e.EndPoint},FailureType={e.FailureType},Message={e.Exception?.Message}", e.Exception);
		}

		/// <summary>
		/// 发生内部错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerInternalError(object sender, InternalErrorEventArgs e)
		{
			Log4netHelper.Debug($"InternalError=>{e.Exception.Message}", e.Exception);
		}

		/// <summary>
		/// 建立物理连接时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
		{
			Log4netHelper.Debug($"ConnectionRestored=>EndPoint={e.EndPoint}");
		}
		#endregion
	}
}
