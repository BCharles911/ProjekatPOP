using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF12016.Model
{
    class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double JedinicnaCena { get; set; }
        public int  KolicinaUMagacinu { get; set; }
        public string TipNamestaja { get; set; }
        public TipNamestaja MyProperty { get; set; }
        public int Kolicina { get; set; }
    }
}
