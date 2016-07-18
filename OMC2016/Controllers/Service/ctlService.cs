using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OMC2016.Models.Services;
using OMC2016.Models.Customer;

namespace OMC2016.Controllers.Service
{
    public static class ctlService
    {
        public static async Task<IList> GetMachineList()
        {
            ServiceDAL DB_Machine = new ServiceDAL();
            CustomerDAL DB_Customer = new CustomerDAL();
            return await Task<IList>.Run(() =>
            {
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
                        }).ToList();
            });
        }

        public static async void UpdateWarrantyExpire()
        {
            ServiceDAL DB_Machine = new ServiceDAL();
            await Task.Run(() =>
            {
                DB_Machine.MIXes.Where(x => x.isexpire.Equals(false)).ToList()
                                .ForEach(c => { c.isexpire = c.exp < DateTime.Today; });
                DB_Machine.SaveChanges();
            });
        }
    }
}