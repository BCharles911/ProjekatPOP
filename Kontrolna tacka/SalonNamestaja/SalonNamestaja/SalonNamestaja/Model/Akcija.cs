using POP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2017.Model
{
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private DateTime pocetakAkcije;
        private DateTime krajAkcije;
        private double popust;
        private Namestaj namestajPopust;
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

        


        

        public DateTime PocetakAkcije
        {
            get { return pocetakAkcije; }
            set
            {
                pocetakAkcije = value;
                OnPropertyChanged("PocetakAkcije");
            }
        }

        

        public DateTime KrajAkcije
        {
            get { return krajAkcije; }
            set
            {
                krajAkcije = value;
                OnPropertyChanged("KrajAkcije");
            }
        }

       

        public double Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }

        private int namestajPopustId;



        public int NamestajPopustId
        {
            get { return namestajPopustId; }
            set
            {
                namestajPopustId = value;
                OnPropertyChanged("NamestajPopustId");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        [XmlIgnore]
        public Namestaj NamestajPopust
        {
            get
            {
                if (namestajPopust == null)
                {
                    namestajPopust = Namestaj.PronadjiNamestaj(NamestajPopustId);
                }
                return namestajPopust;
            }
            set
            {
                namestajPopust = value;
                namestajPopustId = namestajPopust.Id;
                OnPropertyChanged("NamestajPopust");
            }
        }
        public Akcija()
        {
            pocetakAkcije = DateTime.Now;
            krajAkcije = DateTime.Now;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            if (!Obrisan)
            {


                string ispis = $"{Id}. {PocetakAkcije} {KrajAkcije} {Popust} {namestajPopust.Naziv}";
                return ispis;
            }
            return null;

        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Akcija PronadjiAkciju(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcija)
            {
                if (akcija.Id == id)
                {
                    return akcija;
                }

            }
            return null;
        }

        public object Clone()
        {
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.PocetakAkcije = PocetakAkcije;
            kopija.KrajAkcije = KrajAkcije;
            kopija.NamestajPopust = NamestajPopust;
            kopija.Popust = Popust;
            return kopija;
        }
    }


}
