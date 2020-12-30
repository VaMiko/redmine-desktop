namespace RedmineHelper.Services
{
    using System;
    using Newtonsoft.Json;

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override void WriteJson(JsonWriter writer, DateTime value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("yyyy'-'MM'-'dd"));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }
    }
}
