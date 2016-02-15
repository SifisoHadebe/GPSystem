using GPSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GPApp.Models
{
    public class Church
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please specify church name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter address 1")]
        [MaxLength(30)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Telephone number is required")]
        [MaxLength(10)]
        [MinLength(10)]
        public string Telephone { get; set; }

        public int smsBalance { get; set; }

        public DateTime lastRecharge { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime DateCreated { get; set; }


    }
}