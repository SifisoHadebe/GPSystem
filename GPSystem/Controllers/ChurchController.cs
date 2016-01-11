using GPApp.Context;
using GPApp.Models;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GPApp.Controllers
{
    public class ChurchController : Controller
    {
        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();
        // GET: Church
        public ActionResult Index()
        {
            try
            {
                if (Request.IsAuthenticated && User.IsInRole("Admin"))
                {
                    #region
                    string currentUserId = User.Identity.GetUserId(); //getting userId
                    var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                    ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                    #endregion
                    ViewBag.churchNumber = db.Church.ToList().Count();

                    var list = (from c in db.Church
                                orderby (c.Name)
                                select c).ToList();

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

        // GET: Church/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var c = db.Church.Find(id);
                if (c == null)
                    return HttpNotFound();
                return View(c);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Church/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                return View();
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }

        // POST: Church/Create
        [HttpPost]
        public ActionResult Create(Church c)
        {
            try
            {
                c.DateCreated = DateTime.Now;
                // TODO: Add insert logic here
                db.Church.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Church/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                if (db.Church.Find(id) == null)
                    return HttpNotFound();
                return View(db.Church.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Church/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Church c)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return View();
            }
        }

        // GET: Church/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                if (db.Church.Find(id) == null)
                    return HttpNotFound();
                return View(db.Church.Find(id));
        }

        // POST: Church/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    var c = db.Church.Find(id);
                    var ct = new ChurchTrash();
                    ct.Id = id;
                    ct.Name = c.Name;
                    ct.Description = c.Description;
                    ct.Address1 = c.Address1;
                    ct.Address2 = c.Address2;
                    ct.Code = c.Code;
                    ct.Telephone = c.Telephone;
                    ct.DateDeleted = DateTime.Now;
                    db.ChurchTrash.Add(ct);
                    db.Church.Remove(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
