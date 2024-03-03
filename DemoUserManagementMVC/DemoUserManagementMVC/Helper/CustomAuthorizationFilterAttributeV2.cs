using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoUserManagementMVC.Helper
{
    public class CustomAuthorizationFilterAttributeV2 : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //  custom authorization logic 
            SessionClass sessionData = (SessionClass)filterContext.HttpContext.Session["UserSession"];

            if (sessionData == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoginV2", 
                    action = "Index"        
                }));
                return;
            }
            else
            {
                if (sessionData.UserRole != 1)
                {

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "UserRegistrationV2", 
                        action = "Index",        
                    }));
                    return;
                }

            }


        }
    }
}