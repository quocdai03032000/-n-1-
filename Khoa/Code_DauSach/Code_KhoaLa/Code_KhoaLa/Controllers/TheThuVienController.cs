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
        QuanLyThuVienEntities ql = new QuanLyThuVienEntities();
        // GET: TheThuVien
        public ActionResult Index()
        {
            return View(ql.TheThuViens.ToList());
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(TheThuVien the)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in ql.TheThuViens)
                    {
                        if (item.MaThe == the.MaThe)
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Dữ Liệu đã tồn tại');</script>");
                            //return Content("Id bị trun");
                        }
                        else
                        {
                            ql.TheThuViens.Add(the);
                            ql.SaveChanges();
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Thanh Công! ');</script>");
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Thêm Không Thành Công!! Vui Lòng Kiểm Tra Lại");
            }
        }

        public ActionResult Details(String id)
        {
            return View(ql.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }

        public ActionResult Edit(String id)
        {
            return View(ql.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(String id, TheThuVien the)
        {
            ql.Entry(the).State = System.Data.Entity.EntityState.Modified;
            ql.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            return View(ql.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, TheThuVien the)
        {
            try
            {
                the = ql.TheThuViens.Where(s => s.MaThe == id).FirstOrDefault();
                ql.TheThuViens.Remove(the);
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