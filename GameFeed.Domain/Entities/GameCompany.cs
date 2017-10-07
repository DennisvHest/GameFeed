using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameFeed.Common.Enums;

namespace GameFeed.Domain.Entities {

    public class GameCompany {

        [Key, Column(Order = 0)]
        public int GameId { get; set; }

        [Key, Column(Order = 1)]
        public int CompanyId { get; set; }

        public CompanyRole Role { get; set; }

        public virtual Game Game { get; set; }
        public virtual Company Company { get; set; }
    }
}
