using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MariusBudrauskas.Models
{
    public class Task
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        [DisplayName("Task")]
        public string TaskQuestion { get; set; }
        [Required]
        public string Solution { get; set; }
    }
}