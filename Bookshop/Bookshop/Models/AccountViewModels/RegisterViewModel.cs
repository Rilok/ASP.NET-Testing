using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło musi mieć {2} - {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Imię musi mieć {2} - {1} znaków.", MinimumLength = 3)]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Nazwisko musi mieć {2} - {1} znaków.", MinimumLength = 5)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Czy ma być adminem?")]
        public bool isAdmin { get; set; }
    }
}
