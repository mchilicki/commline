using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.Infrastructure.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class SettingsController : Controller
    {
        readonly LineManager _lineManager;
        readonly SettingsManager _settingsManager;

        public SettingsController (
            SettingsManager settingsManager,
            LineManager lineManager)
        {
            _settingsManager = settingsManager;
            _lineManager = lineManager;
        }

        public ActionResult Index()
        {
            ViewBag.LinesIdsNames = GetAllLinesIdsAndNamesOnly();
            return View("Settings", _settingsManager.GetSettings());
        }

        [HttpPost]
        public JsonResult SaveSettings(CommlineSettings settings)
        {
            _settingsManager.SaveSettings(settings);
            return Json("");
        }

        [HttpPost]
        public ActionResult BackToDefault()
        {
            _settingsManager.BackToDefaultSettings();
            return Json("");
        }

        public IEnumerable<SelectListItem> GetAllLinesIdsAndNamesOnly()
        {
            return _lineManager
                .GetAllWithoutReturnLines()
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }
    }
}
