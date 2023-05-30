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
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            var chiTietMuonTras = db.ChiTietMuonTras.Include(c => c.CuonSach).Include(c => c.MuonTra);
            return View(chiTietMuonTras.ToList());
        }

        // GET: ChiTietMuonTras/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

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

        // GET: ChiTietMuonTras/Create
        public ActionResult Create(int id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ChiTietMuonTra chiTietMuonTra = new ChiTietMuonTra()
            {
                isbn = 0,
                ma_phieumuontra = id,
                ma_cuonsach = "CS00_000",
                ma_sinhvien = null,
                ngayGio_tra = null,
                soluong = 0,
                ghichu = null
            };
            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach");
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien");
            ViewBag.ma_cuonsach = new SelectList(db.CuonSaches.Where(s => s.TinhTrang == "Còn sách"), "ma_cuonsach", "ten_cuonsach");
            return View(chiTietMuonTra);
        }

        // POST: ChiTietMuonTras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_phieumuontra,ma_cuonsach,soluong,ghichu")] ChiTietMuonTra chiTietMuonTra)
        {
            var cuonSach = db.CuonSaches.First(cs => cs.ma_cuonsach == chiTietMuonTra.ma_cuonsach);
            if (cuonSach.TinhTrang == "Đang mượn")
            {
                TempData["ThongBaoFailed"] = "Cuốn sách đang được mượn!";
                return RedirectToAction("Create", new {id = chiTietMuonTra.ma_phieumuontra});
            }
            var phieuMuon = db.MuonTras.Find(chiTietMuonTra.ma_phieumuontra);
            if (ModelState.IsValid)
            {
                try
                {
                    chiTietMuonTra.isbn = cuonSach.isbn;
                    chiTietMuonTra.soluong = 1;
                    chiTietMuonTra.ngayGio_tra = null;
                    chiTietMuonTra.ma_sinhvien = phieuMuon.ma_sinhvien;
                    cuonSach.TinhTrang = "Đang mượn";
                    db.ChiTietMuonTras.Add(chiTietMuonTra);
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Thêm sách vào phiếu mượn thành công!";
                    return RedirectToAction("Create", new {id = phieuMuon.ma_phieumuontra});
                }
                catch(Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thêm sách vào phiếu thất bại.";
                    return RedirectToAction("Create", new { id = phieuMuon.ma_phieumuontra });
                }
            }

            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach", chiTietMuonTra.isbn);
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", chiTietMuonTra.ma_phieumuontra);
            return View(chiTietMuonTra);
        }

        // GET: ChiTietMuonTras/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.First(c => c.ma_phieumuontra == id);
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
                try
                {
                    db.Entry(chiTietMuonTra).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật chi tiết phiếu mượn thành công!";
                    return RedirectToAction("Edit", new { id = chiTietMuonTra.ma_phieumuontra });
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Cập nhật chi tiết phiếu mượn thất bại.";
                    return RedirectToAction("Edit", new { id = chiTietMuonTra.ma_phieumuontra });
                }
            }
            ViewBag.isbn = new SelectList(db.CuonSaches, "isbn", "ten_cuonsach", chiTietMuonTra.isbn);
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", chiTietMuonTra.ma_phieumuontra);
            return View(chiTietMuonTra);
        }

        // GET: ChiTietMuonTras/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

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
            try
            {
                ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.First(c => c.ma_phieumuontra == id);
                CuonSach cuonSach = db.CuonSaches.First(c => c.ma_cuonsach == chiTietMuonTra.ma_cuonsach);
                cuonSach.TinhTrang = "Còn sách";
                db.ChiTietMuonTras.Remove(chiTietMuonTra);
                db.SaveChanges();
                TempData["ThongBaoSuccess"] = "Xóa chi tiết phiếu mượn thành công!";
                return RedirectToAction("Details", "MuonTras", new {id = chiTietMuonTra.ma_phieumuontra});
            }
            catch(Exception ex)
            {
                ChiTietMuonTra chiTietMuonTra = db.ChiTietMuonTras.First(c => c.ma_phieumuontra == id);
                TempData["ThongBaoFailed"] = "Xóa chi tiết phiếu mượn thất bại";
                return RedirectToAction("Details", "MuonTras", new { id = chiTietMuonTra.ma_phieumuontra });
            }
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
