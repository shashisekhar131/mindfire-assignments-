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
        public Nullable<int> ObjectType { get; set; }

        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NotesBtn_Click(object sender, EventArgs e)
        {
            int UserId = int.Parse(Request.QueryString["id"]);

          if (service.InsertNotes(NotesInput.Text,UserId, (int)ObjectType))
            {
               List<NoteModel> ListofNotes =  service.GetNotes(UserId,(int)ObjectType);
               notesGridView.DataSource = ListofNotes;
               notesGridView.DataBind();
            }

        }

        
    }
}