using OMC2016.Models.Customer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OMC2016.Controllers.Customer
{
    public static class ctlCustomer
    {
        public static async Task<IList> CustomerList()
        {
            return await Task<IList>.Run(() =>
            {
                using (CustomerDAL DB_Customer = new CustomerDAL())
                {
                    return (from _ADDRBOOK in DB_Customer.ADDRBOOKs
                            join _ARADDRESS in DB_Customer.ARADDRESSes on _ADDRBOOK.ADDB_KEY equals _ARADDRESS.ARA_ADDB
                            join _ARFILE in DB_Customer.ARFILEs on _ARADDRESS.ARA_AR equals _ARFILE.AR_KEY
                            join _ARCAT in DB_Customer.ARCATs on _ARFILE.AR_ARCAT equals _ARCAT.ARCAT_KEY
                            orderby _ARFILE.AR_NAME
                            select new CustomerList
                            {
                                AddressKey = _ADDRBOOK.ADDB_KEY.ToString(),
                                CustomerCode = _ARFILE.AR_CODE.ToString(),
                                CustomeName = _ARFILE.AR_NAME.ToString(),
                                CustomerPhone = _ADDRBOOK.ADDB_PHONE.ToString(),
                                CustomerGroup = _ARCAT.ARCAT_NAME.ToString()
                            }).AsParallel().ToList();
                }
            });
        }
        public static async Task<IList> CustomerGroup()
        {
            return await Task<IList>.Run(() =>
            {
                using (CustomerDAL DB_Customer = new CustomerDAL())
                {
                    return DB_Customer.ARCATs.OrderBy(x => x.ARCAT_NAME).Select(x => new { Key = x.ARCAT_KEY, Value = x.ARCAT_NAME }).ToList();
                }
            });
        }
        public static string GetADDBKEY(string AR_CODE)
        {
            using (CustomerDAL DB_Customer = new CustomerDAL())
            {
                return (from _KEY in DB_Customer.ADDRBOOKs
                        join _ARADDB in DB_Customer.ARADDRESSes on _KEY.ADDB_KEY equals _ARADDB.ARA_ADDB
                        join _ARFILE in DB_Customer.ARFILEs on _ARADDB.ARA_AR equals _ARFILE.AR_KEY
                        where _ARFILE.AR_CODE.Equals(AR_CODE)
                        select _KEY.ADDB_KEY).AsParallel().First().ToString();
            }
        }
        public static IList CustomerInfo(string AR_CODE)
        {
            using (CustomerDAL DB_Customer = new CustomerDAL())
            {
                return (from _ADDRBOOK in DB_Customer.ADDRBOOKs
                        join _ARADDRESS in DB_Customer.ARADDRESSes on _ADDRBOOK.ADDB_KEY equals _ARADDRESS.ARA_ADDB
                        join _ARFILE in DB_Customer.ARFILEs on _ARADDRESS.ARA_AR equals _ARFILE.AR_KEY
                        where _ARFILE.AR_CODE.Equals(AR_CODE)
                        select new CustomerInfo
                        {
                            KEY = _ADDRBOOK.ADDB_KEY,
                            CODE = _ARFILE.AR_CODE,
                            NAME = _ARFILE.AR_NAME,
                            TAX_ID = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_TAX_ID) ? _ADDRBOOK.ADDB_TAX_ID : "N/A",
                            ADDB1 = _ADDRBOOK.ADDB_ADDB_1,
                            ADDB2 = _ADDRBOOK.ADDB_ADDB_2,
                            ADDB3 = _ADDRBOOK.ADDB_ADDB_3,
                            PROVINCE = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_PROVINCE) ? _ADDRBOOK.ADDB_PROVINCE : "N/A",
                            POST = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_POST) ? _ADDRBOOK.ADDB_POST : "N/A",
                            PHONE = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_PHONE) ? _ADDRBOOK.ADDB_PHONE : "N/A",
                            FAX = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_FAX) ? _ADDRBOOK.ADDB_FAX : "N/A",
                            WEB = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_WEBSITE) ? _ADDRBOOK.ADDB_WEBSITE : "N/A",
                            EMAIL = !string.IsNullOrEmpty(_ADDRBOOK.ADDB_EMAIL) ? _ADDRBOOK.ADDB_EMAIL : "N/A"
                        }).AsParallel().ToList();
            }
        }
        public static IList ContactInfo(string ADDBKEY)
        {
            using (CustomerDAL DB_Customer = new CustomerDAL())
            {
                return (from _Contact in DB_Customer.CONTACTs
                        join _ADDB in DB_Customer.ADDRBOOKs on _Contact.CT_ADDB equals _ADDB.ADDB_KEY
                        where _ADDB.ADDB_KEY.ToString().Equals(ADDBKEY)
                        select new ContactList
                        {
                            KEY = _Contact.CT_KEY,
                            NAME = _Contact.CT_NAME,
                            LNAME = _Contact.CT_SURNME,
                            JOB = _Contact.CT_JOBTITLE,
                            PHONE = _Contact.CT_MOBILE,
                            EMAIL = _Contact.CT_EMAIL
                        }).AsParallel().ToList();
            }
        }
    }
}