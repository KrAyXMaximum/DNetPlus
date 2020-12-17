using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord.Rest.Converters
{
    public class JsonOptionalBoolConverter : JsonConverter<Optional<bool>>
    {
        public override Optional<bool> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
                return new Optional<bool>(reader.GetBoolean());
            return default;
        }

        public override void Write(Utf8JsonWriter writer, Optional<bool> id, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(id.Value);
        }
    }
}
