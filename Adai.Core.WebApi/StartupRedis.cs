using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adai.Standard;
using Adai.Standard.Utils;
using StackExchange.Redis;

namespace Adai.WebApi
{
	public class StartupRedis
	{
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
		static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
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
