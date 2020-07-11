using Newtonsoft.Json;
using System.Collections.Generic;
using static R6API.Enums;

namespace R6API
{
    public class Rank
    {
        public static List<string> Ranks = new List<string>
        {
                "Unranked",
                "Copper 4",   "Copper 3",   "Copper 2",   "Copper 1",
                "Bronze 4",   "Bronze 3",   "Bronze 2",   "Bronze 1",
                "Silver 4",   "Silver 3",   "Silver 2",   "Silver 1",
                "Gold 4",     "Gold 3",     "Gold 2",     "Gold 1",
                "Platinum 3", "Platinum 2", "Platinum 1", "Diamond"
        };

        public static List<string> RankIcons = new List<string>
        {
                "https://i.imgur.com/sB11BIz.png",  // unranked
                "https://i.imgur.com/ehILQ3i.jpg",  // copper 4
                "https://i.imgur.com/6CxJoMn.jpg",  // copper 3
                "https://i.imgur.com/eI11lah.jpg",  // copper 2
                "https://i.imgur.com/0J0jSWB.jpg",  // copper 1
                "https://i.imgur.com/42AC7RD.jpg",  // bronze 4
                "https://i.imgur.com/QD5LYD7.jpg",  // bronze 3
                "https://i.imgur.com/9AORiNm.jpg",  // bronze 2
                "https://i.imgur.com/hmPhPBj.jpg",  // bronze 1
                "https://i.imgur.com/D36ZfuR.jpg",  // silver 4
                "https://i.imgur.com/m8GToyF.jpg",  // silver 3
                "https://i.imgur.com/EswGcx1.jpg",  // silver 2
                "https://i.imgur.com/KmFpkNc.jpg",  // silver 1
                "https://i.imgur.com/6Qg6aaH.jpg",  // gold 4
                "https://i.imgur.com/B0s1o1h.jpg",  // gold 3
                "https://i.imgur.com/ELbGMc7.jpg",  // gold 2
                "https://i.imgur.com/ffDmiPk.jpg",  // gold 1
                "https://i.imgur.com/6UyZCN3.png",  // plat 3
                "https://i.imgur.com/wFoo9yD.png",  // plat 2
                "https://i.imgur.com/iW1bJiP.png",  // plat 1
                "https://i.imgur.com/WqshtyV.png"   // diamond
        };

        [JsonIgnore]
        public string Name { get; internal set; }
        [JsonIgnore]
        public string Icon { get; internal set; }
        [JsonIgnore]
        public string PreviousRankIcon => RankId > 0 ? RankIcons[RankId - 1] : "";
        [JsonIgnore]
        public string PreviousRankName => RankId > 0 ? Ranks[RankId - 1] : "";
        [JsonIgnore]
        public string NextRankIcon => RankId < 20 ? RankIcons[RankId + 1] : "";
        [JsonIgnore]
        public string NextRankName => RankId < 20 ? Ranks[RankId + 1] : "";
        [JsonIgnore]
        public string MaxRankIcon => RankIcons[(int)MaxRank];
        [JsonIgnore]
        public double WL => Wins / ((double)Wins + Losses) * 100;
        [JsonProperty("max_mmr")]
        public float MaxMMR { get; internal set; }
        [JsonProperty("mmr")]
        public float MMR { get; internal set; }
        [JsonProperty("wins")]
        public int Wins { get; internal set; }
        [JsonProperty("losses")]
        public int Losses { get; internal set; }
        [JsonProperty("abandons")]
        public int Abandons { get; internal set; }
        [JsonProperty("rank")]
        public int RankId { get; internal set; }
        [JsonProperty("season")]
        public int SeasonId { get; internal set; }
        [JsonProperty("max_rank")]
        public float MaxRank { get; internal set; }
        [JsonProperty("next_rank_mmr")]
        public float NextRankMMR { get; internal set; }
        [JsonProperty("previous_rank_mmr")]
        public float PreviousRankMMR { get; internal set; }
        [JsonProperty("region")]
        [JsonConverter(typeof(RegionConverter))]
        public RankedRegion Region { get; internal set; }
        [JsonProperty("skill_mean")]
        public float SkillMean { get; internal set; }
        [JsonProperty("skill_stdev")]
        public float SkillStDev { get; internal set; }
    }
}
