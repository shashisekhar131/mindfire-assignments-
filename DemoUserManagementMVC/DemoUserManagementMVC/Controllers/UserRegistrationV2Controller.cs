using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyService = DemoUserManagement.Business.Service;

namespace DemoUserManagementMVC.Controllers
{
    public class UserRegistrationV2Controller : Controller
    {
        static MyService service = new MyService();

        // GET: UserRegistrationV2
        public ActionResult Index()
        {
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

    }
}