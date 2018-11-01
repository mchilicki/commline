using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class EditorController : Controller
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly RouteStopManager _routeStopManager;
        readonly DepartureManager _departureManager;

        public EditorController(LineManager lineManager, StopManager stopManager,
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

        public ActionResult Editor()
        {
            return Index();
        }

        [HttpPost]
        public ActionResult Departures()
        {
            var choosenLine = Request.Form["lineDropdown"];
            if (choosenLine == null)
                return RedirectToAction("Index", "Home");
            long lineId = long.Parse(choosenLine);
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            var lineDepartures = _lineManager.GetDeparturesForLine(lineId);
            ViewData["LineDepartures"] = JsonConvert.SerializeObject(lineDepartures);
            return View("Departures", lineDepartures);
        }

        public JsonResult GetAllLines()
        {
            return Json(_lineManager.GetEverything(), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult SaveStops(StopsEditionModel stopsEditionModel)
        {
            string errorMessage = "";
            try
            {
                if (stopsEditionModel.Added != null)
                    _stopManager.Create(stopsEditionModel.Added);
                //if (stopsEditionModel.Modified != null)
                    //_stopManager.Edit(stopsEditionModel.Modified);
                //if (stopsEditionModel.Deleted != null)
                    //_stopManager.Delete(stopsEditionModel.Deleted);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return Json(errorMessage);
        }

        [HttpPost]
        public JsonResult SaveLines(LinesEditionModel linesEditionModel)
        {
            string errorMessage = "";
            try
            {
                if (linesEditionModel.Added != null)
                    _lineManager.Create(linesEditionModel.Added);
                //if (stopsEditionModel.Modified != null)
                //_stopManager.Edit(stopsEditionModel.Modified);
                //if (stopsEditionModel.Deleted != null)
                //_stopManager.Delete(stopsEditionModel.Deleted);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return Json(errorMessage);
        }

        [HttpPost]
        public JsonResult SaveDepartures(LineDeparturesDTO lineDepartures)
        {
            string errorMessage = "";
            try
            {
                _departureManager.ChangeLineDepartures(lineDepartures);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return Json(errorMessage);
        }

        [HttpPost]
        public JsonResult GetStop(long id)
        {
            StopDTO stop = null;
            try
            {
                stop = _stopManager.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return Json(stop, JsonRequestBehavior.AllowGet);
        }
    }
}