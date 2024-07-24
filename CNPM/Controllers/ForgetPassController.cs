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

namespace CNPM.Controllers
{
    public class ForgetPassController : Controller
    {

        // GET: ForgetPass
        DoAnEntities db = new DoAnEntities();
        string code = "";
        string Email = "";
        public ActionResult FormCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormCode(String CODE)
        {
            if (CODE.Equals(code))
            {
                return RedirectToAction("ChangePass");
            }
            else
            {
                TempData["AlertMessage"] = "mã sai nhập lại";
                TempData["AlertType"] = "alert-warning";
                return View();
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
                    Email = EMAIL;
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
                    SINHVIEN sv = db.SINHVIENs.FirstOrDefault();
                    sv.MatKhau = NewPass;
                    db.Entry(sv).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Home", "Home");
                }
            }
        }
    }

}