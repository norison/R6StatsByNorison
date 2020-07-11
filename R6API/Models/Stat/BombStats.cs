using Newtonsoft.Json;

namespace R6API
{
    public class BombStats
    {
        [JsonProperty("plantbombpvp_bestscore:infinite")]
        public int BestScore { get; internal set; }
        [JsonProperty("plantbombpvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("plantbombpvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("plantbombpvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
    }
}
