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
        public ActionResult LogIn([Bind(Include = "Username,Password")] TaiKhoan users)
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
                        Session["Account"] = user;
                        Session["Username"] = user.Username;
                        Session["Fullname"] = user.NhanVien.Hoten;
                        Session["EmployeeID"] = user.MaNV;
                        Session["Role"] = user.LoaiTK;    
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return Redirect("~/Dashboard/Dashboard");       
        }

        public ActionResult Logout()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["Account"] = null;
            Session["Fullname"] = null;
            Session["Username"] = null;
            Session["EmployeeID"] = null;
            Session["Role"] = null;
            Session.Abandon();
            return RedirectToAction("/");
        }
    }
}