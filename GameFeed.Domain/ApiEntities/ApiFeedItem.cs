using System.Collections.Generic;
using GameFeed.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameFeed.Domain.ApiEntities {

    public class ApiFeedItem {

        public int Id { get; set; }
        [JsonProperty("created_at")]
        public long DateCreated { get; set; }
        [JsonProperty("updated_at")]
        public long DateUpdated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FeedItemCategory Category { get; set; }
        [JsonProperty("games")]
        public IEnumerable<int> GameIds { get; set; }
    }
}
