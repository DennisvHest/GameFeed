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
        Task<ScrollResponse> GetScrollableFeed(IEnumerable<int> gameIds);
        Task<IEnumerable<FeedItem>> GetScrollableFeed(string scrollUrl);
    }

    public class FeedApiRepository : IFeedApiRepository {

        private readonly IApiClient _apiClient;

        private readonly IGameRepository _gameRepository;

        public FeedApiRepository(IApiClient apiClient, IGameRepository gameRepository) {
            _apiClient = apiClient;

            _gameRepository = gameRepository;
        }

        /// <summary>
        /// Returns recent feed items from the given game ID's
        /// </summary>
        /// <param name="gameIds">The ID's of the games from which to get the feed items</param>
        /// <returns>Feed items</returns>
        public async Task<ScrollResponse> GetScrollableFeed(IEnumerable<int> gameIds) {
            return await GetScrollableFeed($"pulse_groups/?fields=pulses,game&order=published_at:desc&filter[game][any]={gameIds.CommaSeparate()}&limit=50", true);
        }

        /// <summary>
        /// Returns feed items from continueing the scroll
        /// </summary>
        /// <param name="scrollUrl">The scroll URL to continue scrolling</param>
        /// <returns>Feed items</returns>
        public async Task<IEnumerable<FeedItem>> GetScrollableFeed(string scrollUrl) {
            ScrollResponse response = await GetScrollableFeed(scrollUrl, false);

            return response.Scrollables as IEnumerable<FeedItem>;
        }

        //TODO: Can probably be optimized
        /// <summary>
        /// Returns recent feed items from the endpoint
        /// </summary>
        /// <param name="endpoint">Path from the base URL</param>
        /// <param name="initialScroll">False when continueing from a scroll, otherwise true</param>
        /// <returns>Feed items</returns>
        private async Task<ScrollResponse> GetScrollableFeed(string endpoint, bool initialScroll) {
            //First, obtain the pulse-groups from games with the given ids
            ScrollResponse feedResponse = initialScroll ? _apiClient.Scroll<ApiPulseGroup>(endpoint) : new ScrollResponse {
                Scrollables = _apiClient.GetMultiple<ApiPulseGroup>(endpoint)
            };

            //Then, get the feed items (pulses) themselves
            IEnumerable<ApiFeedItem> pulses = feedResponse.Scrollables.Select(pg => new ApiFeedItem {
                Id = ((ApiPulseGroup)pg).FeedItems.First(),
                Game = ((ApiPulseGroup)pg).Game
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

            feedResponse.Scrollables = feedItems;

            return feedResponse;
        }
    }
}
