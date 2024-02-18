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
        public Dictionary<string, int> InsertUser(UserDetailsModel NewUser,List<AddressDetailsModel> ListofAddresses)
        {

                      
            Dictionary<string, int> InsertedUser = new Dictionary<string, int>();

            InsertedUser["flag"] = 0; // flag - 0 

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

                    // Get the last inserted UserID     

                    int LastInsertedUserId = newUser.UserID;
                    
                    int RoleId = 1;

                    InsertedUser["RoleId"] = RoleId;
                    InsertedUser["UserID"] = LastInsertedUserId;
                    

                    var TempUserRole = new UserRole
                    {
                        UserID = LastInsertedUserId,
                        RoleID = 1
                    };

                    context.UserRoles.Add(TempUserRole);

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

                    context.AddressDetails.Add(PermenantAddress);
                    context.AddressDetails.Add(PresentAddress);

                    context.SaveChanges();
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
            List<UserDetailsModel> userList = new List<UserDetailsModel>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve all user details and project them to UserDetailsModel
                    userList = context.UserDetails
                        .Select(userDetailEntity => new UserDetailsModel
                        {
                            UserID = userDetailEntity.UserID,
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
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return userList;
        }


        // get all addresses
        // Get all address details for all users
        public List<AddressDetailsModel> GetAllUsersAddresses()
        {
            List<AddressDetailsModel> ListOfAddresses = new List<AddressDetailsModel>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve all address details and project them to AddressDetailsModel
                    ListOfAddresses = context.AddressDetails
                        .Select(addressDetailEntity => new AddressDetailsModel
                        {
                            UserID = addressDetailEntity.UserID,
                            Address = addressDetailEntity.Address,
                            Type = addressDetailEntity.Type,
                            CountryID = addressDetailEntity.CountryID,
                            StateID = addressDetailEntity.StateID
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
            UserDetailsModel userDetails = null;
            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieveing user details based on UserId
                    var userDetailEntity = context.UserDetails.FirstOrDefault(u => u.UserID == UserId);
                   
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
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return userDetails;
        }



        // get the address details of userId
        public Dictionary<int, AddressDetailsModel> GetAddresses(int userId)
        {
            List<AddressDetailsModel> ListOfAddresses = new List<AddressDetailsModel>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve addresses based on UserId and project them to AddressDetailsModel
                    ListOfAddresses = context.AddressDetails
                        .Where(a => a.UserID == userId)
                        .Select(addressDetailEntity => new AddressDetailsModel
                        {
                            Address = addressDetailEntity.Address,
                            Type = addressDetailEntity.Type,
                            UserID = addressDetailEntity.UserID,
                            StateID = addressDetailEntity.StateID,
                            CountryID = addressDetailEntity.CountryID
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }


            Dictionary<int, AddressDetailsModel> AddressDictionary = ListOfAddresses
    .ToDictionary(address => address.Type, address => address);

            return AddressDictionary;
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

                        var presentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == (int)Enums.AddressType.Present);
                        var permanentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == IdToUpdate && a.Type == (int)Enums.AddressType.Permanent);
                                           
                            // Update addresses 
                        presentAddress.Address = ListofAddresses[0].Address;
                        permanentAddress.Address = ListofAddresses[1].Address;
                            

                        context.SaveChanges();
                        flag = true;
                    }
                    
                
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData(ex);
            }

            return flag;
        }


        public bool InsertNotes(string InputNoteText, int UserId,int ObjectType )
        {
            bool flag = false;

            try
            {
                 using (var context = new UserManagementEntities())
                 {
                    Note newNote = new Note
                    {
                       
                        NoteText = InputNoteText,
                        ObjectID = UserId,
                        ObjectType = ObjectType,
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
                LoggerClass.AddData(ex);
            }
            return flag;
        }

        // ObjectType - 1 means page - userDetails 
        public List<NoteModel> GetNotes(int UserId,int ObjectType)
        {

            List <NoteModel> ListofNotes = new List<NoteModel>();
            try
            {
                using (var context = new UserManagementEntities())
                {

                    var userNotes = context.Notes
                        .Where(n => n.ObjectID == UserId && n.ObjectType == ObjectType)
                        .ToList();

                    ListofNotes = userNotes.Select(note => new NoteModel
                    {
                        ObjectType = note.ObjectType,
                        ObjectID=note.ObjectID,
                        NoteText=note.NoteText,
                        NotesID = note.NotesID,
                        CreatedDate = note.CreatedDate

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
            using (var context = new UserManagementEntities())
            {
                IQueryable<Note> Query = context.Notes.Where(n => n.ObjectID == ObjectID && n.ObjectType == ObjectType);

                // Apply dynamic Sorting
                Query = ApplySorting(Query, SortExpression,SortDirection);

                // Apply paging
                List<Note> Notes = Query.Skip(PageIndex * PageSize).Take(PageSize).ToList();

                // Map the result to NoteModel and return the result
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
                
                    // Add more cases as needed for other properties
            }

            return Query;
        }

        public List<DocumentModel> GetSortedAndPagedDocuments(int ObjectID, int ObjectType, string SortExpression, string SortDirection, int PageIndex, int PageSize)
        {
            List<DocumentModel> DocumentsList = new List<DocumentModel>();
            using (var context = new UserManagementEntities())
            {
                IQueryable<Document> Query = context.Documents.Where(d => d.ObjectID == ObjectID && d.ObjectType == ObjectType);

                // Apply dynamic Sorting
                Query = ApplySorting(Query, SortExpression, SortDirection);

                // Apply paging
                List<Document> Documents = Query.Skip(PageIndex * PageSize).Take(PageSize).ToList();

                // Map the result to DocumentModel and return the result
                DocumentsList = Documents.Select(doc => new DocumentModel
                {
                    DocumentID = doc.DocumentID,
                    DocumentType = doc.DocumentType,
                    ObjectType = doc.ObjectType,
                    DocumentOriginalName = doc.DocumentOriginalName,
                    ObjectID = doc.ObjectID,
                    TimeStamp = doc.TimeStamp
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
                    // Add more cases as needed for other properties

            }

            return Query;
        }



        public List<string> GetAllCountries()
        {
            List<string> countriesList = new List<string>();
            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve all countries from the Country table
                    var countries = context.Countries
                        .Select(c => c.CountryName)
                        .ToList();

                    // Now, 'countries' contains a list of all country names
                    countriesList.AddRange(countries);
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return countriesList;
        }



        public List<string> GetStatesForCountry(string CountryName)
        {
            List<string> statesList = new List<string>();
            try
            {
                using (var context = new UserManagementEntities())
                {
                    // Retrieve the states for the selected country from the State table
                    statesList = context.States
                        .Where(s => s.Country.CountryName == CountryName)
                        .Select(s => s.StateName)
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return statesList;
        }



       public Dictionary<string,int> GetCountryAndStateID(string CountryName,string StateName)
        {
         
            Dictionary<string, int> Id = new Dictionary<string, int>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    var Country = context.Countries.FirstOrDefault(c => c.CountryName == CountryName);
                    var State = context.States.FirstOrDefault(s => s.StateName == StateName);
                  
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
            using (var context = new UserManagementEntities())
            {
                IQueryable<UserDetail> query = context.UserDetails;

                // Apply dynamic Sorting
                query = ApplySorting(query, SortExpression, SortDirection);

                // Apply paging
                List<UserDetail> users = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                // Map the result to UserDetailsModel and return the result
                UsersList = users.Select(user => new UserDetailsModel
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    AlternatePhoneNumber = user.AlternatePhoneNumber,
                    Email = user.Email,
                    AlternateEmail = user.AlternateEmail,
                    DOB = user.DOB,
                    Favouritecolor = user.Favouritecolor,
                    Aadhaar = user.Aadhaar,
                    PAN = user.PAN,
                    PreferedLanguage = user.PreferedLanguage,
                    MaritalStatus = user.MaritalStatus,
                    Upto10th = user.Upto10th,
                    PercentageUpto10th = user.PercentageUpto10th,
                    Upto12th = user.Upto12th,
                    PercentageUpto12th = user.PercentageUpto12th,
                    Graduation = user.Graduation,
                    PercentageInGraduation = user.PercentageInGraduation

                }).ToList();

                return UsersList;
            }
        }

        private IQueryable<UserDetail> ApplySorting(IQueryable<UserDetail> query, string SortExpression, string SortDirection)
        {
            switch (SortExpression)
            {
                case "UserID":
                    query = (SortDirection == "ASC") ? query.OrderBy(u => u.UserID) : query.OrderByDescending(u => u.UserID);
                    break;
                case "FirstName":
                    query = (SortDirection == "ASC") ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName":
                    query = (SortDirection == "ASC") ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName);
                    break;
                    // Add more cases as needed for other properties
            }

            return query;
        }


        public int TotalUsers()
        {
            int length = 0;
            try
            {
                using (var context = new UserManagementEntities())
                {
                    length = context.UserDetails.ToList().Count;
                    
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return length;
        }

        public int TotalNoteRows()
        {
            int length = 0;
            try
            {
                using (var context = new UserManagementEntities())
                {
                    length = context.Notes.ToList().Count;

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return length;
        }


        public int TotalDocumentRows()
        {
            int length = 0;
            try
            {
                using (var context = new UserManagementEntities())
                {
                    length = context.Documents.ToList().Count;

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }
            return length;
        }


        public Dictionary<string, string> GetCountryAndStateNames(int countryID, int stateID)
        {
            Dictionary<string, string> NameDictionary = new Dictionary<string, string>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    var country = context.Countries.FirstOrDefault(c => c.CountryID == countryID);
                    var state = context.States.FirstOrDefault(s => s.StateID == stateID);

                    NameDictionary.Add("CountryName", country.CountryName);
                    NameDictionary.Add("StateName", state.StateName);
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }


            return NameDictionary;
        }



        public bool InsertDocument(string FileName, string uniqueGuid, int ObjectID, int ObjectType, int DocumentType)
        {
            bool flag = false;   
            try
            {
                using (var context = new UserManagementEntities())
                {
                   
                    Document TempDocument = new Document
                    {
                        DocumentOriginalName = FileName,
                        DocumentGuidName = uniqueGuid,
                        ObjectType = ObjectType,
                        ObjectID = ObjectID,
                        DocumentType = DocumentType,
                        TimeStamp = DateTime.Now.ToString(),
                    };

                    // Add the document to the context and save changes
                    context.Documents.Add(TempDocument);
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(ex);
            }

            return flag;
        }


        public List<DocumentModel> GetDocumentsForUser(int objectID, int objectType)
        {
            List<DocumentModel> ListOfDocuments= new List<DocumentModel>();

            try
            {
                using (var context = new UserManagementEntities())
                {
                    ListOfDocuments = context.Documents
                        .Where(d => d.ObjectID == objectID && d.ObjectType == objectType)
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
                using (var context = new UserManagementEntities())
                {
                    // Assuming you have a User entity in your DbContext
                    var user = context.UserDetails
                        .FirstOrDefault(u => u.Email == UserEmail && u.Password == UserPassword);

                    // If user is not null, it means a user with the provided credentials exists
                    if (user != null)
                    {
                        User["IsUserExists"] = 1;                    
                            
                        var UserRole = context.UserRoles.FirstOrDefault(u=> u.UserID ==  user.UserID);
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
                using (var context = new UserManagementEntities())
                {
                    // Check if any user has the provided email
                    var UserWithSameEmail = context.UserDetails.FirstOrDefault(u => u.Email == Email);

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


    }
}
