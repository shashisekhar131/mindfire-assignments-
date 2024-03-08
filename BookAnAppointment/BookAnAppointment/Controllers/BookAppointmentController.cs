using BookAnAppointment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness = BookAnAppointment.Business.Business;

namespace BookAnAppointment.Controllers
{
    public class BookAppointmentController : Controller
    {
        MyBusiness Business = new MyBusiness();
        // GET: BookAppointment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllDoctors()
        {
            List<DoctorModel> doctors = Business.GetAllDoctors();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableSlotsForDoctor(int doctorId, DateTime date)
        {
            var availablityInfo = Business.GetAvailableSlotsForDoctor(doctorId, date);
            return Json(availablityInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertAppointment(AppointmentModel appointmentInfo)
        {
            bool flag = Business.InsertAppointment(appointmentInfo);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
    }
}