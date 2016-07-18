using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMC2016.Models.Services
{
    public class MachineList
    {
        public int MIXID { get; set; }
        public string ISEXP { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string SN { get; set; }
        public string SALE_DATE { get; set; }
        public string EXP_DATE { get; set; }
        public string ISTRANSFER { get; set; }
        public string REMARK { get; set; }
    }
}