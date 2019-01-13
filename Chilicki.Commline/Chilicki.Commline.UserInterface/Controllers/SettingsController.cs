using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.UserInterface.Controllers
{
    [Authorize]
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
