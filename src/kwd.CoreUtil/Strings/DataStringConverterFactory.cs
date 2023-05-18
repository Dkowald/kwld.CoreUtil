#if NET6_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kwd.CoreUtil.Strings
{
    /// <summary>
    /// Custom JSON serialize for <see cref="IDataString"/> types.
    /// </summary>
    public class DataStringConverterFactory : JsonConverterFactory
    {
        /// <summary>
        /// True if type is a <see cref="IDataString"/>.
        /// </summary>
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert.IsAssignableTo(typeof(IDataString));
        
        /// <summary>
        /// Implement <see cref="JsonConverterFactory.CreateConverter"/>.
        /// </summary>
        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => Activator.CreateInstance(
                typeof(DataStringConverter<>)
                    .MakeGenericType(typeToConvert)) as JsonConverter;
    }
}
#endif