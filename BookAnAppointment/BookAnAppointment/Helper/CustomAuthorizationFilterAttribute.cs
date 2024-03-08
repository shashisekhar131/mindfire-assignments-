using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookAnAppointment.Utils;

namespace BookAnAppointment.Helper
{
    public class CustomAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            SessionClass sessionData = (SessionClass)filterContext.HttpContext.Session["DoctorSession"];


            var doctorIdFromRoute = filterContext.RouteData.Values["id"];
            var controllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();

            if (controllerName == "DoctorLogin" && sessionData != null)
            {  // logged in again came to login page
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "DoctorAppointments",
                    action = "Index",
                    id = sessionData.DoctorID
                }));
                return;
            }

            if (controllerName == "DoctorAppointments")
            {
                if (doctorIdFromRoute != null)
                {
                    if (sessionData == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "DoctorLogin",
                            action = "Index",

                        }));
                        return;
                    }
                    else
                    {
                        if (int.TryParse(doctorIdFromRoute.ToString(), out int doctorId) && doctorId != sessionData.DoctorID)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "DoctorAppointments",
                                action = "Index",
                                id = sessionData.DoctorID
                            }));
                            return;
                        }
                    }
                }                
            }
        }

    }
}