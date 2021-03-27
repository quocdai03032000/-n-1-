using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_QuanLyThuVien.Models;

namespace DoAn1_QuanLyThuVien.Controllers
{
    public class SachController : Controller
    {
        QuanLyThuVienEntities database = new QuanLyThuVienEntities();
        // GET: Sach
        public ActionResult Index()
        {
            return View(database.Saches.ToList());
        }

        //Tạo mới sách
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            database.Saches.Add(sach);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        //Xem Chi tiết sách        
        public ActionResult Details(int id)
        {
            return View(database.Saches.Where(a => a.MaSach == id).FirstOrDefault());
                       
        }

        //Edit sách
        public ActionResult Edit(int id)
        {
            return View(database.Saches.Where(a=>a.MaSach==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Sach sach)
        {
            database.Entry(sach).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return Redirect("Index");
        }

        //Xoá sách

        public ActionResult Delete(int id)
        {
            return View(database.Saches.Where(a => a.MaSach == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, Sach sach)
        {
            sach = database.Saches.Where(a => a.MaSach == id).FirstOrDefault();
            database.Saches.Remove(sach);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}