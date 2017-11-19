using POP.Model;
using Projekat2017.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Projekat2017.Util
{
    public class GenericSerializer
    {
        
        public static void Serialize<T>(string fileName, List<T> objToSerialize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));

                using (var sw = new StreamWriter($@"../../Data/{fileName}"))
                {
                    serializer.Serialize(sw, objToSerialize);

                }
            }
            catch (IOException)
            {

                throw new Exception($"Greska prilikom ucitavanja {fileName} sa diska");
            }
        }

        public static List<T> Deserialize<T>(string fileName) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));

                using (var sw = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (List<T>)serializer.Deserialize(sw);

                }
            }
            catch (Exception)
            {

                throw new Exception($"Greska prilikom upisa datoteke {fileName} sa diska");
            }
        }


     /*   public static Sale(bool trf, double popust,  Namestaj namestaj)
        {
     
            var pocetnaCena = namestaj.Cena;
            double manje = pocetnaCena / popust;
            double krajnjaCena = pocetnaCena - manje;
            
            
        }*/



        //using keyword u okviru toka izvrsavanja programa  pr : using( deklarisemo promenjive za koje smo sigurni da ce biti unistene prilikom izlaska iz bloka)
        // using( var 0 = new object () ){ 
        // o.naziv = pera;
        // } uradi se poziv dispose metode nad tom promenljivom
        //proces komadanja informacija | @-neophodno kada se korsite apsolutne putanje


    }

}
