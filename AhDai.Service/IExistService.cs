using AhDai.Service.Models;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IExistService
/// </summary>
public interface IExistService<TInput>
    where TInput : BaseExistInput
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<bool> ExistAsync(long id, TInput input);
}

/// <summary>
/// ICodeExistService
/// </summary>
public interface ICodeExistService<TInput> : IExistService<TInput>
    where TInput : CodeExistInput
{

}