using AhDai.Integration.Abstractions;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models.Vod;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// IAliyunVodService
/// </summary>
public interface IAliyunVodService : IBaseService<AliyunVodConfig>
{
    /// <summary>
    /// 获取对象链接
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<GetPlayInfoOutput> GetPlayInfoAsync(GetPlayInfoInput input);
}
