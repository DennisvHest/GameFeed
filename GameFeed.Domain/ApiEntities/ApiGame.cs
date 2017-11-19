using System.Collections.Generic;
using GameFeed.Domain.Entities;
using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {

    public class ApiGame {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        [JsonProperty("first_release_date")]
        public long FirstReleaseDate { get; set; }
        [JsonProperty("aggregated_rating")]
        public float AggregatedRating { get; set; }
        [JsonProperty("release_dates")]
        public IEnumerable<ApiGamePlatform> GamePlatforms { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<ApiImage> Screenshots { get; set; }
        public IEnumerable<Company> Developers { get; set; }
        public IEnumerable<Company> Publishers { get; set; }
        public ApiImage Cover { get; set; }
    }
}
