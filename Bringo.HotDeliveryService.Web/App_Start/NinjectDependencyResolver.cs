using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace Bringo.HotDeliveryService.Web.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}