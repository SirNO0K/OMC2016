using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMC2016.Models.Tools
{
    public class ApplicationConfig
    {
        [Required]
        [Display(Name = "System Server : ")]
        public string SystemServer { get; set; }
        [Required]
        [Display(Name = "System DB Name : ")]
        public string SystemDatabase { get; set; }
        [Required]
        [Display(Name = "System DB Username : ")]
        public string SystemUsername { get; set; }
        [Required]
        [Display(Name = "System DB Password : ")]
        public string SystemPassword { get; set; }

        [Required]
        [Display(Name = "ERP Server : ")]
        public string AccuontServer { get; set; }
        [Required]
        [Display(Name = "ERP DB Name : ")]
        public string AccuontDatabase { get; set; }
        [Required]
        [Display(Name = "ERP DB Username : ")]
        public string AccuontUsername { get; set; }
        [Required]
        [Display(Name = "ERP DB Password : ")]
        public string AccuontPassword { get; set; }
    }
}