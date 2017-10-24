using System.Collections.Generic;
using System.Linq;
using GameFeed.Domain.ApiEntities;

namespace GameFeed.Domain.ApiRepositories {

    public interface IFeedApiRepository {
        IEnumerable<ApiFeedItem> GetFeedsByGameIds(IEnumerable<int> gameIds);
    }

    public class FeedApiRepository : IFeedApiRepository {

        private readonly IApiClient _apiClient;

        public FeedApiRepository(IApiClient apiClient) {
            _apiClient = apiClient;
        }

        public IEnumerable<ApiFeedItem> GetFeedsByGameIds(IEnumerable<int> gameIds) {
            string filter = "filter[games.id]";

            filter += gameIds.First();
            foreach (int gameId in gameIds.Skip(1)) {
                filter += $",{gameId}";
            }

            IEnumerable<ApiFeedItem> feed = _apiClient.GetMultiple<ApiFeedItem>($"feeds/{filter}");
        }
    }
}
