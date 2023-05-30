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
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ViewBag.ma_sinhvien = new SelectList(db.SinhViens.Where(e => !db.TheThuViens.Any(u => u.ma_sinhvien == e.ma_sinhvien)), "ma_sinhvien", "ma_sinhvien");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_sinhvien,Hoten, Ngaysinh, Tinhtrangthe")] TheThuVien theThuVien)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (db.TheThuViens.Any(a => a.ma_sinhvien == theThuVien.ma_sinhvien))
                    {
                        TempData["ThongBaoFailed"] = "Đã tồn tại Thẻ thư viện: " + theThuVien.ma_sinhvien;
                        return RedirectToAction("Create");
                    }
                    try
                    {
                        db.TheThuViens.Add(theThuVien);
                        db.SaveChanges();
                        TempData["ThongBaoSuccess"] = "Tạo thẻ thành công " + theThuVien.ma_sinhvien;
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        TempData["ThongBaoFailed"] = "Thất bại khi tạo thẻ!";
                        return RedirectToAction("Create");
                    }
                }

                ViewBag.ma_sinhvien = new SelectList(db.SinhViens, "ma_sinhvien", "ma_sinhvien", theThuVien.ma_sinhvien);
                
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
                try
                {
                    db.Entry(theThuVien).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật thành công thẻ thư viện " + theThuVien.ma_sinhvien;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật thẻ thư viện " + theThuVien.ma_sinhvien;
                    return RedirectToAction("Edit", new { id = theThuVien.ma_sinhvien });
                }
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