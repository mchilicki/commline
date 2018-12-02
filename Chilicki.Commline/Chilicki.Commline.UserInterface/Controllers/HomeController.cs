using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly DepartureManager _departureManager;
        readonly SettingsManager _settingsManager;

        public HomeController(
            LineManager lineManager, 
            StopManager stopManager, 
            DepartureManager departureManager,
            SettingsManager settingsManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _departureManager = departureManager;
            _settingsManager = settingsManager;
        }

        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            TempData["settings"] = _settingsManager.GetSettings();
            return View("Home");
        }

        public JsonResult GetAllLines()
        {
            return Json(_lineManager.GetEverything(), JsonRequestBehavior.AllowGet);
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