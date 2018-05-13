using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookshop.Models
{
    public class Store
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Ilość sztuk")]
        public int amount { get; set; }
        [Required]
        [Display(Name = "Kwota")]
        public decimal price { get; set; }
        [Required]
        [Display(Name = "Tytuł książki")]
        public int BookID { get; set; }
        [Required]
        [Display(Name = "Nazwa wydawcy")]
        public int PublisherID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
        [ForeignKey("PublisherID")]
        public virtual Publisher Publisher { get; set; }

    }
}
