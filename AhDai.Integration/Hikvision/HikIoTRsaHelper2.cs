using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// HikIoTRsaHelper
/// </summary>
public static class HikIoTRsaHelper2
{
    const string algorithm = "RSA/ECB/PKCS1Padding";

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string EncryptWithPrivateKey(string privateKey, string content)
    {
        var privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
        var bytes = Transform(privateKeyParam, Encoding.UTF8.GetBytes(content), true);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string DecryptWithPrivateKey(string privateKey, string content)
    {
        var privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
        var bytes = Transform(privateKeyParam, Convert.FromBase64String(content), false);
        return HttpUtility.UrlDecode(Encoding.UTF8.GetString(bytes));
    }

    private static byte[] Transform(AsymmetricKeyParameter key, byte[] contentData, bool forEncryption)
    {
        var cipher = CipherUtilities.GetCipher(algorithm);
        cipher.Init(forEncryption, key);

        var rsaKey = (RsaKeyParameters)key;
        var modulusLength = rsaKey.Modulus.BitLength / 8;

        using var ms = new MemoryStream();
        if (forEncryption)
        {
            var maxDataLength = modulusLength - 11;

            for (var i = 0; i < contentData.Length; i += maxDataLength)
            {
                var blockLength = Math.Min(maxDataLength, contentData.Length - i);
                var block = new byte[blockLength];
                Array.Copy(contentData, i, block, 0, blockLength);
                var encryptedBlock = cipher.DoFinal(block);
                ms.Write(encryptedBlock, 0, encryptedBlock.Length);
            }
        }
        else
        {
            for (var i = 0; i < contentData.Length; i += modulusLength)
            {
                var blockLength = Math.Min(modulusLength, contentData.Length - i);
                var block = new byte[blockLength];
                Array.Copy(contentData, i, block, 0, blockLength);
                var decryptedBlock = cipher.DoFinal(block);
                ms.Write(decryptedBlock, 0, decryptedBlock.Length);
            }
        }

        return ms.ToArray();
    }
}