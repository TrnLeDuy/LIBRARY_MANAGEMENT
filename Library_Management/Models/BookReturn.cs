//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookReturn
    {
        public BookReturn()
        {
            this.Fines = new HashSet<Fine>();
        }
    
        public int ReturnID { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public string StudentID { get; set; }
        public Nullable<int> BookID { get; set; }
    
        public virtual ICollection<Fine> Fines { get; set; }
    }
}
