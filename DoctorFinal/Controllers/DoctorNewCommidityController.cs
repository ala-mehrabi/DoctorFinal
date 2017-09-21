using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoctorFinal.Controllers
{
    public class DoctorNewCommidityController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index(int? id)
        {
            Session["DoctorID"] = id.ToString();
            return RedirectToAction("Show");
        }
        public ActionResult Show()
        {
            Session["DoctorNewCommidityID"] = db.DoctorNewCommidity.Max(p => p.DoctorNewCommidityID + 1).ToString();
            return View();
        }
        [HttpGet]
        public ActionResult RegisterNewDoctorCommidity(DoctorNewCommidity DoctorNewCommidity)
        {
            if (DoctorNewCommidity.DoctorNewCommidityID == 0)
            {
                DoctorNewCommidity.DoctorNewCommidityID = db.DoctorNewCommidity.Max(p => p.DoctorNewCommidityID) + 1;
            }
            db.DoctorNewCommidity.Add(DoctorNewCommidity);
            db.SaveChanges();
            return Content("ok");
        }

        [HttpGet]
        public ActionResult Register(DoctorComInfo DoctorComInfo)
        {
            if (DoctorComInfo.DoctorComInfoID == 0)
            {
                DoctorComInfo.DoctorComInfoID = db.DoctorComInfo.Max(p => p.DoctorComInfoID) + 1;
                db.DoctorComInfo.Add(DoctorComInfo);
            }
            else
                db.Entry(DoctorComInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("ok");
        }
    }
}