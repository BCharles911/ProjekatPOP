using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF12016.Model
{ public  enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
     public class Korisnik : INotifyPropertyChanged
    {

        

        private int id;
        private string ime;
        private string prezime;
        private string korisnickoime;
        private string lozinka;
        public TipKorisnika tipkorisnika;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }
        public string  Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
        public string KorisnickoIme
        {
            get { return korisnickoime; }
            set
            {
                korisnickoime = value;
                OnPropertyChanged("KorisnickoIme");
            }
        }
        public string Lozinka
        {
            get { return lozinka; }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }

        public TipKorisnika TipKorisnika
        {
            get { return tipkorisnika; }
            set
            {
                tipkorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }




       
        public bool Obrisan { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); //on salje, zato je this object sender
            }
        }
    }



}



