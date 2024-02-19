using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class LogoutPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            Response.Redirect("~/LoginPage.aspx");

        }
    }
}