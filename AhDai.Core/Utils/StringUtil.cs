using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AhDai.Core.Utils;

/// <summary>
/// StringHelper
/// </summary>
public static class StringUtil
{
    const string Base36Chars = "abcdefghijklmnopqrstuvwxyz0123456789";
    const string DefaultBound = "AsBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
    const string DigitalBound = "0123456789";

    /// <summary>
    /// 转为36进制字符串
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ConvertToBase36(long number)
    {
        Span<char> buffer = stackalloc char[13];
        int position = buffer.Length;

        long current = number;
        do
        {
            var remainder = (int)(current % 36);
            // 从尾部向头部逆序填充，完全避免了平移拷贝，时间复杂度绝对 $O(N)$
            buffer[--position] = Base36Chars[remainder];
            current /= 36;
        }
        while (current > 0);

        // 切片提取有效位，并以极速快路径构造出唯一的 string 对象返回
        return new string(buffer[position..]);
    }

    /// <summary>
    /// ToBase64String
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static async Task<string> ConvertToBase64Async(IFormFile file)
    {
        // 绝不拉入 MemoryStream，也绝不调用 ms.ToArray()！
        // 动态计算出最终 Base64 字符串的精确长度，一次性在托管堆上分配精准空间的 string 外壳。
        var base64Length = (int)((file.Length + 2) / 3 * 4);

        // 使用现代 .NET 的租借机制或大对象堆防御。如果文件较大，直接租用大对象缓冲区，防碎片化
        var rentBuffer = System.Buffers.ArrayPool<byte>.Shared.Rent((int)file.Length);
        try
        {
            // 纯流式直达：直接把网络硬件网卡接收到的输入流灌进租借的缓冲区内
            using var stream = file.OpenReadStream();
            int totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(rentBuffer.AsMemory(totalBytesRead, (int)file.Length - totalBytesRead)).ConfigureAwait(false)) > 0)
            {
                totalBytesRead += bytesRead;
            }

            // 利用一句话指针转换，在目标字符串的物理内存中就地进行 Base64 硬件级转码，没有二次中转垃圾
            return string.Create(base64Length, (rentBuffer, totalBytesRead), static (destSpan, state) =>
            {
                var success = Convert.TryToBase64Chars(state.rentBuffer.AsSpan(0, state.totalBytesRead), destSpan, out int charsWritten);
                if (!success || charsWritten != destSpan.Length)
                {
                    throw new InvalidOperationException("Base64 转码发生未知严重异常");
                }
            });
        }
        finally
        {
            // 用完立刻把大数组归还给全局对象池，托管堆上的积压开销瞬间归零！
            System.Buffers.ArrayPool<byte>.Shared.Return(rentBuffer);
        }
    }

    /// <summary>
    /// 移除方向标记
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveDirectionalFormatting(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        var source = str.AsSpan();

        // 先在线快速扫一遍，如果文本里根本没有这些罕见的控制符（99.9% 的常规场景），
        // 0 开销直接返回原字符串，坚决不发生任何内存分配！
        var hasFormatting = false;
        foreach (char c in source)
        {
            if (c is >= '\u202A' and <= '\u202E' or '\u200E' or '\u200F')
            {
                hasFormatting = true;
                break;
            }
        }
        if (!hasFormatting) return str;

        // 如果确实包含控制符，在栈上利用快慢双指针（Two-Pointer）就地提取有效字符
        Span<char> destBuffer = str.Length <= 512 ? stackalloc char[str.Length] : new char[str.Length];
        var destIndex = 0;

        for (var i = 0; i < source.Length; i++)
        {
            var c = source[i];
            if (c is >= '\u202A' and <= '\u202E' or '\u200E' or '\u200F')
            {
                continue; // 过滤、跳过控制符
            }
            destBuffer[destIndex++] = c;
        }

        // 仅对裁剪后的实际有效长度生成一次新字符串
        return new string(destBuffer[..destIndex]);
    }

    /// <summary>
    /// 生成指定长度的随机数字字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateRandomDigital(int length) => GenerateRandom(DigitalBound, length);

    /// <summary>
    /// 生成指定长度的随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateRandom(int length) => GenerateRandom(DefaultBound, length);

    /// <summary>
    /// 生成验指定长度的随机字符串
    /// </summary>
    /// <param name="bound">源字符集范围</param>
    /// <param name="length">期望长度</param>
    /// <returns></returns>
    public static string GenerateRandom(string bound, int length)
    {
        if (length <= 0) return string.Empty;
        return RandomNumberGenerator.GetString(bound, length);
    }

    /// <summary>
    /// 零分配无锁非对称高频缓冲区填充（针对极致并发榨汁场景）
    /// </summary>
    /// <remarks>
    /// 如果你需要在中间件或高频网络包里就地生成随机串并写入目的地，连 string 本身都不想实例化，用这个 Span 版。
    /// </remarks>
    public static void GenerateRandomToSpan(ReadOnlySpan<char> bound, Span<char> destination)
    {
        if (bound.IsEmpty) throw new ArgumentException("字符集不能为空", nameof(bound));
        if (destination.IsEmpty) return;

        RandomNumberGenerator.GetItems(bound, destination);
    }
}
