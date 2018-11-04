using Chilicki.Commline.Application.DTOs.Search;
using Chilicki.Commline.Application.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class SearchController : Controller
    {
        readonly SearchManager _searchManager;
        readonly LineManager _lineManager;

        public SearchController(
            SearchManager searchManager,
            LineManager lineManager)
        {
            _searchManager = searchManager;
            _lineManager = lineManager;
        }
        
        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("Search");
        }

        [HttpPost]
        public JsonResult SearchConnections(SearchInputDTO searchInput)
        {
            try
            {
                _searchManager.SearchFastestConnections(searchInput);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }            
            return Json(searchInput);
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}