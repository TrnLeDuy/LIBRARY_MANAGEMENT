//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        public string ma_sinhvien { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public System.DateTime han_sd { get; set; }
    
        public virtual TheThuVien TheThuVien { get; set; }
    }
}
