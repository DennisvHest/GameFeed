using System.Linq;
using GameFeed.Domain.Concrete;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.Repositories {

    public interface IGameRepository {

        bool GameAlreadyInDatabase(int id);
        Game GetGame(int id);
    }

    public class GameRepository : IGameRepository {

        private readonly DatabaseContext context;

        public GameRepository(DatabaseContext context) {
            this.context = context;
        }

        public bool GameAlreadyInDatabase(int id) {
            return context.Games.Any(g => g.ID == id);
        }

        /// <summary>
        /// Retrieves the Game with the given ID from the database
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The Game</returns>
        public Game GetGame(int id) {
            return context.Games.FirstOrDefault(g => g.ID == id);
        }
    }
}
