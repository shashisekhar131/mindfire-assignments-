using DemoUserManagement.Models;
using DemoUserManagement.Utils;
using DemoUserManagementMVC.Helper;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagementMVC.Controllers
{
    public class UserRegistrationV2Controller : Controller
    {
        static MyService service = new MyService();

        // GET: UserRegistrationV2
        [CustomAuthorizationFilterAttributeV2]
        public ActionResult Index(int? id)
        {
            TempData["UserID"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult GetStatesForCountry(int SelectedCountryID)
        {
            // Your logic to retrieve states based on the selected country
            var States = service.GetStatesForCountry(SelectedCountryID);


            return Json(States, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetCountries()
        {
            // Your logic to retrieve states based on the selected country
            var Countries = service.GetAllCountries();


            return Json(Countries, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int CheckIfEmailExists(string Email, int UserID)
        {

            int IsEmailExists = service.CheckIfEmailExists(Email, UserID);

            return IsEmailExists;
        }

        [HttpPost]
        public  ActionResult Submit_Form(UserFormData UserFormData, int UserId)
        {
            
                // ceate user Deatails model from form data and send to insert or update 
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
                    Favouritecolor = UserFormData.FavouriteColor,
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
                    return Json(Message, JsonRequestBehavior.AllowGet);
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

                    Session["UserSession"] = sessionData;
                    return Json(InsertedUser, JsonRequestBehavior.AllowGet);
                }         
           

        }




        [HttpPost]
        public JsonResult UploadDocument(HttpPostedFileBase file, int DocumentType, int ObjectType, int ObjectID)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ExternalFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("FolderPath");

                    if (!Directory.Exists(ExternalFolderPath))
                    {
                        Directory.CreateDirectory(ExternalFolderPath);
                    }

                    // Generate a Unique identifier (Guid) for the file name
                    Guid UniqueGuid = Guid.NewGuid();

                    string fileExtension = Path.GetExtension(file.FileName);

                    // Combine the Guid with the file extension to create a Unique file name
                    string UniqueFileName = UniqueGuid.ToString() + fileExtension;

                    // Save file to External folder
                    string filePath = Path.Combine(ExternalFolderPath, UniqueFileName);
                    file.SaveAs(filePath);


                    service.InsertDocument(file.FileName, UniqueFileName, ObjectID, ObjectType, DocumentType);

                    return Json(new { success = true, message = "File uploaded successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "No file selected" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error uploading file: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Download(string fileName)
        {
            // Ensure fileName is not null or empty, handle errors if needed

            string filePath = Path.Combine(ConfigurationManager.AppSettings.Get("FolderPath"), fileName);

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                string mimeType = MimeMapping.GetMimeMapping(fileName);

                // Set the content type based on the file extension
                Response.ContentType = mimeType;

                // Set the content-disposition header to force the browser to prompt for download
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                // Write the file content to the response
                Response.BinaryWrite(fileBytes);

                // End the response to avoid any additional content being sent
                Response.End();

                // Optionally, you can return an empty result or another action result
                return new EmptyResult();
            }
            else
            {
                // Handle file not found
                return HttpNotFound();
            }
        }

        [HttpPost]
        public  JsonResult GetUserData(int UserId)
        {
            UserDetailsModel UserDetails = service.GetUserDetails(UserId);



            string UserRole = service.GetUserRoleForUserID(UserId);
            UserFormData FormData = new UserFormData
            {
                FirstName = UserDetails.FirstName,
                LastName = UserDetails.LastName,
                Password = UserDetails.Password,
                PhoneNumber = UserDetails.PhoneNumber,
                AlternatePhoneNumber = UserDetails.AlternatePhoneNumber,
                Email = UserDetails.Email,
                AlternateEmail = UserDetails.AlternateEmail,
                DateOfBirth = DateTime.Now.ToString(),
                FavouriteColor = UserDetails.Favouritecolor,
                MaritalStatus = UserDetails.MaritalStatus,
                PreferredLanguage = UserDetails.PreferedLanguage,
                PresentCountry = UserDetails.PresentAddress.CountryID.ToString(),
                PermanentCountry = UserDetails.PermanentAddress.CountryID.ToString(),
                PresentState = UserDetails.PresentAddress.StateID.ToString(),
                PermanentState = UserDetails.PermanentAddress.StateID.ToString(),
                PresentAddress = UserDetails.PresentAddress.Address, 
                PermanentAddress = UserDetails.PermanentAddress.Address, 
                PrimaryEducation = UserDetails.Upto10th,
                PercentageIn10th = (int)UserDetails.PercentageUpto10th,
                IntermediateEducation = UserDetails.Upto12th,
                IntermediatePercentage = (int)UserDetails.PercentageUpto12th,
                BTech = UserDetails.Graduation,
                BTechPercentage = (int)UserDetails.PercentageInGraduation,
                UserRole = UserRole,

            };

            return Json(FormData, JsonRequestBehavior.AllowGet);
        }


    


    }
}