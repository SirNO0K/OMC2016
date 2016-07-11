using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using OMC2016.Controllers.Customer;
using System.Dynamic;
using OMC2016.Models.Customer;

namespace OMC2016.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View(ctlCustomer.CustomerList().Result);
            });
        }

        public async Task<JsonResult> GetCustomerGroup()
        {
            return await Task.Run(() =>
            {
                return Json(ctlCustomer.CustomerGroup().Result, JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult Detail(string id)
        {
            string ADDBKEY = ctlCustomer.GetADDBKEY(id);

            dynamic myModel = new ExpandoObject();
            myModel.CustomerInfo = ctlCustomer.CustomerInfo(id);
            myModel.ContactList = ctlCustomer.ContactInfo(ADDBKEY);

            return View(myModel);
        }
    }
}