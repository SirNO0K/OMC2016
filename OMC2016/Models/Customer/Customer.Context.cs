﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CustomerDAL : DbContext
    {
        public CustomerDAL()
            : base(DBLibrary.AccountConnectionString(ModelList.Customer))
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADDRBOOK> ADDRBOOKs { get; set; }
        public virtual DbSet<ARADDRESS> ARADDRESSes { get; set; }
        public virtual DbSet<ARCAT> ARCATs { get; set; }
        public virtual DbSet<ARFILE> ARFILEs { get; set; }
        public virtual DbSet<CONTACT> CONTACTs { get; set; }
    }
}
