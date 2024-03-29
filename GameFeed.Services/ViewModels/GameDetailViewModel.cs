﻿using System.Collections.Generic;
using GameFeed.Domain.Entities;

namespace GameFeed.Services.ViewModels {

    public class GameDetailViewModel {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string FirstReleaseDate { get; set; }
        public float Rating { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<GamePlatform> Platforms { get; set; }
        public IEnumerable<string> Screenshots { get; set; }
        public IEnumerable<string> Developers { get; set; }
        public IEnumerable<string> Publishers { get; set; }
        public string Cover { get; set; }
        public bool CurrentUserIsFollowing { get; set; }
    }
}
