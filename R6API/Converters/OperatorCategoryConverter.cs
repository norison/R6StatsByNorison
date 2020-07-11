using Newtonsoft.Json;
using System;
using static R6API.Enums;

namespace R6API.Converters
{
    public class OperatorCategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "def":
                    return OperatorCategory.Defense;
                case "atk":
                    return OperatorCategory.Attack;
                default:
                    throw new Exception($"Couldn't convert {reader.ValueType} to {objectType}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
