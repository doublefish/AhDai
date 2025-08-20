﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AhDai.Core.Utils;

/// <summary>
/// DatetimeJsonConverter
/// </summary>
/// <param name="format"></param>
public class DatetimeJsonConverter(string format) : JsonConverter<DateTime>
{
    readonly string _format = format;

    /// <summary>
    /// Read
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
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
        writer.WriteStringValue(value.ToString(_format));
    }
}
