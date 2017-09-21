using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoctorFinal.Controllers
{
    public class InComeController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index(int? id)
        {
            Session["DoctorID"] = id.ToString();
            return RedirectToAction("ShowInCome");
        }

        [HttpGet]
        public ActionResult ShowInCome()
        {
            var idd = Session["DoctorID"];
            Int32 id = Convert.ToInt32(idd);
            var _listdata = (from p in db.Noskhe
                             where p.Customer.Doctor.DoctorID == id
                             select new { p.NoskhePrice, p.NoskheID, p.NoskheDate, p.CustomerID, p.Customer.DoctorID }).ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.DataNoskhe = js.Serialize(_listdata);
            return View();
        }
    }
}