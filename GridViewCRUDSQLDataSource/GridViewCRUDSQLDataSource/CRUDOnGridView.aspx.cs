using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewCRUDSQLDataSource
{
    public partial class CRUDOnGridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Refreshdata();
            }
        }

        public void Refreshdata()
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";
            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("select * from tbl_data", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from tbl_data where id =@id", con);
                cmd.Parameters.AddWithValue("id", id);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                Refreshdata();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Refreshdata();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Refreshdata();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";

            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                TextBox txtname = GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
                TextBox txtcity = GridView1.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
                int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());

                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE tbl_data SET name = @name, city = @city WHERE id = @id", con);

                cmd.Parameters.AddWithValue("@name", txtname.Text); // Use .Text to get the text from the TextBox
                cmd.Parameters.AddWithValue("@city", txtcity.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery(); // Execute the UPDATE command

                con.Close();

                GridView1.EditIndex = -1;
                Refreshdata();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        protected void InsertRow_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";

            try
            {

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_data (name, city) VALUES (@name, @city)", con);

                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@city", city.Text);

                cmd.ExecuteNonQuery();
                con.Close();



                // Refresh the GridView after inserting
                Refreshdata();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Refreshdata();
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False";

            try
            {
                // get the data 
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Build the SQL query with dynamic ORDER BY clause
                    string sqlQuery = "SELECT * FROM tbl_data";
                    if (!string.IsNullOrEmpty(e.SortExpression))
                    {
                        sqlQuery += " ORDER BY " + e.SortExpression + " " + (e.SortDirection == SortDirection.Ascending ? "ASC" : "DESC");
                    }

                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Rebind the sorted data to the GridView
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}