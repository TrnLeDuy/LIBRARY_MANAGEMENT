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
    public class DauSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: DauSaches
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

            var dauSach = from l in db.DauSaches
                           select l;
            if (!String.IsNullOrEmpty(s))
            {
                dauSach = dauSach.Where(mcs => mcs.ten_dausach.Contains(s));
            }

            

            dauSach = dauSach.OrderBy(id => id.isbn);
            foreach(var item in dauSach)
            {
                item.soluong = db.CuonSaches.Count(i => i.isbn == item.isbn);
            }

            return View(dauSach.ToPagedList(pageNum, pageSize));
        }

        // GET: DauSaches/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DauSach dauSach = db.DauSaches.Find(id);
            if (dauSach == null)
            {
                return HttpNotFound();
            }
            return View(dauSach);
        }

        // GET: DauSaches/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            return View();
        }

        public int GetNextCateID()
        {
            int dsID = 1;
            if (db.DauSaches.Any(s => s.isbn == dsID))
            {
                dsID = db.DauSaches.Max(id => id.isbn) + 1;
            }
            return dsID;
        }

        // POST: DauSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ten_dausach")] DauSach dauSach)
        {
            if (ModelState.IsValid)
            {
                if(db.DauSaches.Any(d => d.ten_dausach == dauSach.ten_dausach))
                {
                    TempData["ThongBaoFailed"] = "Đã tồn tại đầu sách " + dauSach.ten_dausach;
                    return RedirectToAction("Index");
                }
                try
                {
                    int nextCateID = GetNextCateID();
                    dauSach.isbn = nextCateID;
                    dauSach.soluong = db.CuonSaches.Count(i => i.isbn == dauSach.isbn);
                    dauSach.trangthai = "Hết sách";
                    db.DauSaches.Add(dauSach);
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Thêm thành công đầu sách " + dauSach.ten_dausach;
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi thêm đầu sách!";
                    return RedirectToAction("Create");
                }
                
            }

            return View(dauSach);
        }

        // GET: DauSaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DauSach dauSach = db.DauSaches.Find(id);
            if (dauSach == null)
            {
                return HttpNotFound();
            }
            return View(dauSach);
        }

        // POST: DauSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,ten_dausach,soluong,trangthai")] DauSach dauSach)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(dauSach).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật thành công đầu sách " + dauSach.ten_dausach;
                    return RedirectToAction("Edit", new { id = dauSach.isbn});
                }
                catch(Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật đầu sách " + dauSach.ten_dausach;
                    return RedirectToAction("Edit", new { id = dauSach.isbn });
                }
            }
            return View(dauSach);
        }

        // GET: DauSaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DauSach dauSach = db.DauSaches.Find(id);
            if (dauSach == null)
            {
                return HttpNotFound();
            }
            return View(dauSach);
        }

        // POST: DauSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DauSach dauSach = db.DauSaches.Find(id);
            try
            {
                db.DauSaches.Remove(dauSach);
                db.SaveChanges();
                TempData["ThongBaoSuccess"] = "Xóa thành công đầu sách " + dauSach.ten_dausach;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ThongBaoFailed"] = "Thất bại khi xóa đầu sách " + dauSach.ten_dausach;
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

        public ActionResult CapNhatTrangThai()
        {
            try
            {
                var dauSach = db.DauSaches.ToList();
                foreach (var item in dauSach)
                {
                    if (db.CuonSaches.Count(s => s.isbn == item.isbn) > 0)
                        item.trangthai = "Còn sách";
                    else
                        item.trangthai = "Hết sách";
                    db.SaveChanges();
                }
                TempData["ThongBaoSuccess"] = "Cập nhật trạng thái đầu sách thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ThongBaoFailed"] = "Cập nhật trạng thái đầu sách thất bại!";
                return RedirectToAction("Index");
            }
        }
    }
}
