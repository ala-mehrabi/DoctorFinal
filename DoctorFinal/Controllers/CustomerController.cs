using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DoctorFinal.Controllers
{
    public class CustomerController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index(int? id)
        {
            Session["DoctorID"] = id.ToString();
            return RedirectToAction("Reg");
        }
        public ActionResult Reg()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterCustomer(Customer Customer)
        {
            if (Customer.CustomerID == 0)
            {
                Customer.CustomerID = db.Customer.Max(p => p.CustomerID) + 1;
            }
            db.Customer.Add(Customer);
            db.SaveChanges();
            return Content("ok");
        }

        [HttpGet]
        public ActionResult List(int? id)
        {
            Session["DoctorID"] = id.ToString();
            return RedirectToAction("ShowList");
        }
        public ActionResult ShowList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomer(int? id)
        {
            var _list = db.Customer.Where(p => p.DoctorID == id).Select(p => new { p.CustomerAddress, p.CustomerCode, p.CustomerFullName, p.CustomerID, p.CustomerMobile, p.CustomerTel, p.Doctor.DoctorID, p.NationalCode }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Select(int? id)
        {
            Session["CustomerID"] = id.ToString();
            return RedirectToAction("SelectCustomer");
        }
        public ActionResult SelectCustomer()
        {
            var id = Session["CustomerID"];
            Int32 CID = Convert.ToInt32(id);
            var Customer = db.Customer.Select(p => new { p.CustomerAddress, p.CustomerCode, p.CustomerFullName, p.CustomerID, p.CustomerMobile, p.CustomerTel, p.DoctorID, p.NationalCode }).SingleOrDefault(p => p.CustomerID == CID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.Customer = js.Serialize(Customer);
            return View();
        }

        [HttpPost]
        public ActionResult GetCommidityType()
        {
            var _list = db.CommidityType.Select(p => new { p.CommidityTypeID, p.CommidityTypeDescription }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCommidity(int? index)
        {
            var _list = db.Commidity.Where(p => p.CommidityType.CommidityTypeID == index).Select(p => new { p.CommidityID, p.CommidityName, p.CommidityPrice }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string Save()
        {
            string str = Request["Data"];
            JavaScriptSerializer js = new JavaScriptSerializer();
            var noskhe = js.Deserialize<Noskhe>(str.Split(';')[0]);
            if (noskhe.NoskheID == -1)
            {
                noskhe.NoskheID = db.Noskhe.Max(p => p.NoskheID) + 1;
                db.Noskhe.Add(noskhe);
            }
            else
            {
                db.Entry(noskhe).State = System.Data.Entity.EntityState.Modified;
            }
            var ListNoskheCommidity = js.Deserialize<List<NoskheCommidity>>(str.Split(';')[1]);

            db.NoskheCommidity.RemoveRange(db.NoskheCommidity.Where(p => p.NoskheID == noskhe.NoskheID).ToList());
            ListNoskheCommidity.ForEach(p => p.NoskheID = noskhe.NoskheID);

            db.NoskheCommidity.AddRange(ListNoskheCommidity);
            db.SaveChanges();
            return "ok;;;" + noskhe.NoskheID;
        }

        [HttpGet]
        public ActionResult RegisterNobat(Sabeghe Sabeghe)
        {
            if (Sabeghe.SabegheID == 0)
            {
                Sabeghe.SabegheID = db.Sabeghe.Max(p => p.SabegheID) + 1;
                db.Sabeghe.Add(Sabeghe);
            }
            else
                db.Entry(Sabeghe).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("ok");
        }

        [HttpGet]
        public ActionResult UpLoad(int? id)
        {
            Session["CustomerID"] = id.ToString();
            return RedirectToAction("UpLoadPage");
        }
        public ActionResult UpLoadPage()
        {
            return View(new CustomerInfo());
        }

        [HttpPost]
        public ActionResult UploadData(CustomerInfo _CustomerInfo, HttpPostedFileBase upload)
        {
            CustomerInfo ci = new CustomerInfo();
            var id = Session["CustomerID"];
            Int32 CID = Convert.ToInt32(id);
            if (_CustomerInfo.CustomerInfoID == 0)
            {
                ci.CustomerInfoID = db.CustomerInfo.Max(p => p.CustomerInfoID) + 1;
                ci.CustomerID = CID;
                ci.CustomerInfoGhad = _CustomerInfo.CustomerInfoGhad;
                ci.CustomerInfoPicName = _CustomerInfo.CustomerInfoPicName;
                ci.CustomerInfoRegDate = _CustomerInfo.CustomerInfoRegDate;
                ci.CustomerInfoVazn = _CustomerInfo.CustomerInfoVazn;
            }

            if ( upload != null && upload.ContentLength > 0)
            {
                ci.FileName = upload.FileName;
                ci.ContentType= upload.ContentType;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    ci.CustomerInfoData = binaryReader.ReadBytes(upload.ContentLength);
                }
            }
            db.CustomerInfo.Add(ci);
            db.SaveChanges();
            return RedirectToAction("SelectCustomer");
        }

        [HttpPost]
        public ActionResult GetUploadImage()
        {
            var id = Session["CustomerID"];
            Int32 CID = Convert.ToInt32(id);
            var _list = db.CustomerInfo.Where(p => p.CustomerID == CID).Select(p => new { p.CustomerID, p.CustomerInfoGhad, p.CustomerInfoID, p.CustomerInfoPicName, p.CustomerInfoRegDate, p.CustomerInfoVazn }).ToList();
            return Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SelectImage(int? id)
        {
            var ImageSelect = db.CustomerInfo.Select(p => new { p.CustomerInfoData, p.FileName, p.ContentType, p.CustomerInfoID }).FirstOrDefault(p => p.CustomerInfoID == id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            var base64 = System.Convert.ToBase64String(ImageSelect.CustomerInfoData, 0, ImageSelect.CustomerInfoData.Length);
            string path = "data:" + ImageSelect.ContentType + ";base64," + base64;
            return Content(path);
        }
    }
}
