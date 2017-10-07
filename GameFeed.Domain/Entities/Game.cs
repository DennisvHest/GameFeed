using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Game {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime FirstReleaseDate { get; set; }
        public float Rating { get; set; }

        public virtual Image Cover { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Image> Screenshots { get; set; }
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; }

        public virtual ICollection<GameCompany> GameCompanies { get; set; }
    }
}
