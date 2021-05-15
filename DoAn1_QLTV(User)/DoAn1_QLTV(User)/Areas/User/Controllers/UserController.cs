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
        
        public ActionResult MainUser(string text)
        {
            if (text == null)
                return View(db.DauSaches.ToList());
            else
            {
                return View(db.DauSaches.Where(s => s.TenSach.Contains(text)||s.TheLoai.Contains(text)).ToList());
            }
        }


        public ActionResult Main(string text)
        {
            if (text == null)
                return View(db.DauSaches.ToList());
            else
            {
                return View(db.DauSaches.Where(s => s.TenSach.Contains(text)||s.TheLoai.Contains(text)).ToList());
            }
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

        public ActionResult Detail_Account(DangKyTheTV tv)
        {
            string mathe = Session["MaThe"].ToString();
            tv = db.DangKyTheTVs.Where(s => s.MaThe.Equals(mathe)).FirstOrDefault();
            if (tv != null)
                return View("Detail_Account",tv);
            else
                return View("Main");
        }

        //Mượn sách
        /* [HttpPost]
         public ActionResult Register_RentBook(DauSach ds)
         {
             if (Session["MaThe"] == null)
             {
                 return RedirectToAction("Login");
             }
             else
             {
                 ds = db.DauSaches.Where(m => m.MaDauSach.Equals(ds.MaDauSach)).FirstOrDefault();
                 DKyMuonSach s = new DKyMuonSach();
                 s.MaDangKyMuonSach = "abc";
                 s.MaThe = Session["MaThe"].ToString();
                 s.MaSach = 1;
                 db.DKyMuonSaches.Add(s);
                 db.SaveChanges();
                 ViewBag.Msg_rentbook = "Mượn thành công";
                 return RedirectToAction("Register_RentBook");
             }
         }

         public ActionResult Details_Book(int id)
         {
             return View(db.DauSaches.Where(s => s.MaDauSach.Equals(id)).FirstOrDefault());
         }*/

        /* public ActionResult Register_RentBook()
         {
             return View(db.DKyMuonSaches.ToList());
         }*/

        

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddItem(int id)
        {
            var ds = db.DauSaches.SingleOrDefault(s => s.MaDauSach.Equals(id));
            if (Session["MaThe"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ds != null)
                {
                    GetCart().add_item(ds);
                }
                return RedirectToAction("Cart", "User");
            }
        }

        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Cart", "User");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult Details_Book(int id)
        {
            return View(db.DauSaches.Where(s => s.MaDauSach.Equals(id)).FirstOrDefault());
        }

/*        public ActionResult sach()
        {            
            return View(db.Saches.ToList());
        }*/
    }
}