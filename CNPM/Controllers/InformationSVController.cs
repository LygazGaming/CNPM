using System;
using System.Collections.Generic;
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
            SINHVIEN sv = db.SINHVIENs.Find(email);
            var student = db.SINHVIENs.ToList();
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
    }
}