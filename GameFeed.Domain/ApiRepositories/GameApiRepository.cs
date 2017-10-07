using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Entities;
using GameFeed.Domain.ObjectMappers;

namespace GameFeed.Domain.ApiRepositories {

    public interface IGameApiRepository {
        Game GetGame(int id);
    }

    public class GameApiRepository : IGameApiRepository {

        private readonly IPlatformApiRepository _platformApiRepository;
        private readonly IApiClient _apiClient;

        public GameApiRepository(IApiClient apiClient, IPlatformApiRepository platformApiRepository) {
            _platformApiRepository = platformApiRepository;
            _apiClient = apiClient;
        }

        public Game GetGame(int id) {
            ApiGame apiGame = _apiClient.Get<ApiGame>($"games/{id}?fields=id,name,summary,first_release_date,screenshots.url,cover.url,release_dates,release_dates,aggregated_rating&expand=game,genres,developers,publishers");

            //Get the platforms this game is on
            apiGame.GamePlatforms = apiGame.GamePlatforms
                .GroupBy(x => x.PlatformId) //Group by platform
                .Select(x => x.Aggregate((p1, p2) => p1.ReleaseDate < p2.ReleaseDate ? p1 : p2));//Takes the platform with the minimum release date

            IEnumerable<int> platformIds = apiGame.GamePlatforms.Select(x => x.PlatformId);

            apiGame.Platforms = _platformApiRepository.GetPlatforms(platformIds);

            return ApiGameMapper.MapGame(apiGame);
        }
    }
}
