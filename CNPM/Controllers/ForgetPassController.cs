using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Data.Entity;
using Newtonsoft.Json.Linq;

namespace CNPM.Controllers
{
    public class ForgetPassController : Controller
    {

        string code = "";
        string countdefault = "0000000";
        // GET: ForgetPass
        DoAnEntities db = new DoAnEntities();
        public ActionResult FormCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormCode(String CODE)
        {
            string count_string = TempData["Input"] as string;
            string code = TempData["Key"] as string;
            if (CODE.Equals(code))
            {
                return RedirectToAction("ChangePass");
            }
            else
            {
                TempData["Key"] = code;
             
                if (count_string != null)
                {
                    int count = int.Parse(count_string.ToString());
                    if (count < 6)
                    {
                        TempData["Input"] = (count + 1).ToString();
                        TempData["AlertMessage"] = "mã sai nhập lại";
                        TempData["AlertType"] = "alert-warning";
                        return RedirectToAction("FormCode");
                    }
                    else
                    {
                        return RedirectToAction("FindEmail");
                    }
                }
                else
                {
                    return RedirectToAction("FormCode");
                }
            }
        }
        public ActionResult FindEmail()
        {
            
           return View();
        }
        [HttpPost]
        public ActionResult FindEmail(String EMAIL)
        {
            var StudentCheck = db.SINHVIENs.Where(x => x.Email.Equals(EMAIL)).ToList();
            if (StudentCheck.Count > 0)
            {
                SendEmail(EMAIL);
                TempData["Key"] = code;
                TempData["Input"] = countdefault;
                TempData["email"] = EMAIL;
                return RedirectToAction("FormCode");
            }
            else
            {
                TempData["AlertMessage"] = "Không tìm thấy email";
                TempData["AlertType"] = "alert-warning";
                return View();
            }
        }
        public bool SendEmail(string EMAIL)
        {
            Random rd = new Random();
            code = rd.Next(100000, 999999).ToString();
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("ELSA", "nguyennha6a6kl@gmail.com"));
                email.To.Add(new MailboxAddress("Người nhận", EMAIL));

                email.Subject = "Testing out email sending";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = "Mã Số xác nhận của bạn là : "+ code
                };
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication

                    smtp.Authenticate("nguyennha6a6kl@gmail.com", "tsol zvsa dswy wtyx");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
           
        }
        [HttpPost]
        public ActionResult Error_WrongCode()
        {
            return RedirectToAction("FormCode");
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string NewPass, string ReNewPass)
        {
            if (NewPass == null|| ReNewPass == null)
            {
                TempData["AlertMessage"] = "xin hãy nhập đầy đủ";
                TempData["AlertType"] = "alert-warning";
                return View();
            }
            else
            {
                if (!NewPass.Equals(ReNewPass))
                {
                    TempData["AlertMessage"] = "nhập lại mật khẩu khác với mật khẩu mới";
                    TempData["AlertType"] = "alert-warning";
                    return View();
                }
                else
                {
                    string email = TempData["email"] as string;
                    var list_sinhvien = db.SINHVIENs.Where(x => x.Email.Equals(email)).ToList();
                    SINHVIEN sv = db.SINHVIENs.Find(list_sinhvien.FirstOrDefault().MaSV);
                    sv.MatKhau = NewPass;
                    db.Entry(sv).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Home", "Home");
                }
            }
        }
    }

}