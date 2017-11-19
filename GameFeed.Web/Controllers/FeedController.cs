using System.Threading.Tasks;
using System.Web.Mvc;
using GameFeed.Services;

namespace GameFeed.Web.Controllers {

    [Authorize]
    public class FeedController : Controller {

        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService) {
            _feedService = feedService;
        }

        [HttpPost]
        public async Task<ActionResult> ScrollFeed(string scrollUrl) {
            return PartialView("_FeedScrollResult", await _feedService.ScrollFeed(scrollUrl));
        }
    }
}