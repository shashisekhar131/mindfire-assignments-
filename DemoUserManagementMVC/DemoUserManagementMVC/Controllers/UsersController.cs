using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

using MyService = DemoUserManagement.Business.Service;
using DemoUserManagement.Models;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.Helpers;
using DemoUserManagementMVC.Helper;

namespace DemoUserManagementMVC.Controllers
{

    public class UsersController : Controller
    {

        static MyService service = new MyService();

        // GET: Users
    /*    [CustomAuthorizationFilter]*/

        public ActionResult GetAllUsers()
        {                       
            return View();
        }

      
        [HttpPost]
        public JsonResult GetData()
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
            var CustomData = service.GetSortedAndPagedUsers(SortExpression, SortDirection, PageIndex,PageSize);
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

        [CustomAuthorizationFilter]

        public ActionResult EditUser(int id)
        {
            int UserID = id;
            
            TempData["UserID"] = UserID;
            return RedirectToAction("Index","UserRegistration");
        }

    }
}