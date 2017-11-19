using System.Collections.Generic;
using GameFeed.Domain.Models;
using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {
    public class ApiPulseGroup : IScrollable {

        public int Game { get; set; }
        [JsonProperty("pulses")]
        public IEnumerable<int> FeedItems { get; set; }
    }
}
