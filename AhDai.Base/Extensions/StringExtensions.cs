using System;
using System.Globalization;

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
        public static byte ToByte(this string s, byte error = byte.MinValue)
        {
            if (byte.TryParse(s, out byte result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string s, sbyte error = sbyte.MinValue)
        {
            if (sbyte.TryParse(s, out sbyte result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string s, bool error = false)
        {
            if (bool.TryParse(s, out bool result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static short ToInt16(this string s, short error = short.MinValue)
        {
            if (short.TryParse(s, out short result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int ToInt32(this string s, int error = int.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static long ToInt64(this string s, long error = long.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string s, ulong error = ulong.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static float ToSingle(this string s, float error = float.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static double ToDouble(this string s, double error = double.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s, decimal error = decimal.MinValue)
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
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            return s.ToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime error)
        {
            if (DateTime.TryParse(s, out DateTime result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <param name="styles"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeExact(this string s, string format, IFormatProvider provider, DateTimeStyles styles)
        {
            return s.ToDateTimeExact(format, provider, styles, DateTime.MinValue);
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
        public static DateTime ToDateTimeExact(this string s, string format, IFormatProvider provider, DateTimeStyles styles, DateTime error)
        {
            if (DateTime.TryParseExact(s, format, provider, styles, out DateTime result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="formats"></param>
        /// <param name="provider"></param>
        /// <param name="styles"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeExact(this string s, string[] formats, IFormatProvider provider, DateTimeStyles styles)
        {
            return s.ToDateTimeExact(formats, provider, styles, DateTime.MinValue);
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
        public static DateTime ToDateTimeExact(this string s, string[] formats, IFormatProvider provider, DateTimeStyles styles, DateTime error)
        {
            if (DateTime.TryParseExact(s, formats, provider, styles, out DateTime result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s)
        {
            return s.ToTimeSpan(TimeSpan.MinValue);
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s, TimeSpan error)
        {
            if (TimeSpan.TryParse(s, out TimeSpan result))
            {
                return result;
            }
            return error;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpanExact(this string s, string format, IFormatProvider provider)
        {
            return s.ToTimeSpanExact(format, provider, TimeSpan.MinValue);
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpanExact(this string s, string format, IFormatProvider provider, TimeSpan error)
        {
            if (TimeSpan.TryParseExact(s, format, provider, TimeSpanStyles.None, out TimeSpan result))
            {
                return result;
            }
            return error;
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
        /// 去除空格、回车、换行符和制表符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToInline(this string s)
        {
            return s.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }
    }
}
