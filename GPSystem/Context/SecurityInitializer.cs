using GPSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GPSystem.Context
{
    public class SecurityInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Seed(ApplicationDbContext context)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole {Id = "1", Name = "Admin" },
                new IdentityRole {Id = "2", Name = "Pastor" },
                new IdentityRole {Id = "3", Name = "Member" }
            };
            foreach (var role in roles) { db.Roles.Add(role); }
            db.SaveChanges();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string name = "admin@gp.com";
            string password = "Lungelo1!";

            var user = UserManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser {
                    Name = "Lungelo",
                    Surname = "Nzimande",
                    ChurchId = 1,
                    UserName = name,
                    Email = name,
                    Gender = GPSys.Models.Gender.Female
                };
                var result = UserManager.Create(user, password);
                result = UserManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = UserManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(roles[0].Name))
            {
                var result = UserManager.AddToRole(user.Id, roles[0].Name);
            }

            db.SaveChanges();
            base.Seed(context);
        }
    }
}