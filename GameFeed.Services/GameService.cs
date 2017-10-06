using System.Linq;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Repositories;
using GameFeed.Services.ViewModels;

namespace GameFeed.Services {

    public interface IGameService {

        bool GameExistsInDatabase(int id);
        GameDetailViewModel Detail(int id);
    }

    public class GameService : IGameService {

        private IGameRepository gameRepository;
        private IGameApiRepository gameApiRepository;

        public GameService(IGameRepository gameRepository, IGameApiRepository gameApiRepository) {
            this.gameRepository = gameRepository;
            this.gameApiRepository = gameApiRepository;
        }

        /// <summary>
        /// Checks if the Game with the given ID already exists in the database
        /// </summary>
        /// <param name="id">ID of the Game to check for</param>
        /// <returns>True = Game already exists, False = Game does not exist</returns>
        public bool GameExistsInDatabase(int id) {
            return gameRepository.GameExistsInDatabase(id);
        }

        /// <summary>
        /// Creates the ViewModel for the game detail page
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The GameDetailViewModel</returns>
        public GameDetailViewModel Detail(int id) {
            Game game;

            //If the game doesn't already exist in the database, get the game from the IGDB API
            if (GameExistsInDatabase(id)) {
                game = gameRepository.GetGame(id);
            } else {
                game = gameApiRepository.GetGame(id);
                gameRepository.InsertGame(game);
            }

            GameDetailViewModel viewModel = new GameDetailViewModel() {
                Name = game.Name,
                Cover = game.Cover.URL,
                FirstReleaseDate = game.FirstReleaseDate.ToShortDateString(),
                Genres = game.Genres.Select(g => g.Name),
                Platforms = game.GamePlatforms.Select(x => x.Platform.Name),
                Screenshots = game.Screenshots.Select(s => s.URL),
                Summary = game.Summary
            };

            return viewModel;
        }
    }
}
