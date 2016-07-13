using System.Threading.Tasks;
using System.Web.Mvc;

namespace OMC2016.Controllers.Service
{
    public class ServiceController : Controller
    {
        // GET: Service
        public async Task<ActionResult> MachineSearch()
        {
            return await Task.Run(() =>
            {
                return View(ctlService.GetMachineList().Result);
            });

        }
    }
}