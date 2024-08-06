using System;
using System.Text;

namespace AhDai.Base.Utils
{
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
        public static string ToBase64String(string s, Encoding encode = null)
        {
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
        public static string ToString(string s, Encoding encode = null)
        {
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
            s = s.Replace(" ", "+").Replace("-", "+").Replace("_", "/");
            return Convert.FromBase64String(s);
        }
    }
}
