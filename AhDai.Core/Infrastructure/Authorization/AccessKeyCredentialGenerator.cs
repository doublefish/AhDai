using AhDai.Core.Models.Authorization;
using System;
using System.Security.Cryptography;

namespace AhDai.Core.Infrastructure.Authorization;

/// <summary>
/// AccessKey 凭据生成器
/// </summary>
public class AccessKeyCredentialGenerator
{
    /// <summary>
    /// 生成一组新的 AccessKey 凭据
    /// </summary>
    public static AccessKeyCredential Generate()
    {
        var accessKeyId = Guid.NewGuid().ToString("N");
        var accessKeySecret = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return new AccessKeyCredential(accessKeyId, accessKeySecret);
    }
}
