using BookAnAppointment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness = BookAnAppointment.Business.Business;

namespace BookAnAppointment.Controllers
{
    public class DoctorRegistrationController : Controller
    {
        MyBusiness Business = new MyBusiness();
        // GET: DoctorRegistration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InsertDoctor(DoctorInfo doctorInfo)
        {
            bool flag = Business.InsertDoctor(doctorInfo);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
    }
}