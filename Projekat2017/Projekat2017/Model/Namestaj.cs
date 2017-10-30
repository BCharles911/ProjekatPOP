using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF12016.Model
{
     public class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double JedinicnaCena { get; set; }
        public int  KolicinaUMagacinu { get; set; }
        public TipNamestaja TipN { get; set; }
        public int Kolicina { get; set; }
        public object Akcija { get;  set; }
        public string Sifra { get;  set; }

        internal void Add(int id1)
        {
            throw new NotImplementedException();
        }




    }
}
