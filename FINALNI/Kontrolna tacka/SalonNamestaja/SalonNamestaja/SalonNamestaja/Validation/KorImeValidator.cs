using SalonNamestaja.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SalonNamestaja.Validation
{
   public class KorImeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string korisnickoIme = value as string;
            if (korisnickoIme.Length == 0)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
            if (korisnickoIme == null)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
            foreach (var korisnik in Projekat.Instance.Korisnik1)
            {
                if (korisnik.KorisnickoIme == korisnickoIme)
                {
                    return new ValidationResult(false, "To korisnicko ime vec postoji");
                }
            }
            return new ValidationResult(true, null);


        }



















    }
}
