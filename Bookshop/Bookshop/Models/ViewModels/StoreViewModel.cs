using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Models.ViewModels
{
    public class StoreViewModel
    {
        public Book BookObj { get; set; }
        public Publisher PubObj { get; set; }
        public IEnumerable<Store> StoreObj { get; set; }

    }
}
