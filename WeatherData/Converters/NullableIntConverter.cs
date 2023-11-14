using Newtonsoft.Json;

namespace WeatherData.Converters
{
    public class NullableIntConverter: Newtonsoft.Json.JsonConverter<int?>
    {
        public override int? ReadJson(JsonReader reader, Type objectType, int? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            return Convert.ToInt32(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, int? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value);
            else
                writer.WriteNull();
        }
    }
}