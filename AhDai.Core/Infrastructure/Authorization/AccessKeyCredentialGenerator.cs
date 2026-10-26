using AhDai.Core.Models.Authorization;
using System;
using System.Security.Cryptography;

namespace AhDai.Core.Infrastructure.Authorization;

/// <summary>
/// AccessKey 凭据生成器
/// </summary>
public class AccessKeyCredentialGenerator
{
    const string SecretChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    /// <summary>
    /// 生成一组新的 AccessKey 凭据
    /// </summary>
    public static AccessKeyCredential Generate()
    {
        var accessKeyId = Guid.NewGuid().ToString("N");
        var chars = new char[32];
        for (var i = 0; i < chars.Length; i++)
        {
            chars[i] = SecretChars[RandomNumberGenerator.GetInt32(SecretChars.Length)];
        }
        var accessKeySecret = new string(chars);
        return new AccessKeyCredential(accessKeyId, accessKeySecret);
    }
}
