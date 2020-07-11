using Newtonsoft.Json;
using System;

namespace R6API
{
    public class GeneralStats
    {
        [JsonIgnore]
        public double KD => (double)Kills / Deaths;
        [JsonConverter(typeof(TimeSpanFromSecondsConverter))]
        [JsonProperty("generalpvp_timeplayed:infinite")]
        public TimeSpan TimePlayed { get; internal set; }
        [JsonProperty("generalpvp_matchplayed:infinite")]
        public int MatchPlayed { get; internal set; }
        [JsonProperty("generalpvp_matchwon:infinite")]
        public int MatchWon { get; internal set; }
        [JsonProperty("generalpvp_matchlost:infinite")]
        public int MatchLost { get; internal set; }
        [JsonProperty("generalpvp_kills:infinite")]
        public int Kills { get; internal set; }
        [JsonProperty("generalpvp_death:infinite")]
        public int Deaths { get; internal set; }
        [JsonProperty("generalpvp_bullethit:infinite")]
        public int BulletHits { get; internal set; }
        [JsonProperty("generalpvp_bulletfired:infinite")]
        public int BulletFired { get; internal set; }
        [JsonProperty("generalpvp_killassists:infinite")]
        public int KillAssists { get; internal set; }
        [JsonProperty("generalpvp_revive:infinite")]
        public int Revives { get; internal set; }
        [JsonProperty("generalpvp_headshot:infinite")]
        public int Headshots { get; internal set; }
        [JsonProperty("generalpvp_penetrationkills:infinite")]
        public int PenetrationKills { get; internal set; }
        [JsonProperty("generalpvp_meleekills:infinite")]
        public int MeleeKills { get; internal set; }
        [JsonProperty("generalpvp_dbnoassists:infinite")]
        public int DBNOAssists { get; internal set; }
        [JsonProperty("generalpvp_suicide:infinite")]
        public int Sucicides { get; internal set; }
        [JsonProperty("generalpvp_barricadedeployed:infinite")]
        public int BarricadesDeployed { get; internal set; }
        [JsonProperty("generalpvp_reinforcementdeploy:infinite")]
        public int ReinforcementsDeployed { get; internal set; }
        [JsonProperty("generalpvp_totalxp:infinite")]
        public int TotalXP { get; internal set; }
        [JsonProperty("generalpvp_rappelbreach:infinite")]
        public int RappelBreach { get; internal set; }
        [JsonProperty("generalpvp_distancetravelled:infinite")]
        public long DistanceTravelled { get; internal set; }
        [JsonProperty("generalpvp_revivedenied:infinite")]
        public int RevivesDenied { get; internal set; }
        [JsonProperty("generalpvp_dbno:infinite")]
        public int DBNOs { get; internal set; }
        [JsonProperty("generalpvp_gadgetdestroy:infinite")]
        public int GadgetsDestroyed { get; internal set; }
        [JsonProperty("generalpvp_blindkills:infinite")]
        public int BlindKills { get; internal set; }
    }
}
