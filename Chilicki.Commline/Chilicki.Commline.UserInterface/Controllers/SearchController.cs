using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class SearchController : Controller
    {
        readonly SearchManager _searchManager;
        readonly LineManager _lineManager;
        readonly SettingsManager _settingsManager;

        public SearchController(
            SearchManager searchManager,
            LineManager lineManager,
            SettingsManager settingsManager)
        {
            _searchManager = searchManager;
            _lineManager = lineManager;
            _settingsManager = settingsManager;
        }
        
        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            TempData["settings"] = _settingsManager.GetSettings();
            return View("Search");
        }

        [HttpPost]
        public JsonResult SearchConnections(SearchInputDTO searchInput)
        {
            FastestPathDTO fastestPath = null;
            try
            {
                fastestPath = _searchManager.SearchFastestConnections(searchInput);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }            
            return Json(fastestPath);
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}