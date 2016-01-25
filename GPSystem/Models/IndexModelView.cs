using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class IndexModelView
    {
        public List<Event> Events { get; set; }

        public List<ApplicationUser> Members { get; set; }
    }
}