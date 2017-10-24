using System;
using System.Collections.Generic;
using GameFeed.Common.Enums;

namespace GameFeed.Domain.Entities {

    public class FeedItem {

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public FeedItemCategory Category { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
