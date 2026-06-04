using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunVodService
/// </summary>
internal class AliyunVodService(IAliyunVodConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : AliyunService<AliyunVodConfig, IAliyunVodConfigProvider>(configProvider, httpClientFactory, "2017-03-21", 0), IAliyunVodService
{
    public async Task<GetPlayInfoOutput> GetPlayInfoAsync(GetPlayInfoInput input)
    {
        var query = Utils.ObjectUtls.ToSortedDictionary(input);
        //var res = await new AliyunVodTestService(Config).GetPlayInfoAsync(query);
        return await SendAsync<GetPlayInfoOutput>(HttpMethod.Post, "GetPlayInfo", null, query);
    }
}
