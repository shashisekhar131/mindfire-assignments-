using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DemoUserManagement.Models;

using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagement
{
    public partial class Users : System.Web.UI.Page
    {

        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {

            string UserRole = HttpContext.Current.Session["UserRole"] as string;

            if (UserRole!= "Admin")
            {
                Response.Redirect("~/LoginPage.aspx");
            }
            // should bind data to grid only for first time not for every request
            if (!IsPostBack)
            {
                
                ViewState["SortDirection"] = "ASC";

                // Set the sort expression to the clicked column
                ViewState["SortExpression"] = "UserID";

                BindGridView();
            }



        }


        public void BindGridView()
        {
/*
            List<UserDetailsModel> UserDetailsList = service.GetAllUsers();

            userDetailsGridView.DataSource = UserDetailsList;
            userDetailsGridView.DataBind();*/

            int currentPageIndex = userDetailsGridView.PageIndex;
            int pageSize = userDetailsGridView.PageSize;
            string sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            int totalCount = GetTotalCount();

            userDetailsGridView.VirtualItemCount = totalCount;

            userDetailsGridView.DataSource = service.GetSortedAndPagedUsers(sortExpression, sortDirection, currentPageIndex * pageSize, pageSize);
            userDetailsGridView.DataBind();


            //List<AddressDetailsModel> AddressList = service.GetAllUsersAddresses();
            //addressGridView.DataSource = AddressList;
            //addressGridView.DataBind();

        }
        private int GetTotalCount()
        {
            int TotalCount = 0;
            TotalCount = service.TotalUsers();
            return TotalCount;
        }
        protected void EditBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDetails.aspx?id=" + int.Parse(UserIdInput.Text) + " ");

        }


        protected void UserDetailsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            // Set the new page index
            userDetailsGridView.PageIndex = e.NewPageIndex;

            // Rebind the data to the GridView with the updated page index
            BindGridView(); // Replace with your data binding logic
        }

        protected void userDetailsGridView_Sorting(object sender, GridViewSortEventArgs e)
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

    }
}