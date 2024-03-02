using DemoUserManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Services.Description;

using MyService = DemoUserManagement.Business.Service;             
using DemoUserManagement.Models;
using DemoUserManagement.Utils;
using DemoUserManagementMVC.Helper;

namespace DemoUserManagementMVC.Controllers
{
    public class LoginController : Controller
    {
        static MyService service = new MyService();
        // GET: Login
        public ActionResult Index(string error = null)
        {

            ViewBag.Error = error;
            return View();
        }

        public ActionResult CheckIfUserExists(string UserEmail, string UserPassword)
        {  // UserEmail, UserPassword parameters get values using input name in html 

            Dictionary<string, int> User = service.CheckIfUserExists(UserEmail, UserPassword);

            if (User["IsUserExists"] == 1)
            {
                // set the session data 
                SessionClass sessionData = new SessionClass
                {
                    UserID = User["UserID"],
                    UserRole = User["RoleID"]
                };

                Session["UserSession"] = sessionData;

                return RedirectToAction("GetAllUsers", "Users");
            }
            
            return RedirectToAction("Index","Login", new { error = "Invalid email or password" });
        }
    }
}