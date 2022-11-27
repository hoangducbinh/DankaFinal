using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DankaFinal.Models;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;


namespace DankaFinal.Controllers
{
    public class HomeController : Controller
    {
        DanKaEDMEntities db = new DanKaEDMEntities();

        private List<Khoahocedm> Laykhoahocmoi(int count)
        {
            return db.Khoahocedms.OrderByDescending(a => a.ngayCapnhat).Take(count).ToList();
        }
        public ActionResult Index(int? page, string searchString)
        {
            var khoahocs = from l in db.Khoahocedms select l;
            if (!String.IsNullOrEmpty(searchString))
            {

                khoahocs = khoahocs.Where(s => s.tenKhoahoc.Contains(searchString));
            }
           
            int PageNum = (page ?? 1);
            int PageSize = 6;
            
            return View(db.Khoahocedms.ToList().OrderBy(n => n.maKhoahoc).ToPagedList(PageNum, PageSize));


         
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [ChildActionOnly]
        public ActionResult SubjectTheloai()
        {
            return PartialView(db.theloais.ToList());
        }
        
        
        [ChildActionOnly]
        public ActionResult SubjectKhoahoc()
        {
            
            return PartialView(db.Khoahocedms.OrderByDescending(a => a.ngayCapnhat).Take(4).ToList());

            

           





        }

        public ActionResult Khoahoctheotheloai(int id, int? page)
        {
            foreach (var item in db.theloais)
            {
                if (item.maTl == id)
                {
                    ViewBag.tenchude = item.tenTheloai.ToString();
                }
            }

            ViewBag.maTl = id;
            int Size = 4;
            int PageNum = (page ?? 1);
            var sach = (from s in db.Khoahocedms where s.maTl == id select s).ToList();
            return View(sach.ToPagedList(PageNum, Size));
          
            


        }


        
        public ActionResult Chitietkhoahoc(int id)
        {

            var sach = from s in db.Khoahocedms 
                       where s.maKhoahoc == id 
                       select s;
            
            return View(sach.Single());
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");

        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Gioithieu()
        {
            return View();
        }

        public ActionResult Hoctructiep()
        {
            return View();
        }


    }
}