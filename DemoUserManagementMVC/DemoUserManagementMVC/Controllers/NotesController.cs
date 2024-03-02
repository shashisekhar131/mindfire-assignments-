using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagementMVC.Controllers
{
    public class NotesController : Controller
    {
        static MyService service = new MyService();

        // GET: Notes   

        [HttpPost]
        public JsonResult SaveNote(string NoteText, int UserID, int ObjectType)
        {
            if (service.InsertNotes(NoteText, UserID, ObjectType))
            {
                return Json(new { success = true, message = "Note saved successfully" });
            }

            return Json(new { success = false, message = "Failed to save note" });
        }



        [HttpPost]
        public JsonResult GetNotes(int ObjectID,int ObjectType)
        {
            //var request = Request.Form;
            var Draw = Convert.ToInt32(Request.Form["draw"]);
            var Start = Convert.ToInt32(Request.Form["start"]);

            var Length = Convert.ToInt32(Request.Form["length"]);
            var SortExpression = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];

            var SortDirection = Request.Form["order[0][dir]"].ToUpper();

            if (SortDirection == null) SortDirection = "ASC";
            //var SearchValue = Request.Form["search[value]"].FirstOrDefault();           

            int PageIndex = Start / Length;
            int PageSize = Length;

           

            var CustomData = service.GetSortedAndPagedNotes(ObjectID,ObjectType,SortExpression, SortDirection, PageIndex, PageSize);
            int TotalRecord = service.TotalUsers();

            var jsonData = new
            {
                recordsFiltered = TotalRecord,
                recordsTotal = TotalRecord,
                data = CustomData,
                draw = Draw,
            };
            return Json(jsonData);

        }


    }
}