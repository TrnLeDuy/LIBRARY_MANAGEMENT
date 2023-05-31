using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace QuanLyThuVien.Models
{
    public class ThuVienHS
    {
        public string ma_sinhvien { get; set; }
        public string Hoten { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string Tinhtrangthe { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime han_sd { get; set; }

    }
}