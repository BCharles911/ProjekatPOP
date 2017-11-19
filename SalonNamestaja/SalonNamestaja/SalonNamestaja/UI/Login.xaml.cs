using Projekat2017.Model;
using System;
using System.Collections.Generic;
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
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (tbKorIme.Text.Equals(korisnik.KorisnickoIme) && tbPass.Password.Equals(korisnik.Lozinka))
                {
                    var namestajProzor = new NamestajWindow();

                    namestajProzor.ShowDialog();
                }
                else
                {
                    if (String.IsNullOrEmpty(tbKorIme.Text) || String.IsNullOrEmpty(tbPass.Password))
                    {
                        MessageBox.Show("Niste uneli nista!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                        else
                        {
                           foreach (var korisnik2 in Projekat.Instance.Korisnici)
                                if (tbKorIme.Text != korisnik2.KorisnickoIme || tbPass.Password != korisnik2.Lozinka)
                                {
                                    MessageBox.Show("Uneli ste nepostojece podatke", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }

                    }
                }
                   

            }

        }
    }
}
