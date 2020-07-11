using Newtonsoft.Json;
using System;

namespace R6API
{
    public class RankedStats
    {
        [JsonProperty("rankedpvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("rankedpvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("rankedpvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
        [JsonProperty("rankedpvp_kills:infinite")]
        public int Kills { get; internal set; }
        [JsonProperty("rankedpvp_death:infinite")]
        public int Deaths { get; internal set; }
        [JsonConverter(typeof(TimeSpanFromSecondsConverter))]
        [JsonProperty("rankedpvp_timeplayed:infinite")]
        public TimeSpan TimePlayed { get; internal set; }
    }
}
