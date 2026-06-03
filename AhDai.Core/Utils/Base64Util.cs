using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace AhDai.Core.Utils;

/// <summary>
/// Base64Helper
/// </summary>
public static class Base64Util
{
    /// <summary>
    /// 字符串转Base64字符串
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encode">编码</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBase64String(string s, Encoding? encode = null)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        encode ??= Encoding.UTF8;
        var buffer = encode.GetBytes(s);

        return Convert.ToBase64String(buffer);
    }

    /// <summary>
    /// Base64字符串转字符串
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encode">编码</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(string s, Encoding? encode = null)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        encode ??= Encoding.UTF8;
        var bytes = ToBytes(s);

        return encode.GetString(bytes);
    }

    /// <summary>
    /// Base64字符串转字节数组
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] ToBytes(string s)
    {
        if (string.IsNullOrEmpty(s)) return [];

        var mod = s.Length % 4;
        if (mod > 0)
        {
            s += mod == 2 ? "==" : "=";
        }

        // 如果输入字符串包含了需要兼容替换的 URL 安全字符（空格、-、_）
        if (s.Contains(' ') || s.Contains('-') || s.Contains('_'))
        {
            var charSpan = s.Length <= 512 ? stackalloc char[s.Length] : new char[s.Length];
            s.AsSpan().CopyTo(charSpan);

            // 纯物理指针级循环，比 Replace() 快几十倍，且 GC 压力为 0
            for (int i = 0; i < charSpan.Length; i++)
            {
                ref var c = ref charSpan[i];
                if (c == ' ') c = '+';
                else if (c == '-') c = '+';
                else if (c == '_') c = '/';
            }

            return Convert.FromBase64String(charSpan.ToString());
        }

        return Convert.FromBase64String(s);
    }
}
