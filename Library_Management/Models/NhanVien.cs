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
    
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.QuaTrinhMuons = new HashSet<QuaTrinhMuon>();
        }
    
        public int MaNV { get; set; }
        public string Hoten { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string Gioitinh { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Diachi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhMuon> QuaTrinhMuons { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}