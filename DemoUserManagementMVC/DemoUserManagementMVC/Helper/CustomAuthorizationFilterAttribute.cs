using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebGrease.Css.Ast;

namespace DemoUserManagementMVC.Helper
{
    public class CustomAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {  
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //  custom authorization logic 
            SessionClass sessionData = (SessionClass)filterContext.HttpContext.Session["UserSession"];

            
            var UserIdFromRoute = filterContext.RouteData.Values["id"] ;
            var ControllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();

            
            if (ControllerName == "Login" && sessionData != null)
            {  // logged in again came to login page
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "UserRegistration",  
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
                        controller = "Login",
                        action = "Index",
                       
                    }));
                    return;
                }

                if (sessionData.UserRole != 1) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "UserRegistration",
                        action = "Index",
                        id = sessionData.UserID
                    }));
                    return;
                } 
            }

            if (ControllerName == "UserRegistration")
                {
                    
                    if (UserIdFromRoute != null)
                    {
                        if(sessionData == null)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "Login",
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
                                controller = "UserRegistration",
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