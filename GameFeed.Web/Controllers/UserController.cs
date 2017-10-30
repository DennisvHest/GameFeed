using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameFeed.Web.App_Start;
using GameFeed.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GameFeed.Web.Controllers {

    public class UserController : Controller {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set {
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

        public ActionResult Logout() {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}