using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.System.Parameter;

/// <summary>
/// ParameterService
/// </summary>
[Attributes.Service]
internal class ParameterService : IParameterService
{
    public Task<Dictionary<string, string>> GetValueByCategoryAsync(string category, long tenantId, bool includeDeleted = false)
    {
        var result = new Dictionary<string, string>();
        switch (category)
        {
            case "BaiduOcr":
                result["Host"] = "https://aip.baidubce.com";
                result["AppId"] = "122429680";
                result["ApiKey"] = "zHkcwRutyBWLMUzhhIRvsR2k";
                result["AppSecret"] = "75e3Aad1ppVJt0Jh272IY6yaVg2ppxa9";
                break;
            case "HikIoT":
                result["Host"] = "https://open-api.hikiot.com";
                result["AppKey"] = "2061987339252871241";
                result["AppSecret"] = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAKMgx9yOzWWpfIMletirijz5IO8ttL8mMhq7r4SGk1IlWE+1LVPtmHFatobRH20a7eXzCpdujaA5P42ZuJvKNbomR8ePi0uJe7Dh5CQ2YjJA8t/ESs1nzCoZItG2KYsEqPS3qmqfzW8mAv0imwQHSDjY+6YuewvpJRbLl6+nJH6xAgMBAAECgYBghvk87dNkoNHo2LjElV0Lj2+JmEYoBfYIE59ckDWEmkyTfeYAj8tw5/ix9fTenty/AP33dZegg7+zjo7KwPDg8ALiFPMzdvYcGXm6KeAZyjiAjdQ4SaG9jfz5s219J7pQd94FTg9ggEUWCrH3C+2jgngFpckOQ4OyvH3FhDTgAQJBAM4C6sPBWvAmjv6x75TxDoRrcXwPbwJeAmy9zO1da7Y0ZpNq6KHToNIC1LgoMc3CnNHr6pGQNEZtn2TExDOJYTECQQDKtgWlcJTTd33wd0jOlIZl4L3nxJwepokp73bM0Poeo4Cv1jjLv5CzSX8orDcHIs3oF094l5X89Q4mDFaQqZWBAkBhAnSkm8+D2NRrUUT9gQoSBzpYbjgbCEPiCvqOJ/jJwyEueCB1298WJekfkyXou6T3IT3DMAG9zG9Ll3DAyAdxAkBxgbTtq3maY6mAcbqx+ha7izmrQrtMqmHZun3iOA4mA9W2IBUTecPzsG7kfnIdq85ybEMSuobA6xMuGPCL9nmBAkBDdmtnuwfKxh3IJCMq99ulSq0Iiki9HtLKPWqtuRSk2501oUylvDroJls0HNxU0cZ3uFzi765Vt0F1ecbkHPhh";
                result["EnableSecret"] = "true";
                result["Username"] = "15055192316";
                result["Password"] = "sl@112233";
                break;
            case "Tianyancha":
                result["Host"] = "http://open.api.tianyancha.com";
                result["Key"] = "0cd3f431-32c6-4eb5-912b-6955e409eb5f";
                break;
            default:
                throw new ArgumentException($"Unsupported category: {category}");
        }
        return Task.FromResult(result);
    }
}
