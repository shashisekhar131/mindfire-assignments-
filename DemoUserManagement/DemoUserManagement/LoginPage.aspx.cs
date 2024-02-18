using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoUserManagement.Models;

using MyService = DemoUserManagement.Business.Service;


namespace DemoUserManagement
{
    public partial class LoginPage : System.Web.UI.Page
    {
        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }


       [WebMethod] 
        public static Dictionary<string, int> CheckIfUserExists(string UserEmail,string UserPassword)
        {

            Dictionary<string, int> User =  service.CheckIfUserExists(UserEmail, UserPassword);
            return User;
        }


    }
}