using AhDai.Integration.Abstractions;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using System.Collections.Generic;
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
    /// <param name="phoneNumber"></param>
    /// <param name="templateCode"></param>
    /// <param name="templateParam"></param>
    /// <param name="signName"></param>
    /// <returns></returns>
    Task<SmsOutput> SendAsync(string phoneNumber, string templateCode, IDictionary<string, string> templateParam, string signName);
}
