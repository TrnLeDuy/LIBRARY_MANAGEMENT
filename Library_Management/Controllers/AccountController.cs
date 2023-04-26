using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Management.Models;

namespace Library_Management.Controllers
{
    public class AccountController : Controller
    {
        CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        public ActionResult DanhSachNhanVien()
        {
            List<NhanVien> danhsachNhanVien = db.NhanViens.ToList();
            return View(danhsachNhanVien);
        }

        public ActionResult DanhSachTaiKhoan()
        {
            List<TaiKhoan> danhsachTaiKhoan = db.TaiKhoans.ToList();
            return View(danhsachTaiKhoan);
        }

        public ActionResult ChiTietNV()
        {
            var detail = new CNPM_QLTVEntities();
            return View();
        }
    }
}