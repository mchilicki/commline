using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories;
using Chilicki.Commline.UserInterface.Controllers;
using Ninject;
using System;
using System.Collections.Generic;

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
            _kernel.Bind<CommlineDBContext>().ToSelf().InSingletonScope();

            _kernel.Bind<StopRepository>().ToSelf().InSingletonScope();
            _kernel.Bind<LineRepository>().ToSelf().InSingletonScope();
            _kernel.Bind<DepartureRepository>().ToSelf().InSingletonScope();
            _kernel.Bind<RouteStopRepository>().ToSelf().InSingletonScope();
            _kernel.Bind<MixedRepository>().ToSelf().InSingletonScope();

            _kernel.Bind<StopManager>().ToSelf();
            _kernel.Bind<LineManager>().ToSelf();
            _kernel.Bind<DepartureManager>().ToSelf();
            _kernel.Bind<RouteStopManager>().ToSelf();

            _kernel.Bind<HomeController>().ToSelf();
            _kernel.Bind<EditorController>().ToSelf();
        }
    }
}