using GPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GPSystem.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mail/Create
        [HttpPost]
        public ActionResult Create(MailModel mail)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    string from = "gwala.lungeloe@gmail.com";
                    using (MailMessage m = new MailMessage(from, mail.To))
                    {
                        m.Subject = mail.Subject;
                        m.Body = mail.Body;
                        m.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential netCred = new NetworkCredential(from, "lungelokalindo");

                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = netCred;
                        smtp.Port = 587;
                        smtp.Send(m);
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Mail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mail/Edit/5
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

        // GET: Mail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mail/Delete/5
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
