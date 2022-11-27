using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DankaFinal.Models;

namespace DankaFinal.Areas.Admin.Controllers
{
    public class ccController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

        // GET: Admin/cc
        public ActionResult Index()
        {
            var khoahocedms = db.Khoahocedms.Include(k => k.theloai);
            return View(khoahocedms.ToList());
        }

        // GET: Admin/cc/Details/5
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

        // GET: Admin/cc/Create
        public ActionResult Create()
        {
            ViewBag.maTl = new SelectList(db.theloais, "maTl", "tenTheloai");
            return View();
        }

        // POST: Admin/cc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admin/cc/Edit/5
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

        // POST: Admin/cc/Edit/5
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

        // GET: Admin/cc/Delete/5
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

        // POST: Admin/cc/Delete/5
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
