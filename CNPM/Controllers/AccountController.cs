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
                var studentCheck = db.SINHVIENs.Where(x => x.Email.Equals(EMAIL) && x.MatKhau.Equals(PASSWORD)).ToList();
                var professorCheck = db.GIANGVIENs.Where(x => x.Email.Equals(EMAIL) && x.MatKhau.Equals(PASSWORD)).ToList();
                if (EMAIL == "admin")
                {
                    var adminCheck = db.ADMIN_ACCOUNT.Where(x => x.MatKhau == PASSWORD).ToList();
                    if (adminCheck.Count() > 0)
                    {
                        return Redirect("/Admin/HomeAdmin/Index");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    if (studentCheck.Count() > 0)
                    {
                        Session["Email"] = studentCheck.FirstOrDefault().Email;
                        Session["SoDienThoai"] = studentCheck.FirstOrDefault().SoDienThoai;
                        Session["MaSV"] = studentCheck.FirstOrDefault().MaSV;
                        Session["Name"] = studentCheck.FirstOrDefault().HoTen;
                        Session["GioiTinh"] = studentCheck.FirstOrDefault().GioiTinh;
                        Session["MaKhoa"] = studentCheck.FirstOrDefault().MaKhoa;
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        if (professorCheck.Count() > 0)
                        {
                            Session["Email"] = professorCheck.FirstOrDefault().Email;
                            Session["SoDienThoai"] = professorCheck.FirstOrDefault().SoDienThoai;
                            Session["MaGV"] = professorCheck.FirstOrDefault().MaGV;
                            Session["Name"] = professorCheck.FirstOrDefault().HoTen;
                            Session["GioiTinh"] = professorCheck.FirstOrDefault().GioiTinh;
                            Session["MaKhoa"] = professorCheck.FirstOrDefault().MaKhoa;
                            return RedirectToAction("Home", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
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