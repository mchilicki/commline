using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chilicki.Commline.UserInterface.Helpers.ControllerActivators
{
    /**
     * Controler Activator for forcing english language.
     * The problem without this Activator was that in polish/english languages there are different
     * methods of writing number seperator (comma/dot). This Activator is forcing the dot format.
    **/
    public class CultureAwareControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            string language = "en"; 
            CultureInfo culture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}
