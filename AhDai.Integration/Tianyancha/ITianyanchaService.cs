using AhDai.Integration.Tianyancha.Configs;
using AhDai.Integration.Tianyancha.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Tianyancha;

/// <summary>
/// ITianyanchaService
/// </summary>
public interface ITianyanchaService : IBaseService<TianyanchaConfig>
{
    /// <summary>
    /// 查询基本信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<BaseInfoOutput> GetBaseInfoAsync(string id, string name);

    /// <summary>
    /// 查询纳税人资质
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PageOutput<TaxpayerOutput>> GetTaxpayerAsync(string keyword, int pageNum = 1, int pageSize = 20);
}
