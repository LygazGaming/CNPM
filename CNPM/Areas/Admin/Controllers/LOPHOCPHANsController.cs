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
    public class LOPHOCPHANsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/LOPHOCPHANs
        public ActionResult Index()
        {
            var lOPHOCPHAN = db.LOPHOCPHAN.Include(l => l.GIANGVIEN).Include(l => l.MONHOC);
            return View(lOPHOCPHAN.ToList());
        }

        // GET: Admin/LOPHOCPHANs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPHOCPHAN lOPHOCPHAN = db.LOPHOCPHAN.Find(id);
            if (lOPHOCPHAN == null)
            {
                return HttpNotFound();
            }
            return View(lOPHOCPHAN);
        }

        // GET: Admin/LOPHOCPHANs/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.GIANGVIEN, "MaGV", "HoTen");
            ViewBag.MaMH = new SelectList(db.MONHOC, "MaMH", "TenMH");
            return View();
        }

        // POST: Admin/LOPHOCPHANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLHP,TenLHP,CaHoc,Siso,HK,MaGV,MaMH")] LOPHOCPHAN lOPHOCPHAN)
        {
            if (ModelState.IsValid)
            {
                db.LOPHOCPHAN.Add(lOPHOCPHAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GIANGVIEN, "MaGV", "HoTen", lOPHOCPHAN.MaGV);
            ViewBag.MaMH = new SelectList(db.MONHOC, "MaMH", "TenMH", lOPHOCPHAN.MaMH);
            return View(lOPHOCPHAN);
        }

        // GET: Admin/LOPHOCPHANs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPHOCPHAN lOPHOCPHAN = db.LOPHOCPHAN.Find(id);
            if (lOPHOCPHAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GIANGVIEN, "MaGV", "HoTen", lOPHOCPHAN.MaGV);
            ViewBag.MaMH = new SelectList(db.MONHOC, "MaMH", "TenMH", lOPHOCPHAN.MaMH);
            return View(lOPHOCPHAN);
        }

        // POST: Admin/LOPHOCPHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLHP,TenLHP,CaHoc,Siso,HK,MaGV,MaMH")] LOPHOCPHAN lOPHOCPHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOPHOCPHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GIANGVIEN, "MaGV", "HoTen", lOPHOCPHAN.MaGV);
            ViewBag.MaMH = new SelectList(db.MONHOC, "MaMH", "TenMH", lOPHOCPHAN.MaMH);
            return View(lOPHOCPHAN);
        }

        // GET: Admin/LOPHOCPHANs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPHOCPHAN lOPHOCPHAN = db.LOPHOCPHAN.Find(id);
            if (lOPHOCPHAN == null)
            {
                return HttpNotFound();
            }
            return View(lOPHOCPHAN);
        }

        // POST: Admin/LOPHOCPHANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LOPHOCPHAN lOPHOCPHAN = db.LOPHOCPHAN.Find(id);
            db.LOPHOCPHAN.Remove(lOPHOCPHAN);
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
