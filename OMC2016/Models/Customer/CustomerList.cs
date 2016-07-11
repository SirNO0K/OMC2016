using System.ComponentModel.DataAnnotations;

namespace OMC2016.Models.Customer
{
    public class CustomerList
    {
        public string AddressKey { get; set; }

        [Display(Name = "รหัสลูกค้า")]
        public string CustomerCode { get; set; }

        [Display(Name = "ชื่อ - บริษัท")]
        public string CustomeName { get; set; }

        [Display(Name = "หมายเลขโทรศัพท์")]
        public string CustomerPhone { get; set; }

        [Display(Name = "กลุ่มลูกค้า")]
        public string CustomerGroup { get; set; }
    }
}