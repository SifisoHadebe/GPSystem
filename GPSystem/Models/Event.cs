using GPApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set; }

        public virtual long ChurchId { get; set; }
        [ForeignKey("ChurchId")]
        public virtual Church Church { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(180)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EventDate { get; set; }

        [Required]
        [MaxLength(60)]
        public string Venue { get; set; }

        [MaxLength(180)]
        public string More { get; set; }

        public bool Archive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ArchiveDate { get; set; }
    }

    public class EventComment
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public long EventId { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }
    }
}