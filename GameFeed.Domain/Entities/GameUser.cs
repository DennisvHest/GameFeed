using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class GameUser {

        [Key, Column(Order = 0)]
        public int GameId { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public bool Following { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
