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

namespace SalonNamestaja.Model
{ 
     public class Korisnik : INotifyPropertyChanged
    {

        

        private int id;
        private string ime;
        private string prezime;
        private string korisnickoime;
        private string lozinka;
        public enum TipKorisnika
        {
            Administrator,
            Prodavac
        }
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

        public TipKorisnika Tip
        {
            get { return tipkorisnika; }
            set
            {
                tipkorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }
        public static Korisnik getById(int id)
        {
            foreach (var k in Projekat.Instance.Korisnik1)
            {
                if (k.Id.Equals(id))
                {
                    return k;
                }
            }
            return null;
        }


        public override string ToString()
        {
            return ime + " " + prezime;
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

        #region Database
        public static ObservableCollection<Korisnik> UcitajKorisnike()
        {
            var korisnik = new ObservableCollection<Korisnik>();


            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))

            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM KORISNIK WHERE OBRISAN = 0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "KORISNIK");  //izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["KORISNIK"].Rows)
                {
                    var k = new Korisnik();
                    k.Id = int.Parse(row["ID"].ToString());
                    k.Ime = row["IME"].ToString();
                    k.Prezime = row["PREZIME"].ToString();
                    k.KorisnickoIme = row["KOR_IME"].ToString();
                    k.Lozinka = row["LOZINKA"].ToString();
                    

                    if (row["TIP_KORISNIKA"].ToString().Equals("True"))
                    {
                        k.Tip = TipKorisnika.Prodavac;
                    }
                    k.Obrisan = bool.Parse(row["OBRISAN"].ToString());
                    korisnik.Add(k);
                }
            }
            return korisnik;
        }


        public static Korisnik Dodaj(Korisnik k)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO KORISNIK (IME,PREZIME, KOR_IME, LOZINKA, TIP_KORISNIKA, OBRISAN) VALUES (@IME, @PREZIME, @KOR_IME, @LOZINKA, @TIP_KORISNIKA, 0);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IME", k.Ime);
                cmd.Parameters.AddWithValue("PREZIME", k.Prezime);
                cmd.Parameters.AddWithValue("KOR_IME", k.KorisnickoIme);
                cmd.Parameters.AddWithValue("LOZINKA", k.Lozinka);
                

                cmd.Parameters.AddWithValue("TIP_KORISNIKA", k.Tip);


                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                k.Id = newId;
            }
            Projekat.Instance.Korisnik1.Add(k); //azuriram i stanje modela
            return k;
        }

        public static void Izmeni(Korisnik k)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE KORISNIK SET IME=@Ime, PREZIME=@Prezime, KOR_IME=@KorisnickoIme, LOZINKA=@Lozinka, TIP_KORISNIKA=@Tip, OBRISAN=@Obrisan WHERE ID=@Id";
                cmd.Parameters.AddWithValue("ID", k.Id);
                cmd.Parameters.AddWithValue("Ime", k.Ime);
                cmd.Parameters.AddWithValue("Prezime", k.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", k.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", k.Lozinka);
                cmd.Parameters.AddWithValue("Tip", k.Tip);
                cmd.Parameters.AddWithValue("Obrisan", k.Obrisan);



                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var korisnik in Projekat.Instance.Korisnik1)
                {
                    if (korisnik.Id == k.Id)
                    {
                        korisnik.Ime = k.Ime;
                        korisnik.Obrisan = k.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Obrisi(Korisnik k)
        {
            k.Obrisan = true;
            Izmeni(k);
        }


        #endregion
        

    }



}



