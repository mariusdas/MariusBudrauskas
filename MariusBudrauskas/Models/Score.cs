using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MariusBudrauskas.Models
{
    public class Score
    {
        public int Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public int Point { get; set; }
        [Required]
        public string Task { get; set; }
        [Required]
        public string Solution { get; set; }
    }
}