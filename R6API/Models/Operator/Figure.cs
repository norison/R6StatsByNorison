using Newtonsoft.Json;

namespace R6API
{
    public class Figure
    {
        [JsonProperty("small")]
        public string Small { get; internal set; }
        [JsonProperty("large")]
        public string Large { get; internal set; }
    }
}
