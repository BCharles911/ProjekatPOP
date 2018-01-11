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
     public class DodatnaUsluga : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        public DodatnaUsluga(int id, string naziv, double cena, bool obrisan)
        {
            this.id = id;
            this.naziv = naziv;
            this.cena = cena;
            this.obrisan = obrisan;
        }

        public DodatnaUsluga()
        {

        }

        public static DodatnaUsluga getById(int id)
        {
            foreach(var x in Projekat.Instance.DodatnaUsluga1)
            {
                if (x.Id.Equals(id))
                {
                    return x;
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


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #region Database
        public static ObservableCollection<DodatnaUsluga> UcitajSveUsluge()
        {
            var usluge = new ObservableCollection<DodatnaUsluga>();


            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))

            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DODATNE_USLUGE WHERE OBRISAN = 0";


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "DODATNE_USLUGE");  //izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["DODATNE_USLUGE"].Rows)
                {
                    var u = new DodatnaUsluga();
                    u.Id = int.Parse(row["ID"].ToString());
                    u.Naziv = row["NAZIV"].ToString();
                    u.Cena = double.Parse(row["CENA"].ToString());
                    u.Obrisan = bool.Parse(row["OBRISAN"].ToString());

                    usluge.Add(u);
                }
            }
            return usluge;
        }


        public static DodatnaUsluga Dodaj(DodatnaUsluga u)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO DODATNE_USLUGE (NAZIV, CENA, OBRISAN) VALUES (@NAZIV, @CENA, 0);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("NAZIV", u.Naziv);
                cmd.Parameters.AddWithValue("CENA", u.Cena);
                


                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                u.Id = newId;
            }
            Projekat.Instance.DodatnaUsluga1.Add(u); //azuriram i stanje modela
            return u;
        }

        public static void Izmeni(DodatnaUsluga u)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE DODATNE_USLUGE SET NAZIV=@Naziv, CENA=@Cena, OBRISAN=@Obrisan WHERE ID=@Id";
               // cmd.CommandText += "UPDATE NAMESTAJ SET OBRISAN=1 WHERE ID=@Id";

                cmd.Parameters.AddWithValue("Id", u.Id);
                cmd.Parameters.AddWithValue("Naziv", u.Naziv);
                cmd.Parameters.AddWithValue("Cena", u.Cena);
                cmd.Parameters.AddWithValue("Obrisan", u.Obrisan);
         



                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var tipNam in Projekat.Instance.DodatnaUsluga1)
                {
                    if (tipNam.Id == u.Id)
                    {
                        tipNam.Naziv = u.Naziv;
                        tipNam.Obrisan = u.Obrisan;
                        break;
                    }
                }
            }
        }



        #endregion

        #region bazaRacun

        public static ObservableCollection<DodatnaUsluga> UcitajZaRacun(int id)
        {
            var usluge = new ObservableCollection<DodatnaUsluga>();

            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DODATNE_USLUGE WHERE ID IN (SELECT ID_USLUGE FROM USLUGE_RACUNA WHERE ID_RACUNA=@RacunId)";
                cmd.Parameters.AddWithValue("RacunId", id);
                SqlDataAdapter sdp = new SqlDataAdapter();
                
                DataSet ds = new DataSet();
                sdp.SelectCommand = cmd;
                sdp.Fill(ds, "DODATNE_USLUGE");

                foreach (DataRow row in ds.Tables["DODATNE_USLUGE"].Rows)
                {
                    var usluga = new DodatnaUsluga();
                    usluga.Id = int.Parse(row["ID"].ToString());
                    usluga.Naziv = row["NAZIV"].ToString();
                    usluga.Cena = double.Parse(row["CENA"].ToString());
                    usluga.Obrisan = bool.Parse(row["OBRISAN"].ToString());

                    usluge.Add(usluga);
                }
                return usluge;


            }

        }

        public static void DodajZaRacun(Racun racun, DodatnaUsluga dodatna)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                racun.Usluge.Add(dodatna);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO USLUGE_RACUNA(ID_RACUNA, ID_USLUGE) VALUES(@RacunId, @UslugeId)";
                cmd.Parameters.AddWithValue("RacunId", racun.Id);
                cmd.Parameters.AddWithValue("UslugeId", dodatna.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void IzbrisiZaRacun(Racun racun, DodatnaUsluga dodatna)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                racun.Usluge.Remove(dodatna);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM USLUGE_RACUNA WHERE ID_RACUNA=@RacunId AND ID_USLUGE=@UslugaId";
                cmd.Parameters.AddWithValue("RacunId", racun.Id);
                cmd.Parameters.AddWithValue("UslugaId", dodatna.Id);
                cmd.ExecuteNonQuery();

            }

        }



        public static void Obrisi(DodatnaUsluga u)
        {
            
                u.Obrisan = true;
                Izmeni(u);



        }






        #endregion

    }
}
