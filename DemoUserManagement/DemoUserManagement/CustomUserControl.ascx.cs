using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DemoUserManagement.Models;

using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagement
{
    public partial class CustomUserControl : System.Web.UI.UserControl
    {
        public int ObjectType { get; set; }
        public int ObjectID { get; set; }

        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["SortDirection"] = "ASC";

            // Set the sort expression to the clicked column
            ViewState["SortExpression"] = "NotesID";

            if (Request.QueryString["id"] != null)
            {
                BindGridView();
            }

        }

        public void BindGridView()
        {
            
            ObjectID =int.Parse(Request.QueryString["id"]);

            int CurrentPageIndex = NotesGridView.PageIndex;
            int PageSize = NotesGridView.PageSize;
            string SortExpression = ViewState["SortExpression"].ToString();
            string SortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";


            NotesGridView.VirtualItemCount = service.TotalNoteRows();


            List<NoteModel> ListofNotes = service.GetSortedAndPagedNotes(ObjectID, ObjectType, SortExpression, SortDirection, CurrentPageIndex, PageSize);

            NotesGridView.DataSource = ListofNotes;
            NotesGridView.DataBind();

        }

        protected void NotesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            NotesGridView.PageIndex = e.NewPageIndex;

            // Rebind the data to the GridView with the updated page index
            BindGridView(); // Replace with your data binding logic
        }

        protected void NotesGridView_Sorting(object sender, GridViewSortEventArgs e)
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
        protected void NotesBtn_Click(object sender, EventArgs e)
        {
            ObjectID = int.Parse(Request.QueryString["id"]);

            service.InsertNotes(NotesInput.Text, ObjectID, ObjectType);
            

        }

        
    }
}