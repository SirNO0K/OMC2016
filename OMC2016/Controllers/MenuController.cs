using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMC2016.Models.Tools;
using System.Threading.Tasks;
using System.Collections;

namespace OMC2016.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            using (MenubarDAL DB_Menu = new MenubarDAL())
            {
                return PartialView("_Menubar", DB_Menu.MenuBarItems.ToList());
            }           
        }
    }
}