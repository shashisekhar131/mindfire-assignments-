using DemoUserManagement.Models;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using static DemoUserManagement.Utils.Enums;
using MyService = DemoUserManagement.Business.Service;


namespace DemoUserManagement
{
    public class BasePage: System.Web.UI.Page
    {
        static MyService service = new MyService();

        public object StoredSession { get; private set; }

        protected void Page_Init()
        {
            string PageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);

            // not login and accessed other pages
            if (PageName != "LoginPage" && !AuthenticateUser())
            {       
                Response.Redirect("~/LoginPage.aspx");
            }

            //  access users.aspx
            if (PageName == "Users" && !AuthorizeUser())
            {
                Response.Redirect("~/Unauthorized.aspx");
            }

            // login but again came to login so redirect to page accordingly admin/standard
            SessionClass StoredSession = HttpContext.Current.Session["UserSession"] as SessionClass;
            if (PageName == "LoginPage" && AuthenticateUser())
            {                
                Response.Redirect("~/UserDetails.aspx?id="+StoredSession.UserID);              
                
            }

           if(PageName == "UserDetails")
            {
                if (Request.QueryString["id"] != null)
                {
                    if (AuthorizeUser())
                    {
                        
                    }
                    else if(int.Parse(Request.QueryString["id"])== StoredSession.UserID)
                    {
                        
                    }
                    else
                    {
                        Response.Redirect("~/UserDetails.aspx?id=" + StoredSession.UserID);
                    }
                }
            }        

            
        }

        protected bool AuthenticateUser()
        {

            SessionClass storedSession = HttpContext.Current.Session["UserSession"] as SessionClass;           

            if (storedSession != null)
            {   // logged in return true
                return true;
            }

            return false;

        }

        protected bool AuthorizeUser()
        {

            SessionClass storedSession = HttpContext.Current.Session["UserSession"] as SessionClass;

            if (storedSession != null && storedSession.UserRole == 1)
            {   // user is admin return true
                return true;
            }

            return false;

        }     





        [WebMethod]
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
        public static int CheckIfEmailExists(string Email, int UserID)
        {

            int IsEmailExists = service.CheckIfEmailExists(Email, UserID);

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


                // when new user registered store his data in session 
                // set the session data in session 
                SessionClass sessionData = new SessionClass
                {
                    UserID = InsertedUser["UserID"], 
                    UserRole = InsertedUser["RoleID"]
                };

                HttpContext.Current.Session["UserSession"] = sessionData;

                return InsertedUser;
            }




        }


        [WebMethod]
        public static Dictionary<string, int> CheckIfUserExists(string UserEmail, string UserPassword)
        {

            Dictionary<string, int> User = service.CheckIfUserExists(UserEmail, UserPassword);
            
            // when user logged in store his data in session 
            if (User["IsUserExists"] == 1)
            {
                SessionClass sessionData = new SessionClass
                {
                    UserID = User["UserID"],
                    UserRole = User["RoleID"]
                };

                HttpContext.Current.Session["UserSession"] = sessionData;
            }
            return User;
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
            string Name = service.GetCountryName(CountryID);
            return Name;
        }
        [WebMethod]
        public static string GetStateName(int StateID)
        {
            string Name = service.GetStateName(StateID);
            return Name;
        }

        [WebMethod]
        public static string InsertNotes(string NoteText,int UserID,int ObjectType)
        {
            if(service.InsertNotes(NoteText, UserID, ObjectType))
            {
                return "inserted";
            }
            else
            {
                return "error";
            }



        }
    }
}
