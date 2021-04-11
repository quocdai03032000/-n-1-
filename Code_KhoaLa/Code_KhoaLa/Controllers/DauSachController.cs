using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code_KhoaLa.Models;

namespace Code_KhoaLa.Controllers
{
    public class DauSachController : Controller
    {
        QuanLyThuVienEntities ql = new QuanLyThuVienEntities();
        // GET: DauSach
        public ActionResult Index()
        {
            return View(ql.DauSaches.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DauSach sach)
        {
            try
            {
                ql.DauSaches.Add(sach);
                ql.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Thêm Không Thành Công!! Vui Lòng Kiểm Tra Lại");
            }
        }

        public ActionResult Details(int id)
        {
            return View(ql.DauSaches.Where(s => s.MaDauSach == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(ql.DauSaches.Where(s =>s.MaDauSach == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, DauSach sach)
        {
            ql.Entry(sach).State = System.Data.Entity.EntityState.Modified;
            ql.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete (int id)
        {
            return View(ql.DauSaches.Where(s => s.MaDauSach == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, DauSach sach)
        {
            try
            {
                sach = ql.DauSaches.Where(s => s.MaDauSach == id).FirstOrDefault();
                ql.DauSaches.Remove(sach);
                ql.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Dữ Liệu Đã Liên Kết!! Không Thể xóa ");
            }
        }
    }
}