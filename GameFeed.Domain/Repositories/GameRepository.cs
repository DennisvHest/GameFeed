﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameFeed.Domain.Repositories;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.Repositories {

    public interface IGameRepository {

        bool GameExistsInDatabase(int id);
        Game GetGame(int id);
        void Insert(Game game);
    }

    public class GameRepository : IGameRepository {

        private readonly DatabaseContext context;

        public GameRepository(DatabaseContext context) {
            this.context = context;
        }

        public bool GameExistsInDatabase(int id) {
            return context.Games.Any(g => g.Id == id);
        }

        /// <summary>
        /// Retrieves the Game with the given ID from the database
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <returns>The Game</returns>
        public Game GetGame(int id) {
            return context.Games.FirstOrDefault(g => g.Id == id);
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
