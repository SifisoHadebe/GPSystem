using GPApp.Models;
using GPSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GPApp.Context
{
    public class Initializer : DropCreateDatabaseIfModelChanges<GPSystemContext>
    {
        private GPSystemContext db = new GPSystemContext();
        protected override void Seed(GPSystemContext context)
        {
            var c1 = new List<Church>
            {
                new Church {Id= 1, Name = "Church1", Description = "This is a test", Address1 = "House No 311", Address2 = "Pinetown", Code = "3600", Telephone = "0317040209", DateCreated = DateTime.Now},
                new Church {Id= 2, Name = "Church2", Description = "This is ather church by Us", Address1 = "House Chest", Address2 = "Pinetown", Code = "3610", Telephone = "0317040209", DateCreated = DateTime.Now },
                new Church {Id= 3, Name = "Church3", Description = "My Own church", Address1 = "House 404", Address2 = "Ekhaya", Code = "3602", Telephone = "0317040209", DateCreated = DateTime.Now }
            };

            var crs = new List<ChurchRequest>
            {
                new ChurchRequest {Id = 1, Name="Lindani", Email = "lindani@mail.com", ChurchName = "ICC", Description="A church next to N3", Date = DateTime.Now, Read = "False",ReadDate = DateTime.Now },
                new ChurchRequest {Id = 2, Name="Siyanda", Email = "siya@mail.com", ChurchName = "Christ Home", Description="A church for everyone", Date = DateTime.Now, Read = "False", ReadDate = DateTime.Now }
            };

            //var events = new List<Event>
            //{
            //    new Event {Id = 1, ChurchId = 1, Name = "Stay With Christ", DateCreated = DateTime.Now, Description="Test Event", EventDate = DateTime.Now.AddDays(20), Venue="Main Hall", More = "" }
            //};

            foreach (var item in c1){  db.Church.Add(item); }
            foreach (var item in crs) { db.ChurchRequest.Add(item); }
            //foreach (var item in events){ db.Event.Add(item); }
            db.SaveChanges();
        }
    }
}