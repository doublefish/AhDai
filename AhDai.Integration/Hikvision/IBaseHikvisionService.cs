using AhDai.Integration.Abstractions;
using AhDai.Integration.Hikvision.Configs;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// IBaseHikvisionService
/// </summary>
public interface IBaseHikvisionService<TConfig> : IBaseService<TConfig> where TConfig : BaseHikvisionConfig
{
}
