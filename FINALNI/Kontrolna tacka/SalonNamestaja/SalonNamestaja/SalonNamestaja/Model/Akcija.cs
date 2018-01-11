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
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private string naziv;
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

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                
                naziv = value;
                OnPropertyChanged("Naziv");
                
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

        public override string ToString()
        {
            return Naziv;
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


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Akcija PronadjiAkciju(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcija1)
            {
                if (akcija.Id.Equals(id))
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
            kopija.Naziv = Naziv;
            kopija.PocetakAkcije = PocetakAkcije;
            kopija.KrajAkcije = KrajAkcije;
            kopija.NamestajPopust = NamestajPopust;
            kopija.Popust = Popust;
            return kopija;
        }



        public static Akcija getById(int id)
        {
            foreach (Akcija akcija in Projekat.Instance.Akcija1)
            {
                if (akcija.Id.Equals(id))
                {
                    return akcija;
                }
            }
            return null;
        }









        #region Database
        public static ObservableCollection<Akcija> UcitajAkcije()
        {
            var akcije = new ObservableCollection<Akcija>();


            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))

            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM AKCIJA WHERE OBRISAN = 0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "AKCIJA");  //izvrsava se query nad bazom

                foreach (DataRow row in ds.Tables["AKCIJA"].Rows)
                {
                    var a = new Akcija();
                    a.Id = int.Parse(row["ID"].ToString());
                    a.Naziv = row["NAZIV"].ToString();
                    a.PocetakAkcije = DateTime.Parse(row["DATUM_OD"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DATUM_DO"].ToString());
                    a.Popust = double.Parse(row["POPUST"].ToString());
                    a.Obrisan = bool.Parse(row["OBRISAN"].ToString());

                    akcije.Add(a);
                }
            }
            return akcije;
        }


        public static Akcija Dodaj(Akcija a)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO AKCIJA (NAZIV, DATUM_OD, DATUM_DO, POPUST, OBRISAN) VALUES (@NAZIV, @DATUM_OD, @DATUM_DO, @POPUST, 0);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("NAZIV", a.Naziv);
                cmd.Parameters.AddWithValue("DATUM_OD", a.PocetakAkcije);
                cmd.Parameters.AddWithValue("DATUM_DO", a.KrajAkcije);
                cmd.Parameters.AddWithValue("POPUST", a.Popust);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                a.Id = newId;
            }
            Projekat.Instance.Akcija1.Add(a);
            return a;
        }



        public static void Izmeni(Akcija a)
        {
            using (SqlConnection con = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE AKCIJA SET NAZIV=@Naziv, DATUM_OD=@PocetakAkcije, DATUM_DO=@KrajAkcije, POPUST=@Popust, OBRISAN=@Obrisan WHERE ID=@Id";
                cmd.Parameters.AddWithValue("ID", a.Id);
                cmd.Parameters.AddWithValue("Naziv", a.Naziv);
                cmd.Parameters.AddWithValue("PocetakAkcije", a.PocetakAkcije);
                cmd.Parameters.AddWithValue("KrajAkcije", a.KrajAkcije);
                cmd.Parameters.AddWithValue("Popust", a.Popust);
                cmd.Parameters.AddWithValue("Obrisan", a.Obrisan);



                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var akcije in Projekat.Instance.Akcija1)
                {
                    if (akcije.Id == a.Id)
                    {
                        akcije.Naziv = a.Naziv;
                        akcije.PocetakAkcije = a.PocetakAkcije;
                        akcije.KrajAkcije = a.KrajAkcije;
                        akcije.Popust = a.Popust;
                        akcije.Obrisan = a.Obrisan;
                        break;
                    }
                }
            }
        }



        public static void Obrisi(Akcija a)
        {
            a.Obrisan = true;
            Izmeni(a);
        }


        #endregion

    }


}
