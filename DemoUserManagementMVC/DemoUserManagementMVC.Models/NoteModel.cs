using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class NoteModel
    {
        public int NotesID { get; set; }
        public string NoteText { get; set; }
        public string CreatedDate { get; set; }
        public int ObjectType { get; set; }
        public int ObjectID { get; set; }
    }
}
