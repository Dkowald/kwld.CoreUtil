#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kwld.CoreUtil.Collections;

/// <summary>
/// A <see cref="JsonConverterFactory"/> to serialize
/// <see cref="RecordArray{T}"/> objects as arrays.
/// </summary>
public class RecordArrayConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsGenericType &&
           typeToConvert.GetGenericTypeDefinition() == typeof(RecordArray<>);

    /// <summary>
    /// Creates a <see cref="Converter{T}"/>
    /// </summary>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var itemType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(Converter<>).MakeGenericType(itemType);

        return Activator.CreateInstance(converterType) as JsonConverter;
    }

    /// <summary>
    /// Custom JSON converter for <see cref="RecordArray{T}"/>.
    /// reads / writes items as an array.
    /// </summary>
    private class Converter<T> : JsonConverter<RecordArray<T>>
    {
        /// <inheritdoc />
        public override RecordArray<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var data = JsonSerializer.Deserialize<T[]>(ref reader, options);
            return data is null ? null : new RecordArray<T>(data);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, RecordArray<T> value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, (IEnumerable<T>)value, options);
    }
}
#endif