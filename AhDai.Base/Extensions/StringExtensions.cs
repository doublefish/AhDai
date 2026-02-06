using System;
using System.Globalization;
using System.Text;

namespace AhDai.Base.Extensions
{
    /// <summary>
    /// StringExt
    /// </summary>
    public static class StringExtensions
    {
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


        /// <summary>
        /// 补齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length">指定长度；输入字符串超出字符时，不做处理，直接返回</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string Complete(this string s, int length, char prefix = '0')
        {
            return s.Length >= length ? s : s.PadLeft(length, prefix);
        }

        /// <summary>
        /// 替换字符
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
            if (string.IsNullOrEmpty(s) || startIndex < 0 || startIndex >= s.Length || length <= 0) return s;

            var actualLength = Math.Min(length, s.Length - startIndex);
            var chars = s.ToCharArray();
            for (int i = 0; i < actualLength; i++)
            {
                chars[startIndex + i] = newChar;
            }
            return new string(chars);
        }

        /// <summary>
        /// 去除空格、回车、换行和制表符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToInline(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var span = s.AsSpan();
            var hasWhiteSpace = false;
            foreach (var c in span)
            {
                if (char.IsWhiteSpace(c))
                {
                    hasWhiteSpace = true;
                    break;
                }
            }
            if (!hasWhiteSpace) return s;

            var builder = new StringBuilder(span.Length);
            foreach (char c in span)
            {
                if (!char.IsWhiteSpace(c))
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}
