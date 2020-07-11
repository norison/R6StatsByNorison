using Newtonsoft.Json;

namespace R6API
{
    public class Weapon
    {
        [JsonIgnore]
        public WeaponDef WeaponDefiniton;
        [JsonIgnore]
        public string Name => WeaponDefiniton.Id;
        [JsonProperty("kills")]
        public int Kills { get; set; }
        [JsonProperty("headshot")]
        public int Headshonts { get; set; }
        [JsonProperty("bullethit")]
        public int Hits { get; set; }
        [JsonProperty("bulletfired")]
        public int Shots { get; set; }
    }
}
