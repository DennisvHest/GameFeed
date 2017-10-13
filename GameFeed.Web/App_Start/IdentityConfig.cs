using System.Web;
using GameFeed.Domain.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace GameFeed.Web.App_Start {

    public class IdentityConfig {

        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(() => new DatabaseContext());
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login")
            });
        }
    }
}