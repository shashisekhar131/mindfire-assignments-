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
    public partial class Users : System.Web.UI.Page
    {

        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {

            // should bind data to grid only for first time not for every request
            if (!IsPostBack)
            {
                // get user with id in query string when this page gets a request 

                // for single user
                // UserDetailsModel UserDetails = service.GetUserDetails(int.Parse(Request.QueryString["id"]));

                // for list of users 


                // becuase GridView.DataSource accepts Enumerable types 

                /*

                List<UserDetailsModel> UserDetailsList = new List<UserDetailsModel> { UserDetails };

                userDetailsGridView.DataSource = UserDetailsList;
                userDetailsGridView.DataBind();

                List<AddressDetailsModel> ListofAddresses = service.GetAddresses(int.Parse(Request.QueryString["id"]));

                addressGridView.DataSource = ListofAddresses;
                addressGridView.DataBind();*/


                BindGridView();
            }



        }


        public void BindGridView()
        {

            List<UserDetailsModel> UserDetailsList = service.GetAllUsers();

            userDetailsGridView.DataSource = UserDetailsList;
            userDetailsGridView.DataBind();


            List<AddressDetailsModel> AddressList = service.GetAllUsersAddresses();
            addressGridView.DataSource = AddressList;
            addressGridView.DataBind();

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx?id=" + int.Parse(UserIdInput.Text) + " ");

        }


        protected void UserDetailsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            userDetailsGridView.PageIndex = e.NewPageIndex;

            // Rebind the data to the GridView with the updated page index
            BindGridView(); // Replace with your data binding logic
        }

      



    }
}