using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Chilicki.Commline.UserInterface.App_Start
{
    public class NinjectResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<DbContext>().To<CommlineDBContext>();
            _kernel.Bind<LineRepository>().ToSelf();
            _kernel.Bind<DepartureRepository>().ToSelf();
            _kernel.Bind<RouteStationRepository>().ToSelf();
            _kernel.Bind<StopRepository>().ToSelf();
            _kernel.Bind<StopManager>().ToSelf();
        }
    }
}