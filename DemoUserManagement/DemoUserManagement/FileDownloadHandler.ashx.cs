using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for FileDownloadHandler
    /// </summary>
    public class FileDownloadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string fileName = context.Request.QueryString["fileName"];

            // external file path
            string filePath = System.Configuration.ConfigurationManager.AppSettings.Get("ServerPath") + fileName;


            // Set the content type based on the file extension
            context.Response.ContentType = MimeMapping.GetMimeMapping(fileName);

            // Set the content-disposition header to force the browser to prompt for download
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

            // Write the file content to the response
            context.Response.WriteFile(filePath);

            // End the response to avoid any additional content being sent
            context.Response.End();
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