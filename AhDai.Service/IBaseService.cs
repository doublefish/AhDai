using System.Threading.Tasks;
using AhDai.Service.Models;

namespace AhDai.Service;

/// <summary>
/// IBaseService
/// </summary>
public interface IBaseService
{
}

/// <summary>
/// IBaseService
/// </summary>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
public interface IBaseService<TOutput, TQueryInput>
    : IBaseService
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="dataDepth">数据深度</param>
    /// <returns></returns>
    Task<TOutput> GetByIdAsync(long id, int dataDepth = 1, bool includeDeleted = false);

    /// <summary>
    /// 根据Id查询（批量）
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <returns></returns>
    Task<TOutput[]> GetByIdsAsync(long[] ids, int dataDepth = 1, bool includeDeleted = false);

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TOutput[]> GetAsync(TQueryInput input);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData<TOutput>> PageAsync(TQueryInput input);

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<long> CountAsync(TQueryInput input);
}


/// <summary>
/// IBaseService
/// </summary>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
public interface IBaseService<TInput, TOutput, TQueryInput>
    : IBaseService<TOutput, TQueryInput>
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<long> AddAsync(TInput input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(long id, TInput input);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteByIdAsync(long id);

    /// <summary>
    /// 删除（批量）
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task DeleteByIdsAsync(long[] ids);
}

