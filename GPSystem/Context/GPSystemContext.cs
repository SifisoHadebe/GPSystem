using GPApp.Models;
using GPSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GPApp.Context
{
    public class GPSystemContext : IdentityDbContext<ApplicationUser>
    {
        public GPSystemContext() : 
            base("Name=DefaultConnection")
        {

        }

        public DbSet<Church> Church { get; set; }
        public DbSet<ChurchTrash> ChurchTrash { get; set; }
        public DbSet<ChurchRequest> ChurchRequest { get; set; }
        public DbSet<Event> Event { get; set; }

        public DbSet<EventComment> EventComment { get; set; }
        public DbSet<Group> Group { get; set; }

        public DbSet<Post> Post { get; set; }
        public System.Data.Entity.DbSet<GPSystem.Models.GroupMember> GroupMembers { get; set; }
    }
}