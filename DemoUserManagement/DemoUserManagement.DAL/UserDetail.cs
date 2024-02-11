//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoUserManagement.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserDetail()
        {
            this.AddressDetails = new HashSet<AddressDetail>();
            this.Notes = new HashSet<Note>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }
    }
}
