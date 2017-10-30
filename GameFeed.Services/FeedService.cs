using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Repositories;
using GameFeed.Services.ViewModels;

namespace GameFeed.Services {

    public interface IFeedService {

        Task<HomeViewModel> Home(string userId = null);
    }

    public class FeedService : IFeedService {

        private readonly IFeedApiRepository _feedApiRepository;
        private readonly IGameRepository _gameRepository;

        public FeedService(IFeedApiRepository feedApiRepository, IGameRepository gameRepository) {
            _feedApiRepository = feedApiRepository;
            _gameRepository = gameRepository;
        }

        public async Task<HomeViewModel> Home(string userId = null) {
            return new HomeViewModel {
                Feed = userId != null ? await GetPersonalisedHomeFeed(userId) : new List<FeedItem>()
            };
        }

        private async Task<IEnumerable<FeedItem>> GetPersonalisedHomeFeed(string userId) {
            //Retrieve the ID's of the games the user is following
            IEnumerable<int> followingGames = _gameRepository.GetFollowingGamesFromUser(userId).Select(g => g.Id);

            //Retrieve the personalised feed
            return await _feedApiRepository.GetFeedByGameIds(followingGames);
        }
    }
}
