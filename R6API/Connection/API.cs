using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static R6API.Enums;

namespace R6API
{
    public class API
    {
        private readonly string Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("email:password"));
        private string SessionId;
        private string AuthKey;
        private static API api;

        public static string BaseUrl { get; set; } = "https://game-rainbow6.ubi.com/";
        public string OperatorsEndpoint { get; set; } = BaseUrl + "assets/data/operators.d72ec5b825.json";
        public string WeaponsEndpoint { get; set; } = BaseUrl + "assets/data/weapons.8a9b3d9e7d.json";
        public string SeasonsEndpoint { get; set; } = BaseUrl + "assets/data/seasons.3a4769b878.json";

        private API() => api = this;

        public static API GetAPI() => api ?? new API();


        /// <summary>
        ///  Юбики обновляют json раз в пол года, считывать каждый раз при запуске проги нет смысла, использовать, когда обновиться инфа!
        /// </summary>
        private void LoadEndpoints()
        {
            using (WebClient wc = new WebClient())
            {
                var html = wc.DownloadString(BaseUrl);

                var mainSearch = "<script src=\"assets/scripts/main.";
                var vendorSearch = "<script src=\"assets/scripts/vendor.";
                var endSearch = ".js\"></script>";

                string FindBetween(string str, string start, string end)
                {
                    var startPos = str.IndexOf(start) + start.Length;
                    var endPos = str.IndexOf(end, startPos);
                    return str.Substring(startPos, endPos - startPos);
                }

                var mainUrl = BaseUrl + "assets/scripts/main." + FindBetween(html, mainSearch, endSearch) + ".js";
                var vendorUrl = BaseUrl + "assets/scripts/vendor." + FindBetween(html, vendorSearch, endSearch) + ".js";

                var mainJs = wc.DownloadString(mainUrl);
                var vendorJs = wc.DownloadString(vendorUrl);

                var json = ".json";

                OperatorsEndpoint = BaseUrl + "assets/data/operators." + FindBetween(mainJs, "assets/data/operators.", json) + json;
                WeaponsEndpoint = BaseUrl + "assets/data/weapons." + FindBetween(mainJs, "assets/data/weapons.", json) + json;
                SeasonsEndpoint = BaseUrl + "assets/data/seasons." + FindBetween(mainJs, "assets/data/seasons.", json) + json;
            }
        }

        /// <summary>
        /// Делаем POST запрос
        /// </summary>
        /// <returns> Получаем ключ и номер сессии </returns>
        public async Task Connect()
        {
            if (AuthKey is null)
            {
                WebRequest request = WebRequest.Create("https://public-ubiservices.ubi.com/v3/profiles/sessions");
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic " + Token);
                request.Headers.Add("Ubi-AppId", "39baebad-39e5-4552-8c25-2c9b919064e2");
                request.ContentType = "application/json; charset=UTF-8";

                using (await request.GetRequestStreamAsync())
                {
                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream, Encoding.Default);
                            var data = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());

                            if (data.ContainsKey("ticket"))
                            {
                                AuthKey = data["ticket"].Value<string>();
                                SessionId = data["sessionId"].Value<string>();
                            }
                            else
                                throw new Exception("Connection error");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// любой GET запрос
        /// </summary>
        /// <param name="uri"> Ссылка запроса </param>
        /// <returns> Получаем данные </returns>
        public async Task<string> GetAsync(string uri)
        {
            var webHeader = new WebHeaderCollection
            {
                    { "Authorization", "Ubi_v1 t=" + AuthKey },
                    { "Ubi-AppId", "39baebad-39e5-4552-8c25-2c9b919064e2" },
                    { "Ubi-SessionId", SessionId },
            };

            var request = WebRequest.Create(uri);
            request.Method = "GET";
            request.Headers = webHeader;

            using (var response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    var StreamReader = new StreamReader(stream, Encoding.Default);
                    return StreamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///  Получаем игрока
        /// </summary>
        /// <param name="player"> Ник игрока </param>
        /// <param name="platform"> Платформа игрока </param>
        /// <returns> Получаем объект игрока </returns>
        public async Task<Player> GetPlayer(string player, Platform platform)
        {
            if (AuthKey is null)
                await Connect();

            var data = JsonConvert.DeserializeObject<JObject>(await GetAsync($"https://public-ubiservices.ubi.com/v2/profiles?nameOnPlatform={player}&platformType={platform.ToStringValue()}"));
            return data["profiles"].ToObject<IEnumerable<Player>>().FirstOrDefault() ?? throw new Exception("Player not found");
        }

        private Dictionary<string, OperatorDef> Operators;
        /// <summary>
        /// Запрос на допольнительную информацию о всех оперативниках
        /// </summary>
        /// <returns> получаем данные </returns>
        public async Task<IDictionary<string, OperatorDef>> GetOperatorDefinitions()
        {
            if (Operators is null)
            {
                var json = await GetAsync(OperatorsEndpoint);
                Operators = JsonConvert.DeserializeObject<Dictionary<string, OperatorDef>>(json);
            }
            return Operators;
        }

        private Dictionary<string, WeaponDef> Weapons;
        /// <summary>
        /// Запрос на допольнительную информацию о всех типах оружия
        /// </summary>
        /// <returns> Получаем данные </returns>
        public async Task<IDictionary<string, WeaponDef>> GetWeaponDefinitions()
        {
            if (Weapons is null)
            {
                var json = await GetAsync(WeaponsEndpoint);
                Weapons = JsonConvert.DeserializeObject<Dictionary<string, WeaponDef>>(json);
            }
            return Weapons;
        }

        private Dictionary<int, Season> Seasons;
        /// <summary>
        /// Запрос на допольнительную информацию о всех сезонах
        /// </summary>
        /// <returns> Получаем данные </returns>
        public async Task<Dictionary<int, Season>> GetSeasons()
        {
            if (Seasons is null)
            {
                Seasons = new Dictionary<int, Season>();

                var json = await GetAsync(SeasonsEndpoint);

                var data = JsonConvert.DeserializeObject<JObject>(json);

                Season.LatestSeason = data.Last.First.ToObject<int>();
                foreach (var sjson in data.First.Values())
                {
                    var s = sjson.Last.ToObject<Season>();
                    s.Id = int.Parse(((JProperty)sjson).Name);
                    Seasons.Add(s.Id, s);
                }
            }
            return Seasons;
        }
    }
}
