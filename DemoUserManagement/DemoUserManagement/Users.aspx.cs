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
                UserDetailsModel UserDetails = service.GetUserDetails(int.Parse(Request.QueryString["id"]));


                // becuase GridView.DataSource accepts Enumerable types 
                List<UserDetailsModel> UserDetailsList = new List<UserDetailsModel> { UserDetails };

                userDetailsGridView.DataSource = UserDetailsList;
                userDetailsGridView.DataBind();

                List<AddressDetailsModel> ListofAddresses = service.GetAddresses(int.Parse(Request.QueryString["id"]));

                addressGridView.DataSource = ListofAddresses;
                addressGridView.DataBind();
            }

           

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Default.aspx?id=" +Request.QueryString["id"]);


        }
    }
}