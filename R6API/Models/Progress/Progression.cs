using Newtonsoft.Json;

namespace R6API
{
    public class Progression
    {
        [JsonProperty("xp")]
        public int XP { get; internal set; }
        [JsonProperty("lootbox_probability")]
        public float LootBoxChance { get; internal set; }
        [JsonProperty("level")]
        public int Level { get; internal set; }
    }
}
