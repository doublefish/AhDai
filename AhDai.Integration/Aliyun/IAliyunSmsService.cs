using AhDai.Integration.Abstractions;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models.Sms;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// IAliyunSmsService
/// </summary>
public interface IAliyunSmsService : IBaseService<AliyunSmsConfig>
{
    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SendOutput> SendAsync(SendInput input);
}
