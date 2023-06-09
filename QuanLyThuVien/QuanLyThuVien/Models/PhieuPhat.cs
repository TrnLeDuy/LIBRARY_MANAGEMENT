//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PhieuPhat
    {
        public int ma_phieumuontra { get; set; }
        public int MaNV { get; set; }
        public string ma_sinhvien { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> ngay_lapphieu { get; set; }
        public string nguyen_nhan { get; set; }
        public Nullable<int> Songay_quahan { get; set; }
        public Nullable<decimal> Tongtien { get; set; }
        public Nullable<decimal> Sotienthu { get; set; }
        public Nullable<decimal> Conlai { get; set; }

        public virtual MuonTra MuonTra { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual TheThuVien TheThuVien { get; set; }
    }
}
