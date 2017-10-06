using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {

    public class ApiGamePlatform {

        public int GameId { get; set; }
        [JsonProperty("platform")]
        public int PlatformId { get; set; }
        [JsonProperty("date")]
        public long ReleaseDate { get; set; }
    }
}
