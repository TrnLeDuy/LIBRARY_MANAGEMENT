using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class TheThuViensController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();
        // GET: TheThuViens
        public ActionResult Index(string currentFilter, string s, int? page )
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

            var theThuVien = from l in db.TheThuViens
                          select l;
            if (!String.IsNullOrEmpty(s))
            {
                theThuVien = theThuVien.Where(mcs => mcs.ma_sinhvien.Contains(s));
            }

            theThuVien = theThuVien.OrderBy(id => id.ma_sinhvien);

            return View(theThuVien.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheThuVien theThuVien = db.TheThuViens.First(mttv => mttv.ma_sinhvien == id);
            if (theThuVien == null)
            {
                return HttpNotFound();
            }
            return View(theThuVien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_sinhvien,Hoten, Ngaysinh, Tinhtrangthe")] TheThuVien theThuVien)
        {
            if (ModelState.IsValid)
            {
                db.TheThuViens.Add(theThuVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theThuVien);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheThuVien theThuVien= db.TheThuViens.Find(id);
            if (theThuVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", theThuVien.ma_sinhvien);
            return View(theThuVien);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_sinhvien,Hoten")] TheThuVien theThuVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theThuVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_sinhhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", theThuVien.ma_sinhvien);
            return View(theThuVien);
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheThuVien theThuVien= db.TheThuViens.Find(id);
            if (theThuVien == null)
            {
                return HttpNotFound();
            }
            return View(theThuVien);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TheThuVien theThuVien= db.TheThuViens.Find(id);
            db.TheThuViens.Remove(theThuVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}