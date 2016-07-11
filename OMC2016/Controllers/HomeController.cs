﻿using OMC2016.Filters;
using System.Web.Mvc;

namespace OMC2016.Controllers
{
    [AuthActionFilter]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}