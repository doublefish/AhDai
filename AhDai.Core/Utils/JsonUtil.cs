using AhDai.Base;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AhDai.Core.Utils;

/// <summary>
/// JsonUtil
/// </summary>
public static class JsonUtil
{
    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    static JsonUtil()
    {
        Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new Converters.DateTimeJsonConverter1(Const.Iso8601WithOffsetDateTimeFormat), new Converters.DateOnlyJsonConverter(Const.Iso8601WithOffsetDateTimeFormat) },
        };
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string Serialize<T>(T obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options ?? Options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options ?? Options);
    }
}
