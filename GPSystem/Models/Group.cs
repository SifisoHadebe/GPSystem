using GPApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class Group
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

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime DateCreated { get; set; }

        public virtual List<ApplicationUser> Members { get; set; }
    }

    public class GroupMember
    {
        public long id { get; set; }

        public long GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group group { get; set; }

        public string UserId { get; set; }
    }
}