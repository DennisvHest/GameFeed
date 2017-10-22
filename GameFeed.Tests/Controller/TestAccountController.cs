using System.Threading.Tasks;
using System.Web.Mvc;
using FakeItEasy;
using GameFeed.Web.App_Start;
using GameFeed.Web.Controllers;
using GameFeed.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameFeed.Tests.Controller {

    [TestClass]
    public class TestAccountController : TestController {

        private LoginModel _validLogin;

        [TestInitialize]
        public void Initialize() {
            _validLogin = new LoginModel {
                Username = "test",
                Password = "test"
            };
        }

        [TestMethod]
        public async Task Login_ShouldHaveNoModelErrors_WhenValidLogin() {
            //Arrange
            AccountController target = new AccountController {
                ControllerContext = A.Fake<ControllerContext>(),
                SignInManager = A.Fake<ApplicationSignInManager>(),
                UserManager = A.Fake<ApplicationUserManager>()
            };

            SetupDefaultIdentity(target);

            A.CallTo(() => 
                target.SignInManager.PasswordSignInAsync(A<string>.Ignored, A<string>.Ignored, A<bool>.Ignored, A<bool>.Ignored))
                .Returns(SignInStatus.Success);

            string returnUrl = "return";

            //Act
            RedirectResult result = (RedirectResult)await target.Login(_validLogin, returnUrl);

            //Assert
            Assert.AreEqual(result.Url, returnUrl); //Redirected to the correct URL
            Assert.IsTrue(target.TempData["model-errors"] == null); //Has no model errors
            Assert.IsTrue(target.TempData["signin-success"] == null); //Sign in succeeded
        }

        [TestMethod]
        public async Task Login_ShouldHaveModelErrors_WhenModelIsNotValid() {
            //Arrange
            AccountController target = new AccountController {
                ControllerContext = A.Fake<ControllerContext>(),
                SignInManager = A.Fake<ApplicationSignInManager>(),
                UserManager = A.Fake<ApplicationUserManager>()
            };

            SetupDefaultIdentity(target);

            target.ModelState.AddModelError("test", "test");

            string returnUrl = "return";

            //Act
            RedirectResult result = (RedirectResult)await target.Login(_validLogin, returnUrl);

            //Assert
            Assert.AreEqual(result.Url, returnUrl); //Redirected to the correct URL
            Assert.IsTrue(target.TempData["model-errors"] != null); //Has model errors
            Assert.IsTrue(target.TempData["signin-success"] == null); //Sign in was not a success
        }
    }
}
