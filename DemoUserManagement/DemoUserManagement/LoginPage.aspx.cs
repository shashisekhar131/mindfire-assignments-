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
    public partial class LoginPage : BasePage
    {                                                                                                                                          
        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }


                                                 
    }
}