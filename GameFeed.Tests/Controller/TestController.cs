using System.Security.Claims;
using System.Security.Principal;
using FakeItEasy;

namespace GameFeed.Tests.Controller {
    public class TestController {

        protected const string DefaultUserId = "123";

        protected void SetupDefaultIdentity(System.Web.Mvc.Controller controller) {
            SetupIdentity(controller, DefaultUserId);
        }

        protected void SetupIdentity(System.Web.Mvc.Controller controller, string userId) {
            GenericIdentity identity = new GenericIdentity("test");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            IPrincipal fakePrincipal = A.Fake<IPrincipal>();
            A.CallTo(() => fakePrincipal.Identity).Returns(identity);

            A.CallTo(() => controller.ControllerContext.HttpContext.User).Returns(fakePrincipal);
        }
    }
}
