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
        public ActionResult Index(int? id)
        {
            if (id != null)
            {   
                TempData["doctorId"] = id;
            }
            return View();
        }

        [HttpPost]
        public JsonResult InsertDoctor(DoctorInfo doctorInfo)
        {
            bool flag = Business.InsertDoctor(doctorInfo);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDoctorDetails(int doctorId)
        {
            DoctorInfo doctorInfo = Business.GetDoctorDetails(doctorId);
            return Json(doctorInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateDoctor(DoctorInfo doctorInfo)
        {

            bool flag = Business.UpdateDoctor(doctorInfo);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }


    }
}