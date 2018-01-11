using System;
using SalonNamestaja.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SalonNamestaja.Model
{
     public class StavkaRacuna : INotifyPropertyChanged
    {
        private int idNamestaja { get; set; }
        private int brKomada { get; set; }

        private double cena;

        public StavkaRacuna()
        {

        }

        public StavkaRacuna(Namestaj namestaj, int kolicina)
        {
            this.idNamestaja = namestaj.Id;
            this.brKomada = kolicina;
        }








        public Namestaj Namestaj
        {
            get
            {
                return Model.Namestaj.getById(idNamestaja);
            }
            set
            {
                idNamestaja = value.Id;
                OnPropertyChanged("Namestaj");
                OnPropertyChanged("idNamestaja");
            }
        }

        public int BrojKomada
        {
            get
            {
                return brKomada;
            }
            set
            {
                brKomada = value;
                OnPropertyChanged("BrojKomada");
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }




        #region Database za racun

        public static ObservableCollection<StavkaRacuna> UcitajStavkeZaRacun(int id)
        {
            ObservableCollection<StavkaRacuna> stavke = new ObservableCollection<StavkaRacuna>();
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM STAVKE_RACUNA WHERE ID_RACUNA = @RacunId";
                cmd.Parameters.AddWithValue("RacunId", id);
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                DataAdapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                DataAdapter.Fill(ds, "STAVKE_RACUNA");


                foreach(DataRow row in ds.Tables["STAVKE_RACUNA"].Rows)
                {
                    StavkaRacuna stavka = new StavkaRacuna();
                    stavka.idNamestaja = int.Parse(row["ID_NAMESTAJA"].ToString());
                    stavka.brKomada = int.Parse(row["BROJ_KOMADA"].ToString());
                    stavke.Add(stavka);
                }
                return stavke;

            }

        }

        public static void Dodaj(Racun r, StavkaRacuna s)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                r.Stavke.Add(s);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO STAVKE_RACUNA(ID_RACUNA, ID_NAMESTAJA, BROJ_KOMADA) VALUES(@RacunId, @NamestajId, @BrojKomada)";
                cmd.Parameters.AddWithValue("RacunId", r.Id);
                cmd.Parameters.AddWithValue("NamestajId", s.Namestaj.Id);
                cmd.Parameters.AddWithValue("BrojKomada", s.BrojKomada);
                cmd.ExecuteNonQuery();
            }
        }



        public static void izmeniZaRacun(Racun racun, StavkaRacuna stavka)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                if (stavka.brKomada == 0)
                
                {
                    cmd.CommandText = "DELETE FROM STAVKE_RACUNA WHERE ID_RACUNA=@RacunId AND ID_NAMESTAJA=@NamestajId";
                    cmd.Parameters.AddWithValue("RacunId", racun.Id);
                    cmd.Parameters.AddWithValue("NamestajId", stavka.idNamestaja);
                    
                }
                else
                {
                 
                    cmd.CommandText = "UPDATE STAVKE_RACUNA SET BROJ_KOMADA=@BrojKomada WHERE ID_RACUNA=@RacunId AND ID_NAMESTAJA=@NamestajId";
                    cmd.Parameters.AddWithValue("NamestajId", stavka.idNamestaja);
                    cmd.Parameters.AddWithValue("BrojKomada", stavka.brKomada);
                    cmd.Parameters.AddWithValue("RacunId", racun.Id);
                }

                cmd.ExecuteNonQuery();
            }



        }




        #endregion











    }
}
