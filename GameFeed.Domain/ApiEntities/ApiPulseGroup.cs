using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {
    public class ApiPulseGroup {

        public int Game { get; set; }
        [JsonProperty("pulses")]
        public IEnumerable<int> FeedItems { get; set; }
    }
}
