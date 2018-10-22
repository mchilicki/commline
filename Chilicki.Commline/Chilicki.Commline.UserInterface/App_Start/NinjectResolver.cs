using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories;
using Chilicki.Commline.UserInterface.Controllers;
using Ninject;
using Ninject.Web.Common;
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
            _kernel.Bind<CommlineDBContext>().ToSelf().InRequestScope();

            _kernel.Bind<StopRepository>().ToSelf().InRequestScope();
            _kernel.Bind<LineRepository>().ToSelf().InRequestScope();
            _kernel.Bind<DepartureRepository>().ToSelf().InRequestScope();
            _kernel.Bind<RouteStopRepository>().ToSelf().InRequestScope();
            _kernel.Bind<MixedRepository>().ToSelf().InRequestScope();

            _kernel.Bind<StopManager>().ToSelf();
            _kernel.Bind<LineManager>().ToSelf();
            _kernel.Bind<DepartureManager>().ToSelf();
            _kernel.Bind<RouteStopManager>().ToSelf();

            _kernel.Bind<LineValidator>().ToSelf();

            _kernel.Bind<HomeController>().ToSelf();
            _kernel.Bind<EditorController>().ToSelf();
        }
    }
}