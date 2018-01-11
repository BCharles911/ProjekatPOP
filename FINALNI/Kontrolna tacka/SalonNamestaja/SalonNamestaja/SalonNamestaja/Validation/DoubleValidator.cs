using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalonNamestaja.Validation
{
    class DoubleValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrednost = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");
            try
            {
                double broj = 0;
                broj = Double.Parse(vrednost);

                if (broj < 0)
                    return new ValidationResult(false, "Broj ne moze biti manji od nule");
                else if (broj == 0)
                    return new ValidationResult(false, "Broj ne moze biti nula");
                else
                    return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan, ceo broj");
            }

        }


    }
}
