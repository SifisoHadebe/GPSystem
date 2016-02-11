using GPApp.Context;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                try
                {
                    #region
                    string currentUserId = User.Identity.GetUserId(); //getting userId
                    var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                    ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                    ViewBag.Church = db.Church.Find(query.ChurchId).Name;
                    #endregion

                    IndexModelView imv = new IndexModelView();

                    imv.Posts = (from p in db.Post
                                 where p.ChurchId == query.ChurchId

                                 orderby p.Date descending
                                 select p).Take(3).ToList();

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
                catch
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    return RedirectToAction("Login", "Account");
                }
                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    #region
                    string currentUserId = User.Identity.GetUserId(); //getting userId
                    var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
                    ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
                    ViewBag.Church = db.Church.Find(query.ChurchId).Name;
                    #endregion


                    var church = db.Church.Find(query.ChurchId);
                    return View(church);
                }
                catch
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}