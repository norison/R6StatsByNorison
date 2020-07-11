using Newtonsoft.Json;
using System;
using static R6API.Enums;

namespace R6API
{

    public class RegionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "emea":
                    return RankedRegion.EU;
                case "ncsa":
                    return RankedRegion.NA;
                case "apac":
                    return RankedRegion.ASIA;
                default:
                    throw new Exception($"Couldn't convert {reader.ValueType} to {objectType}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((RankedRegion)value)
            {
                case RankedRegion.EU:
                    writer.WriteValue("emea");
                    break;
                case RankedRegion.NA:
                    writer.WriteValue("ncsa");
                    break;
                case RankedRegion.ASIA:
                    writer.WriteValue("apac");
                    break;
            }
        }
    }
}
