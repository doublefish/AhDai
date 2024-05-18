using AhDai.Core;
using AhDai.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// BaseCrudController
/// </summary>
/// <typeparam name="Service"></typeparam>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
/// <param name="service"></param>
[Route("api/v1/[controller]")]
public abstract class BaseCrudController<Service, TInput, TOutput, TQueryInput>(Service service)
    : BaseQueryController<Service, TOutput, TQueryInput>(service)
    where Service : class, IBaseService<TInput, TOutput, TQueryInput>
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost()]
    public virtual async Task<IApiResult<long>> AddAsync([FromBody] TInput input)
    {
        var res = await _service.AddAsync(input);
        return ApiResult.Success(res);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public virtual async Task<IApiResult<string>> UpdateAsync([FromRoute] long id, [FromBody] TInput input)
    {
        await _service.UpdateAsync(id, input);
        return ApiResult.Success();
    }

    ///// <summary>
    ///// 删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpDelete("{id}")]
    //public virtual async Task<IApiResult<string>> DeleteByIdAsync([FromRoute] long id)
    //{
    //	await _service.DeleteByIdAsync(id);
    //	return ApiResult.Success();
    //}

    /// <summary>
    /// 删除（批量）
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("{ids}")]
    public virtual async Task<IApiResult<string>> DeleteByIdsAsync([FromRoute] long[] ids)
    {
        await _service.DeleteByIdsAsync(ids);
        return ApiResult.Success();
    }

}
