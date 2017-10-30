using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameFeed.Domain.ApiRepositories;
using GameFeed.Domain.Repositories;
using GameFeed.Services;
using Ninject;

namespace GameFeed.Web.Infrastructure {

    public class DependencyResolver : IDependencyResolver {

        private IKernel kernel;

        public DependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {
            kernel.Bind<DatabaseContext>().To<DatabaseContext>();

            kernel.Bind<IApiClient>().To<ApiClient>();
            kernel.Bind<IGameApiRepository>().To<GameApiRepository>();
            kernel.Bind<IFeedApiRepository>().To<FeedApiRepository>();
            kernel.Bind<IPlatformApiRepository>().To<PlatformApiRepository>();

            kernel.Bind<IGameRepository>().To<GameRepository>();

            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IFeedService>().To<FeedService>();
        }
    }
}