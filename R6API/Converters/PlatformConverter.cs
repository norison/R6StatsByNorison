using Newtonsoft.Json;
using System;
using static R6API.Enums;

namespace R6API
{
    public class PlatformConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "uplay":
                    return Platform.UPLAY;
                case "xbl":
                    return Platform.XBOX;
                case "psn":
                    return Platform.PLAYSTATION;
                default:
                    throw new Exception($"Couldn't convert {reader.ValueType} to {objectType}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((Platform)value)
            {
                case Platform.UPLAY:
                    writer.WriteValue("uplay");
                    break;
                case Platform.XBOX:
                    writer.WriteValue("xbl");
                    break;
                case Platform.PLAYSTATION:
                    writer.WriteValue("psn");
                    break;
            }
        }
    }
}
