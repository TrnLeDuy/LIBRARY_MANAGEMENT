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
    public class PhieuPhatsController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: PhieuPhats
        public ActionResult Index(string currentFilter, string s, int? page)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");


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

            var phieuPhat = from l in db.PhieuPhats
                           select l;
            if (!String.IsNullOrEmpty(s))
            {
                phieuPhat = phieuPhat.Where(p => p.ma_phieumuontra.ToString().Contains(s) ||
                                                p.NhanVien.Hoten.Contains(s) ||
                                                p.ngay_lapphieu.ToString().Contains(s) ||
                                                p.nguyen_nhan.Contains(s) ||
                                                p.TheThuVien.Hoten.Contains(s));
            }

            phieuPhat = phieuPhat.OrderBy(id => id.ngay_lapphieu);

            return View(phieuPhat.ToPagedList(pageNum, pageSize));
        }

        // GET: PhieuPhats/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = db.PhieuPhats.Find(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhat);
        }

        // GET: PhieuPhats/Create
        public ActionResult Create()
        {
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras.Where(e => !db.PhieuPhats.Any(u => u.ma_phieumuontra == e.ma_phieumuontra)), "ma_phieumuontra", "ma_phieumuontra");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten");
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten");
            return View();
        }

        // POST: PhieuPhats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_phieumuontra,nguyen_nhan,Songay_quahan,Tongtien,Sotienthu,Conlai")] PhieuPhat phieuPhat)
        {
            if (ModelState.IsValid)
            {
                var phieuMuon = db.MuonTras.Find(phieuPhat.ma_phieumuontra);
                var nhanVien = Session["Account"] as TaiKhoan;
                if(nhanVien != null)
                {
                    phieuPhat.MaNV = nhanVien.MaNV;
                }
                phieuPhat.ma_sinhvien = phieuMuon.ma_sinhvien;
                phieuPhat.ngay_lapphieu = DateTime.Now;
                db.PhieuPhats.Add(phieuPhat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_sinhvien", phieuPhat.ma_phieumuontra);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", phieuPhat.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", phieuPhat.ma_sinhvien);
            return View(phieuPhat);
        }

        // GET: PhieuPhats/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = db.PhieuPhats.Find(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_phieumuontra", phieuPhat.ma_phieumuontra);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", phieuPhat.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", phieuPhat.ma_sinhvien);
            return View(phieuPhat);
        }

        // POST: PhieuPhats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_phieumuontra,MaNV,ma_sinhvien,nguyen_nhan,ngay_lapphieu,Songay_quahan,Tongtien,Sotienthu,Conlai")] PhieuPhat phieuPhat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(phieuPhat).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật phiếu phạt thành công!";
                    return RedirectToAction("Edit", new { id = phieuPhat.ma_phieumuontra });
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật phiếu phạt ";
                    return RedirectToAction("Edit", new { id = phieuPhat.ma_phieumuontra});
                }

            }
            ViewBag.ma_phieumuontra = new SelectList(db.MuonTras, "ma_phieumuontra", "ma_phieumuontra", phieuPhat.ma_phieumuontra);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", phieuPhat.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", phieuPhat.ma_sinhvien);
            return View(phieuPhat);
        }

        // GET: PhieuPhats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = db.PhieuPhats.Find(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhat);
        }

        // POST: PhieuPhats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuPhat phieuPhat = db.PhieuPhats.Find(id);
            db.PhieuPhats.Remove(phieuPhat);
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
