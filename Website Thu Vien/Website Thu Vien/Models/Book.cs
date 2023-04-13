//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Website_Thu_Vien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public Book()
        {
            this.BookReturn = new HashSet<BookReturn>();
            this.Borrow = new HashSet<Borrow>();
        }
    
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Describe { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.DateTime> Publish { get; set; }
        public string Publisher { get; set; }
        public string BookImage { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<BookReturn> BookReturn { get; set; }
        public virtual ICollection<Borrow> Borrow { get; set; }
        public int Book { get; internal set; }
    }
}