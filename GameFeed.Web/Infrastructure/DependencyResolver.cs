using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameFeed.Domain.Concrete;
using Ninject;

namespace GameFeed.Web.Infrastructure {

    public class DependencyResolver : IDependencyResolver {

        private IKernel kernel;

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {
            kernel.Bind<DatabaseContext>().To<DatabaseContext>();
        }
    }
}