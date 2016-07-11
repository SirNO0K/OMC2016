﻿using System;
using System.Reflection;
using System.Web.Mvc;

namespace OMC2016.Filters
{
    public class HttpParamActionAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[methodInfo.Name] != null;
        }
    }
}