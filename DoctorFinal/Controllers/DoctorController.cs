using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorFinal.Controllers
{
    public class DoctorController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetOstan()
        {
            var _list = db.Ostan.Select(p => new { p.OstanID, p.OstanName }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetShahrestan(int? index)
        {
            var _list = db.Shahrestan.Where(p => p.Ostan.OstanID == index).Select(p => new { p.ShahrestanID, p.ShahrestanName }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RegisterDoctor(Doctor Doctor)
        {
            if (Doctor.DoctorID == 0)
            {
                Doctor.DoctorID = db.Doctor.Max(p => p.DoctorID) + 1;
            }
            db.Doctor.Add(Doctor);
            db.SaveChanges();
            return Content("ok");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(db.Doctor.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Doctor Doctor)
        {
            db.Entry(Doctor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("/Index", "Account");
        }
    }
}