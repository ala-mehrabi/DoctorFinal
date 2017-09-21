using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorFinal.Controllers
{
    public class PostController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Register()
        {
            return View(new Post());
        }

        [HttpPost]
        public ActionResult RegisterPost(Post _Post, HttpPostedFileBase upload)
        {
            Post po = new Post();
            if (_Post.PostID == 0)
            {
                po.PostID = db.Post.Max(p => p.PostID) + 1;
                po.PostTitle = _Post.PostTitle;
                po.PostDate = _Post.PostDate;
                po.PostDetile = Convert.ToString(Request["PostDetile"]);
                po.PostBody = Convert.ToString(Request["PostBody"]);
            }
            if (upload != null && upload.ContentLength > 0)
            {
                po.PostFileName = upload.FileName;
                po.PostContentType = upload.ContentType;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    po.PostImageData = binaryReader.ReadBytes(upload.ContentLength);
                }
            }
            db.Post.Add(po);
            db.SaveChanges();
            return RedirectToAction("Register");
        }
    }
}