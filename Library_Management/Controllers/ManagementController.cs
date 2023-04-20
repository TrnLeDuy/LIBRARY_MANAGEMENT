using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Books()
        {
            return View();
        }
        //Đọc giả: Học sinh - Tạo thẻ thư viện
        public ActionResult Readers()
        {
            return View();
        }

        public ActionResult Borrows()
        {
            return View();
        }
    }
}