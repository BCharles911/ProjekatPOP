using POP.Model;
using POP_SF12016.Model;
using Projekat2017.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2017.Model
{
    public class Projekat
    {
        public static Projekat Instance { get;} = new Projekat();



        private List<Namestaj> namestaj;
        private List<TipNamestaja> tipoviNamestaja;
        private List<Korisnik> korisnik;

        public List<Namestaj> Namestaj
        {
            get
            {
                this.namestaj =  GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return this.namestaj;
            }
            set
            {
                this.namestaj = value;
                 GenericSerializer.Serialize<Namestaj>("namestaj.xml", this.namestaj);
            }
        }

        public List<TipNamestaja> TipoviNamestaja
        {
            get
            {
                tipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tip_namestaja.xml");
                return tipoviNamestaja;
            }
            set
            {
                tipoviNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tip_namestaja.xml", tipoviNamestaja);
            }
        }



        public List<Korisnik> Korisnici
        {
            get
            {
                korisnik = GenericSerializer.Deserialize<Korisnik>("Korisnik.xml");
                return korisnik;
            }
            set
            {
                korisnik = value;
                GenericSerializer.Serialize<Korisnik>("Korisnik.xml", korisnik);
            }
        }

        
    }
}
