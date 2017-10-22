using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameFeed.Services;
using GameFeed.Services.ViewModels;
using Microsoft.AspNet.Identity;

namespace GameFeed.Web.Controllers {

    public class GameController : Controller {

        private readonly IGameService _gameService;

        public GameController(IGameService gameService) {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult Detail(int id) {
            GameDetailViewModel viewModel = User.Identity.IsAuthenticated
                ? _gameService.Detail(id, User.Identity.GetUserId())
                : _gameService.Detail(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ToggleFollow(int gameId, string userId) {
            if (User.Identity.GetUserId() != userId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            await _gameService.ToggleFollow(gameId, userId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}