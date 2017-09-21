using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoctorFinal.Controllers
{
    public class AccountController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        [HttpGet]
        public ActionResult LogOn()
        {
            return View(new Doctor());
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            // return RedirectToAction("LogOn");
            return RedirectToAction("/Index", "Home");
        }
        [HttpPost]
        public ActionResult LogOn(Doctor Doctor)
        {
            if (db.Doctor.Any(p => p.DoctorFullName == Doctor.DoctorFullName && p.DoctorPassWord == "Admin"))
            {
                FormsAuthentication.SetAuthCookie(Doctor.DoctorFullName, true);
                FormsAuthentication.RedirectFromLoginPage(Doctor.DoctorFullName, true);
                return RedirectToAction("/Index", "Admin");
            }
            else
            if (db.Doctor.Any(p => p.DoctorFullName == Doctor.DoctorFullName && p.DoctorPassWord == Doctor.DoctorPassWord))
            {
                FormsAuthentication.SetAuthCookie(Doctor.DoctorFullName, true);
                FormsAuthentication.RedirectFromLoginPage(Doctor.DoctorFullName, true);
                var doc = db.Doctor.Where(p => p.DoctorFullName.Equals(Doctor.DoctorFullName) && p.DoctorPassWord.Equals(Doctor.DoctorPassWord)).FirstOrDefault();
                if (doc != null)
                {
                    Session["DoctorID"] = doc.DoctorID.ToString();
                    return RedirectToAction("/Index","Doctor");
                }

            }
            else
                ViewBag.Error = "  کلمه کاربری یا رمز عبور اشتباست ";
            return View(Doctor);
        }
        public ActionResult Index()
        {
            //var id = Convert.ToInt32(Session["DoctorID"]);
            if (Session["DoctorID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }
    }
}