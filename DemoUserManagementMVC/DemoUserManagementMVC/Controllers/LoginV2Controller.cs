using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagementMVC.Controllers
{
    public class LoginV2Controller : Controller
    {
        static MyService service = new MyService();

        // GET: LoginV2
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
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

            }

            return Json(User, JsonRequestBehavior.AllowGet);


        }


    }
}