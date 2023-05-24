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
    public class TaiKhoansController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: TaiKhoans
        public ActionResult Index(string currentFilter, string search, int? page)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            int pageSize = 10;
            int pageNum = (page ?? 1);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var taikhoan = from l in db.TaiKhoans
                           select l;
            if (!String.IsNullOrEmpty(search))
            {
                taikhoan = taikhoan.Where(tk => tk.Username.Contains(search));
            }

            taikhoan = taikhoan.OrderBy(id => id.Username);

            return View(taikhoan.ToPagedList(pageNum, pageSize));
        }

        // GET: TaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ViewBag.MaNV = new SelectList(db.NhanViens.Where(e => !db.TaiKhoans.Any(u => u.MaNV == e.MaNV)), "MaNV", "Hoten");
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,Username,Password,LoaiTK")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                if(db.TaiKhoans.Any(a => a.Username == taiKhoan.Username))
                {
                    TempData["ThongBaoFailed"] = "Đã tồn tại tên người dùng " + taiKhoan.Username;
                    return RedirectToAction("Create");
                }
                try
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Thêm thành công tài khoản " + taiKhoan.Username;
                    return RedirectToAction("Create");
                }
                catch(Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi thêm tài khoản!";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,Username,Password,LoaiTK")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật thành công tài khoản " + taiKhoan.Username;
                    return RedirectToAction("Edit", new {id = taiKhoan.MaNV});
                }
                catch(Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật tài khoản " + taiKhoan.Username;
                    return RedirectToAction("Edit", new { id = taiKhoan.MaNV });
                }
                
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Hoten", taiKhoan.MaNV);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            try
            {
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
                TempData["ThongBaoSuccess"] = "Xóa thành công tài khoản " + taiKhoan.Username;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ThongBaoFailed"] = "Thất bại khi xóa tài khoản " + taiKhoan.Username;
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
