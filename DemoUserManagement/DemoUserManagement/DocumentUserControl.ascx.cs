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

            ViewState["SortDirection"] = "ASC";

            // Set the sort expression to the clicked column
            ViewState["SortExpression"] = "DocumentID";

            //  selected a user to edit from user.aspx list 
            if (Request.QueryString["id"] != null)
            {
                BindGridView();
                }
        }

        static MyService service = new MyService();

        public void BindGridView()
        {

            int CurrentPageIndex = DocumentGridView.PageIndex;
            int PageSize = DocumentGridView.PageSize;
            string SortExpression = ViewState["SortExpression"].ToString();
            string SortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";


            DocumentGridView.VirtualItemCount = service.TotalDocumentRows();

            List<DocumentModel> ListofDocuments = service.GetSortedAndPagedDocuments(Convert.ToInt32(Request.QueryString["id"]), ObjectType,  SortExpression,  SortDirection,  CurrentPageIndex, PageSize);
            DocumentGridView.DataSource = ListofDocuments;
            DocumentGridView.DataBind();


        }

        protected void DocumentGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            DocumentGridView.PageIndex = e.NewPageIndex;

            // Rebind the data to the GridView with the updated page index
            BindGridView(); 
        }

        protected void DocumentGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the current sort direction from ViewState or default to "ASC"
            string SortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            // Toggle the sort direction
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            // Update the ViewState with the new sort direction
            ViewState["SortDirection"] = SortDirection;

            // Set the sort expression to the clicked column
            ViewState["SortExpression"] = e.SortExpression;

            BindGridView();

        }
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
                BindGridView();
            }

        }
    }
}