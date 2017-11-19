using POP_SF12016.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2017.Model
{
    public class Login
    {
        Korisnik  k1 = new Korisnik();



        public static void loginAsSaleMan()
        {

            Console.WriteLine("Odabrali ste da se logujete kao prodavac");
            Console.WriteLine("Type username: ");
            String UserId1 = Console.ReadLine();
            Console.WriteLine("Type password");
            String Pass = Console.ReadLine();

            String UserIdCorrect = "test1";
            String PassCorrect = "password1";
            int MaxAttempts = 3;



            Console.ReadKey();

            if (UserId1 != UserIdCorrect && Pass != PassCorrect)
            {
                MaxAttempts++;

            }


            Console.ReadKey();
        }


        public static double loginasAdmin()
        {
            throw new NotImplementedException();
        }
    }
}


/*private static void IspisiGlavniMeni()
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
            Console.ReadLine();
            break;

        default:
            break;

    } */