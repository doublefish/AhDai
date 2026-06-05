using System;
using System.Globalization;

namespace AhDai.Core.Extensions;

/// <summary>
/// StringExt
/// </summary>
public static class StringExtensions
{
    #region Boolean

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool? ToBooleanOrNull(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s) && bool.TryParse(s.AsSpan(), out bool result))
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
    public static bool ToBoolean(this string s, bool? error = null)
    {
        return s.ToBooleanOrNull() ?? error ?? default;
    }

    #endregion

    #region SByte/Byte

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static sbyte? ToSByteOrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && sbyte.TryParse(s.AsSpan(), style, provider, out sbyte result))
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
    public static sbyte ToSByte(this string s, sbyte? error = default)
    {
        return s.ToSByteOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static sbyte ToSByte(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, sbyte error = default)
    {
        return s.ToSByteOrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static byte? ToByteOrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && byte.TryParse(s.AsSpan(), style, provider, out byte result))
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
    public static byte ToByte(this string s, byte? error = default)
    {
        return s.ToByteOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static byte ToByte(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, byte error = default)
    {
        return s.ToByteOrNull(style, provider) ?? error;
    }

    #endregion

    #region Int16/Int32/Int64

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static short? ToInt16OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && short.TryParse(s.AsSpan(), style, provider, out short result))
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
    public static short ToInt16(this string s, short? error = default)
    {
        return s.ToInt16OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static short ToInt16(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, short error = default)
    {
        return s.ToInt16OrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static int? ToInt32OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s.AsSpan(), style, provider, out int result))
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
    public static int ToInt32(this string s, int? error = default)
    {
        return s.ToInt32OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static int ToInt32(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, int error = default)
    {
        return s.ToInt32OrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static long? ToInt64OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && long.TryParse(s.AsSpan(), style, provider, out long result))
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
    public static long ToInt64(this string s, long? error = default)
    {
        return s.ToInt64OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static long ToInt64(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, long error = default)
    {
        return s.ToInt64OrNull(style, provider) ?? error;
    }

    #endregion

    #region UInt16/UInt32/UInt64

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static ushort? ToUInt16OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && ushort.TryParse(s.AsSpan(), style, provider, out ushort result))
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
    public static ushort ToUInt16(this string s, ushort? error = default)
    {
        return s.ToUInt16OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ushort ToUInt16(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, ushort error = default)
    {
        return s.ToUInt16OrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static uint? ToUInt32OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && uint.TryParse(s.AsSpan(), style, provider, out uint result))
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
    public static uint ToUInt32(this string s, uint? error = default)
    {
        return s.ToUInt32OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static uint ToUInt32(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, uint error = default)
    {
        return s.ToUInt32OrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static ulong? ToUInt64OrNull(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && ulong.TryParse(s.AsSpan(), style, provider, out ulong result))
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
    public static ulong ToUInt64(this string s, ulong? error = default)
    {
        return s.ToUInt64OrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ulong ToUInt64(this string s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null, ulong error = default)
    {
        return s.ToUInt64OrNull(style, provider) ?? error;
    }

    #endregion

    #region Single/Double/Decimal

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static float? ToSingleOrNull(this string s, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && float.TryParse(s.AsSpan(), style, provider, out float result))
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
    public static float ToSingle(this string s, float? error = default)
    {
        return s.ToSingleOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static float ToSingle(this string s, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = null, float error = default)
    {
        return s.ToSingleOrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static double? ToDoubleOrNull(this string s, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && double.TryParse(s.AsSpan(), style, provider, out double result))
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
    public static double ToDouble(this string s, double? error = default)
    {
        return s.ToDoubleOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static double ToDouble(this string s, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = null, double error = default)
    {
        return s.ToDoubleOrNull(style, provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static decimal? ToDecimalOrNull(this string s, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && decimal.TryParse(s.AsSpan(), style, provider, out decimal result))
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
    public static decimal ToDecimal(this string s, decimal? error = default)
    {
        return s.ToDecimalOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static decimal ToDecimal(this string s, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = null, decimal error = default)
    {
        return s.ToDecimalOrNull(style, provider) ?? error;
    }

    #endregion

    #region DateTime

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static DateTime? ToDateTimeOrNull(this string s, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s.AsSpan(), provider, styles, out DateTime result))
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
    public static DateTime ToDateTime(this string s, DateTime? error = default)
    {
        return s.ToDateTimeOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string s, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None, DateTime error = default)
    {
        return s.ToDateTimeOrNull(provider, styles) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static DateTime? ToDateTimeExactOrNull(this string s, string format, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParseExact(s.AsSpan(), format, provider, styles, out DateTime result))
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
    /// <param name="styles"></param>
    /// <returns></returns>
    public static DateTime? ToDateTimeExactOrNull(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParseExact(s.AsSpan(), formats, provider, styles, out DateTime result))
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
    public static DateTime ToDateTimeExact(this string s, string format, DateTime? error = null)
    {
        return s.ToDateTimeExactOrNull(format) ?? error ?? default;
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
    public static DateTime ToDateTimeExact(this string s, string format, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None, DateTime error = default)
    {
        return s.ToDateTimeExactOrNull(format, provider, styles) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static DateTime ToDateTimeExact(this string s, string[] formats, DateTime? error = default)
    {
        return s.ToDateTimeExactOrNull(formats) ?? error ?? default;
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
    public static DateTime ToDateTimeExact(this string s, string[] formats, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None, DateTime error = default)
    {
        return s.ToDateTimeExactOrNull(formats, provider, styles) ?? error;
    }

    #endregion

    #region TimeSpan

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpanOrNull(this string s, IFormatProvider? provider = null)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeSpan.TryParse(s.AsSpan(), provider, out TimeSpan result))
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
    public static TimeSpan ToTimeSpan(this string s, TimeSpan? error = default)
    {
        return s.ToTimeSpanOrNull() ?? error ?? default;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="provider"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string s, IFormatProvider? provider = null, TimeSpan error = default)
    {
        return s.ToTimeSpanOrNull(provider) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpanExactOrNull(this string s, string format, IFormatProvider? provider = null, TimeSpanStyles styles = TimeSpanStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeSpan.TryParseExact(s.AsSpan(), format, provider, styles, out TimeSpan result))
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
    /// <param name="styles"></param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpanExactOrNull(this string s, string[] formats, IFormatProvider? provider = null, TimeSpanStyles styles = TimeSpanStyles.None)
    {
        if (!string.IsNullOrWhiteSpace(s) && TimeSpan.TryParseExact(s.AsSpan(), formats, provider, styles, out TimeSpan result))
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
    public static TimeSpan ToTimeSpanExact(this string s, string format, TimeSpan? error = default)
    {
        return s.ToTimeSpanExactOrNull(format) ?? error ?? default;
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
    public static TimeSpan ToTimeSpanExact(this string s, string format, IFormatProvider? provider = null, TimeSpanStyles styles = TimeSpanStyles.None, TimeSpan error = default)
    {
        return s.ToTimeSpanExactOrNull(format, provider, styles) ?? error;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formats"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpanExact(this string s, string[] formats, TimeSpan? error = default)
    {
        return s.ToTimeSpanExactOrNull(formats) ?? error ?? default;
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
    public static TimeSpan ToTimeSpanExact(this string s, string[] formats, IFormatProvider? provider = null, TimeSpanStyles styles = TimeSpanStyles.None, TimeSpan error = default)
    {
        return s.ToTimeSpanExactOrNull(formats, provider, styles) ?? error;
    }

    #endregion

    #region DateOnly

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

    #endregion

    #region TimeOnly

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

    #endregion


    /// <summary>
    /// 替换指定索引范围内的字符（默认打码为 '*'）
    /// </summary>
    /// <param name="s"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Replace(this string s, int startIndex, int length)
    {
        return s.Replace('*', startIndex, length);
    }

    /// <summary>
    /// 替换字符
    /// </summary>
    /// <param name="s"></param>
    /// <param name="newChar"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Replace(this string s, char newChar, int startIndex, int length)
    {
        if (string.IsNullOrEmpty(s) || startIndex < 0 || startIndex >= s.Length || length <= 0)
            return s;

        var actualLength = Math.Min(length, s.Length - startIndex);
        return string.Create(s.Length, (s, newChar, startIndex, actualLength), static (destSpan, state) =>
        {
            state.s.AsSpan().CopyTo(destSpan);
            destSpan.Slice(state.startIndex, state.actualLength).Fill(state.newChar);
        });
    }

    /// <summary>
    /// 去除空格、回车、换行和制表符
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToInline(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;

        var source = s.AsSpan();

        var hasTargetWhiteSpace = false;
        foreach (char c in source)
        {
            if (c is ' ' or '\r' or '\n' or '\t')
            {
                hasTargetWhiteSpace = true;
                break;
            }
        }
        if (!hasTargetWhiteSpace) return s;

        var buffer = s.Length <= 512 ? stackalloc char[s.Length] : new char[s.Length];
        var destIndex = 0;
        for (var i = 0; i < source.Length; i++)
        {
            char c = source[i];
            if (c is ' ' or '\r' or '\n' or '\t')
            {
                continue;
            }
            buffer[destIndex++] = c;
        }
        return new string(buffer[..destIndex]);
    }

    /// <summary>
    /// 安全截取字符串（达到最大长度时自动截断，不抛出异常）
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string str, int maxLength) => string.IsNullOrEmpty(str) || str.Length <= maxLength ? str : str[..maxLength];

    /// <summary>
    /// 截取字符串并追加省略号
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string TruncateWithEllipsis(this string str, int maxLength) => string.IsNullOrEmpty(str) || str.Length <= maxLength ? str : str[..maxLength] + "...";

    /// <summary>
    /// ToCamelCase
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        var firstChar = char.ToLower(str[0]);
        if (str.Length == 1)
        {
            return firstChar.ToString();
        }
        return firstChar + str[1..];
    }
}
