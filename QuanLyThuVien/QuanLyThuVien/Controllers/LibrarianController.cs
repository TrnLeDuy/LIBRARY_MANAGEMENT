using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class LibrarianController : Controller
    {
        // GET: Librarian
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}