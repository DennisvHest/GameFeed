﻿using System.Collections.Generic;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Models;
using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {

    public class ApiFeedItem : IScrollable {

        public int Id { get; set; }
        [JsonProperty("published_at")]
        public long DatePublished { get; set; }
        [JsonProperty("updated_at")]
        public long DateUpdated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        [JsonProperty("pulse_image")]
        public ApiImage Image { get; set; }
        public int Game { get; set; }
    }
}
