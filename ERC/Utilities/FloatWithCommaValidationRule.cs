using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ERC.Utilities
{
    public class FloatWithCommaValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!float.TryParse((string)value, out var result)) return new ValidationResult(false, "Только дробные числа");

            return new ValidationResult(true, null);
        }
    }
}
