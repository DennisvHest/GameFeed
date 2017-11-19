using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Models;
using GameFeed.Domain.Repositories;
using GameFeed.Services.ViewModels;

namespace GameFeed.Services {

    public interface IFeedService {

        Task<HomeViewModel> Home(string userId = null);
        Task<IEnumerable<FeedItem>> ScrollFeed(string scrollUrl);
    }

    public class FeedService : IFeedService {

        private readonly IFeedApiRepository _feedApiRepository;
        private readonly IGameRepository _gameRepository;

        public FeedService(IFeedApiRepository feedApiRepository, IGameRepository gameRepository) {
            _feedApiRepository = feedApiRepository;
            _gameRepository = gameRepository;
        }

        public async Task<HomeViewModel> Home(string userId = null) {
            HomeViewModel model;

            if (userId != null) {
                ScrollResponse feedResponse = await GetPersonalisedHomeFeed(userId);
                model = new HomeViewModel {
                    Feed = feedResponse.Scrollables as IEnumerable<FeedItem>,
                    FeedPageCount = feedResponse.PageCount,
                    FeedScrollUrl = feedResponse.ScrollUrl
                };
            } else {
                model = new HomeViewModel {
                    Feed = new List<FeedItem>()
                };
            }

            return model;
        }

        private async Task<ScrollResponse> GetPersonalisedHomeFeed(string userId) {
            //Retrieve the ID's of the games the user is following
            IEnumerable<int> followingGames = _gameRepository.GetFollowingGamesFromUser(userId).Select(g => g.Id);

            //Retrieve the personalised feed
            return followingGames.Any()
                ? await _feedApiRepository.GetScrollableFeed(followingGames)
                : new ScrollResponse { Scrollables = new List<FeedItem>() };
        }

        public async Task<IEnumerable<FeedItem>> ScrollFeed(string scrollUrl) {
            return await _feedApiRepository.GetScrollableFeed(scrollUrl);
        }
    }
}
