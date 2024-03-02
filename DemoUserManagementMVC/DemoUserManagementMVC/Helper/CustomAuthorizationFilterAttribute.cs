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
    public class CustomAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //  custom authorization logic 
            SessionClass sessionData = (SessionClass)filterContext.HttpContext.Session["UserSession"];

            if(sessionData == null )
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",  // Replace with your login controller
                    action = "Index"         // Replace with your login action
                }));
                return;
            }
            else
            {
                if(sessionData.UserRole != 1)
                {                 

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "UserRegistration",  // Replace with your login controller
                        action = "Index",         // Replace with your login action
                    }));
                    return;
                }
                
            }
            

        }
    }
}