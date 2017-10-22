using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using GameFeed.Common.Enums;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Repositories;
using GameFeed.Services.ViewModels;

namespace GameFeed.Services {

    public interface IGameService {

        bool GameExistsInDatabase(int id);
        GameDetailViewModel Detail(int id, string userId = null);
        Task ToggleFollow(int gameId, string userId);
    }

    public class GameService : IGameService {

        private readonly IGameRepository _gameRepository;
        private readonly IGameApiRepository _gameApiRepository;

        public GameService(IGameRepository gameRepository, IGameApiRepository gameApiRepository) {
            _gameRepository = gameRepository;
            _gameApiRepository = gameApiRepository;
        }

        /// <summary>
        /// Checks if the Game with the given ID already exists in the database
        /// </summary>
        /// <param name="id">ID of the Game to check for</param>
        /// <returns>True = Game already exists, False = Game does not exist</returns>
        public bool GameExistsInDatabase(int id) {
            return _gameRepository.GameExistsInDatabase(id);
        }

        /// <summary>
        /// Creates the ViewModel for the game detail page
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <param name="userId">User ID for checking if the user is following this game (default null if not authenticated)</param>
        /// <returns>The GameDetailViewModel</returns>
        public GameDetailViewModel Detail(int id, string userId = null) {
            Game game;

            //If the game doesn't already exist in the database, get the game from the IGDB API
            if (GameExistsInDatabase(id)) {
                game = _gameRepository.GetGame(id);
            } else {
                game = _gameApiRepository.GetGame(id);
                _gameRepository.Insert(game);
            }

            //Check when the user is authenticated if he/she is following this game
            bool currentUserIsFollowing = game.GameUsers != null && game.GameUsers.Any(x => x.UserId == userId && x.Following);

            return new GameDetailViewModel() {
                Id = game.Id,
                Name = game.Name,
                Cover = game.Cover.URL,
                FirstReleaseDate = game.FirstReleaseDate.ToShortDateString(),
                Rating = game.Rating,
                Genres = game.Genres.Select(g => g.Name),
                Platforms = game.GamePlatforms,
                Developers = game.GameCompanies.Where(c => c.Role == CompanyRole.Developer).Select(c => c.Company.Name),
                Publishers = game.GameCompanies.Where(c => c.Role == CompanyRole.Publisher).Select(c => c.Company.Name),
                Screenshots = game.Screenshots.Select(s => s.URL),
                Summary = game.Summary,
                CurrentUserIsFollowing = currentUserIsFollowing
            };
        }

        public async Task ToggleFollow(int gameId, string userId) {
            bool following = await _gameRepository.IsFollowing(gameId, userId);

            await _gameRepository.UpdateGameUserPair(new GameUser {
                GameId = gameId,
                UserId = userId,
                Following = !following
            });
        }
    }
}
