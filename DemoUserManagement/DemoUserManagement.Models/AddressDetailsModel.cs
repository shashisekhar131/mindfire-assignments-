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
        public Nullable<int> Type { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}
