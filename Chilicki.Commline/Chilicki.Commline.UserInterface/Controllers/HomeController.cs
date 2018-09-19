using Chilicki.Commline.Application.Managers;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        StopManager _stopManager;
        LineManager _lineManager;

        public HomeController(StopManager stopManager, LineManager lineManager)
        {
            _stopManager = stopManager;
            _lineManager = lineManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var line = _lineManager.GetById(1);
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}