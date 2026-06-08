using AhDai.Integration.Abstractions;
using AhDai.Integration.AntChain.Configs;
using AhDai.Integration.AntChain.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.AntChain;

/// <summary>
/// IAntChainNotaryService
/// </summary>
public interface IAntChainNotaryService : IBaseService<AntChainNotaryConfig>
{
    /// <summary>
    /// 创建存证事务
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TwcNotaryTransCreateOutput> CreateTransAsync(TwcNotaryTransCreateInput input);

    /// <summary>
    /// 获取事务中的所有存证信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TwcNotaryTransGetOutput> GetTransAsync(TwcNotaryTransGetInput input);

    /// <summary>
    /// 创建文件存证
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TwcNotaryFileCreateOutput> CreateFileAsync(TwcNotaryFileCreateInput input);

    /// <summary>
    /// 获取文件存证内容
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TwcNotaryFileGetOutput> GetFileAsync(TwcNotaryFileGetInput input);

    /// <summary>
    /// 获取蚂蚁链存证证明
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TwcNotaryCertificateDetailGetOutput> GetCertificateDetailAsync(TwcNotaryCertificateDetailGetInput input);
}
