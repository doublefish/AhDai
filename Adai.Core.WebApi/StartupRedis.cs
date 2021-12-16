using Adai.Standard.Utils;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Adai.WebApi
{
	/// <summary>
	/// StartupRedis
	/// </summary>
	public class StartupRedis
	{
		#region 接收通知事件
		/// <summary>
		/// 当节点被明确请求通过广播重新配置时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug($"ConfigurationChangedBroadcast=>=>EndPoint={e.EndPoint}");
		}

		/// <summary>
		/// 检测到配置更改时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void ConfigurationChanged(object sender, EndPointEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug($"ConfigurationChanged=>EndPoint={e.EndPoint}");
		}

		/// <summary>
		/// 当哈希槽被重新定位时/更改集群
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void HashSlotMoved(object sender, HashSlotMovedEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug($"HashSlotMoved=>NewEndPoint={e.NewEndPoint},OldEndPoint={e.OldEndPoint}");
		}

		/// <summary>
		/// 发生错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void ErrorMessage(object sender, RedisErrorEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug($"ErrorMessage=>{e.Message}");
		}

		/// <summary>
		/// 发生内部错误时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void InternalError(object sender, InternalErrorEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug(e.Exception, $"InternalError=>{e.Exception.Message}");
		}

		/// <summary>
		/// 连接失败，如果重新连接成功将不会收到这个通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void ConnectionFailed(object sender, ConnectionFailedEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug(e.Exception, $"ConnectionFailed=>EndPoint={e.EndPoint},FailureType={e.FailureType},Message={e.Exception?.Message}");
		}

		/// <summary>
		/// 建立物理连接时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void ConnectionRestored(object sender, ConnectionFailedEventArgs e)
		{
			var logger = GetLogger();
			logger?.LogDebug($"ConnectionRestored=>EndPoint={e.EndPoint}");
		}
		#endregion

		/// <summary>
		/// GetLogger
		/// </summary>
		/// <returns></returns>
		static ILogger GetLogger()
		{
			return ServiceHelper.GetService<ILogger>();
		}
	}
}
