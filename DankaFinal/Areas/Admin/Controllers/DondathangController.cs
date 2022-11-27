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
    public class DondathangController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

        // GET: Admin/Dondathang
        public ActionResult Index()
        {
            var dondathangs = db.dondathangs.Include(d => d.khachhang);
            return View(dondathangs.ToList());
        }

        // GET: Admin/Dondathang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dondathang dondathang = db.dondathangs.Find(id);
            if (dondathang == null)
            {
                return HttpNotFound();
            }
            return View(dondathang);
        }

        // GET: Admin/Dondathang/Create
        public ActionResult Create()
        {
            ViewBag.maKh = new SelectList(db.khachhangs, "maKh", "hoTenkh");
            return View();
        }

        // POST: Admin/Dondathang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "soDh,maKh,ngayDh,triGia,htThanhtoan,htGiaohang")] dondathang dondathang)
        {
            if (ModelState.IsValid)
            {
                db.dondathangs.Add(dondathang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKh = new SelectList(db.khachhangs, "maKh", "hoTenkh", dondathang.maKh);
            return View(dondathang);
        }

        // GET: Admin/Dondathang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dondathang dondathang = db.dondathangs.Find(id);
            if (dondathang == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKh = new SelectList(db.khachhangs, "maKh", "hoTenkh", dondathang.maKh);
            return View(dondathang);
        }

        // POST: Admin/Dondathang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "soDh,maKh,ngayDh,triGia,htThanhtoan,htGiaohang")] dondathang dondathang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dondathang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKh = new SelectList(db.khachhangs, "maKh", "hoTenkh", dondathang.maKh);
            return View(dondathang);
        }

        // GET: Admin/Dondathang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dondathang dondathang = db.dondathangs.Find(id);
            if (dondathang == null)
            {
                return HttpNotFound();
            }
            return View(dondathang);
        }

        // POST: Admin/Dondathang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dondathang dondathang = db.dondathangs.Find(id);
            db.dondathangs.Remove(dondathang);
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
