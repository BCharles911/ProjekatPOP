using POP_SF12016.Model;
using Projekat2017.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP.Model
{
    public class Namestaj : INotifyPropertyChanged // 1. napisemo, pa dodamo prostor imena, implementiramo interfejs
    {
        private int id;
        private string naziv;
        private double cena;
        private int kolicina;
        private int tipNId;
        private string sifra;
        private TipNamestaja tipNamestaja;

        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            {   if(tipNamestaja == null )
                {
                    tipNamestaja = TipNamestaja.GetById(TipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }



        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public double Cena
        {
            get { return cena; }
            set
            {  
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public int KolicinaUMagacinu
        {
            get { return kolicina; }
            set
            {
                kolicina = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }
        public int TipNamestajaId
        {
            get { return tipNId; }
            set
            {
                tipNId = value;
                OnPropertyChanged("TipNamestajaId");
            }
        }






        /*  public int Id { get; set; }
            public string Naziv { get; set; }
            public double Cena { get; set; }
            public int KolicinaUMagacinu { get; set; }
            public int TipNamestajaId { get; set; } */

        public bool Obrisan { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;

            //ovo zamenjuje sve ovo gore iznad (lambda funkcija)
            //return Projekat.Instance.TipoviNamestaja.SingleOrDefault(x => x.Id == id);
        }

        public override string ToString()
        {
           if (!Obrisan)
            {
                string ispis = "";
                return ispis += $"{Naziv}";
            }
            return null;
            
            // return $"{Naziv}, {Cena}, {TipNamestaja.GetById(id).Naziv}";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); //on salje, zato je this object sender
            }
        }

        public static Namestaj PronadjiNamestaj(int id)
        {
            var lista = Projekat.Instance.Namestaj;
            foreach (var namestaj in lista)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }
            }
            return null;
        }
        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.KolicinaUMagacinu = KolicinaUMagacinu;
            kopija.Cena = Cena;
            kopija.TipNamestaja = TipNamestaja;
            return kopija;
        }

    }

}
