using BookAnAppointment.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyBusiness = BookAnAppointment.Business.Business;

namespace BookAnAppointment.Controllers
{
    public class DoctorLoginController : Controller
    {
        MyBusiness Business = new MyBusiness();
        // GET: DoctorLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckIfDoctorExists(string UserEmail, string UserPassword)
        {
            int doctorId = Business.CheckIfDoctorExists(UserEmail, UserPassword);
            return Json(doctorId, JsonRequestBehavior.AllowGet);
        }
    }
}