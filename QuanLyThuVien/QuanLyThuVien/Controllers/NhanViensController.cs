using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyThuVien.Models;
using PagedList;

namespace QuanLyThuVien.Controllers
{
    public class NhanViensController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: NhanViens
        public ActionResult Index(string currentFilter, string search, int? page)
        {
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

            var employees = from l in db.NhanViens
                            select l;
            if (!String.IsNullOrEmpty(search))
            {
                employees = db.NhanViens.Where((
                        nhanvien => nhanvien.MaNV.ToString().Contains(search) ||
                        nhanvien.Hoten.Contains(search) ||
                        nhanvien.SDT.Contains(search) ||
                        nhanvien.Email.Contains(search)));
            }
            employees = employees.OrderBy(id => id.MaNV);

            return View(employees.ToPagedList(pageNum, pageSize));
        }

        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.TaiKhoans, "MaNV", "Username");
            return View();
        }

        public int GetNextUserID()
        {
            int id = 1;
            if (db.NhanViens.Any())
            {
                id = db.NhanViens.Max(nv => nv.MaNV) + 1;
            }
            return id;
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hoten,NgaySinh,Gioitinh,Email,SDT,Diachi")] NhanVien nhanVien)
        {
            int nextUserID = GetNextUserID();
            nhanVien.MaNV = nextUserID;
            if (db.NhanViens.Any())
            {

            }
            if (ModelState.IsValid)
            {

                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.TaiKhoans, "MaNV", "Username", nhanVien.MaNV);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hoten,NgaySinh,Gioitinh,Email,SDT,Diachi")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.TaiKhoans, "MaNV", "Username", nhanVien.MaNV);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if(taiKhoan != null)
            {
                db.TaiKhoans.Remove(taiKhoan);
            }
            db.NhanViens.Remove(nhanVien);
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
