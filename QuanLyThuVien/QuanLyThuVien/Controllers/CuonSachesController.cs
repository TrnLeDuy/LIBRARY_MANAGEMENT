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
    public class CuonSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: CuonSaches
        public ActionResult Index(string s)
        {
            var cuonSaches = from l in db.CuonSaches select l;
            return View(cuonSaches);
        }

        // GET: CuonSaches/Details/5
        public ActionResult Details(string id)
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

        // GET: CuonSaches/Create
        public ActionResult Create()
        {
            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach");
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach");
            return View();
        }

        public string GetNextBookID()
        {
            int id = 1;
            if (db.NhanViens.Any())
            {
                id = db.NhanViens.Max(nv => nv.MaNV) + 1;
            }
            return "NB" + id;
        }

        // POST: CuonSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "isbn,ten_cuonsach,tacgia,namxuatban,nhaxuatban,ma_loaisach,Mota")] CuonSach cuonSach)
        {
            string nextID = GetNextBookID();
            cuonSach.ma_cuonsach = nextID;
            if (ModelState.IsValid)
            {
                cuonSach.TinhTrang = "Còn sách";
                db.CuonSaches.Add(cuonSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // GET: CuonSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach== id);
            if (cuonSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // POST: CuonSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,ma_cuonsach,ten_cuonsach,tacgia,namxuatban,nhaxuatban,ma_loaisach,TinhTrang,Mota")] CuonSach cuonSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuonSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // GET: CuonSaches/Delete/5
        public ActionResult Delete(string id)
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

        // POST: CuonSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach == id);
            db.CuonSaches.Remove(cuonSach);
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
