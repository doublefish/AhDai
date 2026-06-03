using System;
using System.Security.Cryptography;
using System.Xml;

namespace AhDai.Core.Extensions;

/// <summary>
/// 现代 RSA 高性能跨平台兼容扩展
/// </summary>
public static class RsaExtensions
{
    /// <summary>
    /// 跨平台安全地从老旧的 XML 字符串中加载 RSA 密钥（解决原生 FromXmlString 跨平台缺陷）
    /// </summary>
    public static void ImportFromXmlStringExt(this RSA rsa, string xmlString)
    {
        if (string.IsNullOrWhiteSpace(xmlString)) throw new ArgumentNullException(nameof(xmlString));

        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlString);

        if (xmlDoc.DocumentElement?.Name != "RSAKeyValue") throw new ArgumentException("无效的Xml格式的RSA密钥", nameof(xmlString));

        var root = xmlDoc.DocumentElement;
        var parameters = new RSAParameters()
        {
            Modulus = GetNodeBytes(root.GetInnerText("Modulus")),
            Exponent = GetNodeBytes(root.GetInnerText("Exponent")),
            D = GetNodeBytes(root.GetInnerText("D")),
            P = GetNodeBytes(root.GetInnerText("P")),
            Q = GetNodeBytes(root.GetInnerText("Q")),
            DP = GetNodeBytes(root.GetInnerText("DP")),
            DQ = GetNodeBytes(root.GetInnerText("DQ")),
            InverseQ = GetNodeBytes(root.GetInnerText("InverseQ"))
        };

        rsa.ImportParameters(parameters);

        static byte[]? GetNodeBytes(string base64Str) =>
            string.IsNullOrEmpty(base64Str) ? null : Convert.FromBase64String(base64Str);
    }

    /// <summary>
    /// 将当前 RSA 实例导出为标准的兼容 XML 字符串
    /// </summary>
    public static string ToXmlStringExt(this RSA rsa, bool includePrivateParameters)
    {
        var parameters = rsa.ExportParameters(includePrivateParameters);

        var modulus = parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null;
        var exponent = parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null;
        var d = parameters.D != null ? Convert.ToBase64String(parameters.D) : null;
        var p = parameters.P != null ? Convert.ToBase64String(parameters.P) : null;
        var q = parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null;
        var dp = parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null;
        var dq = parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null;
        var inverseQ = parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null;

        return $"<RSAKeyValue><Modulus>{modulus}</Modulus><Exponent>{exponent}</Exponent><D>{d}</D><P>{p}</P><Q>{q}</Q><DP>{dp}</DP><DQ>{dq}</DQ><InverseQ>{inverseQ}</InverseQ></RSAKeyValue>";
    }
}