using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Core.Utils;

/// <summary>
/// DatetimeJsonConverter
/// </summary>
/// <param name="writeFormat"></param>
/// <param name="readFormats"></param>
public class DatetimeJsonConverter(string writeFormat, string[]? readFormats = null) : JsonConverter<DateTime>
{
    readonly string _writeFormat = writeFormat;

    readonly string[] _readFormats = readFormats ?? [];

    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string but was {reader.TokenType}");
        }
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString))
        {
            return default;
        }
        if (_readFormats.Length > 0 && DateTime.TryParseExact(dateString, _readFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result1))
        {
            return result1;
        }
        if (DateTime.TryParse(dateString, out var result2))
        {
            return result2;
        }
        throw new JsonException($"Unable to parse '{dateString}' as a DateTime. Supported formats: {string.Join(", ", _readFormats)}");
    }

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_writeFormat));
    }
}
