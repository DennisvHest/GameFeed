using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.ApiRepositories {

    public interface IPlatformApiRepository {

        IEnumerable<Platform> GetPlatforms(IEnumerable<int> platformIds);
    }

    public class PlatformApiRepository : IPlatformApiRepository {

        private readonly IApiClient _apiClient;

        public PlatformApiRepository(IApiClient apiClient) {
            _apiClient = apiClient;
        }

        public IEnumerable<Platform> GetPlatforms(IEnumerable<int> platformIds) {
            StringBuilder platformIdsString = new StringBuilder();
            platformIdsString.Append(platformIds.First());
            foreach (int platformId in platformIds.Skip(1)) {
                platformIdsString.Append($",{platformId}");
            }

            return _apiClient.GetMultiple<Platform>($"platforms/{platformIdsString}?fields=id,name");
        }
    }
}
