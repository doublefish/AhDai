using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models.OfficialAccount;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// IWeChatOfficialAccountService
/// </summary>
public interface IWeChatOfficialAccountService : IBaseWeChatService<WeChatOfficialAccountConfig>
{
    /// <summary>
    /// 创建二维码
    /// </summary>
    /// <param name="token"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<QrCodeCreateOutput> CreateQrCodeAsync(string token, QrCodeCreateInput input);

    /// <summary>
    /// 发送模板消息
    /// </summary>
    /// <param name="token"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TemplateMessageOutput> SendTemplateMessageAsync(string token, TemplateMessageInput input);

    /// <summary>
    /// 发送订阅消息
    /// </summary>
    /// <param name="token"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SubscribeMessageOutput> SendSubscribeMessageAsync(string token, SubscribeMessageInput input);
}
