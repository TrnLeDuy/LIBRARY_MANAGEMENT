using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class LoaiSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: LoaiSaches
        public ActionResult Index()
        {
            return View(db.LoaiSaches.ToList());
        }

        // GET: LoaiSaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // GET: LoaiSaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_loaisach,ten_loaisach")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSaches.Add(loaiSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSach);
        }

        // GET: LoaiSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // POST: LoaiSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_loaisach,ten_loaisach")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSach);
        }

        // GET: LoaiSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // POST: LoaiSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            db.LoaiSaches.Remove(loaiSach);
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
