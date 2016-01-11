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
    public class ChurchRequestController : Controller
    {
        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();
        // GET: ChurchRequest
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                try
                {
                    #region
                    string currentUserId = User.Identity.GetUserId(); //getting userId
                    var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                    ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                    #endregion
                    var list = (from cr in db.ChurchRequest
                                orderby (cr.Date)
                                select cr).ToList();
                    ViewBag.RequestCount = list.Where(cr => cr.Read == "False").Count();

                    return View(list);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ChurchRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin")) {
                #region
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                #endregion

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var request = db.ChurchRequest.Find(id);

                if (request == null)
                    return HttpNotFound();

                if (request.Read != "true")
                {
                    request.Read = "True";
                    request.ReadDate = DateTime.Now;
                    UpdateModel(request);
                    db.SaveChanges();
                }
                return View(request);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ChurchRequest/Create
        public ActionResult Create()
        {

                return View();
        }

        // POST: ChurchRequest/Create
        [HttpPost]
        public ActionResult Create(ChurchRequest cr)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    cr.Date = DateTime.Now;
                    cr.ReadDate = DateTime.Now;
                    cr.Read = "False";
                    db.ChurchRequest.Add(cr);
                    db.SaveChanges();
                }
                return RedirectToAction("Info");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ChurchRequest/Edit/5
        public ActionResult Edit(int id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                #region
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                #endregion

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ChurchRequest/Edit/5
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

        // GET: ChurchRequest/Delete/5
        public ActionResult Delete(int id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                #region
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                #endregion

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ChurchRequest/Delete/5
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

        public ActionResult Info()
        {
            return View();
        }
    }
}
