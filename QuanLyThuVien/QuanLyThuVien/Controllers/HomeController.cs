using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

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
    }
}