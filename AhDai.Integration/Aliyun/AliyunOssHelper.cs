using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AhDai.Integration.Aliyun;


internal class AliyunOssHelper
{
    public static string GetScope(string date, string region) => string.Join('/', date, region, "oss/aliyun_v4_request");

    public static string GetHost(string region, string bucket) => $"https://{bucket}.oss-{region}.aliyuncs.com";

    public static string GetDate(DateTime time) => time.ToString("yyyyMMdd");

    public static string GetTimestamp(DateTime time) => time.ToString("yyyyMMdd'T'HHmmss'Z'");

    public static bool VerifySignature(string pem, string data, string authorization)
    {
        var key = Convert.FromBase64String(pem);
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(data));
        var signature = Convert.FromBase64String(authorization);

        using var rsa = new RSACryptoServiceProvider();
        rsa.ImportSubjectPublicKeyInfo(key, out _);

        var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
        rsaDeformatter.SetHashAlgorithm("MD5");

        return rsaDeformatter.VerifySignature(hash, signature);
    }

    static bool IsDefaultSignedHeader(string key)
        => key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)
        || key.Equals("Content-MD5", StringComparison.OrdinalIgnoreCase)
        || key.StartsWith("x-oss-", StringComparison.OrdinalIgnoreCase);

    public static List<string> GetAdditionalHeaderName(IDictionary<string, string?>? headers)
    {
        var list = new List<string>();
        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)
            {
                if (!IsDefaultSignedHeader(header.Key))
                {
                    list.Add(header.Key.ToLower());
                }
            }
        }
        return list;
    }

    public static string JoinHeaderToSign(IDictionary<string, string?>? headers, List<string> additionalHeaderNames)
    {
        if (headers == null || headers.Count == 0) return "";
        var builder = new StringBuilder();
        foreach (var header in headers)
        {
            var lowerKey = header.Key.ToLower();
            if (IsDefaultSignedHeader(lowerKey) || additionalHeaderNames.Contains(lowerKey))
            {
                builder.Append($"{lowerKey}:{header.Value ?? ""}\n");
            }
        }
        return builder.ToString();
    }

}
