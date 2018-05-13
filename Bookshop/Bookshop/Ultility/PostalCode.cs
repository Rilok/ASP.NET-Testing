using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Bookshop.Ultility
{
    public class PostalCode : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " jest wymagany.");
            }

            var postalCode = value.ToString();
            if (Regex.IsMatch(postalCode, "^[0-9]{2}[-][0-9]{3}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Nieprawidłowy kod pocztowy (użyj formatu: xx-xxx).");
        }
    }
}