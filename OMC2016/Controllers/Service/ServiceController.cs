using System.Threading.Tasks;
using System.Web.Mvc;
using System.Dynamic;

namespace OMC2016.Controllers.Service
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult MachineDetail()
        {
            ctlService.UpdateWarrantyExpire();
            return View(ctlService.GetMachineList());
        }

        public JsonResult GetJobList(string CusCode, string Model, string SN)
        {
            return Json(ctlService.GetJobList(CusCode, Model, SN), JsonRequestBehavior.AllowGet);
        }
    }
}