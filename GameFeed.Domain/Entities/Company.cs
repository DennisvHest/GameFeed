using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFeed.Domain.Entities {

    public class Company {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GameCompany> GameCompanies { get; set; }
    }
}
