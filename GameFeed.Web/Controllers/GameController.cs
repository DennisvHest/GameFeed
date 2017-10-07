using System.Web.Mvc;
using GameFeed.Services;
using GameFeed.Services.ViewModels;

namespace GameFeed.Web.Controllers {

    public class GameController : Controller {

        private IGameService gameService;

        public GameController(IGameService gameService) {
            this.gameService = gameService;
        }

        public ActionResult Detail(int id) {
            GameDetailViewModel viewModel = gameService.Detail(id);
            return View(viewModel);
        }
    }
}