using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Ultility;

namespace Bookshop.Models
{
    public class Book
    {
        public int id { get; set; }

        [Required, StringLength(40, MinimumLength = 3)]
        [UppercaseFirst]
        [Display(Name = "Tytuł")]
        public string name { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        [UppercaseFirst]
        [Display(Name = "Gatunek")]
        public string genre { get; set; }
        [Required]
        [Range(1900, 2018)]
        [Display(Name = "Rok wydania")]
        public int year { get; set; }
    }
}
