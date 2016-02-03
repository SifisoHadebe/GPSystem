using GPApp.Context;
using GPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPSystem.Controllers
{
    public class PostPicturesController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: PostPictures
        public ActionResult Index(long Id)
        {
            ViewBag.PostId = Id;
            return View();
        }

        public ActionResult Upload(long Id)
        {
            return View(Id);
        }

        [HttpPost]
        public ActionResult Upload(long Id, HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/imgRepo/PostRepo/" + Id + file.FileName);

            PostPictures p = new PostPictures();

            p.PostId = Id;
            p.FileName = Id + file.FileName;

            db.PostPicture.Add(p);
            db.SaveChanges();
            file.SaveAs(path);

            return RedirectToAction("Index", "News");
        }
    }
}