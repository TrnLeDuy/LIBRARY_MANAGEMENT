using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Management.Models;
using PagedList;
using PagedList.Mvc;

namespace Library_Management.Controllers
{
    public class DauSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: DauSaches
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageNum = (page ?? 1);

            var dauSaches = db.DauSaches.Include(d => d.TuaSach).ToList();
            return View(dauSaches.ToPagedList(pageNum, pageSize));
        }

        // GET: DauSaches/Details/5
        public ActionResult Details(int? id)
        {
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
            ViewBag.ma_tuasach = new SelectList(db.TuaSaches, "ma_tuasach", "TuaSach1");
            return View();
        }


        // POST: DauSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "isbn,ma_tuasach,ngonngu,bia,trangthai")] DauSach dauSach, HttpPostedFileBase bia)
        {
            if (ModelState.IsValid)
            {

                if (bia != null)
                {
                    var fileName = Path.GetFileName(bia.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    dauSach.bia = fileName;
                    bia.SaveAs(path);
                }

                db.DauSaches.Add(dauSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_tuasach = new SelectList(db.TuaSaches, "ma_tuasach", "TuaSach1", dauSach.ma_tuasach);
            return View(dauSach);
        }

        // GET: DauSaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DauSach dauSach = db.DauSaches.Find(id);
            if (dauSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_tuasach = new SelectList(db.TuaSaches, "ma_tuasach", "ma_loaisach", dauSach.ma_tuasach);
            return View(dauSach);
        }

        // POST: DauSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,ma_tuasach,ngonngu,bia,trangthai")] DauSach dauSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dauSach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_tuasach = new SelectList(db.TuaSaches, "ma_tuasach", "ma_loaisach", dauSach.ma_tuasach);
            return View(dauSach);
        }

        // GET: DauSaches/Delete/5
        public ActionResult Delete(int? id)
        {
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
            db.DauSaches.Remove(dauSach);
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
