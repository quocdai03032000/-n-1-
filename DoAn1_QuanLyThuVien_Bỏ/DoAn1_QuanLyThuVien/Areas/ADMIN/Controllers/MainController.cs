using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_QuanLyThuVien.Models;

namespace DoAn1_QuanLyThuVien.Areas.ADMIN.Controllers
{
    public class MainController : Controller
    {
        QuanLyThuVienEntities database = new QuanLyThuVienEntities();
        // GET: ADMIN/Main

        //
        /******** Login ************/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Account_Admin ad)
        {
            var check = database.Account_Admin.Where(s => s.User.Equals(ad.User) && s.Password.Equals(ad.Password)).FirstOrDefault();
            if (check == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View("Index", ad);
            }
            else
            {
                Session["User"] = ad.User;
                return RedirectToAction("Main", "Main");
            }
        }

        /*********** Đăng xuất ******/
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Main");
        }

        /***************************************************/
        /******** Main ************/
        public ActionResult Main()
        {
            return View();
        }


        /******** Đầu sách ************/
        //public ActionResult DauSach(string _name)
        //{
        //    if (_name == null)
        //        return View(database.DauSaches.ToList());
        //    else
        //        return View(database.DauSaches.Where(s => s.TenSach.Contains(_name)).ToList());
        //    return View();
        //}

        public ActionResult DauSach()
        {
            return View();
        }

        /* Xem chi tiết đầu sách */
        public ActionResult DauSach_ViewDetail(int id)
        {
            return View(database.DauSaches.Where(a=>a.MaDauSach == id).FirstOrDefault());
        }

        public ActionResult test()
        {
            return View();
        }
        
    }
}