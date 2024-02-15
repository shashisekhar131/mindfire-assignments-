using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Utils
{
    public class Enums
    {

        public enum AddressType
        {
            Present = 0,
            Permanent = 1
        }

         
        // ObjectType - type of the page
        public enum ObjecType{ 
          
            Users =1
        }

        public enum DocumentType
        {
            PassportPic = 1,
            Aadhaar = 2,
            PAN =3,
            Others =4 
        }
    }
}
