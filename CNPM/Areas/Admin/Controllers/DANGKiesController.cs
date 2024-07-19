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
    public class DANGKiesController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/DANGKies
        public ActionResult Index()
        {
            var dANGKY = db.DANGKY.Include(d => d.LOPHOCPHAN).Include(d => d.SINHVIEN);
            return View(dANGKY.ToList());
        }

        // GET: Admin/DANGKies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANGKY dANGKY = db.DANGKY.Find(id);
            if (dANGKY == null)
            {
                return HttpNotFound();
            }
            return View(dANGKY);
        }

        // GET: Admin/DANGKies/Create
        public ActionResult Create()
        {
            ViewBag.MaLHP = new SelectList(db.LOPHOCPHAN, "MaLHP", "TenLHP");
            ViewBag.MaSV = new SelectList(db.SINHVIEN, "MaSV", "HoTen");
            return View();
        }

        // POST: Admin/DANGKies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,MaLHP,DiemChuyenCan,DiemGiuaKy,DiemCuoiKy")] DANGKY dANGKY)
        {
            if (ModelState.IsValid)
            {
                db.DANGKY.Add(dANGKY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLHP = new SelectList(db.LOPHOCPHAN, "MaLHP", "TenLHP", dANGKY.MaLHP);
            ViewBag.MaSV = new SelectList(db.SINHVIEN, "MaSV", "HoTen", dANGKY.MaSV);
            return View(dANGKY);
        }

        // GET: Admin/DANGKies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANGKY dANGKY = db.DANGKY.Find(id);
            if (dANGKY == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLHP = new SelectList(db.LOPHOCPHAN, "MaLHP", "TenLHP", dANGKY.MaLHP);
            ViewBag.MaSV = new SelectList(db.SINHVIEN, "MaSV", "HoTen", dANGKY.MaSV);
            return View(dANGKY);
        }

        // POST: Admin/DANGKies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,MaLHP,DiemChuyenCan,DiemGiuaKy,DiemCuoiKy")] DANGKY dANGKY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANGKY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLHP = new SelectList(db.LOPHOCPHAN, "MaLHP", "TenLHP", dANGKY.MaLHP);
            ViewBag.MaSV = new SelectList(db.SINHVIEN, "MaSV", "HoTen", dANGKY.MaSV);
            return View(dANGKY);
        }

        // GET: Admin/DANGKies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANGKY dANGKY = db.DANGKY.Find(id);
            if (dANGKY == null)
            {
                return HttpNotFound();
            }
            return View(dANGKY);
        }

        // POST: Admin/DANGKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DANGKY dANGKY = db.DANGKY.Find(id);
            db.DANGKY.Remove(dANGKY);
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
