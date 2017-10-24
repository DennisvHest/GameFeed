using GameFeed.Common;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.ObjectMappers {
    public static class ApiFeedItemMapper {

        public static FeedItem MapFeedItem(ApiFeedItem apiFeedItem) {
            return new FeedItem {
                Id = apiFeedItem.Id,
                DateCreated = Constants.UnixEpoch.AddMilliseconds(apiFeedItem.DateCreated),
                DateUpdated = Constants.UnixEpoch.AddMilliseconds(apiFeedItem.DateUpdated),
                Url = apiFeedItem.Url,
                Title = apiFeedItem.Title,
                Category = apiFeedItem.Category,

            };
        }
    }
}
