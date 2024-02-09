using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NETpractice_app
{
    public partial class CustomUserControl : System.Web.UI.UserControl
    {

        public string Name { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
                        
        }

        protected void AddNotes(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Assuming 'name' is the column where you want to store the notes
                    string insertQuery = "INSERT INTO userNotes (id,notes,page) VALUES (@id,@Notes,@Page)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Notes",NotesInput.Text );
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@Page",Name);

                        cmd.ExecuteNonQuery();

                        ShowNotes();
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }



        protected void ShowNotes()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT notesid,notes FROM userNotes WHERE id = @id AND page = @Page";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@Page", Name);

                        DataTable dt = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(dt);

                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                    } 
                } 
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

       

       


       



    }
}