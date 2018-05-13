using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bookshop.Ultility
{
    public class UppercaseFirst : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Pole " + validationContext.DisplayName + " nie zawiera nic.");
            }
            char z = value.ToString().First();

            if (!char.IsUpper(z))
            {
                return new ValidationResult("Pole " + validationContext.DisplayName + " musi zaczynać się z dużej litery.");
            }

            return ValidationResult.Success;
        }
    }
}