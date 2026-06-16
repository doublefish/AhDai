using AhDai.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
        var action = "";
        switch (action)
        {
            case "Aliyun":
                await TestAliyunAsync();
                break;
            case "Baidu":
                await TestBaiduAsync();
                break;
            case "ESign":
                await TestESignAsync();
                break;
            case "Hikvision":
                await TestHikvisionAsync();
                break;
            case "WeChat":
                await TestWeChatAsync();
                break;
            case "File":
                await TestFileAsync();
                break;
            default: break;
        }
    }

    async Task TestAliyunAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Aliyun.IAliyunSmsService>();

        var res = await service.SendAsync(new Integration.Aliyun.Models.Sms.SendInput()
        {
            PhoneNumbers = "",
            SignName = "",
            TemplateCode = "",
            TemplateParam = new Dictionary<string, string>()
            {
                ["code"] = "1234"
            }
        });
    }

    async Task TestBaiduAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Baidu.IBaiduOcrService>();

        var accessToken = await service.GetAccessTokenAsync(true);

        var filePath = @"C:\Users\doubl\Desktop\af8a5c9a-a990-43fb-93f9-b58406f40a86.png";
        var bytes = await File.ReadAllBytesAsync(filePath);
        var base64 = Convert.ToBase64String(bytes);

        var res = await service.BankReceiptAsync(accessToken.AccessToken, new Integration.Baidu.Models.Ocr.BankReceiptInput()
        {
            Image = base64
        });
    }

    async Task TestESignAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.ESign.IESignService>();

        var res = await service.GetSignFlowDetailAsync("2f090f8cb12d4fefb7c20511916bdc81");
    }

    async Task TestHikvisionAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Hikvision.IHikIoTService>();

        var accessToken = await service.GetAppAccessTokenAsync(true);
        var authCode = "c5f1dca84ed6ac25e92f75c7ca9ae173";
        //var userAccessToken = "ut-8f53c67f-5c23-4e49-ab13-220f6b504492";

        var userAccessToken = await service.GetUserAccessTokenAsync(accessToken.AppAccessToken, new Integration.Hikvision.Models.IoT.UserAccessTokenInput()
        {
            AuthCode = authCode,
        });

        var refreshUserAccessToken = await service.RefreshUserAccessTokenAsync(accessToken.AppAccessToken, new Integration.Hikvision.Models.IoT.RefreshUserAccessTokenInput()
        {
            UserAccessToken = userAccessToken.UserAccessToken,
            RefreshUserToken = userAccessToken.RefreshUserToken,
        });

        var context = new Integration.Hikvision.Models.IoT.AccessContext(accessToken.AppAccessToken, userAccessToken.UserAccessToken);

        var deviceToken = await service.GetDeviceTokenAsync(context, new Integration.Hikvision.Models.IoT.DeviceTokenInput()
        {
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        });

        var deviceTokens = await service.GetDeviceTokensAsync(context, [new Integration.Hikvision.Models.IoT.DeviceTokenInput() {
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        }]);

        var opsToken = await service.GetOpsTokenAsync(context);

        var streamingMediaAttrs = await service.GetStreamingMediaAttrsAsync(context, new Integration.Hikvision.Models.IoT.StreamingMediaAttrsInput()
        {
            DeviceSerial = "FM0666785",
            Payload = new Integration.Hikvision.Models.IoT.StreamingMediaAttrsPayload()
            {
                ChannelNo = 54,
                Attrs = ["videoLevel"],
            }
        });

        var cameras = await service.PageCameraAsync(context, new Integration.Hikvision.Models.IoT.PageInput());

        var devices = await service.PageDeviceAsync(context, new Integration.Hikvision.Models.IoT.PageInput());

        var deviceCapacities = await service.GetDeviceCapacitiesAsync(context, new Integration.Hikvision.Models.IoT.DeviceCapacitiesQueryInput()
        {
            DeviceSerial = "FM0666785",
            ResType = "Channel",
            ResIdentifier = null,
        });

        var deviceResource = await service.GetDeviceResourceAsync(context, new Integration.Hikvision.Models.IoT.DeviceResourceQueryInput()
        {
            Id = null,
            DeviceSerial = "FM0666785",
            ChannelNo = 54,
        });
    }

    async Task TestTianyanchaAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.Tianyancha.ITianyanchaService>();

        var baseInfo = await service.GetBaseInfoAsync("", "山东华建铝业集团有限公司");
        var taxpayer = await service.GetTaxpayerAsync("山东华建铝业集团有限公司");

    }

    async Task TestWeChatAsync()
    {
        var service = _serviceProvider.GetRequiredService<Integration.WeChat.IWeChatOfficialAccountService>();

        var accessToken = await service.GetAccessTokenAsync(true);
    }

    async Task TestFileAsync()
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
