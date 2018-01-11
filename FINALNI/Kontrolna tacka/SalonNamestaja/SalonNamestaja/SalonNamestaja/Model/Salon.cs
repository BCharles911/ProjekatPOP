using SalonNamestaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonNamestaja.Test.Model
{   [Serializable]
     public class Salon
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Websajt { get; set; }
        public int PIB { get; set; }
        public string MaticniBroj { get; set; }
        public string BrojZiroRacuna { get; set; }
        public bool Obrisan { get; set; }


    }
}
