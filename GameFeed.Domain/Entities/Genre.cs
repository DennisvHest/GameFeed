using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Genre {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
