using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DemoUserManagement.Models;
using DemoUserManagement.Utils;

namespace DemoUserManagement.DAL
{
    public class DataAccess
    {

        // insert new user into database
        public Dictionary<string, int> InsertUser(UserDetailsModel NewUser,List<AddressDetailsModel> ListofAddresses,int RoleID)
        {

                      
            Dictionary<string, int> InsertedUser = new Dictionary<string, int>();

            InsertedUser["flag"] = 0; // flag - 0 

            try
            {
                using (var Context = new UserManagementEntities())
                {

                    // create user object
                    var TempNewUser = new UserDetail
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

                    
                    // pass it to Context API 
                    Context.UserDetails.Add(TempNewUser);

                    Context.SaveChanges();

                    // Get the last inserted UserID     

                    int LastInsertedUserId = TempNewUser.UserID;
                    

                    InsertedUser["RoleId"] = RoleID;
                    InsertedUser["UserID"] = LastInsertedUserId;
                    

                    var TempUserRole = new UserRole
                    {
                        UserID = LastInsertedUserId,
                        RoleID = RoleID
                    };

                    Context.UserRoles.Add(TempUserRole);

                    var PresentAddress = new AddressDetail
                    {
                        Address = ListofAddresses[0].Address,
                        Type = ListofAddresses[0].Type,
                        UserID = LastInsertedUserId,
                        CountryID = ListofAddresses[0].CountryID,
                        StateID = ListofAddresses[0].StateID
                    };

                    var PermenantAddress = new AddressDetail
                    {
                        Address = ListofAddresses[1].Address,
                        Type = ListofAddresses[1].Type,
                        UserID = LastInsertedUserId,
                        CountryID = ListofAddresses[1].CountryID,
                        StateID = ListofAddresses[1].StateID

                    };

                    Context.AddressDetails.Add(PermenantAddress);
                    Context.AddressDetails.Add(PresentAddress);

                    Context.SaveChanges();
                    InsertedUser["flag"] = 1; // flag = 1;
                  
                }
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData(ex);
            }
           
            return InsertedUser;

        }
        // get all users from database 
        public List<UserDetailsModel> GetAllUsers()
        {
            List<UserDetailsModel> UserList = new List<UserDetailsModel>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve all user details and project them to UserDetailsModel
                    UserList = Context.UserDetails
                        .Select(UserDetailEntity => new UserDetailsModel
                        {
                            UserID = UserDetailEntity.UserID,
                            FirstName = UserDetailEntity.FirstName,
                            LastName = UserDetailEntity.LastName,
                            Password = UserDetailEntity.Password,
                            PhoneNumber = UserDetailEntity.PhoneNumber,
                            AlternatePhoneNumber = UserDetailEntity.AlternatePhoneNumber,
                            Email = UserDetailEntity.Email,
                            AlternateEmail = UserDetailEntity.AlternateEmail,
                            DOB = UserDetailEntity.DOB,
                            Favouritecolor = UserDetailEntity.Favouritecolor,
                            Aadhaar = UserDetailEntity.Aadhaar,
                            PAN = UserDetailEntity.PAN,
                            PreferedLanguage = UserDetailEntity.PreferedLanguage,
                            MaritalStatus = UserDetailEntity.MaritalStatus,
                            Upto10th = UserDetailEntity.Upto10th,
                            PercentageUpto10th = UserDetailEntity.PercentageUpto10th,
                            Upto12th = UserDetailEntity.Upto12th,
                            PercentageUpto12th = UserDetailEntity.PercentageUpto12th,
                            Graduation = UserDetailEntity.Graduation,
                            PercentageInGraduation = UserDetailEntity.PercentageInGraduation
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return UserList;
        }


        // get all addresses
        // Get all address details for all users
        public List<AddressDetailsModel> GetAllUsersAddresses()
        {
            List<AddressDetailsModel> ListOfAddresses = new List<AddressDetailsModel>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve all address details and project them to AddressDetailsModel
                    ListOfAddresses = Context.AddressDetails
                        .Select(AddressDetailEntity=> new AddressDetailsModel
                        {
                            UserID = AddressDetailEntity.UserID,
                            Address = AddressDetailEntity.Address,
                            Type = AddressDetailEntity.Type,
                            CountryID = AddressDetailEntity.CountryID,
                            StateID = AddressDetailEntity.StateID
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }


            return ListOfAddresses;
        }





        // get user details and show them on grid View
        public UserDetailsModel GetUserDetails(int UserId)
        {
            UserDetailsModel UserDetails = null;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieveing user details based on UserId
                    var UserDetailEntity = Context.UserDetails.FirstOrDefault(u => u.UserID == UserId);
                   
                        // Maping  entity properties to UserDetailsModel
                        UserDetails = new UserDetailsModel
                        {
                            FirstName = UserDetailEntity.FirstName,
                            LastName = UserDetailEntity.LastName,
                            Password = UserDetailEntity.Password,
                            PhoneNumber = UserDetailEntity.PhoneNumber,
                            AlternatePhoneNumber = UserDetailEntity.AlternatePhoneNumber,
                            Email = UserDetailEntity.Email,
                            AlternateEmail = UserDetailEntity.AlternateEmail,
                            DOB = UserDetailEntity.DOB,
                            Favouritecolor = UserDetailEntity.Favouritecolor,
                            Aadhaar = UserDetailEntity.Aadhaar,
                            PAN = UserDetailEntity.PAN,
                            PreferedLanguage = UserDetailEntity.PreferedLanguage,
                            MaritalStatus = UserDetailEntity.MaritalStatus,
                            Upto10th = UserDetailEntity.Upto10th,
                            PercentageUpto10th = UserDetailEntity.PercentageUpto10th,
                            Upto12th = UserDetailEntity.Upto12th,
                            PercentageUpto12th = UserDetailEntity.PercentageUpto12th,
                            Graduation = UserDetailEntity.Graduation,
                            PercentageInGraduation = UserDetailEntity.PercentageInGraduation
                        };

                    }
                
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return UserDetails;
        }



        // get the address details of userId
        public Dictionary<int, AddressDetailsModel> GetAddresses(int userId)
        {
            List<AddressDetailsModel> ListOfAddresses = new List<AddressDetailsModel>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve addresses based on UserId and project them to AddressDetailsModel
                    ListOfAddresses = Context.AddressDetails
                        .Where(a => a.UserID == userId)
                        .Select(AddressDetailEntity=> new AddressDetailsModel
                        {
                            Address = AddressDetailEntity.Address,
                            Type = AddressDetailEntity.Type,
                            UserID = AddressDetailEntity.UserID,
                            StateID = AddressDetailEntity.StateID,
                            CountryID = AddressDetailEntity.CountryID
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }


            Dictionary<int, AddressDetailsModel> AddressDictionary = ListOfAddresses
    .ToDictionary(Address => Address.Type, Address => Address);

            return AddressDictionary;
        }


        public bool UpdateUser(UserDetailsModel UserInfo,List<AddressDetailsModel> ListofAddresses,int IdToUpdate,int RoleID)
        {
            
            bool Flag = false;
           
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve the user details from the database based on the user ID
                    var ExistingUser = Context.UserDetails.FirstOrDefault(u => u.UserID == IdToUpdate);

                    
                        // Update user details
                        ExistingUser.FirstName = UserInfo.FirstName;
                        ExistingUser.LastName = UserInfo.LastName;
                        ExistingUser.Password = UserInfo.Password;
                        ExistingUser.PhoneNumber = UserInfo.PhoneNumber;
                        ExistingUser.AlternatePhoneNumber = UserInfo.AlternatePhoneNumber;
                        ExistingUser.Email = UserInfo.Email;
                        ExistingUser.AlternateEmail = UserInfo.AlternateEmail;
                        ExistingUser.DOB = UserInfo.DOB;
                        ExistingUser.Favouritecolor = UserInfo.Favouritecolor;
                        ExistingUser.Aadhaar = UserInfo.Aadhaar;
                        ExistingUser.PAN = UserInfo.PAN;
                        ExistingUser.MaritalStatus = UserInfo.MaritalStatus;
                        ExistingUser.PreferedLanguage = UserInfo.PreferedLanguage;
                        ExistingUser.Upto10th = UserInfo.Upto10th;
                        ExistingUser.PercentageUpto10th = UserInfo.PercentageUpto10th;
                        ExistingUser.Upto12th = UserInfo.Upto12th;
                        ExistingUser.PercentageUpto12th = UserInfo.PercentageUpto12th;
                        ExistingUser.Graduation = UserInfo.Graduation;
                        ExistingUser.PercentageInGraduation = UserInfo.PercentageInGraduation;

                        var PresentAddress = Context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == (int)Enums.AddressType.Present);
                        var PermanentAddress = Context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == (int)Enums.AddressType.Permanent);
                                           
                            // Update addresses 
                        PresentAddress.Address = ListofAddresses[0].Address;
                        PermanentAddress.Address = ListofAddresses[1].Address;

                       // update role  
                      var UserRole = Context.UserRoles.FirstOrDefault(u => u.UserID == IdToUpdate);
                    UserRole.UserRoleID = RoleID; 
                      

                    Context.SaveChanges();
                        Flag = true;
                    }
                    
                
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData(ex);
            }

            return Flag;
        }


        public bool InsertNotes(string InputNoteText, int UserId,int ObjectType )
        {
            bool Flag = false;

            try
            {
                 using (var Context = new UserManagementEntities())
                 {
                    Note newNote = new Note
                    {
                       
                        NoteText = InputNoteText,
                        ObjectID = UserId,
                        ObjectType = ObjectType,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
                    };

                   
                    // Add the new note to the Context
                    Context.Notes.Add(newNote);

                    Context.SaveChanges();
                    Flag = true;
                   
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return Flag;
        }

        // ObjectType - 1 means page - userDetails 
        public List<NoteModel> GetNotes(int UserId,int ObjectType)
        {

            List <NoteModel> ListofNotes = new List<NoteModel>();
            try
            {
                using (var Context = new UserManagementEntities())
                {

                    var UserNotes = Context.Notes
                        .Where(n => n.ObjectID == UserId && n.ObjectType == ObjectType)
                        .ToList();

                    ListofNotes = UserNotes.Select(Note => new NoteModel
                    {
                        ObjectType = Note.ObjectType,
                        ObjectID=Note.ObjectID,
                        NoteText=Note.NoteText,
                        NotesID = Note.NotesID,
                        CreatedDate = Note.CreatedDate

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return ListofNotes;
        }


        public List<NoteModel> GetSortedAndPagedNotes(int ObjectID, int ObjectType, string SortExpression, string SortDirection, int PageIndex, int PageSize)
     {
            List<NoteModel> NotesList = new List<NoteModel>();
            using (var Context = new UserManagementEntities())
            {
                IQueryable<Note> Query = Context.Notes.Where(n => n.ObjectID == ObjectID && n.ObjectType == ObjectType);

                // Apply dynamic Sorting
                Query = ApplySorting(Query, SortExpression,SortDirection);

                // Apply paging
                List<Note> Notes = Query.Skip(PageIndex * PageSize).Take(PageSize).ToList();

                NotesList = Notes.Select(Note => new NoteModel
                {
                    NotesID = Note.NotesID,                    
                    NoteText = Note.NoteText,
                    ObjectType = Note.ObjectType,
                    ObjectID = Note.ObjectID,
                    CreatedDate = Note.CreatedDate
                }).ToList();

                return NotesList;
            }
        }

        private IQueryable<Note> ApplySorting(IQueryable<Note> Query, string SortExpression, string SortDirection)
        {
            switch (SortExpression)
            {
                case "NotesID":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(n => n.NotesID) : Query.OrderByDescending(n => n.NotesID);
                    break;
                case "NoteText":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(n => n.NoteText) : Query.OrderByDescending(n => n.NoteText);
                    break;
                case "ObjectType":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(n => n.ObjectType) : Query.OrderByDescending(n => n.ObjectType);
                    break;
                case "ObjectID":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(n => n.ObjectID) : Query.OrderByDescending(n => n.ObjectID);
                    break;
                
            }

            return Query;
        }

        public List<DocumentModel> GetSortedAndPagedDocuments(int ObjectID, int ObjectType, string SortExpression, string SortDirection, int PageIndex, int PageSize)
        {
            List<DocumentModel> DocumentsList = new List<DocumentModel>();
            using (var Context = new UserManagementEntities())
            {
                IQueryable<Document> Query = Context.Documents.Where(d => d.ObjectID == ObjectID && d.ObjectType == ObjectType);

                // Apply dynamic Sorting
                Query = ApplySorting(Query, SortExpression, SortDirection);

                // Apply paging
                List<Document> Documents = Query.Skip(PageIndex * PageSize).Take(PageSize).ToList();

                DocumentsList = Documents.Select(Doc => new DocumentModel
                {
                    DocumentID = Doc.DocumentID,
                    DocumentType = Doc.DocumentType,
                    ObjectType = Doc.ObjectType,
                    DocumentOriginalName = Doc.DocumentOriginalName,
                    ObjectID = Doc.ObjectID,
                    TimeStamp = Doc.TimeStamp
                }).ToList();

                return DocumentsList;
            }
        }

        private IQueryable<Document> ApplySorting(IQueryable<Document> Query, string SortExpression, string SortDirection)
        {
            switch (SortExpression)
            {
                case "DocumentID":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(d => d.DocumentID) : Query.OrderByDescending(d => d.DocumentID);
                    break;
                case "DocumentType":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(d => d.DocumentType) : Query.OrderByDescending(d => d.DocumentType);
                    break;
                case "ObjectType":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(d => d.ObjectType) : Query.OrderByDescending(d => d.ObjectType);
                    break;
                case "ObjectID":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(d => d.ObjectID) : Query.OrderByDescending(d => d.ObjectID);
                    break;

            }

            return Query;
        }



        public List<string> GetAllCountries()
        {
            List<string> CountriesList = new List<string>();
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve all countries from the Country table
                    var Countries = Context.Countries
                        .Select(c => c.CountryName)
                        .ToList();

                    CountriesList.AddRange(Countries);
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return CountriesList;
        }



        public List<string> GetStatesForCountry(string CountryName)
        {
            List<string> StatesList = new List<string>();
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Retrieve the states for the selected country from the State table
                    StatesList = Context.States
                        .Where(s => s.Country.CountryName == CountryName)
                        .Select(s => s.StateName)
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return StatesList;
        }



       public Dictionary<string,int> GetCountryAndStateID(string CountryName,string StateName)
        {
         
            Dictionary<string, int> Id = new Dictionary<string, int>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    var Country = Context.Countries.FirstOrDefault(c => c.CountryName == CountryName);
                    var State = Context.States.FirstOrDefault(s => s.StateName == StateName);
                  
                    Id["Country"] = Country.CountryID;
                    Id["State"] = State.StateID;
                }

            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }          

            return Id;

        }



        public List<UserDetailsModel> GetSortedAndPagedUsers(string SortExpression, string SortDirection, int pageIndex, int pageSize)
        {
            List<UserDetailsModel> UsersList = new List<UserDetailsModel>();
            using (var Context = new UserManagementEntities())
            {
                IQueryable<UserDetail> Query = Context.UserDetails;

                // Apply dynamic Sorting
                Query = ApplySorting(Query, SortExpression, SortDirection);

                // Apply paging
                List<UserDetail> Users = Query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                // Map the result to UserDetailsModel and return the result
                UsersList = Users.Select(User => new UserDetailsModel
                {
                    UserID = User.UserID,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Password = User.Password,
                    PhoneNumber = User.PhoneNumber,
                    AlternatePhoneNumber = User.AlternatePhoneNumber,
                    Email = User.Email,
                    AlternateEmail = User.AlternateEmail,
                    DOB = User.DOB,
                    Favouritecolor = User.Favouritecolor,
                    Aadhaar = User.Aadhaar,
                    PAN = User.PAN,
                    PreferedLanguage = User.PreferedLanguage,
                    MaritalStatus = User.MaritalStatus,
                    Upto10th = User.Upto10th,
                    PercentageUpto10th = User.PercentageUpto10th,
                    Upto12th = User.Upto12th,
                    PercentageUpto12th = User.PercentageUpto12th,
                    Graduation = User.Graduation,
                    PercentageInGraduation = User.PercentageInGraduation

                }).ToList();

                return UsersList;
            }
        }

        private IQueryable<UserDetail> ApplySorting(IQueryable<UserDetail> Query, string SortExpression, string SortDirection)
        {
            switch (SortExpression)
            {
                case "UserID":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(u => u.UserID) : Query.OrderByDescending(u => u.UserID);
                    break;
                case "FirstName":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(u => u.FirstName) : Query.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName":
                    Query = (SortDirection == "ASC") ? Query.OrderBy(u => u.LastName) : Query.OrderByDescending(u => u.LastName);
                    break;
            }

            return Query;
        }


        public int TotalUsers()
        {
            int Length = 0;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    Length = Context.UserDetails.ToList().Count;
                    
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return Length;
        }

        public int TotalNoteRows()
        {
            int Length = 0;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    Length = Context.Notes.ToList().Count;

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return Length;
        }


        public int TotalDocumentRows()
        {
            int Length = 0;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    Length = Context.Documents.ToList().Count;

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return Length;
        }


        public Dictionary<string, string> GetCountryAndStateNames(int CountryID, int StateID)
        {
            Dictionary<string, string> NameDictionary = new Dictionary<string, string>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    var Country = Context.Countries.FirstOrDefault(c => c.CountryID == CountryID);
                    var State = Context.States.FirstOrDefault(s => s.StateID == StateID);

                    NameDictionary.Add("CountryName", Country.CountryName);
                    NameDictionary.Add("StateName", State.StateName);
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }


            return NameDictionary;
        }



        public bool InsertDocument(string FileName, string UniqueGuid, int ObjectID, int ObjectType, int DocumentType)
        {
            bool Flag = false;   
            try
            {
                using (var Context = new UserManagementEntities())
                {
                   
                    Document TempDocument = new Document
                    {
                        DocumentOriginalName = FileName,
                        DocumentGuidName = UniqueGuid,
                        ObjectType = ObjectType,
                        ObjectID = ObjectID,
                        DocumentType = DocumentType,
                        TimeStamp = DateTime.Now.ToString(),
                    };

                    // Add the document to the Context and save changes
                    Context.Documents.Add(TempDocument);
                    Context.SaveChanges();
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return Flag;
        }


        public List<DocumentModel> GetDocumentsForUser(int ObjectID, int ObjectType)
        {
            List<DocumentModel> ListOfDocuments= new List<DocumentModel>();

            try
            {
                using (var Context = new UserManagementEntities())
                {
                    ListOfDocuments = Context.Documents
                        .Where(d => d.ObjectID == ObjectID && d.ObjectType == ObjectType)
                        .Select(d => new DocumentModel
                        {
                            DocumentID = d.DocumentID,
                            DocumentOriginalName = d.DocumentOriginalName,
                            DocumentGuidName = d.DocumentGuidName,
                            ObjectID = d.ObjectID,
                            ObjectType = d.ObjectType,
                            DocumentType = d.DocumentType,
                            TimeStamp = d.TimeStamp
                         
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                LoggerClass.AddData(ex);
            }

            return ListOfDocuments;
        }


        public Dictionary<string, int> CheckIfUserExists(string UserEmail, string UserPassword)
        {
           
            Dictionary<string,int> User = new Dictionary<string,int>();
            User["IsUserExists"] = 0;
            User["RoleId"] = 2;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Assuming you have a User entity in your DbContext
                    var user = Context.UserDetails
                        .FirstOrDefault(u => u.Email == UserEmail && u.Password == UserPassword);

                    // If user is not null, it means a user with the provided credentials exists
                    if (user != null)
                    {
                        User["IsUserExists"] = 1;                    
                            
                        var UserRole = Context.UserRoles.FirstOrDefault(u=> u.UserID ==  user.UserID);
                        User["RoleId"] = UserRole.RoleID;
                    }

                }

            }catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
           
            return User;
        }

        public int CheckIfEmailExists(string Email)
        {
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    // Check if any user has the provided email
                    var UserWithSameEmail = Context.UserDetails.FirstOrDefault(u => u.Email == Email);

                    // If userWithSameEmail is not null, it means an account with the provided email exists
                    if (UserWithSameEmail != null)
                    {
                        return 1; // Email exists
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return 0; // Email does not exist
        }



        public int GetRoleIDForRole(string RoleName)
        {
            int RoleID = 2;
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    var Role = Context.Roles.FirstOrDefault(u => u.RoleName == RoleName);

                    RoleID = Role.RoleID;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return RoleID;

        }


        public string GetUserRoleForUserID(int UserID)
        {
            string UserRole= "Admin";
            try
            {
                using (var Context = new UserManagementEntities())
                {
                    var UserRoleEntity = Context.UserRoles.FirstOrDefault(u => u.UserID == UserID);
                    var RoleID = UserRoleEntity.RoleID;
                    var Role =  Context.Roles.FirstOrDefault(u => u.RoleID == RoleID);
                    UserRole  = Role.RoleName;

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return UserRole;
        }
    }
}
