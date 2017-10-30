using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.Repositories {

    public interface IGameRepository {

        Task<bool> GameExistsInDatabase(int id);
        Task<Game> GetGame(int id);
        IEnumerable<Game> GetGamesByIds(IEnumerable<int> gameIds);
        Task<Game> AddIfNotExist(int id);
        void Insert(Game game);
        Task UpdateGameUserPair(GameUser gameUserPair);
        IEnumerable<Game> GetFollowingGamesFromUser(string userId);
        Task<bool> IsFollowing(int gameId, string userId);
    }

    public class GameRepository : IGameRepository {

        private readonly DatabaseContext context;

        private readonly IGameApiRepository _gameApiRepository;

        public GameRepository(DatabaseContext context, IGameApiRepository gameApiRepository) {
            this.context = context;

            _gameApiRepository = gameApiRepository;
        }

        public async Task<bool> GameExistsInDatabase(int id) {
            return await context.Games.AnyAsync(g => g.Id == id);
        }

        /// <summary>
        /// Retrieves the Game with the given ID from the database
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The Game</returns>
        public async Task<Game> GetGame(int id) {
            return await context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public IEnumerable<Game> GetGamesByIds(IEnumerable<int> gameIds) {
            return context.Games.Where(g => gameIds.Contains(g.Id));
        }

        public async Task<bool> IsFollowing(int gameId, string userId) {
            return await context.GameUsers.AnyAsync(x => x.GameId == gameId && x.UserId == userId && x.Following);
        }

        public async Task UpdateGameUserPair(GameUser gameUserPair) {
            //If there isn't already a gameuser entry, add one
            if (context.GameUsers.Any(x => x.GameId == gameUserPair.GameId && x.UserId == gameUserPair.UserId)) {
                context.Entry(gameUserPair).State = EntityState.Modified;
            } else {
                context.GameUsers.Add(gameUserPair);
            }

            await context.SaveChangesAsync();
        }

        public IEnumerable<Game> GetFollowingGamesFromUser(string userId) {
            return context.GameUsers.Where(gu => gu.UserId == userId && gu.Following).Select(gu => gu.Game);
        }

        /// <summary>
        /// Retrieves the game with the given id from the IGDB API if it is not currently in the database
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The new or already existing game</returns>
        public async Task<Game> AddIfNotExist(int id) {
            Game game;

            if (await GameExistsInDatabase(id)) {
                game = await GetGame(id);
            } else {
                game = _gameApiRepository.GetGame(id);
                Insert(game);
            }

            return game;
        }

        /// <summary>
        /// Inserts the given game into the database
        /// </summary>
        /// <param name="game">Game to be inserted into the database</param>
        public void Insert(Game game) {
            //Don't add already existing entries into the database
            AttachExistingGenres(game.Genres);
            AttachExistingPlatforms(game.GamePlatforms.Select(x => x.Platform));
            AddNotAlreadyExistingCompanies(game.GameCompanies);

            context.Games.Add(game);
            context.SaveChanges();
        }

        private void AttachExistingGenres(IEnumerable<Genre> genres) {
            DbSet<Genre> existingGenres = context.Genres;

            foreach (Genre genre in genres) {
                if (existingGenres.Any(g => g.Id == genre.Id)) {
                    context.Genres.Attach(genre);
                }
            }
        }

        private void AttachExistingPlatforms(IEnumerable<Platform> platforms) {
            DbSet<Platform> existingPlatforms = context.Platforms;

            foreach (Platform platform in platforms) {
                if (existingPlatforms.Any(p => p.Id == platform.Id)) {
                    context.Platforms.Attach(platform);
                }
            }
        }

        private void AddNotAlreadyExistingCompanies(IEnumerable<GameCompany> gameCompanies) {
            IList<GameCompany> existingGameCompanies = context.GameCompanies.ToList();

            foreach (GameCompany gameCompany in gameCompanies) {
                if (existingGameCompanies.Any(x => x.Company.Id == gameCompany.Company.Id)) {
                    gameCompany.Company = null;
                } else {
                    existingGameCompanies.Add(gameCompany);
                }
            }
        }
    }
}
