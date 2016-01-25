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
    public class MemberController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: Member
        public ActionResult Index()
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            ChurchMembersView cmv = new ChurchMembersView();
            cmv.ChurchMembers = (from cm in ac.Users
                                 where cm.ChurchId == query.ChurchId
                                 orderby cm.Name
                                 select cm).ToList();

            return View(cmv);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Edit/5
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

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Delete/5
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

        [HttpPost]
        public JsonResult GetMember(string Id)
        {
            var member = from m in ac.Users
                         where m.Id == Id
                         select new
                         {
                             Name = m.Name,
                             Surname = m.Surname,
                             Email = m.UserName,
                             Gender = m.Gender.ToString(),
                             Phone = m.PhoneNumber,
                             mail = m.UserName
                         };

            return Json(member, JsonRequestBehavior.AllowGet);
        }
    }
}
