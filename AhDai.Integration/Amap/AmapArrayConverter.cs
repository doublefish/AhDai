using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap;

/// <summary>
/// AmapArrayConverter
/// </summary>
/// <typeparam name="T"></typeparam>
public class AmapArrayConverter<T> : JsonConverter<T[]>
{
    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override T[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;

                // 核心逻辑：如果是 [] 或 [[]] 或 [[[]]]...
                if (root.GetArrayLength() == 0 || (root[0].ValueKind == JsonValueKind.Array && root[0].GetArrayLength() == 0))
                {
                    return [];
                }

                // 正常数据序列化
                return JsonSerializer.Deserialize<T[]>(root.GetRawText(), options);
            }
        }
        return [];
    }

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, T[] value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
