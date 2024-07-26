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
    public class SINHVIENsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/SINHVIENs
        public ActionResult Index()
        {
            var sINHVIEN = db.SINHVIEN.Include(s => s.KHOA);
            return View(sINHVIEN.ToList());
        }

        // GET: Admin/SINHVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIEN.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Create
        public ActionResult Create()
        {
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/SINHVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoTen,GioiTinh,DiaChi,NienKhoa,SoDienThoai,Email,NgaySinh,MatKhau,MaKhoa")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.SINHVIEN.Add(sINHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", sINHVIEN.MaKhoa);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIEN.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", sINHVIEN.MaKhoa);
            return View(sINHVIEN);
        }

        // POST: Admin/SINHVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoTen,GioiTinh,DiaChi,NienKhoa,SoDienThoai,Email,NgaySinh,MatKhau,MaKhoa")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", sINHVIEN.MaKhoa);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIEN.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // POST: Admin/SINHVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sINHVIEN = db.SINHVIEN.Find(id);
            db.SINHVIEN.Remove(sINHVIEN);
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
