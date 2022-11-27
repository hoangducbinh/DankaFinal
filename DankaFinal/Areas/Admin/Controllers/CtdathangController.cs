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
    public class CtdathangController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

        // GET: Admin/Ctdathang
        public ActionResult Index()
        {
            var ctdathangs = db.ctdathangs.Include(c => c.dondathang).Include(c => c.Khoahocedm);
            return View(ctdathangs.ToList());
        }

        // GET: Admin/Ctdathang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ctdathang ctdathang = db.ctdathangs.Find(id);
            if (ctdathang == null)
            {
                return HttpNotFound();
            }
            return View(ctdathang);
        }

        // GET: Admin/Ctdathang/Create
        public ActionResult Create()
        {
            ViewBag.soDh = new SelectList(db.dondathangs, "soDh", "soDh");
            ViewBag.maKhoahoc = new SelectList(db.Khoahocedms, "maKhoahoc", "tenKhoahoc");
            return View();
        }

        // POST: Admin/Ctdathang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "soDh,maKhoahoc,soLuong,donGia,thanhTien")] ctdathang ctdathang)
        {
            if (ModelState.IsValid)
            {
                db.ctdathangs.Add(ctdathang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.soDh = new SelectList(db.dondathangs, "soDh", "soDh", ctdathang.soDh);
            ViewBag.maKhoahoc = new SelectList(db.Khoahocedms, "maKhoahoc", "tenKhoahoc", ctdathang.maKhoahoc);
            return View(ctdathang);
        }

        // GET: Admin/Ctdathang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ctdathang ctdathang = db.ctdathangs.Find(id);
            if (ctdathang == null)
            {
                return HttpNotFound();
            }
            ViewBag.soDh = new SelectList(db.dondathangs, "soDh", "soDh", ctdathang.soDh);
            ViewBag.maKhoahoc = new SelectList(db.Khoahocedms, "maKhoahoc", "tenKhoahoc", ctdathang.maKhoahoc);
            return View(ctdathang);
        }

        // POST: Admin/Ctdathang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "soDh,maKhoahoc,soLuong,donGia,thanhTien")] ctdathang ctdathang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ctdathang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.soDh = new SelectList(db.dondathangs, "soDh", "soDh", ctdathang.soDh);
            ViewBag.maKhoahoc = new SelectList(db.Khoahocedms, "maKhoahoc", "tenKhoahoc", ctdathang.maKhoahoc);
            return View(ctdathang);
        }

        // GET: Admin/Ctdathang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ctdathang ctdathang = db.ctdathangs.Find(id);
            if (ctdathang == null)
            {
                return HttpNotFound();
            }
            return View(ctdathang);
        }

        // POST: Admin/Ctdathang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ctdathang ctdathang = db.ctdathangs.Find(id);
            db.ctdathangs.Remove(ctdathang);
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
