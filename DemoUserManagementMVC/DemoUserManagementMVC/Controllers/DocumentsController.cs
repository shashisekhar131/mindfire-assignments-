using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagementMVC.Controllers
{
    public class DocumentsController : Controller
    {
        static MyService service = new MyService();

        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult UploadDocument(HttpPostedFileBase file, int DocumentType,int ObjectType, int ObjectID)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ExternalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("FolderPath");

                    if (!Directory.Exists(ExternalFolderPath))
                    {
                        Directory.CreateDirectory(ExternalFolderPath);
                    }

                    // Generate a Unique identifier (Guid) for the file name
                    Guid UniqueGuid = Guid.NewGuid();

                    string fileExtension = Path.GetExtension(file.FileName);

                    // Combine the Guid with the file extension to create a Unique file name
                    string UniqueFileName = UniqueGuid.ToString() + fileExtension;

                    // Save file to External folder
                    string filePath = Path.Combine(ExternalFolderPath, UniqueFileName);
                    file.SaveAs(filePath);                   


                    service.InsertDocument(file.FileName, UniqueFileName, ObjectID, ObjectType, DocumentType);

                    return Json(new { success = true, message = "File uploaded successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "No file selected" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error uploading file: " + ex.Message });
            }
        }


        public ActionResult Download(string fileName)
        {
            // Ensure fileName is not null or empty, handle errors if needed

            string filePath = Path.Combine(ConfigurationManager.AppSettings.Get("FolderPath"), fileName);

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                string mimeType = MimeMapping.GetMimeMapping(fileName);

                // Set the content type based on the file extension
                Response.ContentType = mimeType;

                // Set the content-disposition header to force the browser to prompt for download
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                // Write the file content to the response
                Response.BinaryWrite(fileBytes);

                // End the response to avoid any additional content being sent
                Response.End();

                // Optionally, you can return an empty result or another action result
                return new EmptyResult();
            }
            else
            {
                // Handle file not found
                return HttpNotFound();
            }
        }


        [HttpPost]
        public JsonResult GetDocuments(int ObjectID, int ObjectType)
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


            var CustomData = service.GetSortedAndPagedDocuments(ObjectID, ObjectType, SortExpression, SortDirection, PageIndex, PageSize);
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