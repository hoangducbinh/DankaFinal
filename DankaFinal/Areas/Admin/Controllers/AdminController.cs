using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DankaFinal.Areas;
using System.IO;
using PagedList.Mvc;
using PagedList;
using DankaFinal.Models;



namespace DankaFinal.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DanKaEDMEntities db = new DanKaEDMEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {

            return View();

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var tenDnAdmin = f["UserName"];
            var matKhauAdmin = f["Password"];
            if(string.IsNullOrEmpty(tenDnAdmin))
            {
                ViewData["s1"] = "Phải nhập tài khoản";
            }

            if (string.IsNullOrEmpty(matKhauAdmin))
            {
                ViewData["s1"] = "Phải nhập mật khẩu";
            }


            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.tenDnAdmin == tenDnAdmin && n.matKhauAdmin == matKhauAdmin);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

    }
}
