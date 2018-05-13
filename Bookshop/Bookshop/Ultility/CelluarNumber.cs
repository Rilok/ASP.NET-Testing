using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Bookshop.Ultility
{
    public class CelluarNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " jest wymagany.");
            }

            var phone = value.ToString();
            if (Regex.IsMatch(phone, "^[0-9]{3} [0-9]{3} [0-9]{3}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Nieprawidłowy numer telefonu (wzór: xxx xxx xxx.");
        }
    }
}