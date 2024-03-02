﻿using DemoUserManagement.Models;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoUserManagement.Business;


using MyService = DemoUserManagement.Business.Service;
using System.Web.Services;
using static System.Net.Mime.MediaTypeNames;


namespace DemoUserManagement
{


    public partial class UserDetails : BasePage
    {

        static MyService service = new MyService();

        protected void Page_Load(object sender, EventArgs e)
        {

                      

            NotesInUsersPage.Visible = false;
            UploadInUsersPage.Visible = false;

            if (Request.QueryString["id"] != null)
            {
                /*if (!AuthenticateUser())
                {
                    Response.Redirect("~/LoginPage.aspx");
                }*/
               
                NotesInUsersPage.Visible = true;
                UploadInUsersPage.Visible = true;
            }

        }

       /* [WebMethod]
        public static List<CountryModel> GetCountries()
        {
            List<CountryModel> Countries = service.GetAllCountries();

            return Countries;
        }

        [WebMethod]
        public static List<StateModel> GetStatesForCountry(int SelectedCountryID)
        {

            List<StateModel> States = service.GetStatesForCountry(SelectedCountryID);

            return States;
        }

        [WebMethod]
        public static int CheckIfEmailExists(string Email,int UserID)
        {

            int IsEmailExists = service.CheckIfEmailExists(Email,UserID);

            return IsEmailExists;
        }

        [WebMethod]
        public static Dictionary<string, int> Submit_Form(UserFormData UserFormData, int UserId)
        {
            // instead of UserInfo use UserDetailsModel 

            UserDetailsModel UserInfo = TakeValuesFromForm(UserFormData);


            // edit user 
            if (UserId != 0)
            {
                Dictionary<string, int> Message = new Dictionary<string, int>();

                UserInfo.UserID = UserId;
                if (service.UpdateUser(UserInfo))
                {
                    Message["Updated"] = 1;
                }
                else
                {
                    Message["Updated"] = 0;
                }
                return Message;
            }
            else
            {// insert new user

                Dictionary<string, int> InsertedUser = service.InsertUser(UserInfo);

                HttpContext.Current.Session["UserID"] = InsertedUser["UserID"];

                return InsertedUser;
            }




        }

        public static UserDetailsModel TakeValuesFromForm(UserFormData UserFormData)
        {



            UserDetailsModel UserInfo = new UserDetailsModel
            {
                FirstName = UserFormData.FirstName,
                LastName = UserFormData.LastName,
                Password = UserFormData.Password,
                PhoneNumber = UserFormData.PhoneNumber,
                AlternatePhoneNumber = UserFormData.AlternatePhoneNumber,
                Email = UserFormData.Email,
                AlternateEmail = UserFormData.AlternateEmail,
                DOB = UserFormData.DateOfBirth.ToString(),
                Favouritecolor = UserFormData.FavoriteColor,
                MaritalStatus = UserFormData.MaritalStatus,
                PreferedLanguage = UserFormData.PreferredLanguage,
                Aadhaar = "",
                PAN = "",
                Upto10th = UserFormData.PrimaryEducation,
                PercentageUpto10th = UserFormData.PercentageIn10th,
                Upto12th = UserFormData.IntermediateEducation,
                PercentageUpto12th = UserFormData.IntermediatePercentage,
                Graduation = UserFormData.BTech,
                PercentageInGraduation = UserFormData.BTechPercentage, 
                PresentAddress = new AddressDetailsModel
                {
                    Address = UserFormData.PresentAddress,
                    Type = (int)Enums.AddressType.Present,
                    CountryID = int.Parse(UserFormData.PresentCountry),
                    StateID = int.Parse(UserFormData.PresentState),

                },
                PermanentAddress = new AddressDetailsModel
                {
                    Address = UserFormData.PermanentAddress,
                    Type = (int)Enums.AddressType.Permanent,
                    CountryID = int.Parse(UserFormData.PermanentCountry),
                    StateID = int.Parse(UserFormData.PermanentState)

                }


        };

            string UserSelectedRole = UserFormData.UserRole;
            HttpContext.Current.Session["UserRole"] = UserSelectedRole;
            int RoleID = service.GetRoleIDForRole(UserSelectedRole);           
            
            UserInfo.RoleID = RoleID;

            return (UserInfo);
        }

        [WebMethod]
        public static UserFormData GetUserData(int UserId)
        {
            UserDetailsModel UserDetails = service.GetUserDetails(UserId);


            string UserRole = service.GetUserRoleForUserID(UserId);
            UserFormData FormData = new UserFormData
            {
                FirstName = UserDetails.FirstName,
                LastName = UserDetails.LastName,
                Password = UserDetails.Password,
                RetypePassword = "", // You may need to set a value depending on your logic
                PhoneNumber = UserDetails.PhoneNumber,
                AlternatePhoneNumber = UserDetails.AlternatePhoneNumber,
                Email = UserDetails.Email,
                AlternateEmail = UserDetails.AlternateEmail,
                DateOfBirth = DateTime.Now,
                FavoriteColor = UserDetails.Favouritecolor,
                MaritalStatus = UserDetails.MaritalStatus,
                PreferredLanguage = UserDetails.PreferedLanguage,
                PresentCountry = UserDetails.PresentAddress.CountryID.ToString(),
                PermanentCountry = UserDetails.PermanentAddress.CountryID.ToString(),
                PresentState = UserDetails.PresentAddress.StateID.ToString(),
                PermanentState = UserDetails.PermanentAddress.StateID.ToString(),
                PresentAddress = UserDetails.PresentAddress.Address, // You may need to set a value depending on your logic
                PermanentAddress = UserDetails.PermanentAddress.Address, // You may need to set a value depending on your logic
                PrimaryEducation = UserDetails.Upto10th,
                PercentageIn10th = (int)UserDetails.PercentageUpto10th,
                IntermediateEducation = UserDetails.Upto12th,
                IntermediatePercentage = (int)UserDetails.PercentageUpto12th,
                BTech = UserDetails.Graduation,
                BTechPercentage = (int)UserDetails.PercentageInGraduation,
                UserRole = UserRole,
                
            };

            return FormData;
        }

        [WebMethod]
        public static string GetCountryName(int CountryID)
        {
            string Name= service.GetCountryName(CountryID);
            return Name;
        }
        [WebMethod]
        public static string GetStateName(int StateID)
        {
            string Name= service.GetStateName(StateID);
            return Name;
        }

        protected string SaveFile(FileUpload fileUpload)
        {
            string ExternalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("ServerPath");
            if (!Directory.Exists(ExternalFolderPath))
            {
                Directory.CreateDirectory(ExternalFolderPath);
            }


            // Generate a unique identifier (Guid) for the file name
            Guid uniqueGuid = Guid.NewGuid();

            string fileExtension = Path.GetExtension(fileUpload.FileName);

            // Combine the Guid with the file extension to create a unique file name
            string uniqueFileName = uniqueGuid.ToString() + fileExtension;

            // save file to external folder 
            fileUpload.SaveAs(ExternalFolderPath + uniqueFileName);

            return uniqueFileName;
        }*/
            /*
            private void LoadCountriesAndStates()
            {
                // Call a method to get the list of countries from the database
                List<string> countries = service.GetAllCountries();

                // Bind the countries to the ddlPresentCountry DropDownList
                ddlPresentCountry.DataSource = countries;
                ddlPresentCountry.DataBind();

                ddlPermanentCountry.DataSource = countries;
                ddlPermanentCountry.DataBind();

                List<string> states = service.GetStatesForCountry(countries[0]);

                ddlPermanentState.DataSource = states;
                ddlPermanentState.DataBind();

                ddlPresentState.DataSource = states;
                ddlPresentState.DataBind();

            }

            public void DdlPresentCountry_SelectedIndexChanged(object sender, EventArgs e)
            {
                // Get the selected country from the DropDownList
                string selectedCountry = ddlPresentCountry.SelectedValue;

                List<string> StatesForCountry = service.GetStatesForCountry(selectedCountry);

                ddlPresentState.DataSource = StatesForCountry;
                ddlPresentState.DataBind();
            }

            public void DdlPermanentCountry_SelectedIndexChanged(object sender, EventArgs e)
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
                    UpdateUser(UserInfo, ListofAddresses, int.Parse(Request.QueryString["id"]));
                }
                else
                {
                    InsertNewUser(UserInfo, ListofAddresses);
                }

            }


            public void InsertNewUser(UserDetailsModel NewUser, List<AddressDetailsModel> ListofAddresses)
            {
                Dictionary<string, int> InsertedUser = service.InsertUser(NewUser, ListofAddresses);
                // ResultList[0] - flag 
                // ResultList[1] - NewUserId inserted into db

                // show single user
                //  if (ResultList[0] == 1) Response.Redirect("~/Users.aspx?id=" + ResultList[1]);
                // show multiple users

                string aadharFileName = string.Empty;
                string panFileName = string.Empty;
                if (fuAadhar.HasFile)
                {
                    aadharFileName = SaveFile(fuAadhar);
                    service.InsertDocument(fuAadhar.FileName, aadharFileName, InsertedUser["UserID"], (int)Enums.ObjecType.Users, (int)Enums.DocumentType.Others);

                }

                if (fuPAN.HasFile)
                {
                    panFileName = SaveFile(fuPAN);
                    service.InsertDocument(fuPAN.FileName, panFileName, InsertedUser["UserID"], (int)Enums.ObjecType.Users, (int)Enums.DocumentType.Others);

                }
                if (InsertedUser["flag"] == 1 && InsertedUser["RoleId"] == 1) Response.Redirect("~/Users.aspx");
                else if (InsertedUser["flag"] == 1 && InsertedUser["RoleId"] == 2) UpdateUser(NewUser, ListofAddresses, InsertedUser["UserID"]);

            }

            public void UpdateUser(UserDetailsModel UserInfo, List<AddressDetailsModel> ListofAddresses, int IdToUpdate)
            {
                if (service.UpdateUser(UserInfo, ListofAddresses, IdToUpdate)) Response.Redirect("~/Users.aspx?id=" + IdToUpdate);
            }

            public (UserDetailsModel, List<AddressDetailsModel>) TakeValuesFromForm()
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
                    Aadhaar = "",
                    PAN = "",
                    MaritalStatus = maritalStatus.SelectedValue,
                    PreferedLanguage = language.SelectedValue,
                    Upto10th = txtPrimaryEducation.Text,
                    PercentageUpto10th = int.Parse(txtPercentageIn10th.Text),
                    Upto12th = txtIntermediateEducation.Text,
                    PercentageUpto12th = int.Parse(txtIntermediatePercentage.Text),
                    Graduation = txtBTech.Text,
                    PercentageInGraduation = int.Parse(txtBTechPercentage.Text)
                };

                List<int> ids = service.GetCountryAndStateID(ddlPresentCountry.SelectedValue, ddlPresentState.SelectedValue);


                AddressDetailsModel PresentAddress = new AddressDetailsModel
                {
                    Address = txtPresentAddress.Text,
                    Type = (int)Enums.AddressType.Present,
                    CountryID = ids[0],
                    StateID = ids[1]
                    *//*  "present address" - 0 */
        
        /*

                };

                List<int> ids2 = service.GetCountryAndStateID(ddlPermanentCountry.SelectedValue, ddlPermanentState.SelectedValue);

                AddressDetailsModel PermanentAddress = new AddressDetailsModel
                {
                    Address = txtPermanentAddress.Text,
                    Type = (int)Enums.AddressType.Permanent,
                    CountryID = ids2[0],
                    StateID = ids2[1]
                    *//* "permanent address " - 1 *//*

                };
                List<AddressDetailsModel> ListofAddresses = new List<AddressDetailsModel>();

                ListofAddresses.Add(PresentAddress);
                ListofAddresses.Add(PermanentAddress);

                return (UserInfo, ListofAddresses);
            }



            protected string SaveFile(FileUpload fileUpload)
            {
                string ExternalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("ServerPath");
                if (!Directory.Exists(ExternalFolderPath))
                {
                    Directory.CreateDirectory(ExternalFolderPath);
                }


                // Generate a unique identifier (Guid) for the file name
                Guid uniqueGuid = Guid.NewGuid();

                string fileExtension = Path.GetExtension(fileUpload.FileName);

                // Combine the Guid with the file extension to create a unique file name
                string uniqueFileName = uniqueGuid.ToString() + fileExtension;

                // save file to external folder 
                fileUpload.SaveAs(ExternalFolderPath + uniqueFileName);

                return uniqueFileName;
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
                maritalStatus.SelectedValue = UserDetails.MaritalStatus;
                language.SelectedValue = UserDetails.PreferedLanguage;
                txtPrimaryEducation.Text = UserDetails.Upto10th;
                txtPercentageIn10th.Text = UserDetails.PercentageUpto10th.ToString();
                txtIntermediateEducation.Text = UserDetails.Upto12th;
                txtIntermediatePercentage.Text = UserDetails.PercentageUpto12th.ToString();
                txtBTech.Text = UserDetails.Graduation;
                txtBTechPercentage.Text = UserDetails.PercentageInGraduation.ToString();

                // permanent address 

                List<string> Names = service.GetCountryAndStateNames(ListofAddresses[1].CountryID, ListofAddresses[1].StateID);

                // when page loads dropdown doesn't have <select> tags in it we have to populate them
                List<string> countries = service.GetAllCountries();
                for (int i = 0; i < countries.Count; i++) ddlPermanentCountry.Items.Add(countries[i]);
                ddlPermanentCountry.SelectedValue = Names[0];

                List<string> StatesForCountry = service.GetStatesForCountry(Names[0]);
                for (int i = 0; i < StatesForCountry.Count; i++) ddlPermanentState.Items.Add(StatesForCountry[i]);
                ddlPermanentState.SelectedValue = Names[1];

                txtPermanentAddress.Text = ListofAddresses[1].Address;

                // present address
                List<string> Names2 = service.GetCountryAndStateNames(ListofAddresses[0].CountryID, ListofAddresses[0].StateID);

                List<string> countries2 = service.GetAllCountries();
                for (int i = 0; i < countries2.Count; i++) ddlPresentCountry.Items.Add(countries2[i]);
                ddlPresentCountry.SelectedValue = Names2[0];

                List<string> StatesForCountry2 = service.GetStatesForCountry(Names2[0]);
                for (int i = 0; i < StatesForCountry2.Count; i++) ddlPresentState.Items.Add(StatesForCountry2[i]);
                ddlPresentState.SelectedValue = Names2[1];

                txtPresentAddress.Text = ListofAddresses[0].Address;

            }
       */
        
    }
}