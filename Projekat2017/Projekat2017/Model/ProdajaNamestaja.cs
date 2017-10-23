using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF12016.Model
{
    class ProdajaNamestaja
    {
        public int Id { get; set; }
        public int  BrojKomada { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set; }
        public List<DodatnaUsluga> DodatneUsluge { get; set; }
        public double PDV { get; set; }
        public double UkupnaCena { get; set; }
        public string Kupac { get; set; }
    }
}
