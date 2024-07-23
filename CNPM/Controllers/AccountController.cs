using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using CNPM.Models;

namespace Manager.Controllers
{
    public class AccountController : Controller
    {
        private DoAnEntities db = new DoAnEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String EMAIL, String PASSWORD)
        {
            if (ModelState.IsValid)
            {
                var userCheck = db.SINHVIENs.Where(x => x.Email.Equals(EMAIL) && x.MatKhau.Equals(PASSWORD)).ToList();
                if (EMAIL == "admin")
                {
                    var adminCheck = db.ADMIN_ACCOUNT.Where(x=> x.MatKhau == PASSWORD).ToList();
                    if(adminCheck.Count() > 0)
                    {
                        return Redirect("/Admin/Home/Index");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                if (userCheck.Count() > 0)
                {
                    Session["Email"] = userCheck.FirstOrDefault().Email;
                    Session["SoDienThoai"] = userCheck.FirstOrDefault().SoDienThoai;
                    Session["MaSV"] = userCheck.FirstOrDefault().MaSV;
                    Session["Name"] = userCheck.FirstOrDefault().HoTen;
                    Session["GioiTinh"] = userCheck.FirstOrDefault().GioiTinh;
                    Session["MaKhoa"] = userCheck.FirstOrDefault().MaKhoa;
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    return RedirectToAction("Error");
                }

            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Error() 
        {
            return View();
        }
    }
}