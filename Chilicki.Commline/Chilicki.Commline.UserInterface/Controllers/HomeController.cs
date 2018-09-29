using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly RouteStopManager _routeStopManager;
        readonly DepartureManager _departureManager;

        public HomeController(LineManager lineManager, StopManager stopManager, 
            RouteStopManager routeStopManager, DepartureManager departureManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _routeStopManager = routeStopManager;
            _departureManager = departureManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var line = _lineManager.GetById(1);
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetAllStops()
        {
            return Json(_stopManager.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}