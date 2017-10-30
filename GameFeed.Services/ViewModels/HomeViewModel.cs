using System.Collections.Generic;
using GameFeed.Domain.Entities;

namespace GameFeed.Services.ViewModels {
    public class HomeViewModel {

        public IEnumerable<FeedItem> Feed { get; set; }
    }
}
