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
    public class ChiTietMuonTrasController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: ChiTietMuonTras
        public ActionResult Index()
        {
            var chiTietMuonTras = db.ChiTietMuonTras.Include(c => c.CuonSach).Include(c => c.MuonTra);
            return View(chiTietMuonTras.ToList());
        }

        // GET: ChiTietMuonTras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.Find(id);
            if (chiTietMuonTra == null)
            {
                return HttpNotFound();
            }
            return View(chiTietMuonTra);
        }

        // GET: ChiTietMuonTras/Create
        public ActionResult Create()
        {
            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach");
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien");
            ViewBag.ma_cuonsach = new SelectList(db.CuonSaches, "ma_cuonsach", "ten_cuonsach");
            return View();
        }

        // POST: ChiTietMuonTras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_phieumuontra,ma_cuonsach,soluong,ghichu")] ChiTietMuonTra chiTietMuonTra)
        {
            var cuonSach = db.CuonSaches.First(cs => cs.ma_cuonsach == chiTietMuonTra.ma_cuonsach);
            var phieuMuon = db.MuonTras.Find(chiTietMuonTra.ma_phieumuontra);
            if (ModelState.IsValid)
            {
                chiTietMuonTra.isbn = cuonSach.isbn;
                chiTietMuonTra.soluong = 1;
                chiTietMuonTra.ngayGio_tra = phieuMuon.ngay_hethan;
                chiTietMuonTra.ma_sinhvien = phieuMuon.ma_sinhvien;
                db.ChiTietMuonTras.Add(chiTietMuonTra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach", chiTietMuonTra.isbn);
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", chiTietMuonTra.ma_phieumuontra);
            return View(chiTietMuonTra);
        }

        // GET: ChiTietMuonTras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.Find(id);
            if (chiTietMuonTra == null)
            {
                return HttpNotFound();
            }
            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach", chiTietMuonTra.isbn);
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", chiTietMuonTra.ma_phieumuontra);
            return View(chiTietMuonTra);
        }

        // POST: ChiTietMuonTras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,ma_phieumuontra,ma_cuonsach,ma_sinhvien,ngayGio_tra,soluong,ghichu")] ChiTietMuonTra chiTietMuonTra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietMuonTra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach", chiTietMuonTra.isbn);
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", chiTietMuonTra.ma_phieumuontra);
            return View(chiTietMuonTra);
        }

        // GET: ChiTietMuonTras/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.First(c => c.ma_phieumuontra == id);
            if (chiTietMuonTra == null)
            {
                return HttpNotFound();
            }
            return View(chiTietMuonTra);
        }

        // POST: ChiTietMuonTras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.First(c => c.ma_phieumuontra == id);
            db.ChiTietMuonTras.Remove(chiTietMuonTra);
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
