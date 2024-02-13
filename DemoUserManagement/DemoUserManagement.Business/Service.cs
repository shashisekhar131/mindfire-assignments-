using System;
using System.Collections.Generic;
using System.Data;
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

        public List<int> InsertUser(UserDetailsModel NewUser,List<AddressDetailsModel> ListofAddresses)
        {
            return dataAccess.InsertUser(NewUser,ListofAddresses);

        }

        public UserDetailsModel GetUserDetails(int id)
        {
            return dataAccess.GetUserDetails(id);
        }

        public List<AddressDetailsModel> GetAddresses(int UserId)
        {
            return dataAccess.GetAddresses(UserId);
        }


        public bool UpdateUser(UserDetailsModel UserInfo,List<AddressDetailsModel> ListofAddresses,int IdToUpdate)
        {
            return dataAccess.UpdateUser(UserInfo,ListofAddresses,IdToUpdate);

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

        public List<string> GetAllCountries()
        {
            return dataAccess.GetAllCountries();
        }

        public List<string> GetStatesForCountry(string SelectedCountry)
        {
            return dataAccess.GetStatesForCountry(SelectedCountry);
        }

        public List<AddressDetailsModel> GetAllUsersAddresses()
        {
            return dataAccess.GetAllUsersAddresses();
        }

        public List<int> GetCountryAndStateID(string CountryName,string StateName)
        {
            return dataAccess.GetCountryAndStateID(CountryName, StateName);
        }
    }

}
