using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class AddressDetailsModel
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }
        public int UserID { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }

    }
}
