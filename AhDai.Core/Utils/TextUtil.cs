using System.Text;

namespace AhDai.Core.Utils;

/// <summary>
/// TextHelper
/// </summary>
public static class TextUtil
{
    /// <summary>
    /// GetEncoding
    /// </summary>
    /// <param name="charSet"></param>
    /// <returns></returns>
    public static Encoding GetEncoding(string charSet)
    {
        return charSet.ToLower() switch
        {
            "" or "utf8" or "utf-8" => Encoding.UTF8,
            _ => Encoding.GetEncoding(charSet),
        };
    }
}
