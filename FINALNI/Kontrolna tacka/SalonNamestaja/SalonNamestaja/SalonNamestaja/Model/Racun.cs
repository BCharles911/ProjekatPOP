using System;
using SalonNamestaja;
using SalonNamestaja.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonNamestaja.UI;
using System.Data.SqlClient;
using System.Data;

namespace SalonNamestaja.Model
{
    public class Racun : INotifyPropertyChanged
    {
        private int id { get; set; }
        private int idKorisnika { get; set; }
        private ObservableCollection<StavkaRacuna> stavka { get; set; }
        private ObservableCollection<DodatnaUsluga> usluge { get; set; }
        private string kupacIme { get; set; }
        private double ukupnaCena { get; set; }
        private DateTime datumProdaje { get; set; }
        private bool obrisan { get; set; }

        public Racun()
        {
            this.idKorisnika = Login.Logovani();
            this.ukupnaCena = 0;
            this.kupacIme = "";
            this.datumProdaje = DateTime.Now;
            stavka = new ObservableCollection<StavkaRacuna>();
            usluge = new ObservableCollection<DodatnaUsluga>();


        }

        public Racun(int id, Korisnik korisnik, DateTime datumProdaje, string kupacIme, double ukupnaCena, bool obrisan)
        {
            this.id = id;
            this.idKorisnika = korisnik.Id;
            stavka = new ObservableCollection<StavkaRacuna>();
            this.obrisan = obrisan;
            this.ukupnaCena = ukupnaCena;
            this.kupacIme = kupacIme;
            this.datumProdaje = datumProdaje;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string namestajStavke
        {
            get
            {
                string s = "";
                foreach (var namestaj in Stavke)
                {
                    s += namestaj.Namestaj.Naziv;
                }
                return s;
            }
        }




        public ObservableCollection<DodatnaUsluga> Usluge
        {
            get
            {
                usluge = DodatnaUsluga.UcitajZaRacun(id);
                return usluge;
            }
            set
            {

                usluge = value;
                OnPropertyChanged("Usluge");
                OnPropertyChanged("usluge");
                OnPropertyChanged("UkupnaCena");
            }
        }


        public ObservableCollection<StavkaRacuna> Stavke
        {
            get
            {

                return StavkaRacuna.UcitajStavkeZaRacun(id);
            }
            set
            {
                stavka = value;
                OnPropertyChanged("Stavke");
                OnPropertyChanged("stavka");
                OnPropertyChanged("UkupnaCena");
            }
        }

        public Korisnik Korisnik
        {
            get
            {
                return Korisnik.getById(idKorisnika);
            }
            set
            {
                idKorisnika = value.Id;
                OnPropertyChanged("Korisnik");
                OnPropertyChanged("korisnikId");
            }
        }

        public DateTime DatumProdaje
        {
            get
            {
                return datumProdaje;
            }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }
        public string ImeKupca
        {
            get
            {
                return kupacIme;
            }
            set
            {
                kupacIme = value;
                OnPropertyChanged("ImeKupca");
            }
        }
        
        public double UkupnaCena
        {
            get
            {

                
                double ukupnaCenaRacuna = 0;
                if (Usluge != null)
                {


                    foreach (var namestaj in Stavke)
                    {

                        if (namestaj.Namestaj.Akcija != null)
                        {
                            double snizenje = 0;
                            snizenje = (((namestaj.Namestaj.Akcija).Popust / 100) * namestaj.Namestaj.Cena);
                            double posleSnizenja = Math.Round((namestaj.Namestaj.Cena - snizenje), 2);
                            ukupnaCenaRacuna = namestaj.Namestaj.Cena * namestaj.BrojKomada;


                        }
                        else
                        {
                            foreach (var usluge in Usluge)
                            {
                                ukupnaCenaRacuna += usluge.Cena;


                            }
                            ukupnaCenaRacuna += namestaj.BrojKomada * namestaj.Namestaj.Cena;
                        }
                    


                    }

                }
                ukupnaCena = ukupnaCenaRacuna;
                Console.WriteLine("Trenutna vrednost : " + ukupnaCena);
                return ukupnaCena;
            }
            set
            {
                ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
                OnPropertyChanged("StavkeRacuna");
                OnPropertyChanged("Usluge");
            }
        }

        public string strNamestaj
        {
            get
            {
                string s = "";
                foreach(var x in Stavke)
                {
                    s += x.Namestaj.Naziv;
                }
                return s;
            }
        }


        public bool Obrisan
        {
            get
            {
                return obrisan;
            }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #region Database

        public static ObservableCollection<Racun> UcitajSve()
        {
            var racuni = new ObservableCollection<Racun>();
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM RACUN WHERE OBRISAN=0";


                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(ds, "RACUN"); //Izvrsava se query nad bazom



                foreach (DataRow row in ds.Tables["RACUN"].Rows)
                {

                    var racun = new Racun();
                    racun.Id = int.Parse(row["ID"].ToString());

                    racun.idKorisnika = int.Parse(row["ID_KORISNIKA"].ToString());
                    racun.ImeKupca = (row["IME_KUPCA"].ToString());
                    racun.DatumProdaje = DateTime.Parse(row["DATUM_PRODAJE"].ToString());           
                    racun.UkupnaCena = double.Parse(row["UKUPNA_CENA"].ToString());            
                    racun.Obrisan = bool.Parse(row["OBRISAN"].ToString());


                    racuni.Add(racun);

                }
                return racuni;



            }



        }


        public static Racun Dodaj(Racun racun)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Racun (ID_KORISNIKA,IME_KUPCA, DATUM_PRODAJE, UKUPNA_CENA, OBRISAN) VALUES(@KorisnikId,@ImeKupca,@DatumProdaje, @UkupnaCena, @Obrisan)";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("KorisnikId", racun.idKorisnika);
                cmd.Parameters.AddWithValue("ImeKupca", racun.ImeKupca);
                cmd.Parameters.AddWithValue("DatumProdaje", racun.DatumProdaje);
                cmd.Parameters.AddWithValue("UkupnaCena", racun.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", racun.Obrisan);
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //Izvrsava se query nad bazom

                racun.Id = newId;
            }
            Projekat.Instance.Racun1.Add(racun);
            return racun;

        }




        public static void Izmeni(Racun racun)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE RACUN set ID_KORISNIKA=@KorisnikId, IME_KUPCA=@ImeKupca, DATUM_PRODAJE=@DatumProdaje, UKUPNA_CENA=@UkupnaCena, OBRISAN=@Obrisan WHERE ID=@Id";
                //cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan=@Obrisan";
                //cmd.Parameters.AddWithValue("Obrisan", )
                cmd.Parameters.AddWithValue("Id", racun.Id);
                cmd.Parameters.AddWithValue("KorisnikId", racun.idKorisnika);
                cmd.Parameters.AddWithValue("ImeKupca", racun.ImeKupca);
                cmd.Parameters.AddWithValue("DatumProdaje", racun.DatumProdaje);
                cmd.Parameters.AddWithValue("UkupnaCena", racun.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", racun.Obrisan);
                cmd.ExecuteNonQuery();
                foreach (var racun1 in Projekat.Instance.Racun1)
                {
                    if (racun1.Id == racun.Id)
                    {
                        racun1.idKorisnika = racun.idKorisnika;
                        racun1.ImeKupca = racun.ImeKupca;
                        racun1.DatumProdaje = racun.DatumProdaje;
                        racun1.UkupnaCena = racun.UkupnaCena;
                        racun1.Obrisan = racun.Obrisan;
                        break;
                    }
                }
            }

        }



        public static void Obrisi(Racun racun)
        {

            using(SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                racun.Obrisan = true;
                Projekat.Instance.Racun1.Remove(racun);
                Izmeni(racun);
            }
        }


        #endregion



    }
}
