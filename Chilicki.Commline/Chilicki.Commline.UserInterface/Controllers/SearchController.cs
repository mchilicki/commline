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

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}