﻿using Chilicki.Commline.Application.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

        public JsonResult GetAllLines()
        {
            return Json(_lineManager.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStops()
        {
            return Json(_stopManager.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}