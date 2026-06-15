using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// RoadTransportCertificateOutput
/// </summary>
public class RoadTransportCertificateOutput : BasePdfOutput<Dictionary<string, WordResult[]>>
{
    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrRoadTransportCertificateFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrRoadTransportCertificateFriendlyOutput()
        {
            Number = WordsResult.GetValueOrDefault("道路运输证号")?.FirstOrDefault()?.Word,
            OwnerName = WordsResult.GetValueOrDefault("业户名称")?.FirstOrDefault()?.Word,
            Address = WordsResult.GetValueOrDefault("地址")?.FirstOrDefault()?.Word,
            PlateNumber = WordsResult.GetValueOrDefault("车辆号牌")?.FirstOrDefault()?.Word,
            BusinessLicenseNumber = WordsResult.GetValueOrDefault("经营许可证")?.FirstOrDefault()?.Word,
            VehicleType = WordsResult.GetValueOrDefault("车辆类型")?.FirstOrDefault()?.Word,
            TonnageSeat = WordsResult.GetValueOrDefault("吨座位")?.FirstOrDefault()?.Word,
            Length = WordsResult.GetValueOrDefault("车辆毫米_长")?.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            Width = WordsResult.GetValueOrDefault("车辆毫米_宽")?.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            Height = WordsResult.GetValueOrDefault("车辆毫米_高")?.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            BusinessScope = WordsResult.GetValueOrDefault("经营范围")?.FirstOrDefault()?.Word,
            EconomyType = WordsResult.GetValueOrDefault("经济类型")?.FirstOrDefault()?.Word,
            Remark = WordsResult.GetValueOrDefault("备注")?.FirstOrDefault()?.Word,
            IssuingAuthority = WordsResult.GetValueOrDefault("签发机关")?.FirstOrDefault()?.Word,
            FirstIssueDate = WordsResult.GetValueOrDefault("初领日期")?.FirstOrDefault()?.Word?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            IssueDate = WordsResult.GetValueOrDefault("发证日期")?.FirstOrDefault()?.Word?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            ExpiryDate = WordsResult.GetValueOrDefault("至")?.FirstOrDefault()?.Word?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
        };
        return output;
    }
}
