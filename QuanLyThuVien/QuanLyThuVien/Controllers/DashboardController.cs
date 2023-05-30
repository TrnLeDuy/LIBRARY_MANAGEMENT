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
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ViewBag.countLibCard = db.TheThuViens.Count();
            ViewBag.countBook = db.CuonSaches.Count();
            ViewBag.countISBN = db.DauSaches.Count();
            ViewBag.countBookRent = db.MuonTras.Count();
            return View();
        }
    }
}