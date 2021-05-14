using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_QuanLyThuVien.Models;
namespace DoAn1_QuanLyThuVien.Areas.User.Controllers
{
    public class MainController : Controller
    {
        QuanLyThuVienEntities database = new QuanLyThuVienEntities();
        public ActionResult Index(string text)
        {
            if (text == null)
                return View(database.DauSaches.ToList());
            else
            {
                return View(database.DauSaches.Where(s => s.TenSach.Contains(text) || s.TheLoai.Contains(text)).ToList());
            }
        }

        /*----- LOGIN -----*/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string tk,string pass)
        {
            //kiểm tra tài khoản có trong bảng thẻ tv và có bị block hay không?
            var check = database.TheThuViens.Where(a => a.MaThe == tk && a.Password == pass && a.MaTinhTrang==2).FirstOrDefault();
            //kiểm tra tài khoản đã được kích hoạt chưa ?
            var checkActive = database.DangKyTheTVs.Where(a => a.MaThe == tk).FirstOrDefault();
            //kiểm tra tài khoản có bị block hay k ?
            var checkBlock = database.TheThuViens.Where(a => a.MaThe == tk && a.Password == pass && a.MaTinhTrang != 2).FirstOrDefault();
            if (check!=null)
            {
                Session["User"] = check.HoTen;
                return RedirectToAction("Index", "Main");               
            }
            else if(checkBlock!=null)
            {
                ViewBag.MessLogin = "Tài khoản của bạn đã bị BLOCK";
                return View("Login");
            }
            else if(checkActive!=null)
            {
                ViewBag.MessLogin = "Tài khoản chưa được kích hoạt";
                return View("Login");
            }
            else
            {
                ViewBag.MessLogin = "Sai tài khoản hoặc mật khẩu!";
                return View("Login");
            }
        }
        /*----- Logout -----*/
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Main");

        }

        #region Thẻ thư viện
        /*----- Tạo thẻ thư viện -----*/
        public ActionResult Register()
        {
            DangKyTheTV theTV = new DangKyTheTV();
            return View(theTV);
        }
        [HttpPost]
        public ActionResult Register(DangKyTheTV theTV,string ConfirmPass)
        {
            var CheckTheTV = database.DangKyTheTVs.Where(a => a.MaThe == theTV.MaThe).SingleOrDefault();
            var check = database.TheThuViens.Where(a => a.MaThe == theTV.MaThe).SingleOrDefault();       
            
            if (theTV.Password == ConfirmPass)
            {
                if (CheckTheTV == null && check == null)
                {
                    theTV.NgayLam = DateTime.Now;
                    database.DangKyTheTVs.Add(theTV);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Đã tồn tại MSSV này!";
                    return View("Register");
                }
            }
            else
            {
                ViewBag.Error = "Mật khẩu không trùng khớp";
                return View("Register");
            }
        }
        #endregion

        [HttpGet]
        public ActionResult ChiTietSach(int id)
        {
            return View(database.Saches.Where(a=>a.MaDauSach==id).ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}