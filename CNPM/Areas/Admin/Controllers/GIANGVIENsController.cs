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
    public class GIANGVIENsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/GIANGVIENs
        public ActionResult Index()
        {
            var gIANGVIEN = db.GIANGVIEN.Include(g => g.KHOA);
            return View(gIANGVIEN.ToList());
        }

        // GET: Admin/GIANGVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIEN.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Create
        public ActionResult Create()
        {
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/GIANGVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,HoTen,GioiTinh,SoDienThoai,Email,ChucVu,MatKhau,NgaySinh,NgayVaoLam,DiaChi,MaKhoa")] GIANGVIEN gIANGVIEN)
        {
            if (ModelState.IsValid)
            {
                db.GIANGVIEN.Add(gIANGVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", gIANGVIEN.MaKhoa);
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIEN.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", gIANGVIEN.MaKhoa);
            return View(gIANGVIEN);
        }

        // POST: Admin/GIANGVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,HoTen,GioiTinh,SoDienThoai,Email,ChucVu,MatKhau,NgaySinh,NgayVaoLam,DiaChi,MaKhoa")] GIANGVIEN gIANGVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIANGVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhoa = new SelectList(db.KHOA, "MaKhoa", "TenKhoa", gIANGVIEN.MaKhoa);
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIEN.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANGVIEN);
        }

        // POST: Admin/GIANGVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GIANGVIEN gIANGVIEN = db.GIANGVIEN.Find(id);
            db.GIANGVIEN.Remove(gIANGVIEN);
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
