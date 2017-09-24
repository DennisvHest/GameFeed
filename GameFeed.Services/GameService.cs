using System.Linq;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Repositories;
using GameFeed.Services.ViewModels;

namespace GameFeed.Services {

    public interface IGameService {

        bool GameAlreadyInDatabase(int id);
        GameDetailViewModel Detail(int id);
    }

    public class GameService : IGameService {

        private IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository) {
            this.gameRepository = gameRepository;
        }
        /// <summary>
        /// Checks if the Game with the given ID already exists in the database
        /// </summary>
        /// <param name="id">ID of the Game to check for</param>
        /// <returns>True = Game already exists, False = Game does not exist</returns>
        public bool GameAlreadyInDatabase(int id) {
            return gameRepository.GameAlreadyInDatabase(id);
        }

        /// <summary>
        /// Creates the ViewModel for the game detail page
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The GameDetailViewModel</returns>
        public GameDetailViewModel Detail(int id) {
            Game game = gameRepository.GetGame(id);

            GameDetailViewModel viewModel = new GameDetailViewModel() {
                Name = game.Name,
                Cover = game.Cover.URL,
                FirstReleaseDate = game.FirstReleaseDate.ToShortDateString(),
                Screenshots = game.Screenshots.Select(s => s.URL),
                Summary = game.Summary
            };

            return viewModel;
        }
    }
}
