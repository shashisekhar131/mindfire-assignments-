using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NETpractice_app
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>{
                new Product { ProductId = 1, ProductName = "Product A", Price = 19.99f },
                new Product { ProductId = 2, ProductName = "Product B", Price = 29.99f }
            };
            return products;
        }

    }
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            SelectedInputs.Text = "None";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            var AllSelectedInputs = "";

            //text input
            Console.WriteLine(Password.Text + "hello");



            AllSelectedInputs += UserName.Text + EmailID.Text + Password.Text + ConfirmPassword.Text;



            // select box input
            if (DropDownList1.SelectedValue != "") AllSelectedInputs += "selected city" + DropDownList1.SelectedValue;


            //radio button input
            if (RadioButton1.Checked)
            {
                AllSelectedInputs += "Your gender is " + RadioButton1.Text;
            }
            else AllSelectedInputs += "Your gender is " + RadioButton2.Text;



            // checkbox input
            if (CheckBox1.Checked) AllSelectedInputs += CheckBox1.Text + " ";
            if (CheckBox2.Checked) AllSelectedInputs += CheckBox2.Text + " ";
            if (CheckBox3.Checked) AllSelectedInputs += CheckBox3.Text;

            SelectedInputs.Text = AllSelectedInputs;
        }
    }
}