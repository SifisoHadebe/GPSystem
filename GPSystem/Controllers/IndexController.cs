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
    public class IndexController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: Index
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                #region
                string currentUserId = User.Identity.GetUserId(); //getting userId
                var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                #endregion

                ViewBag.Church = db.Church.Find(query.ChurchId).Name;
                IndexModelView imv = new IndexModelView();
                imv.Events = (from e in db.Event
                              where e.ChurchId == query.ChurchId
                              orderby e.EventDate descending
                              select e).ToList();

                imv.Members = (from m in ac.Users
                               where m.ChurchId == query.ChurchId
                               orderby m.Name ascending
                               select m).Take(5).ToList();

                ViewBag.Members = ac.Users.Where(u => u.ChurchId == query.ChurchId).Count();
                ViewBag.EventCount = db.Event.Where(e => e.ChurchId == query.ChurchId).Count();
                return View(imv);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            var church = db.Church.Find(query.ChurchId);
            return View(church);
        }
    }
}