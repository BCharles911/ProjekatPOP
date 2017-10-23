using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POP_SF12016.Test.Model;
using POP_SF12016.Model;

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
            Console.WriteLine("Unesite sledece podatke: ");
            Console.WriteLine("Id: ");
            Console.WriteLine("Naziv: ");

            Console.WriteLine("Tip Namestaja: ");
            string TipNamestaja = Console.ReadLine();
            Namestaj n1 = new Namestaj();
 
                          

        } }
            

            

            }
               

            }
                





            }
                    }


    }
    
