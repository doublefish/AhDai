using AhDai.Integration.Abstractions;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models.Pay;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// IWeChatPayService
/// </summary>
public interface IWeChatPayService : IBaseService<WeChatPayConfig>
{
    /// <summary>
    /// H5下单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<H5OrderOutput> CreateH5OrderAsync(H5OrderInput input);

    /// <summary>
    /// Native下单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<NativeOrderOutput> CreateNativeOrderAsync(NativeOrderInput input);

    /// <summary>
    /// 获取通知结果
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<OrderNotifyOutput> GetNotifyResultAsync(HttpContext httpContext);

    /// <summary>
    /// 下载交易账单
    /// </summary>
    /// <param name="date"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<object> DownloadBillAsync(DateTime date, string type = "SUCCESS");

}
