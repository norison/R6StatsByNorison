using Newtonsoft.Json;

namespace R6API
{
    public class HostageStats
    {
        [JsonProperty("rescuehostagepvp_bestscore:infinite")]
        public int BestScore { get; internal set; }
        [JsonProperty("rescuehostagepvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("rescuehostagepvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("rescuehostagepvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
    }
}
