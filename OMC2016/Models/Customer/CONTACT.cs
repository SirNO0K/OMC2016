//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMC2016.Models.Customer
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONTACT
    {
        public int CT_KEY { get; set; }
        public string CT_INTL { get; set; }
        public string CT_NAME { get; set; }
        public string CT_SURNME { get; set; }
        public string CT_E_NAME { get; set; }
        public string CT_JOBTITLE { get; set; }
        public string CT_EMAIL { get; set; }
        public string CT_MOBILE { get; set; }
        public int CT_ADDB { get; set; }
        public int CT_STA { get; set; }
        public string CT_REMARK { get; set; }
        public string CT_CANEMAIL { get; set; }
        public string CT_LASTUPD { get; set; }
    
        public virtual ADDRBOOK ADDRBOOK { get; set; }
    }
}
