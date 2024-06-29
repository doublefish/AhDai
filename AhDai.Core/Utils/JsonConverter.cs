using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Core.Utils;

/// <summary>
/// JsonConverter
/// </summary>
/// <param name="format"></param>
public class DatetimeJsonConverter(string format) : JsonConverter<DateTime>
{
    /// <summary>
    /// 格式
    /// </summary>
    public string Format { get; set; } = format;

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
			return reader.GetDateTime();
		}
		return DateTime.TryParse(reader.GetString(), out var date) ? date : reader.GetDateTime();
	}

	/// <summary>
	/// Write
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="value"></param>
	/// <param name="options"></param>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString(Format));
	}
}
