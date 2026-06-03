using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace AhDai.Core.Utils;

/// <summary>
/// ComputerUtil
/// </summary>
public static class ComputerUtil
{
    /// <summary>
    /// 获取Mac地址
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? GetMacAddress()
    {
        return GetMacAddresses().FirstOrDefault();
    }

    /// <summary>
    /// 获取Mac地址
    /// </summary>
    /// <returns></returns>
    public static string[] GetMacAddresses()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        if (interfaces == null || interfaces.Length == 0) return [];

        var addresses = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        Span<char> macSpan = stackalloc char[17];

        foreach (var network in interfaces)
        {
            if (network.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                network.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
            {
                continue;
            }

            if (network.OperationalStatus != OperationalStatus.Up)
            {
                continue;
            }

            var physicalAddress = network.GetPhysicalAddress();
            if (physicalAddress == null) continue;

            // 利用 ReadOnlySpan 内存切片直接读取网卡字节数组，消灭 GetAddressBytes() 的多余堆分配
            ReadOnlySpan<byte> bytes = physicalAddress.GetAddressBytes();
            if (bytes.Length == 0 || IsInvalidMac(bytes))
            {
                continue;
            }

            for (var i = 0; i < bytes.Length; i++)
            {
                var charIndex = i * 3;

                // 将 byte 格式化直接写入复用的 macSpan 对应切片中
                bytes[i].TryFormat(macSpan[charIndex..], out _, "X2");

                if (i < bytes.Length - 1)
                {
                    macSpan[charIndex + 2] = ':';
                }
            }

            // 转换成最终字符串存入结果集
            addresses.Add(macSpan.ToString());
        }

        return [.. addresses];
    }

    /// <summary>
    /// 辅助防线：过滤全零或全 F 的无效虚拟 MAC 地址
    /// </summary>
    static bool IsInvalidMac(ReadOnlySpan<byte> bytes)
    {
        int zeroCount = 0;
        int fCount = 0;

        for (int i = 0; i < bytes.Length; i++)
        {
            if (bytes[i] == 0x00) zeroCount++;
            else if (bytes[i] == 0xFF) fCount++;
        }

        // 如果全 0 (00:00...) 或者全 FF (FF:FF...)，说明是无效虚拟/挂载代理网卡
        return zeroCount == bytes.Length || fCount == bytes.Length;
    }
}
