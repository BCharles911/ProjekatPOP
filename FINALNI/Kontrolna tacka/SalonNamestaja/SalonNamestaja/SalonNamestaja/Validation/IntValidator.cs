using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalonNamestaja.Validation
{
    class IntValidator : ValidationRule
    {
        public bool sucess { get; set; } = true;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrednost = value as string;
            if (vrednost == null)
                return new ValidationResult(false, "Morate popuniti ovo polje!");
            try
            {
                int parsed;
                bool rezultat = Int32.TryParse(vrednost, out parsed);
                if (!rezultat)
                {
                    sucess = false;
                    return new ValidationResult(false, "Morate uneti broj");
                }
                else if (parsed < 0)
                {
                    sucess = false;
                    return new ValidationResult(false, "Morate uneti pozitivan, ceo broj!");

                }
                else
                {
                    sucess = true;
                    return new ValidationResult(true, null);
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan, ceo broj!");
            }

        }





    }
}
