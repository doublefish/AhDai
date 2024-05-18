using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// BaseQueryController
/// </summary>
/// <typeparam name="Service"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
/// <param name="service"></param>
[Route("api/v1/[controller]")]
public abstract class BaseQueryController<Service, TOutput, TQueryInput>(Service service)
    : BaseEmptyController<Service>(service)
    where Service : class, IBaseService<TOutput, TQueryInput>
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dataDepth"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public virtual async Task<IApiResult<TOutput>> GetByIdAsync([FromRoute] long id, int dataDepth = 2)
    {
        var res = await _service.GetByIdAsync(id, dataDepth);
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet()]
    public virtual async Task<IApiResult<TOutput[]>> GetAsync([FromQuery] TQueryInput input)
    {
        var res = await _service.GetAsync(input);
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("page")]
    public virtual async Task<IApiResult<PageData<TOutput>>> PageAsync([FromQuery] TQueryInput input)
    {
        input.DataDepth = input.DataDepth ?? 2;
        var res = await _service.PageAsync(input);
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("count")]
    public virtual async Task<IApiResult<long>> CountAsync([FromQuery] TQueryInput input)
    {
        var res = await _service.CountAsync(input);
        return ApiResult.Success(res);
    }
}
