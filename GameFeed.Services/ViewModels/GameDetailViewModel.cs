using System.Collections.Generic;

namespace GameFeed.Services.ViewModels {

    public class GameDetailViewModel {

        public string Name { get; set; }
        public string Summary { get; set; }
        public string FirstReleaseDate { get; set; }
        public float Rating { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Platforms { get; set; }
        public IEnumerable<string> Screenshots { get; set; }
        public string Cover { get; set; }
    }
}
