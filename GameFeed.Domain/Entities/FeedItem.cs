using System;
using GameFeed.Domain.Models;

namespace GameFeed.Domain.Entities {

    public class FeedItem : IScrollable {

        public int Id { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public Image Image { get; set; }
        public Game Game { get; set; }
    }
}
