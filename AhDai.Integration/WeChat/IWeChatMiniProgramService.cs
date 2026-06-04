using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// IWeChatMiniProgramService
/// </summary>
public interface IWeChatMiniProgramService : IWeChatService<WeChatMiniProgramConfig>
{
    /// <summary>
    /// 根据用户授权码获取会话密钥
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<SessionOutput> GetSessionByJsCodeAsync(string code);
}
