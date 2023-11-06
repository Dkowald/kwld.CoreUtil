#if NET6_0_OR_GREATER
using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kwd.CoreUtil.Strings;

/// <summary>
/// Implement a <see cref="JsonConverter{T}"/> for a <see cref="IDataString"/>.
/// </summary>
internal class DataStringConverter<T> : JsonConverter<T> where T : IDataString
{
    /// <summary>
    /// Read next token as string, and create a new <typeparamref name="T"/> from it.
    /// </summary>
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var data = reader.GetString();

        if (data is null) return default;

        var parseMethod = typeToConvert.GetMethods(
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(m => m.Name == "TryParse")
            .First(m =>
            {
                var args = m.GetParameters();
                return args.Length == 1 &&
                       args[0].ParameterType == typeof(string);
            });

        if (parseMethod is null)
            throw new Exception($"The '{nameof(IDataString)}' type '{typeToConvert.Name}' MUST implement TryParse");
        
        var args = new object[] { data };
        var result = parseMethod.Invoke(null, args);
        return result is T tVal ? tVal : default;
    }

    /// <summary>
    /// Implement <see cref="JsonConverter{T}.Write"/>,
    /// to write a string value using <see cref="IDataString.Value"/>
    /// </summary>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
#endif