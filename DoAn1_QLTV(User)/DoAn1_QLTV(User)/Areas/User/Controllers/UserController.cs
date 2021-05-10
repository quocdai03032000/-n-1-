using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn1_QLTV_User_.Models;
using WebMatrix.WebData;

namespace DoAn1_QLTV_User_.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/User

        QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        public ActionResult Index()
        {
            return View();
        }

        //DauSach
        
        public ActionResult MainUser()
        {
            return View(db.DauSaches.ToList());
        }

        public ActionResult Details_Book(int id)
        {
            return View(db.DauSaches.Where(s => s.MaDauSach == id).FirstOrDefault()); 
        }

        public ActionResult Main()
        {
            return View(db.DauSaches.ToList());
        }


        //Dang ky the thu vien

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(DangKyTheTV tv)
        {
            try
            {
                var check = db.DangKyTheTVs.Where(s => s.MaThe.Equals(tv.MaThe) && s.Password == tv.Password).FirstOrDefault();

                if (check == null)
                {
                    ViewBag.ErrorInfo = "Failed";
                    return View("Login",tv);
                }
                else
                {
                    Session["MaThe"] = tv.MaThe;
                    Session["Password"] = tv.Password;
                    return RedirectToAction("Main");
                }
            }
            catch (Exception)
            {
                return Content( "Login Failed");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("MainUser");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(DangKyTheTV tv)
        {
            try
            {
                db.DangKyTheTVs.Add(tv);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return Content("Error");
            }
        }

        public ActionResult ChangePass()
        {
            if (Session["MaThe"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("MainUser");
            }
        }
        [HttpPost]
        public ActionResult ChangePass(DangKyTheTV tv ,string _pw,string _newPW,string _confirmPW)
        {
            string mathe = Session["MaThe"].ToString();
            string Password = Session["Password"].ToString();
            tv = db.DangKyTheTVs.Where(s => s.MaThe.Equals(mathe)).FirstOrDefault();

            if (tv != null&&Password==_pw)
            {
                if (_newPW == _confirmPW)
                {
                    tv.RePassword = _confirmPW;
                    tv.Password = _newPW;
                    db.Entry(tv).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Msg = "Change Password Success!!";
                }
                else
                {
                    ViewBag.Msg = "RePassword not match!!";
                }
                return View("ChangePass", tv);
            }
            else
            {
                ViewBag.Msg = "Pass incorrect!!";
                return View("ChangePass", tv);
            }
        }

/*        public ActionResult Detail_Account()
        {
            return View();
        }*/

 /*       [HttpPost]*/
        public ActionResult Detail_Account(DangKyTheTV tv)
        {
            string mathe = Session["MaThe"].ToString();
            tv = db.DangKyTheTVs.Where(s => s.MaThe.Equals(mathe)).FirstOrDefault();
            if (tv != null)
                return View("Detail_Account");
            else
                return View("Main");
        }
    }
}