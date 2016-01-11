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
    public class GroupController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: Group
        public ActionResult Index()
        {
            try
            {
                #region
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                #endregion


                #region Refine
                var list = db.Group.Where(g => g.ChurchId == query.ChurchId).ToList();
                var groups = new List<Group>();
                foreach (var g in list)
                {
                    if (db.GroupMembers.Where(gm => gm.GroupId == g.Id).Count() < 2) 
                    {
                        db.Group.Remove(g);
                        var tempMember = db.GroupMembers.Where(gm => gm.GroupId == g.Id).ToList();
                        foreach(var x in tempMember)
                        {
                            db.GroupMembers.Remove(x);
                        }
                    }
                    else
                    {
                        groups.Add(g);
                    }
                }
                db.SaveChanges();
                #endregion

                return View(groups);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        public ActionResult Create(Group g)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    g.ChurchId = ac.Users.Find(User.Identity.GetUserId()).ChurchId;
                    g.DateCreated = DateTime.Now;
                    db.Group.Add(g);

                    //add dum members
                    GroupMember gm = new GroupMember();
                    gm.GroupId = g.Id;
                    gm.UserId = User.Identity.GetUserId();
                    db.GroupMembers.Add(gm);
                    db.SaveChanges();

                    return RedirectToAction("index", "GroupMember", new { Id = g.Id });
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
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

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
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
