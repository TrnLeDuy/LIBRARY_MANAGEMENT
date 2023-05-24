using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class CuonSachesController : Controller
    {
        private CNPM_QLTVEntities db = new CNPM_QLTVEntities();

        // GET: CuonSaches
        public ActionResult Index(string currentFilter, string s, int? page)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");


            int pageSize = 7;
            int pageNum = (page ?? 1);

            if(s != null)
            {
                page = 1;
            }
            else
            {
                s = currentFilter;
            }

            ViewBag.CurrentFilter = s;

            var danhSach = from l in db.CuonSaches
                           select l;
            if (!String.IsNullOrEmpty(s))
            {
                danhSach = danhSach.Where(mcs => mcs.ten_cuonsach.Contains(s) || mcs.DauSach.ten_dausach.Contains(s) || mcs.LoaiSach.ten_loaisach.Contains(s) || mcs.ma_cuonsach.Contains(s));
            }

            danhSach = danhSach.OrderBy(id => id.ma_cuonsach);

            return View(danhSach.ToPagedList(pageNum, pageSize));
        }

        // GET: CuonSaches/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach == id);
            if (cuonSach == null)
            {
                return HttpNotFound();
            }
            return View(cuonSach);
        }

        // GET: CuonSaches/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach");
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach");
            return View();
        }


        // POST: CuonSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "isbn,ten_cuonsach,tacgia,namxuatban,nhaxuatban,ma_loaisach,Mota")] CuonSach cuonSach, HttpPostedFileBase Hinhmota)
        {
            if(Hinhmota == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(db.CuonSaches.Any(s => s.ten_cuonsach == cuonSach.ten_cuonsach))
                    {
                        TempData["ThongBaoFailed"] = "Đã tồn tại cuốn sách " + cuonSach.ten_cuonsach;
                        return RedirectToAction("Create");
                    }
                    //Lấy tên file của hình được up
                    var fileName = Path.GetFileName(Hinhmota.FileName);

                    //Tạo đường dẫn > file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Kiểm tra hình đã tồn tại trong hệ thống chưa
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Ảnh đã tồn tại";
                    }
                    else
                    {
                        Hinhmota.SaveAs(path);
                    }
                    //Lưu tên sách vào trường Hinhmota
                    cuonSach.Hinhmota = fileName;

                    //Tự động tạo mã cuốn sách
                    //string nextID = GetNextBookID();
                    //cuonSach.ma_cuonsach = nextID;

                    var lastCuonsach = db.CuonSaches.OrderByDescending(s => s.ma_cuonsach).FirstOrDefault();
                    string id;
                    if (lastCuonsach.ma_cuonsach == null)
                    {
                        id = "CS" + cuonSach.isbn + "_001";
                        cuonSach.ma_cuonsach = id;
                    }
                    else
                    {
                        string newLast = lastCuonsach.ma_cuonsach.Split('_')[1];
                        cuonSach.ma_cuonsach = "CS" + cuonSach.isbn.ToString() + "_" + (int.Parse(newLast) + 1).ToString().PadLeft(3, '0');
                    }

                    cuonSach.TinhTrang = "Còn sách";


                    db.CuonSaches.Add(cuonSach);
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Đã thêm cuốn sách " + cuonSach.ten_cuonsach;
                    return RedirectToAction("Create");
                }
                catch(Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi thêm mới cuốn sách!";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // GET: CuonSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach== id);
            if (cuonSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // POST: CuonSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,ma_cuonsach,ten_cuonsach,tacgia,namxuatban,nhaxuatban,ma_loaisach,TinhTrang,Mota")] CuonSach cuonSach, HttpPostedFileBase Hinhmota)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Hinhmota != null)
                    {
                        //Lấy tên file của hình được up
                        var fileName = Path.GetFileName(Hinhmota.FileName);

                        //Tạo đường dẫn > file
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                        //Lưu tên
                        cuonSach.Hinhmota = fileName;
                        Hinhmota.SaveAs(path);
                    }

                    db.Entry(cuonSach).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ThongBaoSuccess"] = "Cập nhật thành công sách " + cuonSach.ten_cuonsach;
                    var queryString = "?id=" + cuonSach.ten_cuonsach;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ThongBaoFailed"] = "Thất bại khi cập nhật sách " + cuonSach.ten_cuonsach;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.isbn = new SelectList(db.DauSaches, "isbn", "ten_dausach", cuonSach.isbn);
            ViewBag.ma_loaisach = new SelectList(db.LoaiSaches, "ma_loaisach", "ten_loaisach", cuonSach.ma_loaisach);
            return View(cuonSach);
        }

        // GET: CuonSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach == id);
            if (cuonSach == null)
            {
                return HttpNotFound();
            }
            return View(cuonSach);
        }

        // POST: CuonSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            CuonSach cuonSach = db.CuonSaches.First(mcs => mcs.ma_cuonsach == id);
            try
            {
                db.CuonSaches.Remove(cuonSach);
                db.SaveChanges();
                TempData["ThongBaoSuccess"] = "Xóa thành công sách " + cuonSach.ten_cuonsach;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ThongBaoFailed"] = "Thất bại khi xóa cuốn sách " + cuonSach.ten_cuonsach;
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
