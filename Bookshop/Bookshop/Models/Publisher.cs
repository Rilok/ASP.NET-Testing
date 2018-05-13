using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Ultility;

namespace Bookshop.Models
{
    public class Publisher
    {
        public int id { get; set; }

        [Required, StringLength(30, MinimumLength = 2)]
        [UppercaseFirst]
        [Display(Name = "Nazwa")]
        public string name { get; set; }
        [Required, StringLength(40, MinimumLength = 4)]
        [UppercaseFirst]
        [Display(Name = "Ulica")]
        public string street { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        [Display(Name = "Numer Ulicy")]
        public int streetNumber { get; set; }
        [Required]
        [Display(Name = "Kod pocztowy")]
        [PostalCode]
        public string postalCode { get; set; }
        [Required, StringLength(30, MinimumLength = 2)]
        [UppercaseFirst]
        [Display(Name = "Miasto")]
        public string city { get; set; }
        [Required]
        [Display(Name = "Numer komórki")]
        [CelluarNumber]
        public string phone { get; set; }

    }
}
