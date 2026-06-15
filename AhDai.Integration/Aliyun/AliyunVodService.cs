using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models.Vod;
using AhDai.Integration.Aliyun.Providers;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunVodService
/// </summary>
[Attributes.Service()]
internal class AliyunVodService(IAliyunVodConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseAliyunService<AliyunVodConfig, IAliyunVodConfigProvider>(configProvider, httpClientFactory, "2017-03-21", 0)
    , IAliyunVodService
{
    protected override string ServiceName => "阿里云视频点播";


    public async Task<GetPlayInfoOutput> GetPlayInfoAsync(GetPlayInfoInput input)
    {
        var query = Utils.ObjectUtls.ToSortedDictionary(input);
        //var res = await new AliyunVodTestService(Config).GetPlayInfoAsync(query);
        return await SendAsync<GetPlayInfoOutput>(HttpMethod.Post, "GetPlayInfo", null, query);
    }
}
