using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using static DemoUserManagement.Utils.Enums;

using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        static MyService service = new MyService();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                // Access the file from the request
                HttpPostedFile fileUpload = context.Request.Files[0];

                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string externalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("ServerPath");

                    if (!Directory.Exists(externalFolderPath))
                    {
                        Directory.CreateDirectory(externalFolderPath);
                    }

                    // Generate a unique identifier (Guid) for the file name
                    Guid uniqueGuid = Guid.NewGuid();

                    string fileExtension = Path.GetExtension(fileUpload.FileName);

                    // Combine the Guid with the file extension to create a unique file name
                    string uniqueFileName = uniqueGuid.ToString() + fileExtension;

                    // Save file to external folder
                    string filePath = Path.Combine(externalFolderPath, uniqueFileName);
                    fileUpload.SaveAs(filePath);

                    
                    int ObjectID = int.Parse(context.Request["UserID"]);
                    int DocumentType;
                    int ObjectType = int.Parse(context.Request["ObjectType"]);

                    if (context.Request["FileType"] != null)
                    {
                         DocumentType = int.Parse(context.Request["FileType"]);
                    }
                    else
                    {
                        DocumentType = (int)Enums.DocumentType.Others;
                    }




                    service.InsertDocument(fileUpload.FileName, uniqueFileName, ObjectID, ObjectType, DocumentType);

                    var jsonResponse = new
                    {
                        success = true,
                        message = "File uploaded successfully.",
                        filePath,
                        uniqueFileName,
                        originalFileName = fileUpload.FileName
                    };

                    // Serialize the response to JSON and write it to the response
                    context.Response.Write(new JavaScriptSerializer().Serialize(jsonResponse));
                }
                else
                {
                    context.Response.Write("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}