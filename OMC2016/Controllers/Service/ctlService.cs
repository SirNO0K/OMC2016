using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OMC2016.Models.Services;
using OMC2016.Models.Customer;
using System.Data;

namespace OMC2016.Controllers.Service
{
    public static class ctlService
    {
        public static IList GetMachineList()
        {
            ServiceDAL DB_Machine = new ServiceDAL();
            CustomerDAL DB_Customer = new CustomerDAL();

            var _Machine = DB_Machine.MIXes.ToList();
            var _Customer = DB_Customer.ARFILEs.ToList();

            return (from _MIX in _Machine
                    join _ARFILE in _Customer on _MIX.acccustcode equals _ARFILE.AR_CODE
                    orderby _MIX.isexpire ascending, _MIX.exp ascending
                    where _MIX.isdelete == false
                    select new MachineList
                    {
                        MIXID = _MIX.mix_id,
                        ISEXP = ((_MIX.isexpire) ? "OUT" : "IN"),
                        CODE = _ARFILE.AR_CODE,
                        NAME = _ARFILE.AR_NAME,
                        TYPE = _MIX.type,
                        SN = _MIX.s_no,
                        SALE_DATE = _MIX.sale_date.Value.Date.ToShortDateString(),
                        EXP_DATE = _MIX.exp.Value.Date.ToShortDateString(),
                        ISTRANSFER = ((_MIX.istransfer) ? "Y" : "N"),
                        REMARK = _MIX.remark
                    }).AsParallel().ToList();
        }
        public static void UpdateWarrantyExpire()
        {
            ServiceDAL DB_Machine = new ServiceDAL();
            DB_Machine.MIXes.Where(x => x.isexpire.Equals(false)).ToList()
                            .ForEach(c => { c.isexpire = c.exp < DateTime.Today; });
            DB_Machine.SaveChanges();
        }
        public static IList GetJobList(string CusCode, string Model, string SN)
        {
            ServiceDAL DB_Service = new ServiceDAL();
            var Result = (from _Job in DB_Service.ORDERMAINTENANCEs.AsEnumerable()
                          where _Job.acccustcode == CusCode.ToString()
                             && _Job.type == Model.ToString()
                             && _Job.s_no == SN.ToString()
                          orderby _Job.status descending, _Job.orderdate descending
                          select new
                          {
                              ID = _Job.orderid,
                              CODE = _Job.orderCode,
                              STATUS = _Job.status.Value == 1 ? "Active" : "Closed",
                              OrderNO = _Job.orderCode + "-" + _Job.s_order,
                              OrderDATE = _Job.orderdate.Value.ToShortDateString()
                          }).AsParallel().ToList();
            return Result;
        }
        public static IList GetJobInfo() { return null; }
        public static IList GetJobDetail() { return null; }
        public static IList GetSparepart() { return null; }
    }
}