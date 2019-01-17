using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.UserInterface.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.UserInterface.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly DepartureManager _departureManager;
        readonly EditorManager _editorManager;
        readonly SettingsManager _settingsManager;

        public EditorController(
            LineManager lineManager, 
            StopManager stopManager,
            DepartureManager departureManager,
            EditorManager editorManager,
            SettingsManager settingsManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _departureManager = departureManager;
            _editorManager = editorManager;
            _settingsManager = settingsManager;
        }

        public ActionResult Stops()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            TempData["settings"] = _settingsManager.GetSettings();
            return View("StopEditor");
        }

        public ActionResult Lines()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            TempData["settings"] = _settingsManager.GetSettings();
            return View("LineEditor");
        }

        public ActionResult Departures(Guid lineId)
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            TempData["settings"] = _settingsManager.GetSettings();
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
        public JsonResult GetStop(Guid id)
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
            return Json(stop);
        }
    }
}