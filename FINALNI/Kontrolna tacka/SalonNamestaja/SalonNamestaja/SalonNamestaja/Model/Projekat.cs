using SalonNamestaja.Model;
using SalonNamestaja.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonNamestaja.Model
{
    public class Projekat
    {

        public const string CONNECTION_STRING = @"Integrated Security=true; Initial Catalog=Salon; Data Source=(localdb)\MSSQLLocalDB";

        public static Projekat Instance { get; } = new Projekat();



        public ObservableCollection<Namestaj> Namestaj1;
        public ObservableCollection<TipNamestaja> TipoviNamestaja;
        public ObservableCollection<Korisnik> Korisnik1;
        public ObservableCollection<DodatnaUsluga> DodatnaUsluga1;
        public ObservableCollection<Akcija> Akcija1;
        public ObservableCollection<Racun> Racun1;



        public Projekat()
        {

            TipoviNamestaja = TipNamestaja.UcitajSveTipove();
            Namestaj1 = Namestaj.UcitajNamestaj();
            Korisnik1 = Korisnik.UcitajKorisnike();
            DodatnaUsluga1 = DodatnaUsluga.UcitajSveUsluge();
            Akcija1 = Akcija.UcitajAkcije();
            Racun1 = Racun.UcitajSve();

        } 

        
    }
}
