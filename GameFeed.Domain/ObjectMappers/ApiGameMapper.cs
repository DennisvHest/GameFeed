﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameFeed.Common;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.ObjectMappers {
    public static class ApiGameMapper {

        public static Game MapGame(ApiGame apiGame) {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            //Screenshots
            List<Image> screenshots = new List<Image>();

            foreach (Image screenshot in apiGame.Screenshots) {
                screenshots.Add(new Image() {
                    URL = ImageHelper.GetFullSizedImageUrl(screenshot.URL)
                });
            }

            //Game cover
            Image cover = apiGame.Cover != null ? new Image() {
                URL = ImageHelper.GetFullSizedImageUrl(apiGame.Cover.URL)
            } : null;

            //Platforms
            List<GamePlatform> gamePlatforms = new List<GamePlatform>();
            foreach (ApiGamePlatform gamePlatform in apiGame.GamePlatforms) {
                gamePlatforms.Add(new GamePlatform() {
                    GameId = apiGame.ID,
                    PlatformId = gamePlatform.PlatformId,
                    Platform = apiGame.Platforms.FirstOrDefault(p => p.Id == gamePlatform.PlatformId),
                    ReleaseDate = unixEpoch.AddMilliseconds(gamePlatform.ReleaseDate)
                });
            }

            return new Game() {
                Id = apiGame.ID,
                Name = apiGame.Name,
                Cover = cover,
                FirstReleaseDate = unixEpoch.AddMilliseconds(apiGame.FirstReleaseDate),
                Rating = apiGame.AggregatedRating,
                Genres = apiGame.Genres.ToArray(),
                Screenshots = screenshots,
                Summary = apiGame.Summary,
                GamePlatforms = gamePlatforms
            };
        }
    }
}
