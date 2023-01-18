using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ERC.Utilities
{
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value)) return new ValidationResult(true, null);
            if (!int.TryParse((string)value, out var result)) return new ValidationResult(false, "Только цифры");

            return new ValidationResult(true, null);
        }
    }
}
