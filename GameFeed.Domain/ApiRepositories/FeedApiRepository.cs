using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameFeed.Common.Extensions;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Entities;
using GameFeed.Domain.ObjectMappers;
using GameFeed.Domain.Repositories;

namespace GameFeed.Domain.ApiRepositories {

    public interface IFeedApiRepository {
        Task<IEnumerable<FeedItem>> GetFeedByGameIds(IEnumerable<int> gameIds);
    }

    public class FeedApiRepository : IFeedApiRepository {

        private readonly IApiClient _apiClient;

        private readonly IGameRepository _gameRepository;

        public FeedApiRepository(IApiClient apiClient, IGameRepository gameRepository) {
            _apiClient = apiClient;

            _gameRepository = gameRepository;
        }

        //TODO: Can probably be optimized
        /// <summary>
        /// Returns recent feed items from the given game ID's
        /// </summary>
        /// <param name="gameIds">The ID's of the games from which to get the feed items</param>
        /// <returns>Feed items</returns>
        public async Task<IEnumerable<FeedItem>> GetFeedByGameIds(IEnumerable<int> gameIds) {
            //First, obtain the pulse-groups from games with the given ids
            string filter = $"?fields=pulses,game&order=published_at:desc&filter[game][any]={gameIds.CommaSeparate()}";

            IEnumerable<ApiPulseGroup> pulseGroups = _apiClient.GetMultiple<ApiPulseGroup>($"pulse_groups/{filter}");

            //Then, get the feed items (pulses) themselves
            IEnumerable<ApiFeedItem> pulses = pulseGroups.Select(pg => new ApiFeedItem {
                Id = pg.FeedItems.First(),
                Game = pg.Game
            });

            string fields = "id,published_at,updated_at,url,title,summary,pulse_image";

            IEnumerable<ApiFeedItem> apiFeedItems =
                _apiClient.GetMultiple<ApiFeedItem>($"pulses/{pulses.Select(p => p.Id).CommaSeparate()}/?fields={fields}");

            //Convert the ApiFeedItems to FeedItems
            IList<FeedItem> feedItems = new List<FeedItem>();

            foreach (ApiFeedItem apiFeedItem in apiFeedItems) {
                int gameId = pulses.FirstOrDefault(p => p.Id == apiFeedItem.Id).Game;

                FeedItem feedItem = ApiFeedItemMapper.MapFeedItem(apiFeedItem);
                feedItem.Game = await _gameRepository.GetGame(gameId);
                feedItems.Add(feedItem);
            }

            return feedItems;
        }
    }
}
