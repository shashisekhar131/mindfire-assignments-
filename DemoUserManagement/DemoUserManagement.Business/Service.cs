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
       
        public bool InsertNotes(string InputNoteText,int UserId, string Page)
        {
            return dataAccess.InsertNotes(InputNoteText,UserId,Page);
        }

        public List<NoteModel> GetNotes(int UserId,string Name)
        {
            return dataAccess.GetNotes(UserId,Name);
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
    }

}
