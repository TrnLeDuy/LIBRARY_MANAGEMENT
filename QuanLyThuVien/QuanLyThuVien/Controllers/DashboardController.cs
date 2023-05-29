using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class DashboardController : Controller
    {
        CNPM_QLTVEntities db = new CNPM_QLTVEntities();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.countLibCard = db.TheThuViens.Count();
            ViewBag.countBook = db.CuonSaches.Count();
            ViewBag.countEmployee = db.TaiKhoans.Count(s => s.LoaiTK != "AD");
            ViewBag.countBookRent = db.MuonTras.Count();
            return View();
        }
    }
}