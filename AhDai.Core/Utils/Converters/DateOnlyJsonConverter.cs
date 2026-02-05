using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Core.Utils.Converters;

/// <summary>
/// DateOnlyJsonConverter
/// </summary>
public class DateOnlyJsonConverter(string[]? readFormats = null) : JsonConverter<DateOnly>
{
    readonly string[] _readFormats = readFormats ?? [];

    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default;
        }
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string but was {reader.TokenType}");
        }
        var str = reader.GetString();
        if (string.IsNullOrEmpty(str))
        {
            return default;
        }
        if (_readFormats.Length > 0 && DateOnly.TryParseExact(str, _readFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result1))
        {
            return result1;
        }
        if (DateOnly.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result2))
        {
            return result2;
        }
        if (DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result3))
        {
            return DateOnly.FromDateTime(result3);
        }
        throw new JsonException($"Unable to parse '{str}' as a DateOnly. Supported formats: {string.Join(", ", _readFormats)}");
    }

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
