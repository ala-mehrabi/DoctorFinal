using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoctorFinal.Controllers
{
    public class SellNoskheController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetNoskhe()
        {
            var id = db.SellNoskhe.Max(p => p.SellNoskheID);
            var _list = db.Noskhe.Where(p => p.NoskheID > id).Select(p => new { p.NoskheCode, p.NoskheDate, p.NoskheID, p.Customer.CustomerAddress, p.Customer.CustomerFullName, p.Customer.CustomerID, p.Customer.CustomerMobile, p.Customer.Doctor.DoctorID, p.Customer.Doctor.DoctorFullName }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ShowNoskhe(int? id)
        {
            Session["NoskheID"] = id.ToString();
            return RedirectToAction("ShowNoskheDetile");
        }
        [HttpGet]
        public ActionResult ShowNoskheDetile()
        {
            var idd = Session["NoskheID"];
            Int32 id = Convert.ToInt32(idd);
            var _listdata = (from p in db.NoskheCommidity
                             where p.Noskhe.NoskheID == id
                             select new { p.NoskheID, p.Number, p.PriceInDay, p.Commidity.CommidityName, p.Commidity.CommidityPrice, p.CommidityID,p.Noskhe.Customer.DoctorID,p.Noskhe.Customer.Doctor.DoctorFullName,p.Noskhe.CustomerID,p.Noskhe.Customer.CustomerFullName,p.Noskhe.Customer.CustomerMobile,p.Noskhe.Customer.CustomerAddress,p.Noskhe.NoskhePrice}).Distinct().ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.DataCommidities = js.Serialize(_listdata);
            return View();
        }

        [HttpGet]
        public ActionResult SendFactorToCustomer(SellNoskhe SellNoskhe)
        {
            db.SellNoskhe.Add(SellNoskhe);
            db.SaveChanges();
            return Content("ok");
        }
    }
}