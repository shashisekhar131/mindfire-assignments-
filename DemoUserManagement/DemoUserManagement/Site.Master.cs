using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string IsActive(string pageName)
        {
            // Get the current page name from the HttpContext
            string currentPage = System.IO.Path.GetFileName(Request.Path);

            // Compare the current page with the specified page name
            if (currentPage.Equals(pageName, StringComparison.OrdinalIgnoreCase))
            {
                return " active";
            }           


            return string.Empty;
        }

       
    }
}