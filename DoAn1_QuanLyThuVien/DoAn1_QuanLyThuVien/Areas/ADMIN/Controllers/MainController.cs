using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_QuanLyThuVien.Models;
using System.IO;

namespace DoAn1_QuanLyThuVien.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        QuanLyThuVienEntities database = new QuanLyThuVienEntities();

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

        /*----- Đăng xuất -----*/

        public ActionResult Logout(Account_Admin ad)
        {
            Session.Abandon();
            return RedirectToAction("Index", "Main");

        }

        /*---- Đổi mật khẩu ----*/
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(Account_Admin ad, string _name, string _newPass, string _nhapLaiPass)
        {
            ad = database.Account_Admin.Where(s => s.MaAccount == 1 && s.Password == _name).FirstOrDefault();
            //var check = database.Account_Admin.Where(s=>s.Password == _name && s.MaAccount==1).FirstOrDefault();            
            if (ad != null)
            {
                if (_newPass == _nhapLaiPass)
                {
                    ad.Password = _newPass;
                    database.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    ViewBag.Error_ChangePass = "Đổi mật khẩu thành công";
                }
                else
                {
                    ViewBag.Error_ChangePass = "Mật khẩu không trùng khớp";
                }
                return View("ChangePass", ad);
            }
            else
            {
                ViewBag.Error_ChangePass = "Mật khẩu cũ không đúng!";
                return View("ChangePass", ad);
            }

        }


        /******** Main ************/
        public ActionResult Main()
        {
            /*Kiểm tra xem đã đăng nhập chưa ?*/
            if (Session["User"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Main");

        }

        /* ---- Đầu sách -----*/
        public ActionResult DauSach(string _name)
        {
            if (Session["User"] != null)
            {
                if (_name == null)
                {
                    return View(database.DauSaches.ToList());
                }
                else
                    return View(database.DauSaches.Where(s => s.TenSach.Contains(_name)).ToList());
            }
            else
                return RedirectToAction("Index", "Main");

        }

        public ActionResult ThemDauSach()
        {
            DauSach ds = new DauSach();
            return View(ds);
        }
        [HttpPost]
        public ActionResult ThemDauSach(string MaDauSach, DauSach sach)
        {
            
            try
            {
                if (sach.imageUploader != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sach.imageUploader.FileName);
                    string extension = Path.GetExtension(sach.imageUploader.FileName);
                    fileName = fileName + extension;
                    sach.HinhAnh = "~/Content/imgsach/" + fileName;
                    sach.imageUploader.SaveAs(Path.Combine(Server.MapPath("~/Content/imgsach/"), fileName));
                }
                int maDauSach = int.Parse(MaDauSach);
                var check = database.DauSaches.Where(a => a.MaDauSach == maDauSach).SingleOrDefault();

                if (check == null)
                {
                    sach.SoLuong = 0;
                    database.DauSaches.Add(sach);
                    database.SaveChanges();
                    return RedirectToAction("DauSach", "Main");

                }
                else
                {
                    ModelState.AddModelError("", "Trùng Mã Đầu Sách");
                    return View(sach);
                }
            }
            catch
            {
                return View(Content("Dữ Liệu đã tồn tại!!"));
            }

        }


        public ActionResult SuaDauSach(int id)
        {           
            return View(database.DauSaches.Where(a => a.MaDauSach == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult SuaDauSach(int id, DauSach a)
        {
            a = database.DauSaches.Where(item => item.MaDauSach == id).SingleOrDefault();
            database.Entry(a).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            ViewBag.SuaDauSach_suss = "Sửa thành công !!!";
            return View("SuaDauSach", a);
        }
        [HttpGet]
        public ActionResult XoaDauSach(int id)
        {
            var check = database.Saches.Where(a => a.MaDauSach == id).SingleOrDefault();
            if(check==null)
            {
                var DauSach = database.DauSaches.Where(a => a.MaDauSach == id).SingleOrDefault();
                database.DauSaches.Remove(DauSach);
                database.SaveChanges();                
            }
            else
            {
                ViewBag.Error_XoaDauSach = "Không thể xoá vì còn tồn tại sách thuộc đầu sách này ";
            }
            return RedirectToAction("DauSach", "Main");

        }

        /*----- Sách -----*/
        public ActionResult Sach(string _name)
        {
            if (Session["User"] != null)
            {
                if (_name == null)
                {
                    return View(database.Saches.ToList());
                }
                else
                    return View(database.Saches.Where(s => s.DauSach.TenSach.Contains(_name)).ToList());
            }
            else
                return RedirectToAction("Index", "Main");
        }
        /*----- Thêm Sách -----*/
        public ActionResult ThemSach(int id)
        {
            ViewBag.Id_dauSach = id.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult ThemSach(int id, string sks)
        {
            int soKiemSoat = int.Parse(sks);
            var DauSach = database.DauSaches.Where(a => a.MaDauSach == id).SingleOrDefault();
            var check = database.Saches.Where(a => a.MaDauSach == id && a.SoKiemSoat == soKiemSoat).SingleOrDefault();
            if (check == null)
            {
                var a = new Sach();
                a.MaDauSach = id;
                a.MaTinhTrangSach = 1;
                a.SoKiemSoat = soKiemSoat;
                /*----- Cap nhat so luong -----*/
                DauSach.SoLuong += 1;
                database.Saches.Add(a);
                database.SaveChanges();
                ViewBag.ThemSach_message_suss = "Thêm thành công!";
                return RedirectToAction("DauSach", "Main");
            }
            else
            {
                ViewBag.ThemSach_message_error = "Trùng số kiểm soát, hãy nhập lại!";
                return View("ThemSach");
            }

        }
        /*----- Xoá sách -----*/
        [HttpGet]
        public ActionResult XoaSach(int id)
        {
            var xoaSach = database.Saches.Where(a => a.id == id).SingleOrDefault();
            var dauSach = database.DauSaches.Where(a => a.MaDauSach == xoaSach.MaDauSach).SingleOrDefault();
            /*----- Cap nhat so luon dau sach ----- */
            dauSach.SoLuong -= 1;
            database.Saches.Remove(xoaSach);
            database.SaveChanges();
            return RedirectToAction("Sach", "Main");
        }

        /*----- Thẻ TV -----*/
        public ActionResult DsTheThuVien(string _name)
        {
            if (Session["User"] != null)
            {
                if (_name == null)
                {
                    return View(database.TheThuViens.ToList());
                }
                else
                    return View(database.TheThuViens.Where(s => s.MaThe.Contains(_name)).ToList());
            }
            else
                return RedirectToAction("Index", "Main");

        }
        /*----- block thẻ thư viện -----*/
        [HttpGet]
        public ActionResult BlockTheThuVien(string id)
        {            
            var theTV = database.TheThuViens.Where(a => a.MaThe == id).SingleOrDefault();
            theTV.MaTinhTrang = 1;
            database.Entry(theTV).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DsTheThuVien", "Main");
        }
        /*----- Xoá thẻ thư viện -----*/
        [HttpGet]
        public ActionResult XoaTheThuVien(string id)
        {            
            var xoaTheTV = database.TheThuViens.Where(a => a.MaThe == id).SingleOrDefault();
            database.TheThuViens.Remove(xoaTheTV);
            database.SaveChanges();
            return RedirectToAction("DsTheThuVien", "Main");
        }
        
        
        
        /*----- Danh Sách đăng ký thẻ TV -----*/
        public ActionResult DsDangKyTheTV(string _name)
        {
            if (Session["User"] != null)
            {
                if (_name == null)
                {
                    return View(database.DangKyTheTVs.ToList());
                }
                else
                    return View(database.DangKyTheTVs.Where(s => s.MaThe.Contains(_name)).ToList());
            }
            else
                return RedirectToAction("Index", "Main");

        }
        /*----- Thêm thẻ thư viện từ danh sách đăng ký thẻ -----*/
        [HttpGet]
        public ActionResult ThemTheTV_form_DS(int id)
        {
            //Thêm thẻ thư viện từ bảng đăng ký thẻ sang thẻ thư viện
            var DangKyTheTV = database.DangKyTheTVs.Where(a => a.MaDangKyThe == id).SingleOrDefault();
            var TheTV = new TheThuVien();
            TheTV.MaThe = DangKyTheTV.MaThe;
            TheTV.MaTinhTrang = 2;
            TheTV.Password = DangKyTheTV.Password;
            TheTV.HoTen = DangKyTheTV.HoTen;
            TheTV.NgayLam = DangKyTheTV.NgayLam;
            //lấy ra và ép kiểu datetime từ database
            //DateTime NgayHetHan = DateTime.Parse(DangKyTheTV.NgayLam.ToString());
            //TheTV.NgayHetHan = NgayHetHan.AddYears(4);
            database.TheThuViens.Add(TheTV);
            //Đồng thời xoá thẻ tv trong bảng đăng ký thẻ tv
            database.DangKyTheTVs.Remove(DangKyTheTV);
            database.SaveChanges();
            return RedirectToAction("DsDangKyTheTV", "Main");
        }

        /*----- Xoá thẻ thư viện từ danh sách đăng ký thẻ -----*/
        [HttpGet]
        public ActionResult XoaTheTV_form_DS(int id)
        {
            var DangKyTheTV = database.DangKyTheTVs.Where(a => a.MaDangKyThe == id).SingleOrDefault();
            database.DangKyTheTVs.Remove(DangKyTheTV);
            database.SaveChanges();
            return RedirectToAction("DsDangKyTheTV", "Main");
        }




    }
}