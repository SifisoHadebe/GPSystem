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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}