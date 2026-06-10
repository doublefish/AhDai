using AhDai.Integration.Abstractions;
using AhDai.Integration.Aliyun.Configs;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// IBaseAliyunService
/// </summary>
public interface IBaseAliyunService<TConfig> : IBaseService<TConfig> where TConfig : BaseAliyunConfig
{
}
