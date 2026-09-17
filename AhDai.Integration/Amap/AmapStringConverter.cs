using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap;

/// <summary>
/// AmapStringConverter
/// </summary>
public class AmapStringConverter : JsonConverter<string>
{
    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartArray:
                reader.Skip(); // 跳过空数组 []
                return string.Empty;
            case JsonTokenType.String:
                return reader.GetString() ?? string.Empty;
            case JsonTokenType.Null:
                return string.Empty;
            default: throw new JsonException($"无法将 {reader.TokenType} 转换为 string");
        }
    }

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
