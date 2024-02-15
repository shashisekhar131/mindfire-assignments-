using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using DemoUserManagement.Models;

using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagement
{
    public partial class DocumentUserControl : System.Web.UI.UserControl
    {
        public int ObjectType {get;set;}
        public int ObjectID { get; set;}

        public int DocumentType { get; set;}


        protected void Page_Load(object sender, EventArgs e)
        {
            //  selected a user to edit from user.aspx list 
            if (Request.QueryString["id"] != null)
            {

                List<DocumentModel> ListofDocuments = service.GetDocumentsForUser(Convert.ToInt32(Request.QueryString["id"]), ObjectType);
                DocumentGridView.DataSource = ListofDocuments;
                DocumentGridView.DataBind();
            }
        }

        static MyService service = new MyService();
        // saving file in server 
        protected void BtnUpload_Click(object sender, EventArgs e)
        {



            string ExternalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("ServerPath");
            if(!Directory.Exists(ExternalFolderPath))
            {
                Directory.CreateDirectory(ExternalFolderPath);
            }


            // Generate a unique identifier (Guid) for the file name
            Guid uniqueGuid = Guid.NewGuid();

            string fileExtension = Path.GetExtension(fuFileControl.FileName);

            // Combine the Guid with the file extension to create a unique file name
            string uniqueFileName = uniqueGuid.ToString() + fileExtension;

            // save file to external folder 
            fuFileControl.SaveAs(ExternalFolderPath + uniqueFileName);


            ObjectID = int.Parse(Request.QueryString["id"]);
            DocumentType= Convert.ToInt32(ddlFileType.SelectedValue);

            if (service.InsertDocument(fuFileControl.FileName,uniqueFileName, ObjectID, ObjectType,DocumentType))
            {
                // get documents uploaded by user (userid or objectID) in a page (objecttype) 
                List<DocumentModel> ListofDocuments = service.GetDocumentsForUser(ObjectID, ObjectType);
                DocumentGridView.DataSource = ListofDocuments;
                DocumentGridView.DataBind();
            }

        }
    }
}