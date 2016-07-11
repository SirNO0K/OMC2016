using System.ComponentModel.DataAnnotations;

namespace OMC2016.Models.Customer
{
    public class CustomerInfo
    {
        public int KEY { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string TAX_ID { get; set; }
        public string ADDB1 { get; set; }
        public string ADDB2 { get; set; }
        public string ADDB3 { get; set; }
        public string PROVINCE { get; set; }
        public string POST { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string WEB { get; set; }
        public string EMAIL { get; set; }
    }
}