using Newtonsoft.Json;
using System;

namespace R6API
{
    public class CasualStats
    {
        [JsonProperty("casualpvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("casualpvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("casualpvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
        [JsonProperty("casualpvp_kills:infinite")]
        public int Kills { get; internal set; }
        [JsonProperty("casualpvp_death:infinite")]
        public int Deaths { get; internal set; }
        [JsonConverter(typeof(TimeSpanFromSecondsConverter))]
        [JsonProperty("casualpvp_timeplayed:infinite")]
        public TimeSpan TimePlayed { get; internal set; }
    }
}
