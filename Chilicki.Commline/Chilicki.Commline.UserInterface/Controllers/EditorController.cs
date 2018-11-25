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

        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("Editor");
        }

        public ActionResult Departures(long lineId)
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            var lineDepartures = _lineManager.GetDeparturesForLine(lineId);
            ViewData["LineDepartures"] = JsonConvert.SerializeObject(lineDepartures);
            return View("Departures", lineDepartures);
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
                if (linesEditionModel.Added != null)
                    _lineManager.Create(linesEditionModel.Added);
                //if (stopsEditionModel.Modified != null)
                //_stopManager.Edit(stopsEditionModel.Modified);
                //if (stopsEditionModel.Deleted != null)
                //_stopManager.Delete(stopsEditionModel.Deleted);
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

            }
            return Json(stop, JsonRequestBehavior.AllowGet);
        }
    }
}