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
            SINHVIEN sv = db.SINHVIENs.Find(email);
            if (sv == null)
            {
                return HttpNotFound();
            }
            else { 
                if (sv.MaKhoa == null)
                {
                    sv.MaKhoa = "Khoa chưa cập nhật";
                }
                else
                {
                    Session["MaKhoa"] = sv.MaKhoa;
                }
            }
           
            return View(sv);
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
            var mONHOC = db.MONHOCs.Include(m => m.KHOA);
            return View(mONHOC.ToList());
        }
    }
}