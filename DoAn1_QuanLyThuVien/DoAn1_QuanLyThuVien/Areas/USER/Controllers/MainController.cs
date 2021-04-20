using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn1_QuanLyThuVien.Areas.USER.Controllers
{
    public class MainController : Controller
    {
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

        /*****************/
        /***** Contact ***/
        /*****************/
        public ActionResult Contact()
        {
            return View();
        }
    }
}