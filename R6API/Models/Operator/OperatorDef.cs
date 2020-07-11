using Newtonsoft.Json;
using R6API.Converters;
using static R6API.Enums;

namespace R6API
{
    public class OperatorDef
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("category")]
        [JsonConverter(typeof(OperatorCategoryConverter))]
        public OperatorCategory Category { get; internal set; }
        [JsonProperty("index")]
        public string Index { get; internal set; }
        [JsonProperty("figure")]
        public Figure Figure { get; internal set; }
        [JsonProperty("mask")]
        public string Mask { get; internal set; }
        [JsonProperty("badge")]
        public string Badge { get; internal set; }
    }
}
