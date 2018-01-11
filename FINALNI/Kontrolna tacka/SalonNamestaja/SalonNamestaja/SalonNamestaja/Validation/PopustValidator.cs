using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalonNamestaja.Validation
{
   public class PopustValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var vrednost = value as string;

            try
            {
                double parsed;
                bool rezultat = double.TryParse(vrednost, out parsed);
                if (!rezultat)
                    return new ValidationResult(false, "Morate uneti pozitivan broj!");
                else if (parsed < 0)
                    return new ValidationResult(false, "Morate uneti pozitivan broj!");
                else if (parsed < 0.05 || parsed > 0.9)
                    return new ValidationResult(false, "popust ide od 0.05 do 0.9");
                else
                    return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Neispravan unos!");
            }

        }


    }
}
