using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class GroupMemberView
    {
        public List<ApplicationUser> Members { get; set; }

        public Group Group { get; set; }

        public int groupMembers { get; set; }
    }
}