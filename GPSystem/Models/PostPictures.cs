using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GPSystem.Models
{
    public class PostPictures
    {
        [Key]
        public long Id { get; set; }

        public long PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public string FileName { get; set; }

    }
}