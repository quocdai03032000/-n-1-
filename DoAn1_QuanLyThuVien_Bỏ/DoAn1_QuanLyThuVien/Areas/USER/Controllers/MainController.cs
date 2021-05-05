using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_QuanLyThuVien.Models;

namespace DoAn1_QuanLyThuVien.Areas.USER.Controllers
{
    public class MainController : Controller
    {
        QuanLyThuVienEntities database = new QuanLyThuVienEntities();
        // GET: USER/Main
        public ActionResult Index()
        {
            return View();
        }



        /*****************/
        /***** Login *****/
        /*****************/
        public ActionResult Login()
        {
            return View();
        }

        /*****************/
        /***** Register***/
        /*****************/
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(DangKyTheTV user)
        {
            user.NgayLam = DateTime.Now;
            database.DangKyTheTVs.Add(user);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        /*****************/
        /***** Contact ***/
        /*****************/
        public ActionResult Contact()
        {
            return View();
        }
    }
}