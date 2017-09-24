using System;
using System.Collections.Generic;
using GameFeed.Domain.Entities;

namespace GameFeed.Services.ViewModels {

    public class GameDetailViewModel {

        public string Name { get; set; }
        public string Summary { get; set; }
        public string FirstReleaseDate { get; set; }
        public IEnumerable<string> Screenshots { get; set; }
        public string Cover { get; set; }
    }
}
