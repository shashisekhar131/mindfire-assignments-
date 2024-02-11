using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class UserDetailsModel
    {

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string DOB { get; set; }
        public string Favouritecolor { get; set; }
        public string Aadhaar { get; set; }
        public string PAN { get; set; }
        public string PreferedLanguage { get; set; }
        public string MaritalStatus { get; set; }
        public string Upto10th { get; set; }
        public Nullable<int> PercentageUpto10th { get; set; }
        public string Upto12th { get; set; }
        public Nullable<int> PercentageUpto12th { get; set; }
        public string Graduation { get; set; }
        public Nullable<int> PercentageInGraduation { get; set; }

    }
}
