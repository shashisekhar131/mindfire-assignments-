using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DemoUserManagement.Models;

namespace DemoUserManagement.DAL
{
    public class DataAccess
    {

        // insert new user into database
        public List<int> InsertUser(UserDetailsModel NewUser,List<AddressDetailsModel> ListofAddresses)
        {
            int flag = 0;

            List<int> Resultlist = new List<int>();

            Resultlist.Add(flag);

            try
            {
                using (var context = new UserManagementEntities())
                {

                    // create user object
                    var newUser = new UserDetail
                    {
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName,
                        Password = NewUser.Password,
                        PhoneNumber = NewUser.PhoneNumber,
                        AlternatePhoneNumber = NewUser.AlternatePhoneNumber,
                        Email = NewUser.Email,
                        AlternateEmail = NewUser.AlternateEmail,
                        DOB = NewUser.DOB,
                        Favouritecolor = NewUser.Favouritecolor,
                        Aadhaar = NewUser.Aadhaar,
                        PAN = NewUser.PAN,
                        MaritalStatus = NewUser.MaritalStatus,
                        PreferedLanguage = NewUser.PreferedLanguage,
                        Upto10th = NewUser.Upto10th,
                        PercentageUpto10th = NewUser.PercentageUpto10th,
                        Upto12th = NewUser.Upto12th,
                        PercentageUpto12th = NewUser.PercentageUpto12th,
                        Graduation = NewUser.Graduation,
                        PercentageInGraduation = NewUser.PercentageInGraduation
                    };

                    
                    // pass it to context API 
                    context.UserDetails.Add(newUser);

                    context.SaveChanges();

                    // Get the maximum UserID
                    int maxUserId = context.UserDetails.Max(u => (int?)u.UserID) ?? 0;
                    Resultlist.Add(maxUserId);

                    var PresentAddress = new AddressDetail
                    {
                        Address = ListofAddresses[0].Address,
                        Type = ListofAddresses[0].Type,
                        UserID = maxUserId
                    };

                    var PermenantAddress = new AddressDetail
                    {
                        Address = ListofAddresses[1].Address,
                        Type = ListofAddresses[1].Type,
                        UserID = maxUserId
                    };
                    context.AddressDetails.Add(PermenantAddress);
                    context.AddressDetails.Add(PresentAddress);

                    context.SaveChanges();
                    Resultlist[0] = 1; // flag = 1
                }
            }
            catch (DbUpdateException ex)
            {
               
            }
           
            return Resultlist;

        }


        // get user details and show them on grid View
        public UserDetailsModel GetUserDetails(int UserId)
        {
            UserDetailsModel userDetails = null;

            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieveing user details based on UserId
                    var userDetailEntity = context.UserDetails.FirstOrDefault(u => u.UserID == UserId);

                    // Check if the user was found
                    if (userDetailEntity != null)
                    {
                        // Maping  entity properties to UserDetailsModel
                        userDetails = new UserDetailsModel
                        {
                            FirstName = userDetailEntity.FirstName,
                            LastName = userDetailEntity.LastName,
                            Password = userDetailEntity.Password,
                            PhoneNumber = userDetailEntity.PhoneNumber,
                            AlternatePhoneNumber = userDetailEntity.AlternatePhoneNumber,
                            Email = userDetailEntity.Email,
                            AlternateEmail = userDetailEntity.AlternateEmail,
                            DOB = userDetailEntity.DOB,
                            Favouritecolor = userDetailEntity.Favouritecolor,
                            Aadhaar = userDetailEntity.Aadhaar,
                            PAN = userDetailEntity.PAN,
                            PreferedLanguage = userDetailEntity.PreferedLanguage,
                            MaritalStatus = userDetailEntity.MaritalStatus,
                            Upto10th = userDetailEntity.Upto10th,
                            PercentageUpto10th = userDetailEntity.PercentageUpto10th,
                            Upto12th = userDetailEntity.Upto12th,
                            PercentageUpto12th = userDetailEntity.PercentageUpto12th,
                            Graduation = userDetailEntity.Graduation,
                            PercentageInGraduation = userDetailEntity.PercentageInGraduation
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return userDetails;
        }



        // get the address details of userId
        public List<AddressDetailsModel> GetAddresses(int UserId)
        {
            List<AddressDetailsModel> ListOfAddresses = new List<AddressDetailsModel>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve addresses based on UserId
                    var addressDetailEntities = context.AddressDetails.Where(a => a.UserID == UserId).ToList();

                    // Check if any addresses were found
                    if (addressDetailEntities != null && addressDetailEntities.Any())
                    {
                        foreach (var addressDetailEntity in addressDetailEntities)
                        {
                            // Mapping entity properties to AddressDetailsModel
                            var addressDetailsModel = new AddressDetailsModel
                            {
                                Address = addressDetailEntity.Address,
                                Type = addressDetailEntity.Type,                                
                            };

                            // Add the address details to the list
                            ListOfAddresses.Add(addressDetailsModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return ListOfAddresses;
        }

        public bool UpdateUser(UserDetailsModel UserInfo,List<AddressDetailsModel> ListofAddresses,int IdToUpdate)
        {
            
            bool flag = false;
           
            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve the user details from the database based on the user ID
                    var existingUser = context.UserDetails.FirstOrDefault(u => u.UserID == IdToUpdate);

                    if (existingUser != null)
                    {
                        // Update user details
                        existingUser.FirstName = UserInfo.FirstName;
                        existingUser.LastName = UserInfo.LastName;
                        existingUser.Password = UserInfo.Password;
                        existingUser.PhoneNumber = UserInfo.PhoneNumber;
                        existingUser.AlternatePhoneNumber = UserInfo.AlternatePhoneNumber;
                        existingUser.Email = UserInfo.Email;
                        existingUser.AlternateEmail = UserInfo.AlternateEmail;
                        existingUser.DOB = UserInfo.DOB;
                        existingUser.Favouritecolor = UserInfo.Favouritecolor;
                        existingUser.Aadhaar = UserInfo.Aadhaar;
                        existingUser.PAN = UserInfo.PAN;
                        existingUser.MaritalStatus = UserInfo.MaritalStatus;
                        existingUser.PreferedLanguage = UserInfo.PreferedLanguage;
                        existingUser.Upto10th = UserInfo.Upto10th;
                        existingUser.PercentageUpto10th = UserInfo.PercentageUpto10th;
                        existingUser.Upto12th = UserInfo.Upto12th;
                        existingUser.PercentageUpto12th = UserInfo.PercentageUpto12th;
                        existingUser.Graduation = UserInfo.Graduation;
                        existingUser.PercentageInGraduation = UserInfo.PercentageInGraduation;

                        var presentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == "present address");
                        var permanentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == "permanent address");

                        if (presentAddress != null && permanentAddress != null)
                        {
                            // Update addresses if they exist
                            presentAddress.Address = ListofAddresses[0].Address;
                            permanentAddress.Address = ListofAddresses[1].Address;

                            // Save changes to the database
                            
                        }
                        else
                        {
                            // Handle the case where addresses are not found
                            // You may choose to insert new records or log an error
                        }

                        context.SaveChanges();
                        flag = true;
                    }
                    
                }
            }
            catch (DbUpdateException ex)
            {
            }

            return flag;
        }


        public bool InsertNotes(string InputNoteText, int UserId,string Page)
        {
            bool flag = false;

            try
            {
                 using (var context = new UserManagementEntities())
                 {
                    Note newNote = new Note
                    {
                        NoteText = InputNoteText,
                        UserID = UserId,
                        Page = Page,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
                    };

                   
                    // Add the new note to the context
                    context.Notes.Add(newNote);

                    // Save changes to the database
                    context.SaveChanges();
                    flag = true;
                    // Refresh the displayed notes
                   
                }
            }
            catch (Exception ex)
            {
            }
            return flag;
        }


        public List<NoteModel> GetNotes(int UserId,string Name)
        {

            List <NoteModel> ListofNotes = new List<NoteModel>();
            try
            {
                using (var context = new UserManagementEntities())
                {

                    var userNotes = context.Notes
                        .Where(n => n.UserID == UserId && n.Page == Name)
                        .ToList();

                    ListofNotes = userNotes.Select(note => new NoteModel
                    {
                        NoteText=note.NoteText,
                        NotesID = note.NotesID,
                        CreatedDate = note.CreatedDate

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return ListofNotes;
        }
    }
}
