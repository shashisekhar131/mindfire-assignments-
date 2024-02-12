using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DemoUserManagement.Business;
using DemoUserManagement.Models;

using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagement
{

    public partial class _Default : Page
    {

        static MyService service = new MyService();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // load countries for the first request
                LoadCountries();
            }


            // if user got redirected and came to update or edit then populate the form values from db
            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                // populate the values into every input from reading database
                PopulateValuesIntoForm(int.Parse(Request.QueryString["id"]));

                btnSubmit.Text = "save";
                btnReset.Enabled = false;
                
            }

           

        }

        private void LoadCountries()
        {
            // Call a method to get the list of countries from the database
            List<string> countries = service.GetAllCountries();

            // Bind the countries to the ddlPresentCountry DropDownList
            ddlPresentCountry.DataSource = countries;
            ddlPresentCountry.DataBind();

            ddlPermanentCountry.DataSource = countries;
            ddlPermanentCountry.DataBind();



        }

        public void DdlPresentCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected country from the DropDownList
            string selectedCountry = ddlPresentCountry.SelectedValue;

            List<string> StatesForCountry = service.GetStatesForCountry(selectedCountry);


            ddlPresentState.DataSource = StatesForCountry;
            ddlPresentState.DataBind();




        }

        public void DdlPermanentCountry_SelectedIndexChanged(object sender,EventArgs e)
        {
            // Get the selected country from the DropDownList
            string selectedCountry = ddlPermanentCountry.SelectedValue;

            List<string> StatesForCountry = service.GetStatesForCountry(selectedCountry);


            ddlPermanentState.DataSource = StatesForCountry;
            ddlPermanentState.DataBind();



        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this);
        }

        private void ClearTextBoxes(Control parent)
        {
            // loop through all controls
            foreach (Control control in parent.Controls)
            {
                // we have many controls but we need some of them specifically input controls 
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }

                // Recursive call to handle nested controls
                if (control.HasControls())
                {
                    ClearTextBoxes(control);
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            // get all the details from the form         

            (UserDetailsModel UserInfo, List<AddressDetailsModel> ListofAddresses) = TakeValuesFromForm();

            // means user is in edit url  
            if (Request.QueryString["id"] != null)
            {
                UpdateUser(UserInfo,ListofAddresses, int.Parse(Request.QueryString["id"]));
            }
            else
            {
                InsertNewUser(UserInfo,ListofAddresses);
            }



         }


        public void InsertNewUser(UserDetailsModel NewUser, List<AddressDetailsModel> ListofAddresses)
        {
            List<int> ResultList = service.InsertUser(NewUser, ListofAddresses);
            // ResultList[0] - flag 
            // ResultList[1] - NewUserId inserted into db
            Session["UserId"] = ResultList[1];
            // show single user
            //  if (ResultList[0] == 1) Response.Redirect("~/Users.aspx?id=" + ResultList[1]);
            // show multiple users

            if (ResultList[0] == 1) Response.Redirect("~/Users.aspx");

        }

        public void UpdateUser(UserDetailsModel NewUser, List<AddressDetailsModel> ListofAddresses,int IdToUpdate)
        {
            if (service.UpdateUser(NewUser, ListofAddresses, IdToUpdate)) Response.Redirect("~/Users.aspx?id=" + IdToUpdate);
        }

        public ( UserDetailsModel, List<AddressDetailsModel>) TakeValuesFromForm()
        {

            string formattedDateOfBirth = DateTime.Parse(txtDOB.Text).ToString("yyyy-MM-dd");

            UserDetailsModel UserInfo = new UserDetailsModel
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
                PhoneNumber = txtPhoneNumber.Text,
                AlternatePhoneNumber = txtAlternatePhoneNumber.Text,
                Email = txtEmail.Text,
                AlternateEmail = txtAlternateEmail.Text,
                DOB = formattedDateOfBirth,
                Favouritecolor = txtFavouriteColor.Text,
                Aadhaar = txtAadhaar.Text,
                PAN = txtPAN.Text,
                MaritalStatus = maritalStatus.SelectedValue,
                PreferedLanguage = language.SelectedValue,
                Upto10th = txtPrimaryEducation.Text,
                PercentageUpto10th = int.Parse(txtPercentageIn10th.Text),
                Upto12th = txtIntermediateEducation.Text,
                PercentageUpto12th = int.Parse(txtIntermediatePercentage.Text),
                Graduation = txtBTech.Text,
                PercentageInGraduation = int.Parse(txtBTechPercentage.Text)
            };

            AddressDetailsModel PresentAddress = new AddressDetailsModel
            {
                Address = ddlPresentCountry.SelectedValue + ", " + ddlPresentState.SelectedValue + ", " + txtPresentAddress.Text,
                Type = "present address",
                /* UserID =  set this by getting max id + 1 in DAL */

            };

            AddressDetailsModel PermanentAddress = new AddressDetailsModel
            {
                Address = ddlPermanentCountry.SelectedValue + ", " + ddlPermanentState.SelectedValue + ", " + txtPresentAddress.Text,
                Type = "permanent address",
                /* UserID = */

            };
            List<AddressDetailsModel> ListofAddresses = new List<AddressDetailsModel>();

            ListofAddresses.Add(PresentAddress);
            ListofAddresses.Add(PermanentAddress);

            return (UserInfo, ListofAddresses);
        }

        public void PopulateValuesIntoForm(int UserId)
        {
            UserDetailsModel UserDetails = service.GetUserDetails(UserId);
            List<AddressDetailsModel> ListofAddresses = service.GetAddresses(int.Parse(Request.QueryString["id"]));

            txtFirstName.Text = UserDetails.FirstName;
            txtLastName.Text = UserDetails.LastName;
            txtPassword.Text = UserDetails.Password;
            txtPhoneNumber.Text = UserDetails.PhoneNumber;
            txtAlternatePhoneNumber.Text = UserDetails.AlternatePhoneNumber;
            txtEmail.Text = UserDetails.Email;
            txtAlternateEmail.Text = UserDetails.AlternateEmail;
            txtDOB.Text = UserDetails.DOB;
            txtFavouriteColor.Text = UserDetails.Favouritecolor;
            txtAadhaar.Text = UserDetails.Aadhaar;
            txtPAN.Text = UserDetails.PAN;
            maritalStatus.SelectedValue = UserDetails.MaritalStatus;
            language.SelectedValue = UserDetails.PreferedLanguage;
            txtPrimaryEducation.Text = UserDetails.Upto10th;
            txtPercentageIn10th.Text = UserDetails.PercentageUpto10th.ToString();
            txtIntermediateEducation.Text = UserDetails.Upto12th;
            txtIntermediatePercentage.Text = UserDetails.PercentageUpto12th.ToString();
            txtBTech.Text = UserDetails.Graduation;
            txtBTechPercentage.Text = UserDetails.PercentageInGraduation.ToString();

            // permanent address 
            
            string PermanentaddressString = ListofAddresses[0].Address;

            if (!string.IsNullOrEmpty(PermanentaddressString))
            {
                string[] addressParts = PermanentaddressString.Split(',');

                if (addressParts.Length >= 3)
                {

                    List<string> countries = service.GetAllCountries();
                    for(int i = 0;i<countries.Count;i++) ddlPermanentCountry.Items.Add(countries[i]);                                      
                    ddlPermanentCountry.SelectedValue = addressParts[0].Trim();

                    List<string> StatesForCountry = service.GetStatesForCountry(addressParts[0].Trim());
                    for (int i = 0; i < StatesForCountry.Count; i++) ddlPermanentState.Items.Add(StatesForCountry[i]);
                    ddlPermanentState.SelectedValue = addressParts[1].Trim();


                    txtPermanentAddress.Text = addressParts[2].Trim();

                }
            }

            // present address
            string PresentaddressString = ListofAddresses[0].Address;

            if (!string.IsNullOrEmpty(PresentaddressString))
            {
                string[] addressParts = PresentaddressString.Split(',');

                if (addressParts.Length >= 3)
                {
                    List<string> countries = service.GetAllCountries();
                    for (int i = 0; i < countries.Count; i++) ddlPresentCountry.Items.Add(countries[i]);
                    ddlPresentCountry.SelectedValue = addressParts[0].Trim();

                    List<string> StatesForCountry = service.GetStatesForCountry(addressParts[0].Trim());
                    for (int i = 0; i < StatesForCountry.Count; i++) ddlPresentState.Items.Add(StatesForCountry[i]);
                    ddlPresentState.SelectedValue = addressParts[1].Trim();

                    txtPresentAddress.Text = addressParts[2].Trim();

                }
            }



        }
    }
}