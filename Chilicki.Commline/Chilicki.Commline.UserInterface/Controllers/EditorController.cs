using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.UserInterface.Resources;
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
        readonly DepartureManager _departureManager;
        readonly EditorManager _editorManager;

        public EditorController(
            LineManager lineManager, 
            StopManager stopManager,
            DepartureManager departureManager,
            EditorManager editorManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _departureManager = departureManager;
            _editorManager = editorManager;
        }

        public ActionResult Stops()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("StopEditor");
        }

        public ActionResult Lines()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("LineEditor");
        }

        public ActionResult Departures(long lineId)
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            var lineDepartures = _lineManager.GetDeparturesForLine(lineId);
            ViewData["LineDepartures"] = JsonConvert.SerializeObject(lineDepartures);
            return View("Departures", lineDepartures);
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager 
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }

        [HttpPost]
        public JsonResult SaveStops(StopsEditionModel stopsEditionModel)
        {
            try
            {
                _editorManager.EditStops(stopsEditionModel);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            return Json(new { success = EditorResources.SuccessfullySavedStops } );
        }

        [HttpPost]
        public JsonResult SaveLines(LinesEditionModel linesEditionModel)
        {
            try
            {
                _editorManager.EditLines(linesEditionModel);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            return Json(new { success = EditorResources.SuccessfullySavedLines });
        }

        [HttpPost]
        public JsonResult SaveDepartures(LineDeparturesDTO lineDepartures)
        {
            try
            {
                _departureManager.ChangeLineDepartures(lineDepartures);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            return Json(new { success = EditorResources.SuccessfullySavedDepartures });
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
                return Json(new { error = ex.Message });
            }
            return Json(stop, JsonRequestBehavior.AllowGet);
        }
    }
}