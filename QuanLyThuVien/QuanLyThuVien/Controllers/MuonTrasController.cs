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
    public class MuonTrasController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: MuonTras
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

            var danhSach = from l in db.MuonTras
                           select l;
            if (!String.IsNullOrEmpty(s))
            {
                danhSach = danhSach.Where(mcs => mcs.ma_phieumuontra.ToString().Contains(s) || mcs.NhanVien.Hoten.Contains(s) || mcs.ngayGio_muon.ToString().Contains(s) || mcs.ngay_hethan.ToString().Contains(s));
            }

            danhSach = danhSach.OrderBy(id => id.ngay_hethan);

            return View(danhSach.ToPagedList(pageNum, pageSize));
        }

        // GET: MuonTras/Details/5
        public ActionResult Details(int id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuonTra muonTra = db.MuonTras.Find(id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }
            return View(muonTra);
        }

        // GET: MuonTras/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten");
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "ma_sinhvien");
            ViewBag.ma_phieumuontra = new SelectList(db.PhieuPhats, "ma_phieumuontra", "ma_sinhvien");
            return View();
        }

        // POST: MuonTras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_phieumuontra,MaNV,ngayGio_muon,ma_sinhvien,ngay_hethan")] MuonTra muonTra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nhanVien = Session["Account"] as TaiKhoan;
                    if (nhanVien != null)
                    {
                        muonTra.MaNV = nhanVien.MaNV;
                    }
                    muonTra.ngayGio_muon = DateTime.Now;
                    db.MuonTras.Add(muonTra);
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Phiếu mượn số " + muonTra.ma_phieumuontra.ToString() + " đã được thêm!";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi thêm mới phiếu mượn";
                    return RedirectToAction("Create");
                }
                
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", muonTra.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", muonTra.ma_sinhvien);
            ViewBag.ma_phieumuontra = new SelectList(db.PhieuPhats, "ma_phieumuontra", "ma_sinhvien", muonTra.ma_phieumuontra);
            return View(muonTra);
        }

        // GET: MuonTras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuonTra muonTra = db.MuonTras.Find(id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", muonTra.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", muonTra.ma_sinhvien);
            ViewBag.ma_phieumuontra = new SelectList(db.PhieuPhats, "ma_phieumuontra", "ma_sinhvien", muonTra.ma_phieumuontra);
            return View(muonTra);
        }

        // POST: MuonTras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_phieumuontra,ma_sinhvien,MaNV,ngayGio_muon,ngay_hethan")] MuonTra muonTra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(muonTra).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật thành công phiếu mượn " + muonTra.ma_phieumuontra.ToString();
                    return RedirectToAction("Edit", new { id = muonTra.ma_phieumuontra});
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật phiếu mượn " + muonTra.ma_phieumuontra.ToString();
                    return RedirectToAction("Edit", new { id = muonTra.ma_phieumuontra });
                }
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", muonTra.MaNV);
            ViewBag.ma_sinhvien = new SelectList(db.TheThuViens, "ma_sinhvien", "Hoten", muonTra.ma_sinhvien);
            ViewBag.ma_phieumuontra = new SelectList(db.PhieuPhats, "ma_phieumuontra", "ma_sinhvien", muonTra.ma_phieumuontra);
            return View(muonTra);
        }

        // GET: MuonTras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuonTra muonTra = db.MuonTras.Find(id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }
            return View(muonTra);
        }

        // POST: MuonTras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MuonTra muonTra = db.MuonTras.Find(id);
                var chiTietMuon = db.ChiTietMuonTras.Where(c => c.ma_phieumuontra == id).ToList();
                foreach (var item in chiTietMuon)
                {
                    db.ChiTietMuonTras.Remove(item);
                }
                db.MuonTras.Remove(muonTra);
                db.SaveChanges();
                TempData["ThongBaoSuccess"] = "Xóa thành công phiếu mượn " + muonTra.ma_phieumuontra.ToString();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                MuonTra muonTra = db.MuonTras.Find(id);
                TempData["ThongBaoFailed"] = "Thất bại khi xóa phiếu mượn " + muonTra.ma_phieumuontra.ToString();
                return RedirectToAction("Index");
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
