using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DankaFinal.Models;
using PagedList;
using PagedList.Mvc;

namespace DankaFinal.Areas.Admin.Controllers
{
    public class TheloainhacController : Controller
    {
        private DanKaEDMEntities db = new DanKaEDMEntities();

        // GET: Admin/Theloainhac

        


        public ActionResult Index(int? page)
        {
            int PageNum = (page ?? 1);
            int PageSize = 5;
            var theLoais = db.theloais.Include(s => s.tenTheloai);
            return View(db.theloais.ToList().OrderBy(n=>n.maTl).ToPagedList(PageNum,PageSize));

        }

        // GET: Admin/Theloainhac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theloai theloai = db.theloais.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // GET: Admin/Theloainhac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Theloainhac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maTl,tenTheloai")] theloai theloai)
        {
            if (ModelState.IsValid)
            {
                db.theloais.Add(theloai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theloai);
        }

        // GET: Admin/Theloainhac/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theloai theloai = db.theloais.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // POST: Admin/Theloainhac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTl,tenTheloai")] theloai theloai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theloai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theloai);
        }

        // GET: Admin/Theloainhac/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theloai theloai = db.theloais.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // POST: Admin/Theloainhac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            theloai theloai = db.theloais.Find(id);
            db.theloais.Remove(theloai);
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
