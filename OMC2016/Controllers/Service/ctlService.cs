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
            return await Task<IList>.Run(() =>
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
                            ISEXP = _MIX.isexpire,
                            CODE = _ARFILE.AR_CODE,
                            NAME = _ARFILE.AR_NAME,
                            TYPE = _MIX.type,
                            SN = _MIX.s_no,
                            SALE_DATE = _MIX.sale_date.Value.Date,
                            EXP_DATE = _MIX.exp.Value.Date,
                            ISTRANSFER = _MIX.istransfer,
                            REMARK = _MIX.remark
                        }).ToList();
            });
        }
    }
}