using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View();
        }

        public JsonResult GetAllLines()
        {
            return Json(_lineManager.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStops()
        {
            return Json(_stopManager.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager.GetAll()
                .OrderBy(p => p.Name)
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}