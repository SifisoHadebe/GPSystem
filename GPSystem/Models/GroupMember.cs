using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class GroupMember
    {
        public long Id { get; set; }

        public long GroupId { get; set; }

        public string UserId { get; set; }

        public string MemberRole { get; set; }
    }
}