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
    public class GroupMemberController : Controller
    {

        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: GroupMember
        public ActionResult Index(long Id)
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            ViewBag.GroupName = db.Group.Find(Id).Name;

            GroupMemberView gmv = new GroupMemberView();    //Initiating the viewModel

            gmv.Members = ac.Users.Where(gm => gm.ChurchId == query.ChurchId)
                            .Where(x=>x.Id != currentUserId)
                            .ToList(); //Getting all church members

            gmv.groupMembers = db.GroupMembers.Where(g => g.GroupId == Id).ToList().Count;

            gmv.Group = db.Group.Find(Id);

            return View(gmv);
        }

        public bool AddMember(long groupId, string MemberId)
        {
            List<string> newMembers = new List<string>();
            newMembers.Add(MemberId);

            return true;
        }

    }
}
