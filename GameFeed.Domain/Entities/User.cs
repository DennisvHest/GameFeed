using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameFeed.Domain.Entities {

    public class User : IdentityUser {

        public virtual ICollection<GameUser> GameUsers { get; set; }
    }
}
