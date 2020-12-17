using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord.Rest.Converters
{
    public class JsonOptionalStringConverter : JsonConverter<Optional<string>>
    {
        public override Optional<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string prop = reader.GetString();
            if (string.IsNullOrEmpty(prop))
                return default;
            return new Optional<string>(prop);
        }

        public override void Write(Utf8JsonWriter writer, Optional<string> id, JsonSerializerOptions options)
        {
            writer.WriteStringValue(id.Value);
        }
    }
}