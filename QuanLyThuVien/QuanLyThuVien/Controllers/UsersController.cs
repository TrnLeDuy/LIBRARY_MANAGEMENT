using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class UsersController : Controller
    {
        //DBContext
        CNPM_QLTVEntities db = new CNPM_QLTVEntities();


        /*ACTION ĐĂNG NHẬP (LOG IN)*/
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(TaiKhoan users)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(users.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(users.Password))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var user = db.TaiKhoans.FirstOrDefault(k => k.Username == users.Username && k.Password == users.Password);
                    if (user != null)
                    {
                        //Lưu thông vào session
                        if (user.LoaiTK == "AD")
                        {
                            Session["Admin"] = user;
                            return View("~/Views/Admin/Dashboard.cshtml");
                        }
                        else if (user.LoaiTK == "TT")
                        {
                            Session["ThuThu"] = user;
                            return View("~/Views/Librarian/Dashboard.cshtml");
                        }
                        else
                            return View("~/Views/Employee/Dashboard.cshtml");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["Account"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }
    }
}