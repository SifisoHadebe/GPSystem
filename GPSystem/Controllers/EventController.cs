using GPApp.Context;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

                    var list = db.Event.OrderByDescending(e => e.DateCreated).Where(e=>e.Archive == false).ToList();
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

        public List<EventComment> GetComments(long Id)
        {
            var comments = from ec in db.EventComment
                           where ec.EventId == Id
                           orderby ec.Date
                           select ec;
            return comments.ToList();
        }

    }
}
