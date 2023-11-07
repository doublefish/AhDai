using StackExchange.Redis;
using System;

namespace AhDai.Core.Configs
{
    /// <summary>
    /// Config
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 连接失败时放弃连接
        /// 如果为 true，则 Connect 不会在没有可用服务器时创建连接
        /// </summary>
        public bool AbortConnect { get; set; }
        /// <summary>
        /// 默认库
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
