using System.Threading.Tasks;
using System.Web.Mvc;
using FakeItEasy;
using GameFeed.Services;
using GameFeed.Services.ViewModels;
using GameFeed.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameFeed.Tests.Controller {

    [TestClass]
    public class TestGameController : TestController {

        private const string DefaultUserId = "123";

        private IGameService _fakeGameService;
        private GameDetailViewModel _game;

        [TestInitialize]
        public void Initialize() {
            _game = new GameDetailViewModel { Id = 1 };

            _fakeGameService = A.Fake<IGameService>();
        }

        [TestMethod]
        public async Task Detail_ShouldHaveGameDetailViewModel_WhenSuccess() {
            //Arrange
            SetupGameServiceSuccess();

            GameController target = new GameController(_fakeGameService) {
                ControllerContext = A.Fake<ControllerContext>()
            };

            SetupDefaultIdentity(target);

            //Act
            ViewResult result = (ViewResult) await target.Detail(1);

            //Assert
            Assert.AreEqual(result.Model, _game);
        }

        [TestMethod]
        public async Task ToggleFollow_ShouldReturnOK_WhenSuccess() {
            //Arrange
            SetupGameServiceSuccess();

            //Act
            GameController target = new GameController(_fakeGameService) {
                ControllerContext = A.Fake<ControllerContext>()
            };

            SetupDefaultIdentity(target);

            HttpStatusCodeResult result = (HttpStatusCodeResult) await target.ToggleFollow(1, DefaultUserId);

            Assert.IsTrue(result.StatusCode == 200);
        }

        [TestMethod]
        public async Task ToggleFollow_ShouldReturnUnauthorized_WhenRequestingForOtherUser() {
            //Arrange
            SetupGameServiceSuccess();

            //Act
            GameController target = new GameController(_fakeGameService) {
                ControllerContext = A.Fake<ControllerContext>()
            };

            SetupDefaultIdentity(target);

            HttpStatusCodeResult result = (HttpStatusCodeResult)await target.ToggleFollow(1, "321"); //Different userId than currently logged in user

            Assert.IsTrue(result.StatusCode == 401);
        }

        private void SetupGameServiceSuccess() {
            A.CallTo(() => _fakeGameService.Detail(A<int>.Ignored, A<string>.Ignored)).Returns(_game);
        }
    }
}
