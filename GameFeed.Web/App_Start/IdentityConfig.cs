﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameFeed.Domain.Entities;
using GameFeed.Domain.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace GameFeed.Web.App_Start {

    public class ApplicationUserStore : UserStore<User> {

        public ApplicationUserStore(DatabaseContext context)
            : base(context) {
        }
    }

    public class ApplicationUserManager : UserManager<User> {

        public ApplicationUserManager(IUserStore<User> store)
            : base(store) {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) {
            var manager = new ApplicationUserManager(new UserStore<User>(context.Get<DatabaseContext>()));
            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<User, string> {

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}