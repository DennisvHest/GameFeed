﻿using System.Threading.Tasks;
using System.Web.Mvc;
using GameFeed.Services;
using GameFeed.Services.ViewModels;
using Microsoft.AspNet.Identity;

namespace GameFeed.Web.Controllers {

    public class HomeController : Controller {

        private readonly IFeedService _feedService;

        public HomeController(IFeedService feedService) {
            _feedService = feedService;
        }

        public async Task<ActionResult> Index() {
            HomeViewModel model = User.Identity.IsAuthenticated
                ? await _feedService.Home(User.Identity.GetUserId())
                : await _feedService.Home();

            return View(model);
        }
    }
}