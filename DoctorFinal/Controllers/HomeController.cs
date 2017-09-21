using DoctorFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoctorFinal.Controllers
{
    public class HomeController : Controller
    {
        NewDoctorEntities db = new NewDoctorEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            var IdCount = db.Post.Max(p => p.PostID);
            List<ItemShow> postitem = new List<ItemShow>();
            for (int i = 1; i <= IdCount; i++)
            {
                var _post = db.Post.Select(p => new { p.PostImageData, p.PostContentType, p.PostID, p.PostTitle, p.PostFileName, p.PostDetile, p.PostDate, p.PostBody }).FirstOrDefault(p => p.PostID == i);
                var base64 = System.Convert.ToBase64String(_post.PostImageData, 0, _post.PostImageData.Length);
                string path = "data:" + _post.PostContentType + ";base64," + base64;
                postitem.Add(new ItemShow
                {
                    PID = _post.PostID,
                    PTitle = _post.PostTitle,
                    PDetile = _post.PostDetile,
                    PBody = _post.PostBody,
                    PDate = _post.PostDate,
                    PImage = path
                }); 
            }
            return View(postitem);
        }

        public ActionResult ReadPost(int? id)
        {
            var post = db.Post.Find(id);
            List<ItemShow> selectpost = new List<ItemShow>();
            var base64 = System.Convert.ToBase64String(post.PostImageData, 0, post.PostImageData.Length);
            string path = "data:" + post.PostContentType + ";base64," + base64;
            selectpost.Add(new ItemShow
            {
                PID = post.PostID,
                PTitle = post.PostTitle,
                PDetile = post.PostDetile,
                PBody = post.PostBody,
                PDate = post.PostDate,
                PImage = path
            });
            return View(selectpost);
        }
    }
}