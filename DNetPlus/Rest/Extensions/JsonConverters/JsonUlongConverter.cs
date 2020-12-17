using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord.Rest.Converters
{
    public class JsonUlongConverter : JsonConverter<ulong>
    {
        public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    if (ulong.TryParse(reader.GetString(), out ulong ID))
                        return ID;
                    break;
                case JsonTokenType.Number:
                    return reader.GetUInt64();
            }
            return 0;
        }

        public override void Write(Utf8JsonWriter writer, ulong id, JsonSerializerOptions options) =>
                writer.WriteStringValue(id.ToString());
    }
}
