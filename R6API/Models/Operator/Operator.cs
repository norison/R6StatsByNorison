using Newtonsoft.Json;
using System;

namespace R6API
{
    public class Operator
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public double KD => (double)Kills / Deaths;
        [JsonIgnore]
        public double WL => Wins / ((double)Wins + Losses) * 100;
        [JsonProperty("roundwon")]
        public int Wins { get; internal set; }
        [JsonProperty("roundlost")]
        public int Losses { get; internal set; }
        [JsonProperty("kills")]
        public int Kills { get; internal set; }
        [JsonProperty("death")]
        public int Deaths { get; internal set; }
        [JsonProperty("headshot")]
        public int Headshots { get; internal set; }
        [JsonProperty("meleekills")]
        public int Melees { get; internal set; }
        [JsonProperty("dbno")]
        public int DBNOs { get; internal set; }
        [JsonProperty("totalxp")]
        public int XP { get; internal set; }
        [JsonConverter(typeof(TimeSpanFromSecondsConverter))]
        [JsonProperty("timeplayed")]
        public TimeSpan TimePlayed { get; internal set; }
        [JsonIgnore]
        public string Badge => $"{API.BaseUrl}{OperatorDefinition.Badge}";
        [JsonIgnore]
        public string Figure => $"{API.BaseUrl}{OperatorDefinition.Figure.Small}";
        [JsonIgnore]
        public OperatorDef OperatorDefinition;
    }
}
