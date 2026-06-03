using System;
using System.Runtime.CompilerServices;

namespace AhDai.Core.Utils;

/// <summary>
/// DateTimeUtil
/// </summary>
public static class DateTimeUtil
{
    /// <summary>
    /// 时间戳
    /// </summary>
    public static TimeSpan Timestamp
    {
        // 强行内联，消除高频属性调用的栈跳转
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => DateTimeOffset.UtcNow.TimeOfDay;
    }

    /// <summary>
    /// 时间戳（距1970-01-01的毫秒数）
    /// </summary>
    public static long TimestampOfMilliseconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 时间戳（距1970-01-01的秒数）
    /// </summary>
    public static long TimestampOfSeconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
