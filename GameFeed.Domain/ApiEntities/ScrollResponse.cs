using System.Collections.Generic;
using GameFeed.Domain.Models;

namespace GameFeed.Domain.ApiEntities {

    public class ScrollResponse {

        public IEnumerable<IScrollable> Scrollables { get; set; }
        public string ScrollUrl { get; set; }
        public int? PageCount { get; set; }
    }
}
