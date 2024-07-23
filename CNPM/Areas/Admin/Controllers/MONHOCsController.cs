using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace CNPM.Areas.Admin.Controllers
{
    public class MONHOCsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/MONHOCs
        public ActionResult Index()
        {
            var mONHOC = db.MONHOCs.Include(m => m.KHOA);
            return View(mONHOC.ToList());
        }

        // GET: Admin/MONHOCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            return View(mONHOC);
        }

        // GET: Admin/MONHOCs/Create
        public ActionResult Create()
        {
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/MONHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMH,TenMH,SoTinChi,MaKhoa")] MONHOC mONHOC)
        {
            if (ModelState.IsValid)
            {
                db.MONHOCs.Add(mONHOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", mONHOC.MaKhoa);
            return View(mONHOC);
        }

        // GET: Admin/MONHOCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", mONHOC.MaKhoa);
            return View(mONHOC);
        }

        // POST: Admin/MONHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMH,SoTinChi,MaKhoa")] MONHOC mONHOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", mONHOC.MaKhoa);
            return View(mONHOC);
        }

        // GET: Admin/MONHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            return View(mONHOC);
        }

        // POST: Admin/MONHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MONHOC mONHOC = db.MONHOCs.Find(id);
            db.MONHOCs.Remove(mONHOC);
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
