using GPApp.Context;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPSystem.Controllers
{
    public class EventController : Controller
    {
        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();
        // GET: Event
        public ActionResult Index()
        {
            try
            {
                if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Member"))
                {
                    #region
                    string currentUserId = User.Identity.GetUserId(); //getting userId
                    var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                    ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                    #endregion

                    var list = from e in db.Event
                               orderby e.EventDate ascending
                               where e.ChurchId == query.ChurchId
                               select e;
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            var Event = db.Event.Find(id);
            ViewBag.Month = Event.EventDate.ToString("MMM");
            ViewBag.key = User.Identity.GetUserId();
            return View(Event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event e)
        {

            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    e.DateCreated = DateTime.Now;
                    e.Archive = false;
                    e.ArchiveDate = DateTime.Now;
                    e.ChurchId = query.ChurchId;

                    db.Event.Add(e);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
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

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
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

        public bool Comment(long Id,string comment)
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            EventComment c = new EventComment();
            c.EventId = Id;
            c.Date = DateTime.Now;
            c.UserId = query.Id;
            c.Text = comment;

            db.EventComment.Add(c);
            db.SaveChanges();
            return true;
        }

        public bool removeComment(long Id)
        {
            try
            {
                var comment = db.EventComment.Find(Id);
                db.EventComment.Remove(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult GetComments(long Id)
        {
            //var comments = db.EventComment.Where(e => e.EventId == Id).Join(ac.Users, e => e.UserId, u => u.Id, (e, u) => new
            //{
            //    Username = u.Name + " " + u.Surname,
            //    E = e.Text,
            //    Date = e.Date
            //}).AsEnumerable().Select(x => new {
            //    // wanna create a dummy table
            //    //problem is EventComment 
            //    //do not want to use it like that "new EventComment"
            //    Username  = x.Username,
            //    E = x.E,
            //    Date = x.Date
            //    //Ok, give me 15 minutes I will try it and see if I can redo what I did yesterday
            //    //ok run it
            //    //Let me try it one more time 
            //});

            ////var users = (from u in ac.Users select new { Id = u.Id,
            ////    Name = u.Name + " " + u.Surname }).ToArray();
            var comments = from e in db.EventComment.AsEnumerable()
                           orderby e.Date descending
                           where e.EventId == Id
                    select new
                    {
                        Id = e.Id,
                        Username = "To Change",
                        UserId = e.UserId,
                        Text = e.Text,
                        Day = e.Date.Day,
                        Month = e.Date.ToString("MMMM"),
                        Year = e.Date.Year
                    };

            return Json(comments, JsonRequestBehavior.AllowGet);
        }

    }
}
