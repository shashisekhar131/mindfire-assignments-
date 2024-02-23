using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class UserFormData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FavoriteColor { get; set; }
        public string MaritalStatus { get; set; }
        public string PreferredLanguage { get; set; }
        public string PresentCountry { get; set; }
        public string PermanentCountry { get; set; }
        public string PresentState { get; set; }
        public string PermanentState { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PrimaryEducation { get; set; }
        public int PercentageIn10th { get; set; }
        public string IntermediateEducation { get; set; }
        public int IntermediatePercentage { get; set; }
        public string BTech { get; set; }
        public int BTechPercentage { get; set; }
        public string UserRole { get; set; }

        public CountryModel CountryModel { get; set; }
        public StateModel StateModel { get; set; }
        // Add any other properties as needed
    }
}
