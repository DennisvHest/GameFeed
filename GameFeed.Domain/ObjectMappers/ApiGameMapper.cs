using System;
using System.Collections.Generic;
using System.Linq;
using GameFeed.Common;
using GameFeed.Common.Enums;
using GameFeed.Common.Helpers;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.ObjectMappers {
    public static class ApiGameMapper {

        /// <summary>
        /// Maps an ApiGame to a Game (Entity) object
        /// </summary>
        /// <param name="apiGame">ApiGame to be mapped</param>
        /// <returns>Game object</returns>
        public static Game MapGame(ApiGame apiGame) {
            //Platforms
            IList<GamePlatform> gamePlatforms = new List<GamePlatform>();
            foreach (ApiGamePlatform gamePlatform in apiGame.GamePlatforms) {
                gamePlatforms.Add(new GamePlatform() {
                    GameId = apiGame.ID,
                    PlatformId = gamePlatform.PlatformId,
                    Platform = apiGame.Platforms.FirstOrDefault(p => p.Id == gamePlatform.PlatformId),
                    ReleaseDate = Constants.UnixEpoch.AddMilliseconds(gamePlatform.ReleaseDate)
                });
            }

            //Companies
            IList<GameCompany> gameCompanies = new List<GameCompany>();
            foreach (Company company in apiGame.Developers) {
                gameCompanies.Add(new GameCompany() {
                    GameId = apiGame.ID,
                    CompanyId = company.Id,
                    Company = company,
                    Role = CompanyRole.Developer
                });
            }

            foreach (Company company in apiGame.Publishers) {
                gameCompanies.Add(new GameCompany() {
                    GameId = apiGame.ID,
                    CompanyId = company.Id,
                    Company = company,
                    Role = CompanyRole.Publisher
                });
            }

            return new Game() {
                Id = apiGame.ID,
                Name = apiGame.Name,
                Cover = apiGame.Cover != null ? new Image { Id = apiGame.Cover.Id } : null,
                FirstReleaseDate = Constants.UnixEpoch.AddMilliseconds(apiGame.FirstReleaseDate),
                Rating = apiGame.AggregatedRating,
                Genres = apiGame.Genres.ToArray(),
                Screenshots = apiGame.Screenshots.Select(s => new Image { Id = s.Id }).ToList(),
                Summary = apiGame.Summary,
                GamePlatforms = gamePlatforms,
                GameCompanies = gameCompanies
            };
        }
    }
}
