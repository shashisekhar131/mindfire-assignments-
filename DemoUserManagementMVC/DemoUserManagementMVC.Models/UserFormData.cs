using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DemoUserManagement.Models
{
    public class UserFormData
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RetypePassword { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        public string AlternateEmail { get; set; }

       
        public string DateOfBirth { get; set; }

        public string FavoriteColor { get; set; }

        [Required(ErrorMessage = "Marital Status is required.")]
        public string MaritalStatus { get; set; }

        public string PreferredLanguage { get; set; }

        public string PresentCountry { get; set; }

        public string PermanentCountry { get; set; }

        public string PresentState { get; set; }

        public string PermanentState { get; set; }

        public string PresentAddress { get; set; }

        public string PermanentAddress { get; set; }

        public string PrimaryEducation { get; set; }

        [Required(ErrorMessage = "Percentage in 10th is required.")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public int PercentageIn10th { get; set; }

        public string IntermediateEducation { get; set; }

        [Required(ErrorMessage = "Intermediate Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public int IntermediatePercentage { get; set; }

        public string BTech { get; set; }

        [Required(ErrorMessage = "BTech Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public int BTechPercentage { get; set; }

        public string UserRole { get; set; }

        // Additional models for related data
        public CountryModel CountryModel { get; set; }
        public StateModel StateModel { get; set; }
        // Add any other properties as needed
    }
}
