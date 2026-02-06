using System;
using System.Globalization;

namespace AhDai.Core.Extensions;

/// <summary>
/// StringExt
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static DateOnly? ToDateOnlyOrNull(this string s, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateOnly.TryParse(s.AsSpan(), provider, style, out DateOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnly(this string s, DateOnly? error = default)
    {
        return s.ToDateOnlyOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnly(this string s, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, DateOnly error = default)
    {
        return s.ToDateOnlyOrNull(provider, style) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static DateOnly? ToDateOnlyExactOrNull(this string s, string format, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateOnly.TryParseExact(s.AsSpan(), format, provider, style, out DateOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static DateOnly? ToDateOnlyExactOrNull(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateOnly.TryParseExact(s.AsSpan(), formats, provider, style, out DateOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string format, DateOnly? error = default)
    {
        return s.ToDateOnlyExactOrNull(format) ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string format, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, DateOnly error = default)
    {
        return s.ToDateOnlyExactOrNull(format, provider, style) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string[] formats, DateOnly? error = default)
    {
        return s.ToDateOnlyExactOrNull(formats) ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, DateOnly error = default)
    {
        return s.ToDateOnlyExactOrNull(formats, provider, style) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static TimeOnly? ToTimeOnlyOrNull(this string s, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeOnly.TryParse(s.AsSpan(), provider, style, out TimeOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnly(this string s, TimeOnly? error = default)
    {
        return s.ToTimeOnlyOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnly(this string s, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, TimeOnly error = default)
    {
        return s.ToTimeOnlyOrNull(provider, style) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static TimeOnly? ToTimeOnlyExactOrNull(this string s, string format, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeOnly.TryParseExact(s.AsSpan(), format, provider, style, out TimeOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static TimeOnly? ToTimeOnlyExactOrNull(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeOnly.TryParseExact(s.AsSpan(), formats, provider, style, out TimeOnly result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnlyExact(this string s, string format, TimeOnly? error = default)
    {
        return s.ToTimeOnlyExactOrNull(format) ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnlyExact(this string s, string format, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, TimeOnly error = default)
    {
        return s.ToTimeOnlyExactOrNull(format, provider, style) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnlyExact(this string s, string[] formats, TimeOnly? error = default)
    {
        return s.ToTimeOnlyExactOrNull(formats) ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="provider"></param>
    /// <param name="style"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnlyExact(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles style = DateTimeStyles.None, TimeOnly error = default)
    {
        return s.ToTimeOnlyExactOrNull(formats, provider, style) ?? error;
    }
}
