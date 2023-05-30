using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class HomeController : Controller
    {
        CNPM_QLTVEntities db = new CNPM_QLTVEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult XemLichSu(string currentFilter, string s, int? page)
        {
            int pageSize = 7;
            int pageNum = (page ?? 1);

            if (s != null)
            {
                page = 1;
            }
            else
            {
                s = currentFilter;
            }

            ViewBag.CurrentFilter = s;

            var dsLichsu = from l in db.MuonTras
                           select l;

            if (!String.IsNullOrEmpty(s))
            {
                dsLichsu = dsLichsu.Where(ls => ls.ma_sinhvien.ToString().Contains(s) ||
                ls.TheThuVien.Hoten.Contains(s) ||
                ls.ngayGio_muon.ToString().Contains(s) ||
                ls.ngay_hethan.ToString().Contains(s));
            }

            dsLichsu = dsLichsu.OrderBy(id => s);
            return View(dsLichsu.ToPagedList(pageNum, pageSize));
        }

        public ActionResult XemSach(string currentFilter, string s, int? page)
        {
            int pageSize = 7;
            int pageNum = (page ?? 1);

            if (s != null)
            {
                page = 1;
            }
            else
            {
                s = currentFilter;
            }

            ViewBag.CurrentFilter = s;

            var list = from l in db.CuonSaches
                           select l;
            if (!String.IsNullOrEmpty(s))
            {
                list = list.Where(k => k.ten_cuonsach.Contains(s));
            }

            list = list.OrderBy(id => id.ma_cuonsach);

            return View(list.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Xemchitiet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach == id);
            if (cuonSach == null)
            {
                return HttpNotFound();
            }
            return View(cuonSach);
        }


        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap([Bind(Include = "ma_sinhvien")] TheThuVien tks )
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(tks.ma_sinhvien))
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập mã sinh viên");
               
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var tk = db.TheThuViens.FirstOrDefault(k => k.ma_sinhvien == tks.ma_sinhvien);
                    if (tk != null)
                    {
                        //Lưu thông vào session
                        
                        Session["MSV"] = tk.ma_sinhvien;
                        Session["HT"] = tk.Hoten;
                        Session["NS"] = tk.NgaySinh;
                        Session["TT"] = tk.Tinhtrangthe;
                        return Redirect("XemLichSu");
                    }
                    else
                        ViewBag.ThongBao = "Mã sinh viên không đúng!";
                }
            }
            return View();
        }
        public ActionResult Dangxuat()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["MSV"] = null;
            Session["HT"] = null;
            Session["NS"] = null;
            Session["TT"] = null;
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}