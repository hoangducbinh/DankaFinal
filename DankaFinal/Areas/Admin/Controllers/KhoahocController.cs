using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.IO;
using System.Diagnostics;
using System.Web.UI.WebControls;
using DankaFinal.Models;

namespace DankaFinal.Areas.Admin.Controllers
{
    public class KhoahocController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

       
        public ActionResult Index(int? page)
        {
            int PageNum = (page ?? 1);
            int PageSize = 7;
            var sACHes = db.Khoahocedms.Include(s => s.theloai);
            return View(db.Khoahocedms.ToList().OrderBy(n => n.maKhoahoc).ToPagedList(PageNum, PageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.theloais.ToList().OrderBy(n => n.tenTheloai), "maTl", "tenTl");
            
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Khoahocedm khoahocedm, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            ViewBag.maTl = new SelectList(db.theloais.ToList().OrderBy(n => n.maTl), "maTl", "tenTheloai");
           
            if (fFileUpload == null)
            {
                ViewBag.Thongbao = "Hãy chọn ảnh bìa.";
                ViewBag.tenKhoahoc = f["stenKhoahoc"];
                ViewBag.moTa = f["smoTa"];
                ViewBag.soLuong = int.Parse(f["isoLuong"]);
                ViewBag.giaBan = decimal.Parse(f["mgiaBan"]);
                ViewBag.maTl = new SelectList(db.theloais.ToList().OrderBy(n => n.tenTheloai), "maTl", "tenTheloai", int.Parse(f["maTl"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/imageDankamusic"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    khoahocedm.tenKhoahoc = f["stenKhoahoc"];
                    khoahocedm.moTa = f["smoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    khoahocedm.hinhMinhhoa = sFileName;
                    khoahocedm.ngayCapnhat = Convert.ToDateTime(f["dngayCapnhat"]);
                    khoahocedm.soLuongban = int.Parse(f["isoLuong"]);
                    khoahocedm.donGia = decimal.Parse(f["mgiaBan"]);
                    khoahocedm.maTl = int.Parse(f["maTl"]);
                   
                    db.Khoahocedms.Add(khoahocedm);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();

            }
        }

        // GET: Admin/SACH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoahocedm khoahocedm = db.Khoahocedms.Find(id);
            if (khoahocedm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khoahocedm);
        }


        // POST: Admin/SACH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Admin/SACH/Edit/5
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maKhoahoc,tenKhoahoc,donGia,moTa,hinhMinhhoa,maTl,tengiangvien,ngayCapnhat,soLuongban,soLanxem,linkdemo,lotrinh")] Khoahocedm khoahocedm)
        {
            if (ModelState.IsValid)
            {
                db.Khoahocedms.Add(khoahocedm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai", khoahocedm.maTl);
            return View(khoahocedm);
        }

        // GET: Admin/nha/Edit/5
        [HttpGet]
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

        // POST: Admin/nha/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Admin/nha/Delete/5
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

        // POST: Admin/nha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
