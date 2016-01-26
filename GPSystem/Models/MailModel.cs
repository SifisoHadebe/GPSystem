using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class MailModel
    {
        public long Id { get; set; }
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}