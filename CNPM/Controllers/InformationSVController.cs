using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace Manager.Controllers
{
    public class InformationSVController : Controller
    {
        DoAnEntities db = new DoAnEntities();
        // GET: InformationSV
        public ActionResult Information(string email)
        {
            Session["Email"] = email;
            SINHVIEN sv = db.SINHVIEN.Find(email);
            var student = db.SINHVIEN.ToList();
            return View();
        }

        public ActionResult Timestamp()
        {
            return View();
        }
        public ActionResult Result()
        {
            return View();
        }
        // show curriculum
        public ActionResult Curriculum()
        {
            //fix it for me
            var mONHOC = db.MONHOC.Include((string)Session["MaKhoa"]);
            return View(mONHOC.ToList());
        }
    }
}