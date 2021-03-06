﻿using SalonNamestaja.Model;
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
    [Serializable]
    public class TipNamestaja : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private bool obrisan;


        

        public int Id
        {
            get { return id; }
            set {
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
        public bool Obrisan
        {
            get { return obrisan; }
            set {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }

        /*public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; } */

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Naziv;
        }

        public static TipNamestaja GetById(int id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if(tipNamestaja.Id == id)
                {
                    return (tipNamestaja);
                }

            }
            return null;

        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); //on salje, zato je this object sender
            }
        }

        public object Clone()
        {
            return new TipNamestaja
            {
                id = Id,
                naziv = Naziv,
                obrisan = Obrisan
            };
        }
        #region Database
        public static ObservableCollection<TipNamestaja> UcitajSveTipove()
        {
            var tipoviNamestaja = new ObservableCollection<TipNamestaja>();


            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))

            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TIP_NAMESTAJA WHERE OBRISAN = 0";
                

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "TIP_NAMESTAJA");  //izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["TIP_NAMESTAJA"].Rows)
                {
                    var tn = new TipNamestaja();
                    tn.Id = int.Parse(row["ID"].ToString());
                    tn.Naziv = row["NAZIV"].ToString();
                    tn.Obrisan = bool.Parse(row["OBRISAN"].ToString());

                    tipoviNamestaja.Add(tn);
                }
            }
            return tipoviNamestaja;
        }


        public static TipNamestaja Dodaj(TipNamestaja tn)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO TIP_NAMESTAJA (NAZIV, OBRISAN) VALUES (@NAZIV, 0);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("NAZIV", tn.Naziv);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                tn.Id = newId;
            }
            Projekat.Instance.TipoviNamestaja.Add(tn); //azuriram i stanje modela
            return tn;
        }

        public static void Izmeni(TipNamestaja tn)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TIP_NAMESTAJA SET NAZIV=@Naziv, OBRISAN=@Obrisan WHERE ID=@Id";
                //cmd.CommandText += "UPDATE NAMESTAJ SET OBRISAN=1 WHERE ID=@Id";
                
                cmd.Parameters.AddWithValue("Id", tn.Id);
                cmd.Parameters.AddWithValue("Naziv", tn.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);
               // cmd.Parameters.AddWithValue("1", n.Obrisan);



                cmd.ExecuteNonQuery();

                //cmd.CommandText = "UPDATE NAMESTAJ SET OBRISAN=1 WHERE TIP_NAMESTAJA=@Id";
                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var tipNam in Projekat.Instance.TipoviNamestaja)
                {
                    if (tipNam.Id == tn.Id)
                    {
                        tipNam.Naziv = tn.Naziv;
                        tipNam.Obrisan = tn.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Obrisi(TipNamestaja tn)
        {
            
            tn.Obrisan = true;
            Izmeni(tn);
        }


        #endregion

    }


}
