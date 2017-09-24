using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Game {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime FirstReleaseDate { get; set; }
        public IEnumerable<Image> Screenshots { get; set; }
        public Image Cover { get; set; }
    }
}
