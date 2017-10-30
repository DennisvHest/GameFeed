using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameFeed.Common.Exceptions;
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
        public async Task<ActionResult> Detail(int id) {
            GameDetailViewModel viewModel;

            try {
                viewModel = User.Identity.IsAuthenticated
                    ? await _gameService.Detail(id, User.Identity.GetUserId())
                    : await _gameService.Detail(id);
            } catch (GameDoesNotExistException) {
                //If a game with the given id is not found, return 404
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ToggleFollow(int gameId, string userId) {
            //Avoid users being able to have other users follow a game
            if (User.Identity.GetUserId() != userId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            await _gameService.ToggleFollow(gameId, userId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}