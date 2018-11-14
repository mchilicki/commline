using Chilicki.Commline.Application.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly DepartureManager _departureManager;

        public HomeController(
            LineManager lineManager, 
            StopManager stopManager, 
            DepartureManager departureManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _departureManager = departureManager;
        }

        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("Home");
        }

        public JsonResult GetAllLines()
        {
            return Json(_lineManager.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStopsConnectedToAnyLine()
        {
            return Json(_stopManager.GetAllConnectedToAnyLine(), JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}