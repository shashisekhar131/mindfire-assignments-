using BookAnAppointment.Business;
using BookAnAppointment.Helper;
using BookAnAppointment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyBusiness = BookAnAppointment.Business.Business;

namespace BookAnAppointment.Controllers
{
    [CustomAuthorizationFilter]
    public class DoctorAppointmentsController : Controller
    {
        MyBusiness Business = new MyBusiness();
        // GET: DoctorAppointments
        public ActionResult Index(int? id)
        {
            TempData["doctorId"] = id;     
           
            return View();
        }
        [HttpPost]
        public JsonResult GetAppointmentsForDoctor(int doctorId,DateTime selectedDate)
        {
            //var request = Request.Form;
            var draw = Convert.ToInt32(Request.Form["draw"]);
            var start = Convert.ToInt32(Request.Form["start"]);

            var length = Convert.ToInt32(Request.Form["length"]);
            var sortExpression = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];
            var sortDirection = Request.Form["order[0][dir]"].ToUpper();

            int pageIndex = start / length;
            int pageSize = length;
            var customData =Business.GetSortedAndPagedAppointmentsForDoctor(doctorId,selectedDate,sortExpression, sortDirection, pageIndex, pageSize);
            int totalRecord =Business.TotalAppointmentsForDoctor(doctorId,selectedDate);

            var jsonData = new
            {
                recordsFiltered = totalRecord,
                recordsTotal = totalRecord,
                data = customData,
                draw = draw,
            };
            return Json(jsonData);

        }

        [HttpPost]
        public JsonResult CancelAppointment(int id)
        {
            bool flag = Business.CancelAppointment(id);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CloseAppointment(int id)
        {
            bool flag = Business.CloseAppointment(id);
            return Json(flag, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AppointmentsSummaryForDoctor(int? id)
        {
            TempData["doctorId"] = id;
            int doctorId = (int)id;
            string doctorName = Business.GetDoctorName(doctorId);
            TempData["doctorName"] = doctorName;

            return View();
        }
        public ActionResult DetailedAppointmentsForDoctor(int? id)
        {
            TempData["doctorId"] = id;
            int doctorId = (int)id;
            string doctorName = Business.GetDoctorName(doctorId);
            TempData["doctorName"] = doctorName;

            return View();
        }        
        [HttpPost]
        public JsonResult GetSummaryForMonth(DateTime selectedMonth, int doctorId)
        {
            List <AppointmentSummaryInfo> summaryList = Business.GetSummaryForMonth(selectedMonth, doctorId);
            return Json(summaryList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetDetailedForMonth(DateTime selectedMonth, int doctorId)
        {
            List<AppointmentDetailedInfo> detailedList = Business.GetDetailedForMonth(selectedMonth, doctorId);
            return Json(detailedList, JsonRequestBehavior.AllowGet);

        }
       

    }
}