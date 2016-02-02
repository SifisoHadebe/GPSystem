using GPApp.Context;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPSystem.Controllers
{
    public class NewsController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {

            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            ViewBag.Church = db.Church.Find(query.ChurchId).Name;
            #endregion

            var posts = (from x in db.Post
                         where x.ChurchId == query.ChurchId
                         select x
                        ).ToList();

            return View(posts);
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            ViewBag.Church = db.Church.Find(query.ChurchId).Name;
            #endregion

            PostView pv = new PostView();

            pv.Post = db.Post.Where(x => x.Id == id).Single();

            try
            {
                pv.Pics = db.PostPicture.Where(x => x.PostId == pv.Post.Id).Single();
            }catch (Exception)
            {
                pv.Pics = null;
            }
           

            return View(pv);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        public ActionResult Create(Post P)
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            ViewBag.Church = db.Church.Find(query.ChurchId).Name;
            #endregion

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    P.Author = query.Name + " " + query.Surname;
                    P.ChurchId = query.ChurchId;
                    P.Date = DateTime.Now;
                    db.Post.Add(P);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "PostPictures", new { Id = P.Id});
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
