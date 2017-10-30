using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POP_SF12016.Test.Model;
using POP_SF12016.Model;
using Projekat2017.Util;
using Projekat2017.Model;

namespace Projekat2017
{
    class Program
    {
        static List<Namestaj> Namestaj { get; set; } = new List<Namestaj>();
        static void Main(string[] args)
        {




            Salon s1 = new Salon()
            {
                Id = 1,
                Naziv = "Kod Ofike",
                Adresa = "Pap Pavla 33",
                BrojZiroRacuna = "885-5546546",
                Email = "blabla@gmail.com",
                PIB = 44324266,
                Telefon = "021/-3454-34",
                Websajt = "http://www.ftn.uns.ac.rs",
                MaticniBroj = "3012996543",
                Obrisan = false
            };
            var sofaTipNamestaj = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Sofa"
            };
            var kauc = new TipNamestaja()
            {
                Id = 4,
                Naziv = "Kauc"
            };
            var n1 = new Namestaj()
            {
                Id = 1,
                Naziv = " bla bla ",
                JedinicnaCena = 28,
                KolicinaUMagacinu = 4,
                Sifra = " sdasda",
                TipN = sofaTipNamestaj

            };
            var n2 = new Namestaj()
            {
                Id = 2,
                Naziv = " dasd",
                JedinicnaCena = 45,
                KolicinaUMagacinu = 5,
                Sifra = " SS334",
                TipN = kauc
            };
            var ln1 = new List<Namestaj>();
            ln1.Add(n1);
            ln1.Add(n2);
            GenericSerializer.Serialize<Namestaj>("namestaj.xml", ln1);
            List<Namestaj> ln2 = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            Namestaj.Add(new Namestaj()
            {
                Id = 1,
                JedinicnaCena = 28,
                Naziv = "sofa 12",
                Akcija = null

            });

            Console.WriteLine("Dobrodosli u salon namestaja ");
            IspisiGlavniMeni();
            Console.ReadLine();
        }
        private static void IspisiGlavniMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("1.Rad sa namestajem");
                Console.WriteLine("2.Rad sa tipom namestaja");
                //TODO - dodati ostale entitete 
                Console.WriteLine("0.IZLAZ");

            } while (izbor < 0 || izbor > 2);
            izbor = int.Parse(Console.ReadLine());
            switch (izbor)
            {
                case 1:
                    IspisiMeniNamestaja();
                    Console.ReadLine();
                    break;
                case 2:
                    IspisiMeniTipaNamestaja();
                    break;

                default:
                    break;

            }
        }

        private static void IspisiMeniTipaNamestaja()
        {
            throw new NotImplementedException();
        }

        private static void IspisiMeniNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("1.Listing namestaja");
                Console.WriteLine("2.Dodaj novi namestaj");
                Console.WriteLine("3.Izmeni postojeci");
                Console.WriteLine("4.Izbrisi postojeci");
                Console.WriteLine("0.Povratak na glavni meni");
            } while (izbor < 0 || izbor > 4);
            izbor = int.Parse(Console.ReadLine());
            switch (izbor)
            {
                case 1:
                    IzlistajNamestaj();
                    break;
                case 2:
                    DodajNamestaj();
                    break;
                case 3:
                    IzmeniNamestaj();
                    break;
                case 4:
                    ObrisiNamestaj();
                    break;
                default:
                    break;

            }

        }

        private static void IzlistajNamestaj()
        {
            int index = 0;
            Console.WriteLine("====== LISTA NAMESTAJA =====");
            foreach (var namestaj in Namestaj)
            {
                Console.WriteLine($"{++index }.{namestaj.Naziv}, cena: {namestaj.JedinicnaCena}");
            }
        }

        private static void ObrisiNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void IzmeniNamestaj()
        {
            throw new NotImplementedException();
        }

        private static void DodajNamestaj()
        {

            try { 
            Console.WriteLine("Unesite sledece podatke: ");
            Console.WriteLine("Id: ");
            int id1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Naziv: ");
            string naziv1 = Console.ReadLine();
            Console.WriteLine("Unesite jedinicnu cenu: ");
            double JedCena = double.Parse(Console.ReadLine());
            Console.WriteLine("Unesite kolicinu u magacinu: ");
            int KolUMagacinu = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite sifru uredjaja: ");
            string sifra1 = Console.ReadLine();
            Console.WriteLine("Unesite Id  tipa uredjaja: ");
            int IdTipaUredjaja = int.Parse(Console.ReadLine());
            var n3 = new Namestaj()
            {
                Id = id1,
                Naziv = naziv1,
                JedinicnaCena = JedCena,
                KolicinaUMagacinu = KolUMagacinu,
                Sifra = sifra1
            };
            var ln1 = new List<Namestaj>();
            ln1.Add(n3);
            GenericSerializer.Serialize<Namestaj>("namestaj.xml", ln1);
            List<Namestaj> ln2 = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            Namestaj.Add(new Namestaj());
            Console.WriteLine("Da li zelite da unesete novi uredjaj1/2: ");
            int loop = int.Parse(Console.ReadLine());

            switch (loop)
            {
                case 1:
                    Console.WriteLine("Odabrali ste da unesete novi uredjaj: ");
                    DodajNamestaj();
                    break;
                case 2:
                    IspisiMeniNamestaja();
                    break;

            }



            }

            catch (ArgumentException)
            {

                Console.WriteLine("Unesite odgovarajucu vrednost");
                DodajNamestaj();
            }











            //traziti unos ID tipa namestaja 
            /*            List<Namestaj> namestaj = Projekat.Instance.Namestaj;
                        int i = 0;
                        foreach (var komad in namestaj)
                        {
                            Console.WriteLine($"{++i}| {komad.Naziv}");
                        }
                        Console.ReadLine();
                        */


        }
    }
}




    
