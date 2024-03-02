using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using DemoUserManagement.DAL;
using DemoUserManagement.Models;

namespace DemoUserManagement.Business
{
    public class Service
    {
        DataAccess dataAccess = new DataAccess();

        public Dictionary<string, int> InsertUser(UserDetailsModel NewUser)
        {
            return dataAccess.InsertUser(NewUser);

        }

        public UserDetailsModel GetUserDetails(int id)
        {
            return dataAccess.GetUserDetails(id);
        }

        public Dictionary<int, AddressDetailsModel> GetAddresses(int UserId)
        {
            return dataAccess.GetAddresses(UserId);
        }


        public bool UpdateUser(UserDetailsModel UserInfo)
        {
            return dataAccess.UpdateUser(UserInfo);

        }
       
        public bool InsertNotes(string InputNoteText,int UserId, int ObjectType)
        {
            return dataAccess.InsertNotes(InputNoteText,UserId,ObjectType);
        }

        public List<NoteModel> GetNotes(int UserId,int ObjectType)
        {
            return dataAccess.GetNotes(UserId,ObjectType);
        }

        public List<UserDetailsModel> GetAllUsers()
        {
            return dataAccess.GetAllUsers();
        }

        public List<CountryModel> GetAllCountries()
        {
            return dataAccess.GetAllCountries();
        }

        public List<StateModel> GetStatesForCountry(int SelectedCountryID)
        {
            return dataAccess.GetStatesForCountry(SelectedCountryID);
        }

        public List<AddressDetailsModel> GetAllUsersAddresses()
        {
            return dataAccess.GetAllUsersAddresses();
        }

        public Dictionary<string, int> GetCountryAndStateID(string CountryName,string StateName)
        {
            return dataAccess.GetCountryAndStateID(CountryName, StateName);
        }

        public Dictionary<string, string> GetCountryAndStateNames(int CountryID, int StateID)
        {
            return dataAccess.GetCountryAndStateNames(CountryID, StateID);
        }

        public string GetCountryName(int CountryID)
        {
            return dataAccess.GetCountryName( CountryID);
        }
        public string GetStateName(int StateID)
        {
            return dataAccess.GetStateName(StateID);
        }
        public List<UserDetailsModel> GetSortedAndPagedUsers(string SortExpression, string SortDirection, int PageIndex, int PageSize)
        {
            return dataAccess.GetSortedAndPagedUsers(SortExpression, SortDirection, PageIndex, PageSize);
        }

        public int TotalUsers()
        {
            return dataAccess.TotalUsers();
        }

        public bool InsertDocument(string FileName, string uniqueGuid,int ObjectID,int ObjectType,int DocumentType)
        {
            return dataAccess.InsertDocument(FileName, uniqueGuid, ObjectID, ObjectType, DocumentType);
        }

        public List<DocumentModel> GetDocumentsForUser(int ObjectID,int ObjectType)
        {
            return dataAccess.GetDocumentsForUser(ObjectID, ObjectType);
        }

        public Dictionary<string, int> CheckIfUserExists(string UserEmail, string UserPassword)
        {
            return dataAccess.CheckIfUserExists(UserEmail, UserPassword);
        }

        public List<NoteModel> GetSortedAndPagedNotes(int ObjectID, int ObjectType, string SortExpression, string SortDirection, int PageIndex, int PageSize)
        {
            return dataAccess.GetSortedAndPagedNotes( ObjectID, ObjectType,  SortExpression, SortDirection, PageIndex,PageSize);
        }

        public int TotalNoteRows()
        {
            return dataAccess.TotalNoteRows();
        }

        public int TotalDocumentRows()
        {
            return dataAccess.TotalDocumentRows();
        }
        public List<DocumentModel> GetSortedAndPagedDocuments(int ObjectID, int ObjectType, string SortExpression, string SortDirection, int PageIndex, int PageSize)
        {
            return dataAccess.GetSortedAndPagedDocuments(ObjectID, ObjectType, SortExpression, SortDirection, PageIndex, PageSize);
        }

        public int CheckIfEmailExists(string Email,int UserID)
        {
            return dataAccess.CheckIfEmailExists(Email,UserID);
        }

        public int GetRoleIDForRole(string RoleName)
        {
            return dataAccess.GetRoleIDForRole(RoleName);
        }

        public string GetUserRoleForUserID(int UserID)
        {
            return dataAccess.GetUserRoleForUserID(UserID);
        }

    }

}
