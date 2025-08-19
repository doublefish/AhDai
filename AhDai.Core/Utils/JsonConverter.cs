using AhDai.Base;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Core.Utils;

/// <summary>
/// JsonConverter
/// </summary>
/// <param name="formats"></param>
public class DatetimeJsonConverter(string[]? formats = null) : JsonConverter<DateTime>
{
    readonly string[] _formats = formats ?? [
        Const.Iso8601WithOffsetDateTimeFormat,
        Const.Iso8601UtcDateTimeFormat,
        Const.StandardDateTimeFormat,
    ];

    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (!string.IsNullOrEmpty(dateString))
        {
            foreach (var format in _formats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }
        }
        return reader.GetDateTime();
    }

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_formats[0]));
    }
}
