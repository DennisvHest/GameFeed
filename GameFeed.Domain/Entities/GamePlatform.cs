using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class GamePlatform {

        [Key, Column(Order = 0)]
        public int GameId { get; set; }

        [Key, Column(Order = 1)]
        public int PlatformId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual Game Game { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
