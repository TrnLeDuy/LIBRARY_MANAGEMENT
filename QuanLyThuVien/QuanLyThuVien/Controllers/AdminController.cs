using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using QuanLyThuVien.Models;
using System.Net;

namespace QuanLyThuVien.Controllers
{
    public class AdminController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}