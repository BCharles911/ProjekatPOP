using SalonNamestaja.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SalonNamestaja.Model
{
    public class Namestaj : INotifyPropertyChanged // 1. napisemo, pa dodamo prostor imena, implementiramo interfejs
    {
        private int id;
        private string naziv;
        private double cena;
        private int kolicina;
        private int idAkcije;
        private int tipNId;
        private bool obrisan;



        public Namestaj(int id, string naziv, double cena, int kolicina, Akcija akcija, TipNamestaja tip, bool obrisan)
        {

            this.id = id;
            this.naziv = naziv;
            this.cena = cena;
            this.kolicina = kolicina;
            if (akcija != null) { this.idAkcije = akcija.Id; } else { this.idAkcije = 0; };

            this.tipNId = tip.Id;
            this.obrisan = obrisan;
        }


        public Namestaj()
        {
            this.tipNId = 1;

        }








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

        private Akcija akcija;



        public Akcija Akcija
        {
            get
            {

                return Akcija.getById(idAkcije);

            }
            set
            {
                if (value != null) { idAkcije = value.Id; } else { idAkcije = 0; };
                //OnPropertyChanged("Akcija");
                //OnPropertyChanged("idAkcije");
                ////Da bi promena akcije dinamicki menjala jedinicnu cenu namestaja bez osvezavanja dataGrida.
                //OnPropertyChanged("cena");
                // akcija = value;
                // AkcijaId = akcija.Id;
                OnPropertyChanged("Akcija");
                OnPropertyChanged("idAkcije");
                OnPropertyChanged("Cena");
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


        //public int AkcijaId
        //{
        //    get { return idAkcije;  }
        //    set
        //    {
        //        idAkcije = value;
        //        OnPropertyChanged("AkcijaId");
        //    }
        //}



        /*  public int Id { get; set; }
            public string Naziv { get; set; }
            public double Cena { get; set; }
            public int KolicinaUMagacinu { get; set; }
            public int TipNamestajaId { get; set; } */

        public bool Obrisan { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj1)
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


        public static Namestaj getById(int id)
        {
            foreach (Namestaj n in Projekat.Instance.Namestaj1)
            {
                if (n.Id.Equals(id))
                {
                    return n;
                }
            }
            return null;
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
            var lista = Projekat.Instance.Namestaj1;
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
            kopija.Akcija = Akcija;
            kopija.TipNamestaja = TipNamestaja;
            return kopija;
        }






        #region Database
        public static ObservableCollection<Namestaj> UcitajNamestaj()
        {
            var namestaj = new ObservableCollection<Namestaj>();


            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))

            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM NAMESTAJ WHERE OBRISAN = 0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "NAMESTAJ");  //izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["NAMESTAJ"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = int.Parse(row["ID"].ToString());
                    n.Naziv = row["NAZIV"].ToString();
                    n.KolicinaUMagacinu = int.Parse(row["KOLICINA_MAG"].ToString());
                    n.Cena = double.Parse(row["CENA"].ToString());
                    n.TipNamestajaId = int.Parse(row["TIP_NAMESTAJA"].ToString());

                    n.Obrisan = bool.Parse(row["OBRISAN"].ToString());
                    n.idAkcije = int.Parse(row["ID_AKCIJE"].ToString());
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }


        public static Namestaj Dodaj(Namestaj n)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO NAMESTAJ (NAZIV, KOLICINA_MAG, CENA, TIP_NAMESTAJA, OBRISAN, ID_AKCIJE) VALUES (@NAZIV, @KOLICINA_MAG, @CENA, @TIP_NAMESTAJA, 0, @ID_AKCIJE);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("NAZIV", n.Naziv);
                cmd.Parameters.AddWithValue("KOLICINA_MAG", n.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("CENA", n.Cena);
                cmd.Parameters.AddWithValue("TIP_NAMESTAJA", n.TipNamestajaId);
                cmd.Parameters.AddWithValue("ID_AKCIJE", n.idAkcije);
               

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                n.Id = newId;
            }
            Projekat.Instance.Namestaj1.Add(n); //azuriram i stanje modela
            return n;
        }

        public static void Izmeni(Namestaj n)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE NAMESTAJ SET NAZIV=@Naziv, KOLICINA_MAG=@KolicinaUMagacinu, CENA=@Cena, TIP_NAMESTAJA=@tipNamestaja, ID_AKCIJE=@idAkcije, OBRISAN=@Obrisan WHERE ID=@Id";
                cmd.Parameters.AddWithValue("ID", n.Id);
                cmd.Parameters.AddWithValue("Naziv", n.Naziv);
                cmd.Parameters.AddWithValue("KolicinaUMagacinu", n.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("Cena", n.Cena);
                cmd.Parameters.AddWithValue("tipNamestaja", n.TipNamestajaId);
                cmd.Parameters.AddWithValue("idAkcije", n.idAkcije);
                cmd.Parameters.AddWithValue("Obrisan", n.Obrisan);



                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var namestaj in Projekat.Instance.Namestaj1)
                {
                    if (namestaj.Id == n.Id)
                    {
                        namestaj.Naziv = n.Naziv;
                        namestaj.Cena = n.Cena;
                        namestaj.KolicinaUMagacinu = n.KolicinaUMagacinu;
                        namestaj.TipNamestajaId = n.TipNamestajaId;
                        namestaj.idAkcije = n.idAkcije;
                        namestaj.Obrisan = n.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Obrisi(Namestaj n)
        {
            n.Obrisan = true;
            Izmeni(n);
        }


        #endregion
    }

}
