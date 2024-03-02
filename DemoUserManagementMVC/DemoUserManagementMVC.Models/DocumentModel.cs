using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class DocumentModel
    {
        public int DocumentID { get; set; }
        public int ObjectType { get; set; }
        public string DocumentOriginalName { get; set; }
        public string DocumentGuidName { get; set; }
        public int ObjectID { get; set; }
        public string TimeStamp { get; set; }
        public int DocumentType { get; set; }
    }
}
