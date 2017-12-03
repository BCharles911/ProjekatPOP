using POP.Model;
using POP_SF12016.Model;
using Projekat2017.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2017.Model
{
    public class Projekat
    {
        public static Projekat Instance { get;} = new Projekat();



        public ObservableCollection<Namestaj> Namestaj;
        public ObservableCollection<TipNamestaja> TipoviNamestaja;
        public ObservableCollection<Korisnik> Korisnik;
        public ObservableCollection<DodatnaUsluga> DodatnaUsluga;
        public ObservableCollection<Akcija> Akcija;


        public Projekat()
        {
            TipoviNamestaja = new ObservableCollection<TipNamestaja>(GenericSerializer.Deserialize<TipNamestaja>("tip_namestaja.xml"));
            Namestaj = new ObservableCollection<Namestaj>(GenericSerializer.Deserialize<Namestaj>("namestaj.xml"));
            Korisnik  = new ObservableCollection<Korisnik>(GenericSerializer.Deserialize<Korisnik>("Korisnik.xml"));
            DodatnaUsluga = new ObservableCollection<DodatnaUsluga>(GenericSerializer.Deserialize<DodatnaUsluga>("usluge.xml"));
            Akcija = new ObservableCollection<Akcija>(GenericSerializer.Deserialize<Akcija>("akcije.xml"));

        }

        /*     public ObservableCollection<Namestaj> Namestaj
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

             public ObservableCollection<TipNamestaja> TipoviNamestaja
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

         */






        
    }
}
