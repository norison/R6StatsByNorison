using Newtonsoft.Json;

namespace R6API
{
    public class SecureAreaStats
    {
        [JsonProperty("secureareapvp_bestscore:infinite")]
        public int BestScore { get; internal set; }
        [JsonProperty("secureareapvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("secureareapvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("secureareapvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
    }
}
