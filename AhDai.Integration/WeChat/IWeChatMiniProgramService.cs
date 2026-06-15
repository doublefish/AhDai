using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models.MiniProgram;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// IWeChatMiniProgramService
/// </summary>
public interface IWeChatMiniProgramService : IBaseWeChatService<WeChatMiniProgramConfig>
{
    /// <summary>
    /// 根据用户授权码获取会话密钥
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<SessionOutput> GetSessionByJsCodeAsync(string code);
}
