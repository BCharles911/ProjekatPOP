using SalonNamestaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalonNamestaja.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static int ulogovaniKorisnik = 1;
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (var x in Projekat.Instance.Korisnik1)
            {
                if (x.KorisnickoIme.Equals(tbKorIme.Text) && x.Lozinka.Equals(tbPass.Password.ToString()))
                {
                    if (x.Tip == Korisnik.TipKorisnika.Administrator)
                    {
                        NamestajWindow main = new NamestajWindow();
                        main.Show();
                    }
                    else
                    {
                        RacuniProzor prodavac = new RacuniProzor();
                        prodavac.Show();
                    }
                    ulogovaniKorisnik = x.Id;
                    this.Close();
                    return;

                }




            }
            MessageBox.Show("Nisu dobri");
            tbKorIme.Clear();
            tbPass.Clear();



















            //    var korisnici = Projekat.Instance.Korisnik1;
            //    foreach (var korisnik in korisnici)
            //    {
            //        var user = tbKorIme.Text.Trim();
            //        var pass = tbPass.Password.Trim();
            //        if (user == "" || pass == "")
            //        {
            //            MessageBox.Show("Ne mozete ostaviti prazna polja!", "Error", MessageBoxButton.OK);

            //        }
            //        else if (user == korisnik.KorisnickoIme && pass == korisnik.Lozinka)
            //        {

            //                ulogovaniKorisnik = korisnik.Id;
            //                var prozor = new ProzorProdavac();
            //                this.Close();
            //                prozor.ShowDialog();
            //                return;



            //        }
            //        MessageBox.Show("Uneti podaci nisu tacni", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;

            //    }



            //}






        }


        public static int Logovani()
        {
            return ulogovaniKorisnik;

        }


    }
}