using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Hikvision.Models.IoT;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// TestService
/// </summary>
[Attributes.Service]
internal class TestService(IServiceProvider serviceProvider) : ITestService
{
    readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync()
    {
        //await TestFileAsync();

        //await TestWeChatAsync();
        //await TestBaiduAsync();
        await TestHikvisionAsync();
        //await TestTianyanchaAsync();
    }

    protected async Task TestWeChatAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.WeChat.IWeChatOfficialAccountService>();

        var accessToken = await service.GetAccessTokenAsync(true);
    }

    protected async Task TestBaiduAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Baidu.IBaiduOcrService>();

        var accessToken = await service.GetAccessTokenAsync(true);
    }

    protected async Task TestHikvisionAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Hikvision.IHikIoTService>();

        var accessToken = await service.GetAppAccessTokenAsync(true);
        var userAccessToken = "ut-8a5dad02-dc3a-4160-9781-58230902cdc4";
        var context = new AccessContext(accessToken.AppAccessToken, userAccessToken);

        var deviceToken = await service.GetDeviceTokenAsync(context, new DeviceTokenInput()
        {
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        });

        var deviceTokens = await service.GetDeviceTokensAsync(context, [new DeviceTokenInput() {
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        }]);

        var opsToken = await service.GetOpsTokenAsync(context);

        var streamingMediaAttrs = await service.GetStreamingMediaAttrsAsync(context, new StreamingMediaAttrsInput()
        {
            DeviceSerial = "FM0666785",
            Payload = new StreamingMediaAttrsPayload()
            {
                ChannelNo = 54,
                Attrs = ["videoLevel"],
            }
        });

        var cameras = await service.PageCameraAsync(context, new PageInput());

        var devices = await service.PageDeviceAsync(context, new PageInput());

        var deviceCapacities = await service.GetDeviceCapacitiesAsync(context, new DeviceCapacitiesQueryInput()
        {
            DeviceSerial = "FM0666785",
            ResType = "Channel",
            ResIdentifier = null,
        });

        var deviceResource = await service.GetDeviceResourceAsync(context, new DeviceResourceQueryInput()
        {
            Id = null,
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        });
    }

    protected async Task TestTianyanchaAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Tianyancha.ITianyanchaService>();

        var baseInfo = await service.GetBaseInfoAsync("", "山东华建铝业集团有限公司");
        var taxpayer = await service.GetTaxpayerAsync("山东华建铝业集团有限公司");

    }

    protected async Task TestFileAsync()
    {
        var service = _serviceProvider.GetRequiredService<IBaseFileService>();
        var dir = DateTime.Now.ToString("yyyy-MM-dd"); ;
        var url = "https://erp.ahsanle.cn/Upload/2018-08/20180806090014.xlsx";
        var name = "新四中门窗结算单 ----三乐.xlsx";
        var data = await service.DownloadAsync("D:\\ErpFiles", dir, url, name);
        Console.WriteLine(data.ActualName);
    }

    static async Task GenerateRsaAsync()
    {
        using var rsa = RSA.Create(2048);
        var privateKeyBytes = rsa.ExportRSAPrivateKey();
        var publicKeyBytes = rsa.ExportRSAPublicKey();

        var privateKey = Convert.ToBase64String(privateKeyBytes);
        var publicKey = Convert.ToBase64String(publicKeyBytes);

        using var rsa1 = RSA.Create();
        rsa1.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

        using var rsa2 = RSA.Create();
        rsa2.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);


        await File.WriteAllTextAsync("private_key.pem", privateKey);
        await File.WriteAllTextAsync("public_key.pem", publicKey);

        Console.WriteLine("Private and public keys saved.");
    }
}
