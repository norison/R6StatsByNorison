using Newtonsoft.Json;

namespace R6API
{
    public class WeaponDef
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("index")]
        public int Index { get; internal set; }
    }
}
