using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code_KhoaLa.Models;

namespace Code_KhoaLa.Controllers
{
    public class TheThuVienController : Controller
    {
        QuanLyThuVienEntities3 qltv = new QuanLyThuVienEntities3();
        // GET: TheThuVien
        public ActionResult Index()
        {
            return View(qltv.TheThuViens.ToList());
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(TheThuVien sach)
        {
            //try
            //{
                qltv.TheThuViens.Add(sach);
                qltv.SaveChanges();
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return Content("Thêm Không Thành Công!! Vui Lòng Kiểm Tra Lại");
            //}
        }

        public ActionResult Details(String id)
        {
            return View(qltv.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }

        public ActionResult Edit(String id)
        {
            return View(qltv.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(String id, TheThuVien the)
        {
            qltv.Entry(the).State = System.Data.Entity.EntityState.Modified;
            qltv.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            return View(qltv.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, TheThuVien the)
        {
            try
            {
                the = qltv.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault();
                qltv.TheThuViens.Remove(the);
                qltv.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Dữ Liệu Đã Liên Kết!! Không Thể xóa ");
            }
        }

    }
}