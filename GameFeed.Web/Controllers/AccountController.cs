using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameFeed.Web.App_Start;
using GameFeed.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GameFeed.Web.Controllers {

    public class AccountController : Controller {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel login, string returnUrl) {
            if (!ModelState.IsValid) {
                TempData["model-errors"] = ModelState.Values;
                return Redirect(returnUrl);
            }

            SignInStatus result = await SignInManager.PasswordSignInAsync(login.Username,
                login.Password, true, false);

            if (result != SignInStatus.Success) {
                TempData["signin-success"] = false;
            }

            return Redirect(returnUrl);
        }
    }
}