using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMC2016.Models.Tools;
using OMC2016.Filters;
using System.Transactions;

namespace OMC2016.Controllers.Tools
{
    public class AdministratorController : Controller
    {
        private ApplicationConfig model = new ApplicationConfig();
        // GET: Administrator
        public ActionResult Config()
        {
            Tools.ctlMaster.Instance.GetConnectionString(model);
            return View(model);
        }

        [HttpParamAction]
        public ActionResult Next(ApplicationConfig _Model)
        {
            TempData["message"] = "Comeback";

            return View("Config", _Model);
        }

        [HttpParamAction]
        public ActionResult Publish(ApplicationConfig _Model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Tools.ctlMaster.Instance.UpdateConnectionString(_Model);
                    scope.Complete();
                    ModelState.AddModelError("", "บันทึกเรียบร้อย");
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    //ModelState.AddModelError("", "บันทึกข้อมูลไม่สำเร็จ");
                    ModelState.AddModelError("", ex.ToString());
                }
            }            
            return View("Config", _Model);
        }
    }
}