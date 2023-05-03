using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library_Management.Models;

namespace Library_Management.ViewModel
{
    public class DetailViewModel
    {
        public IEnumerable<NhanVien> NhanViens { get; set; }
        public IEnumerable<TaiKhoan> TaiKhoans { get; set; }
    }
}