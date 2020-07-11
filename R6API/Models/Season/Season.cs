using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace R6API
{
    public class Season : ICloneable
    {
        private string background;

        [JsonIgnore]
        public List<Rank> Ranks { get; internal set; }

        [JsonIgnore]
        public static int LatestSeason { get; internal set; }
        [JsonIgnore]
        public string SeasonColor => Name.Color();
        [JsonIgnore]
        public int Id { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("background")]
        public string Background
        {
            get => API.BaseUrl + background;
            internal set => background = value;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
