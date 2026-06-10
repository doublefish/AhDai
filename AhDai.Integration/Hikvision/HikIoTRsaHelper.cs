using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// HikIoTRsaHelper
/// </summary>
public static class HikIoTRsaHelper
{
    const int PaddingOverhead = 11;

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string EncryptWithPrivateKey(string privateKey, string content)
    {
        using var rsa = CreateRsa(privateKey);

        var data = Encoding.UTF8.GetBytes(content);
        var keySize = rsa.KeySize / 8;
        var maxBlock = keySize - PaddingOverhead;

        using var ms = new MemoryStream();
        for (var i = 0; i < data.Length; i += maxBlock)
        {
            var len = Math.Min(maxBlock, data.Length - i);
            var block = new byte[len];
            Array.Copy(data, i, block, 0, len);

            var encrypted = rsa.Encrypt(block, RSAEncryptionPadding.Pkcs1);
            ms.Write(encrypted, 0, encrypted.Length);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string DecryptWithPrivateKey(string privateKey, string content)
    {
        using var rsa = CreateRsa(privateKey);

        var data = Convert.FromBase64String(content);
        var keySize = rsa.KeySize / 8;

        using var ms = new MemoryStream();
        for (var i = 0; i < data.Length; i += keySize)
        {
            var len = Math.Min(keySize, data.Length - i);
            var block = new byte[len];
            Array.Copy(data, i, block, 0, len);

            var decrypted = rsa.Decrypt(block, RSAEncryptionPadding.Pkcs1);
            ms.Write(decrypted, 0, decrypted.Length);
        }

        var result = Encoding.UTF8.GetString(ms.ToArray());
        return HttpUtility.UrlDecode(result);
    }


    private static RSA CreateRsa(string privateKey)
    {
        var rsa = RSA.Create();
        ImportPrivateKeyFlexible(rsa, Convert.FromBase64String(privateKey));
        return rsa;
    }

    private static void ImportPrivateKeyFlexible(RSA rsa, byte[] keyBytes)
    {
        try
        {
            rsa.ImportPkcs8PrivateKey(keyBytes, out _);
        }
        catch
        {
            try
            {
                rsa.ImportRSAPrivateKey(keyBytes, out _);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RSA 私钥格式不正确（PKCS#1 / PKCS#8 均无法解析）", ex);
            }
        }
    }
}