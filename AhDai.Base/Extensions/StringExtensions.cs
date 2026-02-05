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
        #region 类型转换

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static byte ToByte(this string s, byte? error = null)
        {
            return byte.TryParse(s, out byte result) ? result : error ?? byte.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string s, sbyte? error = null)
        {
            return sbyte.TryParse(s, out sbyte result) ? result : error ?? sbyte.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string s, bool? error = null)
        {
            return bool.TryParse(s, out bool result) ? result : error ?? false;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static short ToInt16(this string s, short? error = null)
        {
            return short.TryParse(s, out short result) ? result : error ?? short.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int ToInt32(this string s, int? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (int.TryParse(s, NumberStyles.Float, null, out int result))
                    {
                        return result;
                    }
                }
                else if (int.TryParse(s, out int result))
                {
                    return result;
                }
            }
            return error ?? int.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static long ToInt64(this string s, long? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (long.TryParse(s, NumberStyles.Float, null, out long result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (long.TryParse(s, out long result))
                    {
                        return result;
                    }
                }
            }
            return error ?? long.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string s, ulong? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (ulong.TryParse(s, NumberStyles.Float, null, out ulong result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (ulong.TryParse(s, out ulong result))
                    {
                        return result;
                    }
                }
            }
            return error ?? ulong.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static float ToSingle(this string s, float? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (float.TryParse(s, NumberStyles.Float, null, out float result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (float.TryParse(s, out float result))
                    {
                        return result;
                    }
                }
            }
            return error ?? float.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static double ToDouble(this string s, double? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (double.TryParse(s, NumberStyles.Float, null, out double result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (double.TryParse(s, out double result))
                    {
                        return result;
                    }
                }
            }
            return error ?? double.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s, decimal? error = null)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                if (s.Contains('e'))
                {
                    if (decimal.TryParse(s, NumberStyles.Float, null, out decimal result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (decimal.TryParse(s, out decimal result))
                    {
                        return result;
                    }
                }
            }
            return error ?? decimal.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime? error = null)
        {
            return DateTime.TryParse(s, out DateTime result) ? result : error ?? DateTime.MinValue;
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
        public static DateTime ToDateTimeExact(this string s, string format, IFormatProvider provider = null, DateTimeStyles? styles = null, DateTime? error = null)
        {
            return DateTime.TryParseExact(s, format, provider, styles ?? DateTimeStyles.RoundtripKind, out DateTime result) ? result : error ?? DateTime.MinValue;
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
        public static DateTime ToDateTimeExact(this string s, string[] formats, IFormatProvider provider = null, DateTimeStyles? styles = null, DateTime? error = null)
        {
            return DateTime.TryParseExact(s, formats, provider, styles ?? DateTimeStyles.RoundtripKind, out DateTime result) ? result : error ?? DateTime.MinValue;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s, TimeSpan? error = null)
        {
            return TimeSpan.TryParse(s, out TimeSpan result) ? result : error ?? TimeSpan.MinValue;
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
        public static TimeSpan ToTimeSpanExact(this string s, string format, IFormatProvider provider = null, TimeSpanStyles? styles = null, TimeSpan? error = null)
        {
            return TimeSpan.TryParseExact(s, format, provider, styles ?? TimeSpanStyles.None, out TimeSpan result) ? result : error ?? TimeSpan.MinValue;
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
        public static TimeSpan ToTimeSpanExact(this string s, string[] formats, IFormatProvider provider = null, TimeSpanStyles? styles = null, TimeSpan? error = null)
        {
            return TimeSpan.TryParseExact(s, formats, provider, styles ?? TimeSpanStyles.None, out TimeSpan result) ? result : error ?? TimeSpan.MinValue;
        }
        #endregion

        /// <summary>
        /// 补齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length">指定长度；输入字符串超出字符时，不做处理，直接返回</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string Complete(this string s, int length, string prefix = "0")
        {
            while (s.Length < length)
            {
                s = prefix + s;
            }
            return s;
        }

        /// <summary>
        /// 替换字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static string Replace(this string s, int startIndex, int endIndex)
        {
            return s.Replace('*', startIndex, endIndex);
        }

        /// <summary>
        /// 替换字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="newChar"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static string Replace(this string s, char newChar, int startIndex, int endIndex)
        {
            if (string.IsNullOrEmpty(s) || s.Length <= startIndex)
            {
                return s;
            }
            var chars = s.ToCharArray();
            if (endIndex >= chars.Length)
            {
                endIndex = chars.Length;
            }
            else
            {
                endIndex++;
            }
            while (startIndex < endIndex)
            {
                chars[startIndex] = newChar;
                startIndex++;
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

            var builder = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                // 过滤掉空格、回车、换行、制表符
                if (c != ' ' && c != '\r' && c != '\n' && c != '\t')
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}
