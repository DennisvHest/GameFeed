using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Platform {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
