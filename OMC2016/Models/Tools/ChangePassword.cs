using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OMC2016.Models.Tools
{
    public class ChangePassword
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        [Required]
        public string OldPwd { get; set; }
        [Required]
        public string NewPwd { get; set; }
        [Required]
        public string ConPwd { get; set; }
    }
}