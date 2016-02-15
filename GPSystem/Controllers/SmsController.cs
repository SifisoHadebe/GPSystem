using GPApp.Context;
using GPSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GPSystem.Controllers
{
    public class SmsController : Controller
    {
        private GPSystemContext db = new GPSystemContext();
        private ApplicationDbContext ac = new ApplicationDbContext();

        // GET: Sms
        public ActionResult Index()
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion
            ViewBag.churchNumber = db.Church.ToList().Count();

            var messages = db.Sms.ToList();

            //Try
            //http://api.clickatell.com/http/sendmsg?user=Lungelo1&password=YKfBOKLNUMYWOV&api_id=3585050&to=27789795029&text=Message
            string Message = "This is cool";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://api.clickatell.com/http/sendmsg?user=Lungelo1&password=YKfBOKLNUMYWOV&api_id=3585050&to=27789795029&text=" + Message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            //Try

            return View(messages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SmsModel sms)
        {
            string message = "Okay";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.mymobileapi.com/api5/http5.aspx?Type=send&Username=GodsProsperity&Password=gpsms&data=" + message + "&numto0789795029");
            HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            return View();
        }

        [HttpPost]
        public JsonResult getMembers()
        {
            #region
            string currentUserId = User.Identity.GetUserId(); //getting userId
            var query = ac.Users.SingleOrDefault(x => x.Id == currentUserId); //Getting UserInfo
            ViewBag.Fullname = query.Name + " " + query.Surname;  //Setting fullname for user
            #endregion

            var members = (from u in ac.Users
                           where u.ChurchId == query.ChurchId
                           select new
                           {
                               Id = u.Id,
                               Name = u.Name + " " + u.Surname
                           }).ToList();

            return Json(members, JsonRequestBehavior.AllowGet);
        }

    }
}