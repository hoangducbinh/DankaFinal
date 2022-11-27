using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DankaFinal.Models;
using PagedList;
using PagedList.Mvc;

namespace DankaFinal.Areas.Admin.Controllers
{
    public class KhoahocedmController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

        // GET: Admin/Khoahocedm
        public ActionResult Index(int? page)
        {
            int PageNum = (page ?? 1);
            int PageSize = 5;
            var sACHes = db.Khoahocedms.Include(s => s.theloai);
            return View(db.Khoahocedms.ToList().OrderBy(n => n.maKhoahoc).ToPagedList(PageNum, PageSize));
        }
        // GET: Admin/Khoahocedm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoahocedm khoahocedm = db.Khoahocedms.Find(id);
            if (khoahocedm == null)
            {
                return HttpNotFound();
            }
            return View(khoahocedm);
        }

        // GET: Admin/Khoahocedm/Create
        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        
        public ActionResult Create(Khoahocedm khoahocedm,HttpPostedFileBase fileUpload)
        {

           

            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai");

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng thêm hình ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/imageDankamusic"), fileName) ;

                    if (System.IO.File.Exists(path))
                   
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);

                    }
                    khoahocedm.hinhMinhhoa = fileName;
                    db.Khoahocedms.Add(khoahocedm);
                    db.SaveChanges();

                }    
            
            }

            return RedirectToAction("Index");
        }






        // POST: Admin/Khoahocedm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Admin/Khoahocedm/Edit/5
        public ActionResult Edit(int? id)
        {


            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoahocedm khoahocedm = db.Khoahocedms.Find(id);
            if (khoahocedm == null)
            {
                return HttpNotFound();
            }
            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai", khoahocedm.maTl);
            return View(khoahocedm);
        }

        // POST: Admin/Khoahocedm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "maKhoahoc,tenKhoahoc,donGia,moTa,hinhMinhhoa,maTl,tengiangvien,ngayCapnhat,soLuongban,soLanxem,linkdemo,lotrinh")] Khoahocedm khoahocedm)
        {
            



            if (ModelState.IsValid)
            {
                db.Entry(khoahocedm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai", khoahocedm.maTl);
            return View(khoahocedm);
        }

        // GET: Admin/Khoahocedm/Delete/5
        public ActionResult Delete(int? id)
        {

            


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoahocedm khoahocedm = db.Khoahocedms.Find(id);
            if (khoahocedm == null)
            {
                return HttpNotFound();
            }
            return View(khoahocedm);
        }

        // POST: Admin/Khoahocedm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(int id)
        {
            


            Khoahocedm khoahocedm = db.Khoahocedms.Find(id);
            db.Khoahocedms.Remove(khoahocedm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
