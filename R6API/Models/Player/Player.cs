using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static R6API.Enums;

namespace R6API
{
    public class Player
    {
        [JsonProperty("profileId")]
        public string ProfileId { get; private set; }
        [JsonProperty("userId")]
        public string UserId { get; private set; }
        [JsonProperty("platformType")]
        [JsonConverter(typeof(PlatformConverter))]
        public Platform Platform { get; private set; }
        public string PlatformUrl => Platform.ToStringUrlValue();
        public string PlatformSpaceId => Platform.ToStringSpacedId();
        [JsonProperty("idOnPlatform")]
        public string IdOnPlatform { get; private set; }
        [JsonProperty("nameOnPlatform")]
        public string Name { get; private set; }

        public string IconUrl => $"https://ubisoft-avatars.akamaized.net/{UserId}/default_256_256.png";

        [JsonIgnore]
        public Progression Progression { get; private set; }
        [JsonIgnore]
        public List<Season> Seasons { get; private set; }
        [JsonIgnore]
        public Stats Stats { get; private set; }
        [JsonIgnore]
        public Operators Operators { get; private set; }
        [JsonIgnore]
        public List<Weapon> Weapons { get; private set; }
        [JsonIgnore]
        public DateTime UpdatedTime { get; private set; }

        /// <summary>
        /// Делаем 5 запросов в 5 потоках по всей статистике игрока и обновляем таймер
        /// </summary>
        /// <returns></returns>
        public async Task LoadAllPlayerStatsAsync()
        {
            Task progression = Task.Run(async () => await LoadProgressionAsync());
            Task stats = Task.Run(async () => await LoadAllStatsAsync());
            Task operators = Task.Run(async () => await LoadAllOperatorsAsync());
            Task seasons = Task.Run(async () => await LoadAllSeasonsAsync());
            Task weapons = Task.Run(async () => await LoadWeaponsAsync());

            await Task.WhenAll(progression, stats, operators, seasons, weapons);

            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// Запрос на прогресс игрока
        /// </summary>
        /// <returns></returns>
        private async Task LoadProgressionAsync()
        {
            var json = await API.GetAPI().GetAsync($"https://public-ubiservices.ubi.com/v1/spaces/{PlatformSpaceId}/sandboxes/{PlatformUrl}/r6playerprofile/playerprofile/progressions?profile_ids={ProfileId}");
            var data = JsonConvert.DeserializeObject<JObject>(json);

            var profile = JsonConvert.DeserializeObject<Progression>(data.First.First.First.ToString());
            profile.LootBoxChance /= 10000;
            Progression = profile;
        }

        /// <summary>
        /// Запрос на ранг игрока
        /// </summary>
        /// <param name="region"> регион </param>
        /// <param name="season"> номер сезона </param>
        /// <returns></returns>
        private async Task<Rank> LoadRankAsync(RankedRegion region, int season = -1)
        {
            var json = await API.GetAPI().GetAsync($"https://public-ubiservices.ubi.com/v1/spaces/{PlatformSpaceId}/sandboxes/{PlatformUrl}/r6karma/players?board_id=pvp_ranked&profile_ids={ProfileId}&region_id={region.ToStringValue()}&season_id={season}");
            var data = JsonConvert.DeserializeObject<JObject>(json);

            Rank rank = JsonConvert.DeserializeObject<Rank>(data["players"][ProfileId].ToString());
            rank.Name = Rank.Ranks[rank.RankId];
            rank.Icon = Rank.RankIcons[rank.RankId];
            return rank;
        }

        /// <summary>
        /// Запрос всех сезонов из апи, клонируем каждый сезон и добавляем в игрока
        /// </summary>
        /// <returns></returns>
        private async Task LoadAllSeasonsAsync()
        {
            var seasons = await API.GetAPI().GetSeasons();

            Seasons = new List<Season>();

            for (int i = Season.LatestSeason; i > 5; i--)
            {
                var season = seasons[i].Clone() as Season;
                season.Ranks = new List<Rank>
                {
                    await LoadRankAsync(RankedRegion.EU, i),
                    await LoadRankAsync(RankedRegion.NA, i),
                    await LoadRankAsync(RankedRegion.ASIA, i)
                };

                season.Ranks = new List<Rank>(season.Ranks.OrderByDescending(rank => rank.MaxMMR));

                Seasons.Add(season);
            }
        }

        /// <summary>
        /// Запросы на всю основую стату игрока
        /// </summary>
        /// <returns></returns>
        private async Task LoadAllStatsAsync()
        {
            await LoadCasualRankedStatsAsync();
            await LoadGeneralAsync();
            await LoadGamemodesAsync();
        }

        /// <summary>
        /// Запрос на статистику и возвращаем только стату по ключу
        /// </summary>
        /// <param name="statistics"> передаем нужную статистику </param>
        /// <returns></returns>
        private async Task<JToken> LoadStatsHelperAsync(List<string> statistics)
        {
            var json = await API.GetAPI().GetAsync($"https://public-ubiservices.ubi.com/v1/spaces/{PlatformSpaceId}/sandboxes/{PlatformUrl}/playerstats2/statistics?populations={ProfileId}&statistics={string.Join(",", statistics)}");
            var data = JsonConvert.DeserializeObject<JObject>(json);
            return data["results"][ProfileId];
        }

        /// <summary>
        /// Запрос на статистику рейтинговой и обычной игры
        /// </summary>
        /// <returns></returns>
        private async Task LoadCasualRankedStatsAsync()
        {
            var statistics = new List<string>
                {
                    "casualpvp_matchwon", "casualpvp_matchlost", "casualpvp_timeplayed",
                    "casualpvp_matchplayed", "casualpvp_kills", "casualpvp_death",
                    "rankedpvp_matchwon", "rankedpvp_matchlost", "rankedpvp_timeplayed",
                    "rankedpvp_matchplayed", "rankedpvp_kills", "rankedpvp_death"
                };

            var data = await LoadStatsHelperAsync(statistics);

            Stats = new Stats
            {
                Ranked = data != null ? JsonConvert.DeserializeObject<RankedStats>(data.ToString()) : new RankedStats(),
                Casual = data != null ? JsonConvert.DeserializeObject<CasualStats>(data.ToString()) : new CasualStats()
            };
        }

        /// <summary>
        /// Запрос на общую статистику
        /// </summary>
        /// <returns></returns>
        private async Task LoadGeneralAsync()
        {
            var statistics = new List<string>
            {
                "generalpvp_timeplayed", "generalpvp_matchplayed", "generalpvp_matchwon",
                "generalpvp_matchlost", "generalpvp_kills", "generalpvp_death",
                "generalpvp_bullethit", "generalpvp_bulletfired", "generalpvp_killassists",
                "generalpvp_revive", "generalpvp_headshot", "generalpvp_penetrationkills",
                "generalpvp_meleekills", "generalpvp_dbnoassists", "generalpvp_suicide",
                "generalpvp_barricadedeployed", "generalpvp_reinforcementdeploy", "generalpvp_totalxp",
                "generalpvp_rappelbreach", "generalpvp_distancetravelled", "generalpvp_revivedenied",
                "generalpvp_dbno", "generalpvp_gadgetdestroy", "generalpvp_blindkills"
            };

            var data = await LoadStatsHelperAsync(statistics);

            if (Stats is null)
                Stats = new Stats();

            Stats.General = data != null ? JsonConvert.DeserializeObject<GeneralStats>(data.ToString()) : new GeneralStats();
        }

        /// <summary>
        /// Запрос на всех оперативников и получаем дополнительную инфу из апи
        /// </summary>
        /// <returns></returns>
        private async Task LoadAllOperatorsAsync()
        {
            var statistics = "operatorpvp_kills,operatorpvp_death,operatorpvp_roundwon,operatorpvp_roundlost,operatorpvp_meleekills,operatorpvp_totalxp,operatorpvp_headshot,operatorpvp_timeplayed,operatorpvp_dbno";

            var json = await API.GetAPI().GetAsync($"https://public-ubiservices.ubi.com/v1/spaces/{PlatformSpaceId}/sandboxes/{PlatformUrl}/playerstats2/statistics?populations={ProfileId}&statistics={statistics}");
            var data = JsonConvert.DeserializeObject<JObject>(json);

            var results = data["results"][ProfileId];

            Operators = new Operators
            {
                Attackers = new List<Operator>(),
                Defenders = new List<Operator>()
            };

            if (results is null)
                return;

            foreach (var opDef in await API.GetAPI().GetOperatorDefinitions())
            {
                var location = $":{opDef.Value.Index}:";
                var values = results.Where(x => x.Path.Contains(location));
                if (values.Count() > 0)
                {
                    var opData = '{' + string.Join(",", values.Select(x => "\"" + x.Path.Split(':')[0].Split('_')[1] + "\":" + x.First)) + '}';
                    var op = JsonConvert.DeserializeObject<Operator>(opData);
                    op.OperatorDefinition = opDef.Value;
                    op.Name = op.OperatorDefinition.Id;

                    if (op.OperatorDefinition.Category == OperatorCategory.Attack)
                        Operators.Attackers.Add(op);
                    else
                        Operators.Defenders.Add(op);
                }
            }
        }

        /// <summary>
        /// Запрос на статистику всех режимов игры (pvp)
        /// </summary>
        /// <returns></returns>
        private async Task LoadGamemodesAsync()
        {
            var statistics = new List<string>{ "secureareapvp_matchwon", "secureareapvp_matchlost", "secureareapvp_matchplayed",
                                                   "secureareapvp_bestscore", "rescuehostagepvp_matchwon", "rescuehostagepvp_matchlost",
                                                   "rescuehostagepvp_matchplayed", "rescuehostagepvp_bestscore", "plantbombpvp_matchwon",
                                                   "plantbombpvp_matchlost", "plantbombpvp_matchplayed", "plantbombpvp_bestscore",
                                                   "generalpvp_servershacked", "generalpvp_serverdefender", "generalpvp_serveraggression",
                                                   "generalpvp_hostagerescue", "generalpvp_hostagedefense" };

            var data = await LoadStatsHelperAsync(statistics);

            if (Stats is null)
                Stats = new Stats();

            Stats.Bomb = data != null ? JsonConvert.DeserializeObject<BombStats>(data.ToString()) : new BombStats();
            Stats.SecureArea = data != null ? JsonConvert.DeserializeObject<SecureAreaStats>(data.ToString()) : new SecureAreaStats();
            Stats.Hostage = data != null ? JsonConvert.DeserializeObject<HostageStats>(data.ToString()) : new HostageStats();
        }

        /// <summary>
        /// Запрос на все виды оружия и получаем дополнительную инфу из апи
        /// </summary>
        /// <returns></returns>
        private async Task LoadWeaponsAsync()
        {
            var json = await API.GetAPI().GetAsync($"https://public-ubiservices.ubi.com/v1/spaces/{PlatformSpaceId}/sandboxes/{PlatformUrl}/playerstats2/statistics?populations={ProfileId}&statistics=weapontypepvp_kills,weapontypepvp_headshot,weapontypepvp_bulletfired,weapontypepvp_bullethit");
            var data = JsonConvert.DeserializeObject<JObject>(json);

            var results = data["results"][ProfileId];

            Weapons = new List<Weapon>();

            if (results is null)
                return;

            foreach (var wepDef in await API.GetAPI().GetWeaponDefinitions())
            {
                var location = wepDef.Value.Index;
                var values = results.Where(x => x.Path.Contains($":{location}:"));
                if (values.Count() > 0)
                {
                    var wepData = '{' + string.Join(",", values.Select(x => "\"" + x.Path.Split(':')[0].Split('_')[1] + "\":" + x.First)) + '}';
                    var wep = JsonConvert.DeserializeObject<Weapon>(wepData);
                    wep.WeaponDefiniton = wepDef.Value;
                    Weapons.Add(wep);
                }
            }
        }
    }
}
