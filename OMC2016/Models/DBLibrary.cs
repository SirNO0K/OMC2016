using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;

namespace OMC2016.Models
{

    public enum ModelList
    {
        Menubar,
        Authentication,
        Customer,
        Service
    }
    public static class DBLibrary
    {
        public static string SystemConnectionString(ModelList model)
        {
            string _System = ConfigurationManager.ConnectionStrings["System"].ConnectionString;
            EntityConnectionStringBuilder _SystemConn = new EntityConnectionStringBuilder(_System);

            switch (model)
            {
                default:
                    throw new Exception("Invalid model, no connection string defined");
                case ModelList.Menubar:
                    _SystemConn.Metadata = "res://*/Models.Tools.Menubar.csdl|res://*/Models.Tools.Menubar.ssdl|res://*/Models.Tools.Menubar.msl";
                    return _SystemConn.ToString();
                case ModelList.Authentication:
                    _SystemConn.Metadata = "res://*/Models.Tools.Authentication.csdl|res://*/Models.Tools.Authentication.ssdl|res://*/Models.Tools.Authentication.msl";
                    return _SystemConn.ToString();
                case ModelList.Service:
                    _SystemConn.Metadata = "res://*/Models.Services.ServiceDAL.csdl|res://*/Models.Services.ServiceDAL.ssdl|res://*/Models.Services.ServiceDAL.msl";
                    return _SystemConn.ToString();
            }
        }

        public static string AccountConnectionString(ModelList model)
        {
            string _Account = ConfigurationManager.ConnectionStrings["Account"].ConnectionString;
            EntityConnectionStringBuilder _AccountConn = new EntityConnectionStringBuilder(_Account);

            switch (model)
            {
                default:
                    throw new Exception("Invalid model, no connection string defined");
                case ModelList.Customer:
                    _AccountConn.Metadata = "res://*/Models.Customer.Customer.csdl|res://*/Models.Customer.Customer.ssdl|res://*/Models.Customer.Customer.msl";
                    return _AccountConn.ToString();
            }
        }

    }
}