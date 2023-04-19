using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website_Thu_Vien.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult XemGioHang()
        {
            return View();
        }
        public ActionResult GioHangPartial()
        {
            return PartialView();
        }
    }
}