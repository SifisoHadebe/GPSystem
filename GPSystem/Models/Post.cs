using GPApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        public long ChurchId { get; set; }
        [ForeignKey("ChurchId")]
        public virtual Church Church { get; set; }

        public string Author { get; set; }
        [ForeignKey("Author")]
        public ApplicationUser User { get; set; }

        [Required]
        [MaxLength(25)]
        public string Topic { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool? Public { get; set; }
    }
}