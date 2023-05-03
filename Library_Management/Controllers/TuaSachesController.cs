using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Management.Models;
using PagedList;

namespace Library_Management.Controllers
{
    public class TuaSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: TuaSaches
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "User");

            int pageSize = 7;
            int pageNum = (page ?? 1);

            var tuaSaches = db.TuaSaches.Include(t => t.LoaiSach).ToList();
            return View(tuaSaches.ToPagedList(pageNum, pageSize));
        }

        // GET: TuaSaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuaSach tuaSach = db.TuaSaches.Find(id);
            if (tuaSach == null)
            {
                return HttpNotFound();
            }
            return View(tuaSach);
        }

        // GET: TuaSaches/Create
        public ActionResult Create()
        {

            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach");
            return View();
        }

        // POST: TuaSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_loaisach,TuaSach1,tacgia,tomtat,namxuatban")] TuaSach tuaSach)
        {
            if (ModelState.IsValid)
            {
                db.TuaSaches.Add(tuaSach);
                tuaSach.ma_tuasach = GenerateMatuasach();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", tuaSach.ma_loaisach);
            return View(tuaSach);
        }

        // GET: TuaSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuaSach tuaSach = db.TuaSaches.Find(id);
            if (tuaSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", tuaSach.ma_loaisach);
            return View(tuaSach);
        }

        // POST: TuaSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_tuasach,ma_loaisach,TuaSach1,tacgia,tomtat,namxuatban")] TuaSach tuaSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuaSach).State = System.Data.Entity.EntityState.Modified;
                tuaSach.ma_tuasach = GenerateMatuasach();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", tuaSach.ma_loaisach);
            return View(tuaSach);
        }

        // GET: TuaSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuaSach tuaSach = db.TuaSaches.Find(id);
            if (tuaSach == null)
            {
                return HttpNotFound();
            }
            return View(tuaSach);
        }

        // POST: TuaSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TuaSach tuaSach = db.TuaSaches.Find(id);
            DauSach dausach = db.DauSaches.FirstOrDefault(ds => ds.ma_tuasach == id); 
            db.TuaSaches.Remove(tuaSach);
            db.DauSaches.Remove(dausach);
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

        //

        private string prefix = "NB";
        private string lastMatuasach;

        //Generate User ID
        private string GenerateTuasachID()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999);
            return "KH" + randomNumber.ToString();
        }

        public void AccountController()
        {
            var lastRecord = db.TuaSaches.OrderByDescending(x => x.ma_tuasach).FirstOrDefault();
            if (lastRecord != null)
            {
                lastMatuasach= lastRecord.ma_tuasach;
            }
        }


        //Prefix for TuaSach.Matuasach
        private string GenerateMatuasach()
        {
            AccountController();
            if (string.IsNullOrEmpty(lastMatuasach))
            {
                lastMatuasach = prefix + "00000001"; // assign a default starting value
                return lastMatuasach;
            }

            int lastNumber;
            string numberPart = lastMatuasach.Substring(prefix.Length);
            if (!int.TryParse(numberPart, out lastNumber))
            {
                {
                    lastNumber = 0;
                }
            }
            lastNumber++;
            int maxNumber = (int)Math.Pow(10, lastMatuasach.Length - prefix.Length) - 1;
            lastNumber = Math.Min(lastNumber, maxNumber);
            lastMatuasach = prefix + lastNumber.ToString().PadLeft(lastMatuasach.Length - prefix.Length, '0');
            return lastMatuasach;
        }
    }
}
