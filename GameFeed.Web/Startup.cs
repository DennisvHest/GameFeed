using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GameFeed.Web.Startup))]

namespace GameFeed.Web {

    public partial class Startup {

        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
