//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoUserManagement.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Note
    {
        public int NotesID { get; set; }
        public string NoteText { get; set; }
        public string Page { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
    }
}
