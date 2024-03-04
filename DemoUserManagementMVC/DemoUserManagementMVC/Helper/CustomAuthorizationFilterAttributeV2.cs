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


            var UserIdFromRoute = filterContext.RouteData.Values["id"];
            var ControllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();


            if (ControllerName == "LoginV2" && sessionData != null)
            {  // logged in again came to login page
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "UserRegistrationV2",
                    action = "Index",
                    id = sessionData.UserID
                }));
                return;

            }


            if (ControllerName == "Users")
            {

                if (sessionData == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoginV2",
                        action = "Index",

                    }));
                    return;
                }

                if (sessionData.UserRole != 1)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "UserRegistrationV2",
                        action = "Index",
                        id = sessionData.UserID
                    }));
                    return;
                }
            }

            if (ControllerName == "UserRegistrationV2")
            {

                if (UserIdFromRoute != null)
                {
                    if (sessionData == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "LoginV2",
                            action = "Index",

                        }));
                        return;
                    }
                    else if (sessionData.UserRole == 1)
                    {

                    }
                    else if (int.TryParse(UserIdFromRoute.ToString(), out int userId) && userId == sessionData.UserID)
                    {

                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "UserRegistrationV2",
                            action = "Index",
                            id = sessionData.UserID
                        }));
                        return;
                    }
                }
            }

        }
    }
}