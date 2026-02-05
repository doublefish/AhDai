using System;
using System.Globalization;

namespace AhDai.Core.Extensions;

/// <summary>
/// StringExt
/// </summary>
public static class StringExtensions
{
    #region 类型转换

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnly(this string s, DateOnly? error = null)
    {
        return DateOnly.TryParse(s, out DateOnly result) ? result : error ?? DateOnly.MinValue;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string format, IFormatProvider? provider = null, DateTimeStyles? styles = null, DateOnly? error = null)
    {
        return DateOnly.TryParseExact(s, format, provider, styles ?? DateTimeStyles.None, out DateOnly result) ? result : error ?? DateOnly.MinValue;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnlyExact(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles? styles = null, DateOnly? error = null)
    {
        return DateOnly.TryParseExact(s, formats, provider, styles ?? DateTimeStyles.None, out DateOnly result) ? result : error ?? DateOnly.MinValue;
    }

    #endregion
   
}
