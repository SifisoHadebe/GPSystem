using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class SMS
    {
        public long Id { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser User { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}