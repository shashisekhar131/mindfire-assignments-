using DemoUserManagement.Models;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyService = DemoUserManagement.Business.Service;


namespace DemoUserManagementMVC.Controllers
{
    public class UserRegistrationController : Controller
    {
        static MyService service = new MyService();
        // GET: UserRegistration

        public ActionResult Index(int? id)
        {   
            
            var countries = service.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem { Value = c.CountryID.ToString(), Text = c.CountryName }).ToList();

            // details get filled into UserformData

            SessionClass sessionData = (SessionClass)Session["UserSession"];
            
               
            if (id!= null)
            {
                // populate data into form admin came to edit
                int UserID =(int) id;

                UserDetailsModel User = service.GetUserDetails(UserID);
                string UserRole = service.GetUserRoleForUserID(UserID);

                UserFormData UserInfo = new UserFormData
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Password = User.Password,
                    PhoneNumber = User.PhoneNumber,
                    AlternatePhoneNumber = User.AlternatePhoneNumber,
                    Email = User.Email,
                    AlternateEmail = User.AlternateEmail,
                    FavoriteColor = User.Favouritecolor,
                    DateOfBirth = User.DOB,
                    MaritalStatus = User.MaritalStatus,
                    PreferredLanguage = User.PreferedLanguage,
                    PresentCountry = User.PresentAddress.CountryID.ToString(),
                    PermanentCountry = User.PresentAddress.CountryID.ToString(),
                    PresentState = User.PresentAddress.StateID.ToString(),
                    PermanentState = User.PresentAddress.StateID.ToString(),
                    PresentAddress = User.PresentAddress.Address,
                    PermanentAddress = User.PermanentAddress.Address,
                    PrimaryEducation = User.Upto10th,
                    PercentageIn10th = (int)User.PercentageUpto10th,
                    IntermediateEducation = User.Upto12th,
                    IntermediatePercentage = (int)User.PercentageUpto12th,
                    BTech = User.Graduation,
                    BTechPercentage = (int)User.PercentageInGraduation,
                    UserRole = UserRole,
                };


                TempData["UserID"] = UserID;
                return View(UserInfo);
                
            }           

            return View(new UserFormData());
        }

        


        [HttpPost]
        public ActionResult FormSubmitted(UserFormData model)
        {
            if (ModelState.IsValid)
            {

                UserDetailsModel UserDetails = new UserDetailsModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    AlternatePhoneNumber = model.AlternatePhoneNumber,
                    Email = model.Email,
                    AlternateEmail = model.AlternateEmail,
                    DOB = model.DateOfBirth.ToString(),
                    Favouritecolor = model.FavoriteColor,
                    MaritalStatus = model.MaritalStatus,
                    PreferedLanguage = model.PreferredLanguage,
                    Aadhaar = "",
                    PAN = "",
                    Upto10th = model.PrimaryEducation,
                    PercentageUpto10th = model.PercentageIn10th,
                    Upto12th = model.IntermediateEducation,
                    PercentageUpto12th = model.IntermediatePercentage,
                    Graduation = model.BTech,
                    PercentageInGraduation = model.BTechPercentage,
                    PresentAddress = new AddressDetailsModel
                    {
                        Address = model.PresentAddress,
                        Type = (int)Enums.AddressType.Present,
                        CountryID = int.Parse(model.PresentCountry),
                        StateID = int.Parse(model.PresentState)

                    },
                    PermanentAddress = new AddressDetailsModel
                    {
                        Address = model.PermanentAddress,
                        Type = (int)Enums.AddressType.Permanent,
                        CountryID = int.Parse(model.PermanentCountry),
                        StateID = int.Parse(model.PermanentState)

                    }
                };

                string UserSelectedRole = model.UserRole;
                int RoleID = service.GetRoleIDForRole(UserSelectedRole);
                UserDetails.RoleID = RoleID;

                if (TempData["UserID"] != null) {
                    UserDetails.UserID =(int)TempData["UserID"];
                    service.UpdateUser(UserDetails);
                    return RedirectToAction("EditUser", "Users", new { id = TempData["UserID"] });

                }
                else {
                    Dictionary<string, int> InsertedUser = service.InsertUser(UserDetails);
                    return RedirectToAction("Index", "Login");
                }
            }

            // If validation fails, reload the page with the model and validation errors
            var countries = service.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem { Value = c.CountryID.ToString(), Text = c.CountryName }).ToList();

            return View("Index",model);
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


    }
}